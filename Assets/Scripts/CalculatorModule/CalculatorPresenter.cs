using Calculator.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Calculator.CalculatorModule
{
    public class CalculatorPresenter
    {
        private readonly CalculatorModel _model;
        private readonly CalculatorViewFactory _viewFactory;
        private readonly IDialogWindow _dialogWindow;

        private CalculatorView _view;
        
        public CalculatorPresenter(
            CalculatorModel model, 
            CalculatorViewFactory viewFactory, 
            IDialogWindow dialogWindow)
        {
            Assert.IsNotNull(model);
            Assert.IsNotNull(viewFactory);
            
            _model = model;
            _viewFactory = viewFactory;
            _dialogWindow = dialogWindow;
        }

        public void LoadAndShow()
        {
            Assert.IsNull(_view, "Already loaded");

            _view = _viewFactory.CreateInstance();
            _view.ApplyInputAndHistory(_model.LastInput, _model.GetHistory());
            _view.OnCalculate += OnCalculateHandler;
            _view.OnInputUpdated += OnInputUpdatedHandler;
        }

        private void OnCalculateHandler(string input)
        {
            bool success = _model.Add(input, out string result);
            if (success)
            {
                _view.ShowResult(result);
            }
            else
            {
                _view.ShowError(result);
                _dialogWindow.Show();
            }

            _view.ShowInput(string.Empty);
        }
        
        private void OnInputUpdatedHandler(string input)
        {
            _model.SetLastInput(input);
        }
    }
}