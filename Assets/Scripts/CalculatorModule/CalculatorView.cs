using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.CalculatorModule
{
    public class CalculatorView : MonoBehaviour
    {
        [SerializeField]
        private int _maxItems = 7;
        [SerializeField]
        private GameObject _historyLabelPrefab;
        [SerializeField]
        private TMP_InputField _inputField;
        [SerializeField]
        private ScrollRect _scrollRect;
        [SerializeField]
        private Button _calculateButton;

        public event Action<string> OnCalculate;
        public event Action<string> OnInputUpdated;

        private readonly List<Transform> _historyItems = new();

        private void Awake()
        {
            _historyLabelPrefab.SetActive(false);
            // _scrollRect.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            _calculateButton.onClick.AddListener(OnCalculateClickHandler);
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            _calculateButton.onClick.RemoveListener(OnCalculateClickHandler);
        }

        private void OnInputFieldValueChanged(string text)
        {
            OnInputUpdated?.Invoke(text);
        }

        private void OnCalculateClickHandler()
        {
            string text = _inputField.text;
            if (string.IsNullOrEmpty(text))
                return;
            
            OnCalculate?.Invoke(_inputField.text);
        }

        public void ApplyInputAndHistory(string input, ICollection<string> history)
        {
            _inputField.text = input;

            foreach (string item in history)
                AddToHistory(item);
        }

        public void ShowResult(string result)
        {
            AddToHistory(result);
        }

        public void ShowError(string message)
        {
            AddToHistory(message);
        }

        public void ShowInput(string input)
        {
            _inputField.text = input;
        }

        private void AddToHistory(string text)
        {
            Transform parent; 
            
            // Find a parent
            if (_historyItems.Count + 1 >= _maxItems)
            {
                parent = _scrollRect.content;
                if (!_scrollRect.gameObject.activeSelf)
                {
                    foreach (var item in _historyItems)
                        item.SetParent(parent);
                }

                _scrollRect.gameObject.SetActive(true);
            }
            else
            {
                parent = _historyLabelPrefab.transform.parent;
                if (_scrollRect.gameObject.activeSelf)
                {
                    foreach (var item in _historyItems)
                        item.SetParent(parent);
                }
                
                _scrollRect.gameObject.SetActive(false);
            }

            // Instantiate
            var go = Instantiate(_historyLabelPrefab, parent, false);
            var trans = go.transform;
            trans.rotation = Quaternion.identity;
            trans.localScale = Vector3.one;

            var label = go.GetComponent<TMP_Text>();
            label.text = text;

            go.SetActive(true);
            
            // Remember
            _historyItems.Add(trans);
        }
    }
}