using System;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.DialogModule
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField]
        private Button _closeButton;
        
        public event Action OnClose;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseClickHandler);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseClickHandler);
        }

        private void OnCloseClickHandler()
        {
            OnClose?.Invoke();
        }
    }
}