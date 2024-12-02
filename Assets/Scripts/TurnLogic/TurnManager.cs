using UnityEngine;
using Toblerone.Toolbox;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CidadeDorme {
    public partial class TurnManager : MonoBehaviour {
        [Header("Timer")]
        [SerializeField] private EventSO timerEndedEvent;
        private EventListener timerEndedListener;
        [SerializeField] private TurnTimer turnTimer;
        [Header("References")]
        [SerializeField] private EventSO playersSetupFinishedEvent;
        [SerializeField] private EventSO gameStartedEvent;
        private EventListener gameStartedListener;
        [SerializeField] private EventSO gameEndedEvent;
        [SerializeField] private PlayerListVariable playersAliveVariable;
        [Header("Match Settings")]
        [SerializeField] private MatchSettings matchSettings;
        private List<Player> playerList;
        private List<PlayerClass> playerClasses;
        private List<Team> teams;
        [SerializeField] private TurnWaitTimes turnWaitTimes;
        [SerializeField] private MessageHandler messageHandler;
        [SerializeField] private VotingInfo votingInfo;
        [SerializeField] private NightChoices nightChoices;
        private UnityEvent TimerCallback = new UnityEvent();
        private bool hasValidTimerCallback = false;
        private Team victoriousTeam = null;
        private PlayerQueue playerQueue;

        private void Awake() {
            playersAliveVariable.Value = new List<Player>();
            messageHandler.Init();
            timerEndedListener = new EventListener(timerEndedEvent, InvokeTimerCallbackOnce);
            gameStartedListener = new EventListener(gameStartedEvent, StartGame);
        }

        private void InvokeTimerCallbackOnce() {
            if (!hasValidTimerCallback)
                return;
            hasValidTimerCallback = false;
            TimerCallback?.Invoke();
        }

        private void OnEnable() {
            timerEndedListener.StartListeningEvent();
            gameStartedListener.StartListeningEvent();
        }

        private void OnDisable() {
            timerEndedListener.StopListeningEvent();
            gameStartedListener.StopListeningEvent();
        }

        private void Update() {
            turnTimer.Update();
        }

        private void StartTimer(UnityAction callback, TurnWaitInfo turnInfo) {
            TimerCallback.RemoveAllListeners();
            TimerCallback.AddListener(callback);
            hasValidTimerCallback = true;
            turnTimer.StartTimer(turnInfo);
        }

        public void StartGame() {
            LoadSettings();
            victoriousTeam = null;
            playersAliveVariable.Value.Clear();
            foreach (Team team in teams) {
                team.Clear();
            }
            GiveClassesToPlayers();
            playerQueue = new PlayerQueue(playerList);
            foreach (PlayerClass playerClass in playerClasses) {
                playerClass.SetupInterface();
            }
            messageHandler.ShowClassList(playerClasses);
            StartTimer(StartNextPlayerIntroduction, turnWaitTimes.Introduction);
        }

        private void LoadSettings() {
            playerList = matchSettings.AllPlayers;
            playerClasses = matchSettings.AvailableClasses;
            teams = matchSettings.Teams;
        }

        private void GiveClassesToPlayers() {
            int[] indexArray = GenerateHelperArray();
            for (int index = 0; index < playerList.Count; index++) {
                if (index < playerClasses.Count) {
                    GiveRandomClassToPlayer(indexArray, index);
                } else {
                    playerList[index].IsPlaying = false;
                }
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

        private void GiveRandomClassToPlayer(int[] indexArray, int index) {
            playersAliveVariable.Value.Add(playerList[index]);
            int randomNumber = Random.Range(0, playerClasses.Count - index);
            int selectedClassIndex = indexArray[randomNumber];

            playerList[index].SetupPlayer(playerClasses[selectedClassIndex]);
            playerList[index].IsPlaying = true;

            indexArray[randomNumber] = indexArray[playerClasses.Count - index - 1];
            indexArray[playerClasses.Count - index - 1] = selectedClassIndex;
        }

        private static void ShowVisibleAllies(Player player) {
            if (!player.PlayerClass.CanSeeAllies) {
                player.ShowClass();
                return;
            }
            foreach (Player ally in player.PlayerClass.Team) {
                if (ally == player)
                    ally.ShowClass();
                else
                    ally.ShowTeam();
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

        // Day

        private void StartDiscussion() {
            messageHandler.ShowDiscussionMessage();
            votingInfo.BeginVotingCheck();
            StartTimer(CheckNeedForAVote, turnWaitTimes.Discussion);
        }

        private void CheckNeedForAVote() {
            messageHandler.HideMessage();
            votingInfo.EndVotingCheck();
            playerQueue.ResetQueue();
            if (votingInfo.VotingShouldHappen) {
                votingInfo.PrepareForVoting(playersAliveVariable.Value);
                StartNextPlayerVote(true);
            } else {
                StartNextPlayerTurn();
            }
        }

        private void StartNextPlayerVote(bool isFirst = false) {
            if (!isFirst)
                votingInfo.EndPlayerVote();
            if (playerQueue.IsEmpty) {
                ShowVoteResults();
            } else {
                votingInfo.GetPlayerVote(playerQueue.Dequeue());
                StartTimer(() => StartNextPlayerVote(), turnWaitTimes.Voting);
            }
        }

        private void ShowVoteResults() {
            Player votedPlayer = votingInfo.GetVoteResult();
            KillPlayer(votedPlayer);
            bool gameEnded = CheckGameEnd();
            messageHandler.ShowVoteResult(votedPlayer, gameEnded);
            if (gameEnded) {
                StartTimer(ShowGameEndMessage, turnWaitTimes.DefaultMessage);
            } else {
                playerQueue.ResetQueue();
                nightChoices.Clear();
                StartTimer(StartNextPlayerTurn, turnWaitTimes.DefaultMessage);
            }
        }

        // Night

        private void StartNextPlayerIntroduction() {
            foreach (Player player in playerList) {
                player.HideInfo();
            }
            if (playerQueue.IsEmpty) {
                messageHandler.ShowMorningMessage(null);
                StartTimer(StartDiscussion, turnWaitTimes.DefaultMessage);
                return;
            }

            Player nextPlayer = playerQueue.Dequeue();
            messageHandler.ShowNextPlayerMessage(nextPlayer, false);
            StartTimer(() => CurrentPlayerIntroduction(nextPlayer), turnWaitTimes.DefaultMessage);
        }

        private void CurrentPlayerIntroduction(Player player) {
            messageHandler.ShowPlayerIntroduction(player);
            ShowVisibleAllies(player);
            StartTimer(StartNextPlayerIntroduction, turnWaitTimes.Introduction);
        }

        private void StartNextPlayerTurn() {
            foreach (Player player in playerList) {
                player.HideInfo();
            }
            if (playerQueue.IsEmpty) {
                ShowNightResults();
            } else {
                Player nextPlayer = playerQueue.Dequeue();
                messageHandler.ShowNextPlayerMessage(nextPlayer, true);
                StartTimer(() => ShowCurrentPlayerChoices(nextPlayer), turnWaitTimes.DefaultMessage);
            }
        }

        private void ShowCurrentPlayerChoices(Player player) {
            messageHandler.HideMessage();
            ShowVisibleAllies(player);
            player.PlayerClass.StartTurn(player);
            StartTimer(() => ShowCurrentPlayerResults(player), turnWaitTimes.PlayerTurn);
        }

        private void ShowCurrentPlayerResults(Player player) {
            string actionResult = player.PlayerClass.GetTurnResult();
            messageHandler.ShowMessage(actionResult);
            StartTimer(StartNextPlayerTurn, turnWaitTimes.DefaultMessage);
        }

        private void ShowNightResults() {
            List<Player> deadPlayers = nightChoices.GetDeadPlayers();
            foreach (Player deadPlayer in deadPlayers) {
                KillPlayer(deadPlayer);
            }
            messageHandler.ShowMorningMessage(deadPlayers);
            bool gameEnded = CheckGameEnd();
            if (gameEnded) {
                StartTimer(ShowGameEndMessage, turnWaitTimes.DefaultMessage);
            } else {
                StartTimer(StartDiscussion, turnWaitTimes.DefaultMessage);
            }
        }
    }
}
