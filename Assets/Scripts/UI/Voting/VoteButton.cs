using UnityEngine;

namespace CidadeDorme {
    public class VoteButton : GenericVoteButton {
        [SerializeField] private VotingInfo votingInfo;

        public override void OnSelect() {
            votingInfo.VoteFor(player);
        }
    }
}
