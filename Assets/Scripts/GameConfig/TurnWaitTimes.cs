using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Configs/Turn Wait Times")]
    public class TurnWaitTimes : ScriptableObject {
        [SerializeField] private TurnWaitInfo defaultMessage;
        public TurnWaitInfo DefaultMessage => defaultMessage;
        [SerializeField] private TurnWaitInfo introduction;
        public TurnWaitInfo Introduction => introduction;
        [SerializeField] private TurnWaitInfo discussion;
        public TurnWaitInfo Discussion => discussion;
        [SerializeField] private TurnWaitInfo voting;
        public TurnWaitInfo Voting => voting;
        [SerializeField] private TurnWaitInfo playerTurn;
        public TurnWaitInfo PlayerTurn => playerTurn;
    }
}
