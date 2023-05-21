using System.Collections.Generic;
using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerTurnHandler : MonoBehaviour {
        public abstract void Init();
        public abstract void ShowPlayerChoices(List<Player> playersAlive);
        public abstract void Hide();
        public abstract string GetActionFeedback();
    }
}
