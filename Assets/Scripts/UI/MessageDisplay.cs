using UnityEngine;
using TMPro;
using Toblerone.Toolbox;

namespace CidadeDorme {
    public class MessageDisplay : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private GameObject rootObject;
        [SerializeField] private StringVariable instructionVariable;
        [SerializeField] private BoolVariable isMessageVisible;
        private VariableObserver<bool> messageVisibleObserver;
        private bool isVisible = false;

        private void Awake() {
            messageVisibleObserver = new VariableObserver<bool>(isMessageVisible, ChangeVisibility);
        }

        private void ChangeVisibility(bool newVisibilityState) {
            if (isVisible == newVisibilityState)
                return;
            isVisible = newVisibilityState;
            if (isVisible)
                Show();
            else
                Hide();
        }

        private void Show() {
            textField.text = instructionVariable.Value;
            rootObject.SetActive(true);
        }

        private void Hide() {
            rootObject.SetActive(false);
            textField.text = string.Empty;
        }

        private void OnEnable() {
            messageVisibleObserver.StartWatching();
        }

        private void OnDisable() {
            messageVisibleObserver.StopWatching();
        }
    }
}
