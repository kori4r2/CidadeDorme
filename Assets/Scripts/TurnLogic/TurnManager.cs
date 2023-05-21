using UnityEngine;
using Toblerone.Toolbox;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CidadeDorme {
    public class TurnManager : MonoBehaviour {
        [SerializeField] private TurnTimer turnTimer;
        [SerializeField] private EventSO timerEndedEvent;
        private EventListener timerEndedListener;
        [SerializeField] private EventSO playersSetupFinishedEvent;
        // TO DO: guarantee number of players based on classes
        // TO DO: establish balancing rules for classes
        [SerializeField] private List<Player> playerList;
        [SerializeField] private List<PlayerClass> playerClasses;
        [SerializeField] private List<Team> teams;
        [SerializeField] private TurnWaitInfo introductionInfo;
        [SerializeField] private TurnWaitInfo discussionInfo;
        [SerializeField] private TurnWaitInfo votingInfo;
        [SerializeField] private TurnWaitInfo playerTurnInfo;
        [SerializeField] private MessageHandler messageHandler;
        private UnityEvent TimerCallback = new UnityEvent();
        private bool hasValidTimerCallback = false;
        private int turnIndex = 0;

        public void StartGame() {
            foreach (Team team in teams) {
                team.Clear();
            }
            GiveClassesToPlayers();
            foreach (PlayerClass playerClass in playerClasses) {
                playerClass.SetupInterface();
            }
            turnIndex = -1;
            messageHandler.ShowClassList(playerClasses);
            StartTimer(StartNextPlayerIntroduction, introductionInfo);
        }

        private void GiveClassesToPlayers() {
            int[] indexArray = GenerateHelperArray();
            for (int index = 0; index < playerClasses.Count; index++) {
                int randomNumber = UnityEngine.Random.Range(0, playerClasses.Count - index);
                int selectedClassIndex = indexArray[randomNumber];

                playerList[index].SetupPlayer(playerClasses[selectedClassIndex]);

                indexArray[randomNumber] = indexArray[playerClasses.Count - index - 1];
                indexArray[playerClasses.Count - index - 1] = selectedClassIndex;
            }
            playersSetupFinishedEvent.Raise();
        }

        private int[] GenerateHelperArray() {
            int[] indexArray = new int[playerClasses.Count];
            for (int index = 0; index < playerClasses.Count; index++) {
                indexArray[index] = index;
            }
            return indexArray;
        }

        private void StartNextPlayerIntroduction() {
            messageHandler.HideMessage();
            foreach (Player player in playerList) {
                player.HideClass();
            }
            CalculateNewTurnIndex();
            if (turnIndex >= playerList.Count) {
                messageHandler.ShowMorningMessage(null);
                StartTimer(StartDiscussion, messageHandler.DefaultWaitInfo);
                return;
            }

            messageHandler.ShowNextPlayerMessage(playerList[turnIndex], false);
            StartTimer(CurrentPlayerIntroduction, introductionInfo);
        }

        private void CalculateNewTurnIndex() {
            turnIndex++;
            while (turnIndex < playerList.Count && !playerList[turnIndex].IsAlive) {
                turnIndex++;
            }
        }

        private void CurrentPlayerIntroduction() {
            messageHandler.HideMessage();
            Player player = playerList[turnIndex];
            messageHandler.ShowPlayerIntroduction(player);
            if (player.PlayerClass.CanSeeAllies) {
                foreach (Player ally in player.PlayerClass.Team) {
                    ally.ShowClass();
                }
            } else {
                player.ShowClass();
            }
            StartTimer(StartNextPlayerIntroduction, introductionInfo);
        }

        private void StartDiscussion() {
            messageHandler.HideMessage();
        }

        private void StartNextPlayerTurn() {
            messageHandler.HideMessage();
        }

        private void Awake() {
            messageHandler.Init();
            timerEndedListener = new EventListener(timerEndedEvent, InvokeTimerCallbackOnce);
        }

        private void InvokeTimerCallbackOnce() {
            if (!hasValidTimerCallback)
                return;
            hasValidTimerCallback = false;
            TimerCallback?.Invoke();
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
            hasValidTimerCallback = true;
            turnTimer.StartTimer(turnInfo.Duration, turnInfo.Thresold);
        }
    }
}
