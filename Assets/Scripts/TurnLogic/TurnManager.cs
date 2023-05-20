using UnityEngine;
using Toblerone.Toolbox;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CidadeDorme {
    public class TurnManager : MonoBehaviour {
        [SerializeField] private TurnTimer turnTimer;
        [SerializeField] private EventSO timerEndedEvent;
        private EventListener timerEndedListener;
        [SerializeField] private List<Player> playerList;
        [SerializeField] private TurnWaitInfo discussionInfo;
        [SerializeField] private TurnWaitInfo votingInfo;
        [SerializeField] private TurnWaitInfo playerTurnInfo;
        [SerializeField] private MessageHandler messageHandler;
        private UnityEvent TimerCallback = new UnityEvent();

        private void Awake() {
            timerEndedListener = new EventListener(timerEndedEvent, InvokeTimerCallbackOnce);
        }

        private void InvokeTimerCallbackOnce() {
            TimerCallback?.Invoke();
            TimerCallback.RemoveAllListeners();
        }

        private void OnEnable() {
            timerEndedListener.StartListeningEvent();
        }

        private void OnDisable() {
            timerEndedListener.StopListeningEvent();
        }

        private void StartTimer(UnityAction callback, TurnWaitInfo turnInfo) {
            TimerCallback.RemoveAllListeners();
            TimerCallback.AddListener(callback);
            turnTimer.StartTimer(turnInfo.Duration, turnInfo.Thresold);
        }
    }
}
