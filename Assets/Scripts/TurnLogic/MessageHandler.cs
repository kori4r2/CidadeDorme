using UnityEngine;
using Toblerone.Toolbox;

namespace CidadeDorme {
    [System.Serializable]
    public class MessageHandler {
        [SerializeField] private TurnWaitInfo waitInfo;
        public TurnWaitInfo WaitInfo => waitInfo;
        [SerializeField] private StringVariable displayMessageVariable;
        [SerializeField] private BoolVariable isMessageVisible;

        public void ShowMessage(string messageDisplayed) {
            displayMessageVariable.Value = messageDisplayed;
            isMessageVisible.Value = true;
        }

        public void HideMessage() {
            isMessageVisible.Value = false;
        }
    }
}
