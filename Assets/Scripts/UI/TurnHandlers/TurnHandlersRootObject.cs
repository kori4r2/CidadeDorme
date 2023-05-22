using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    public class TurnHandlersRootObject : MonoBehaviour {
        [SerializeField] private GameObjectVariable reference;

        private void Awake() {
            reference.Value = gameObject;
        }

        private void OnDestroy() {
            if (reference.Value == gameObject)
                reference.Value = null;
        }
    }
}
