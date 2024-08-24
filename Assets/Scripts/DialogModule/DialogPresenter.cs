using Calculator.CalculatorModule;
using Calculator.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Calculator.DialogModule
{
    public class DialogPresenter : IDialogWindow
    {
        private readonly CalculatorModel _model;
        private readonly DialogViewFactory _viewFactory;

        private DialogView _view;
        
        public DialogPresenter(DialogViewFactory viewFactory)
        {
            Assert.IsNotNull(viewFactory);
            
            _viewFactory = viewFactory;
        }

        public void Show()
        {
            Assert.IsNull(_view, "Already loaded");

            _view = _viewFactory.CreateInstance();
            _view.OnClose += OnCloseHandler;
        }

        public void Close()
        {
            Assert.IsNotNull(_view, "View not loaded");

            _view.OnClose -= OnCloseHandler;
            Object.Destroy(_view.gameObject);
        }

        private void OnCloseHandler()
        {
            Close();
        }
    }
}