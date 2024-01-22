using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    public class StringObserver : MonoBehaviour {
        [SerializeField] private GameObject rootObject;
        [SerializeField] private VariableObserver<string> variableObserver;

        private void OnEnable() {
            variableObserver.OnValueChanged(variableObserver.ObservedVariable.Value);
            variableObserver.StartWatching();
        }

        private void OnDisable() {
            variableObserver.StopWatching();
        }

        public void SetRootObjectVisibility(string newValue) {
            if (rootObject)
                rootObject.SetActive(!string.IsNullOrEmpty(newValue));
        }
    }
}
