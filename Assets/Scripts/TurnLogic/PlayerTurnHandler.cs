using System.Collections.Generic;
using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    public abstract class PlayerTurnHandler : MonoBehaviour {
        [SerializeField] protected PlayerTurnHandlerVariable reference;
        [SerializeField] private PlayerListEvent playersAliveUpdated;
        private GenericEventListener<List<Player>> playersAliveUpdatedListener;
        protected Player currentPlayer;
        public abstract void Init();
        public abstract void ShowPlayerChoices(Player currentPlayer);
        public abstract void Hide();
        public abstract string GetActionFeedback();
        private List<Player> playersAlive = new List<Player>();

        protected virtual void Awake() {
            reference.Value = this;
            playersAliveUpdatedListener = new GenericEventListener<List<Player>>(playersAliveUpdated, newList => playersAlive = new List<Player>(newList));
        }

        protected virtual void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
        }

        protected virtual void OnEnable() {
            playersAliveUpdatedListener.StartListeningEvent();
        }

        protected virtual void OnDisable() {
            playersAliveUpdatedListener.StopListeningEvent();
        }
    }
}
