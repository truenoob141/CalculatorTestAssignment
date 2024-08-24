using System;
using UnityEngine;

namespace Calculator.CalculatorModule
{
    public class CalculatorViewFactory : MonoBehaviour
    {
        private const string pathToView = "CalculatorView";
        
        public CalculatorView CreateInstance()
        {
            // TODO Replace to better resource management system
            var prefab = Resources.Load<GameObject>(pathToView);
            if (prefab == null)
                throw new Exception(
                    $"Failed to create instance of {nameof(CalculatorView)}: Prefab '{pathToView}' not found");
            
            var instance = Instantiate(prefab, transform);
            var view = instance.GetComponent<CalculatorView>();
            if (view == null)
            {
                Destroy(instance);
                throw new Exception(
                    $"Failed to create instance of {nameof(CalculatorView)}: Prefab '{pathToView}' hasn't view component");
            }

            return view;
        }
    }
}