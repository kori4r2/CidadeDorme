using UnityEngine;

namespace CidadeDorme {
    public class WerewolfVoteButton : GenericVoteButton {
        [SerializeField] private PlayerVariable targetReference;

        public override void OnSelect() {
            targetReference.Value = player;
        }
    }
}

