using UnityEngine;
using Toblerone.Toolbox;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CidadeDorme {
    // TO DO: maybe refactor the class into more subclasses
    public partial class TurnManager : MonoBehaviour {
        [SerializeField] private TurnTimer turnTimer;
        [SerializeField] private EventSO timerEndedEvent;
        private EventListener timerEndedListener;
        [SerializeField] private EventSO playersSetupFinishedEvent;
        [SerializeField] private EventSO gameEndedEvent;
        [SerializeField] private PlayerListVariable playersAliveVariable;
        // TO DO: guarantee number of players based on classes
        // TO DO: establish balancing rules for classes
        [SerializeField] private List<Player> playerList;
        [SerializeField] private List<PlayerClass> playerClasses;
        [SerializeField] private List<Team> teams;
        [SerializeField] private TurnWaitTimes turnWaitTimes;
        [SerializeField] private MessageHandler messageHandler;
        [SerializeField] private VotingInfo votingInfo;
        [SerializeField] private NightChoices nightChoices;
        private UnityEvent TimerCallback = new UnityEvent();
        private bool hasValidTimerCallback = false;
        private int turnIndex = 0;
        private Team victoriousTeam = null;

        private void Awake() {
            playersAliveVariable.Value = new List<Player>();
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

        public void StartGame() {
            victoriousTeam = null;
            playersAliveVariable.Value.Clear();
            playersAliveVariable.Value.AddRange(playerList);
            foreach (Team team in teams) {
                team.Clear();
            }
            GiveClassesToPlayers();
            foreach (PlayerClass playerClass in playerClasses) {
                playerClass.SetupInterface();
            }
            turnIndex = -1;
            messageHandler.ShowClassList(playerClasses);
            StartTimer(StartNextPlayerIntroduction, turnWaitTimes.Introduction);
        }

        private void GiveClassesToPlayers() {
            int[] indexArray = GenerateHelperArray();
            for (int index = 0; index < playerClasses.Count; index++) {
                int randomNumber = Random.Range(0, playerClasses.Count - index);
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

        private static void ShowVisibleClasses(Player player) {
            if (!player.PlayerClass.CanSeeAllies) {
                player.ShowClass();
                return;
            }
            foreach (Player ally in player.PlayerClass.Team) {
                ally.ShowClass();
            }
        }

        private void CalculateNewTurnIndex() {
            turnIndex++;
            while (turnIndex < playerList.Count && !playerList[turnIndex].IsAlive) {
                turnIndex++;
            }
        }

        private bool CheckGameEnd() {
            foreach (Team team in teams) {
                if (team.CheckVictory(playersAliveVariable.Value)) {
                    victoriousTeam = team;
                    return true;
                }
            }
            return false;
        }

        private void ShowGameEndMessage() {
            foreach (Player player in playerList) {
                player.ShowClass();
            }
            messageHandler.ShowVictoryMessage(victoriousTeam);
            gameEndedEvent.Raise();
        }

        private void KillPlayer(Player player) {
            if (player == null)
                return;
            playersAliveVariable.Value.Remove(player);
            player.Kill();
        }
    }
}
