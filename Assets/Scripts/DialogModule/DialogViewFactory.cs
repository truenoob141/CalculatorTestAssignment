using System;
using UnityEngine;

namespace Calculator.DialogModule
{
    public class DialogViewFactory : MonoBehaviour
    {
        private const string pathToView = "DialogView";
        
        public DialogView CreateInstance()
        {
            // TODO Replace to better resource management system
            var prefab = Resources.Load<GameObject>(pathToView);
            if (prefab == null)
                throw new Exception(
                    $"Failed to create instance of {nameof(DialogView)}: Prefab '{pathToView}' not found");
            
            var instance = Instantiate(prefab, transform);
            var view = instance.GetComponent<DialogView>();
            if (view == null)
            {
                Destroy(instance);
                throw new Exception(
                    $"Failed to create instance of {nameof(DialogView)}: Prefab '{pathToView}' hasn't view component");
            }

            return view;
        }
    }
}