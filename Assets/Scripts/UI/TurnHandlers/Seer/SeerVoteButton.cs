using UnityEngine;

namespace CidadeDorme {
    public class SeerVoteButton : GenericVoteButton {
        [SerializeField] private PlayerVariable targetReference;

        public override void OnSelect() {
            targetReference.Value = player;
        }
    }
}
