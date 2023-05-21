using UnityEngine;
using TMPro;
using Toblerone.Toolbox;

namespace CidadeDorme {
    public class MessageDisplay : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private GameObject rootObject;
        [SerializeField] private StringVariable messageStringVariable;
        [SerializeField] private BoolVariable isMessageVisible;
        private VariableObserver<bool> messageVisibleObserver;

        private void Awake() {
            Hide();
            messageVisibleObserver = new VariableObserver<bool>(isMessageVisible, ChangeVisibility);
        }

        private void ChangeVisibility(bool newVisibilityState) {
            if (newVisibilityState)
                Show();
            else
                Hide();
        }

        private void Show() {
            textField.text = messageStringVariable.Value;
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
