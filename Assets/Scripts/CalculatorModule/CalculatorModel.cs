using System.Collections.Generic;
using Calculator.Interfaces;
using UnityEngine;

namespace Calculator.CalculatorModule
{
    public class CalculatorModel
    {
        private const string errorSuffix = "=ERROR";
        private const int maxHistorySize = 50;

        public string LastInput
        {
            get
            {
                LoadStateIfNotInit();
                return _lastInput;
            }
            private set
            {
                LoadStateIfNotInit();
                _lastInput = value;
            }
        }

        private readonly IPersistenceService _persistenceService;

        private string _lastInput;
        private readonly List<string> _history = new();

        private bool _init;

        public CalculatorModel(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        public void SetLastInput(string input)
        {
            LastInput = input;
            _persistenceService.SaveLastInput(LastInput);
        }

        public bool Add(string input, out string result)
        {
            LoadStateIfNotInit();

            bool success;
            string[] split = input.Split('+');
            if (split.Length != 2)
            {
                result = input + errorSuffix;
                success = false;
            }
            else
            {
                int leftValue, rightValue;
                if (int.TryParse(split[0], out leftValue) &&
                    int.TryParse(split[1], out rightValue))
                {
                    result = input + "=" + (leftValue + rightValue);
                    success = true;
                }
                else
                {
                    result = input + errorSuffix;
                    success = false;
                }
            }

            // Add to history
            _history.Add(result);
            if (_history.Count > maxHistorySize)
                _history.RemoveAt(0);

            // Save it
            _persistenceService.SaveHistory(_history);

            return success;
        }

        public ICollection<string> GetHistory()
        {
            LoadStateIfNotInit();
            return _history;
        }

        private void LoadStateIfNotInit()
        {
            if (_init)
                return;

            LoadState();
        }

        private void LoadState()
        {
            _init = true;

            LastInput = _persistenceService.LoadLastInput();

            _history.Clear();
            _history.AddRange(_persistenceService.LoadHistory());
        }
    }
}