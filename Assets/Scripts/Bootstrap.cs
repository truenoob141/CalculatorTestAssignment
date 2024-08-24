using Calculator.CalculatorModule;
using Calculator.DialogModule;
using Calculator.Interfaces;
using Calculator.PersistenceModule;
using UnityEngine;

namespace Calculator
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            // There is the entry point of the application
            
            // Declare dependencies (TODO Replace to DI)
            var dialogViewFactory = FindFirstObjectByType<DialogViewFactory>();
            var calculatorViewFactory = FindFirstObjectByType<CalculatorViewFactory>();
            
            IPersistenceService persistenceService = new PersistenceService();
            IDialogWindow dialogWindow = new DialogPresenter(dialogViewFactory);
            
            var model = new CalculatorModel(persistenceService);

            var calculatorPresenter = new CalculatorPresenter(model, calculatorViewFactory, dialogWindow);
            calculatorPresenter.LoadAndShow();
        }
    }
}
