using UnityEngine;
using Toblerone.Toolbox;
using System.Collections.Generic;
using System.Text;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Message Handler")]
    public class MessageHandler : ScriptableObject {
        [SerializeField] private StringVariable displayMessageVariable;
        [SerializeField] private BoolVariable isMessageVisible;
        private StringBuilder stringBuilder = new StringBuilder();

        public void Init() {
            stringBuilder = new StringBuilder();
            isMessageVisible.Value = false;
            displayMessageVariable.Value = string.Empty;
        }

        public void ShowClassList(List<PlayerClass> classList) {
            Dictionary<PlayerClass, int> classesDict = new Dictionary<PlayerClass, int>();
            foreach (PlayerClass playerClass in classList) {
                if (classesDict.ContainsKey(playerClass))
                    classesDict[playerClass]++;
                else
                    classesDict[playerClass] = 1;
            }
            stringBuilder = new StringBuilder("Modo de Jogo escolhido:");
            foreach (PlayerClass playerClass in classesDict.Keys) {
                stringBuilder.Append($"\n{playerClass.ClassName} x {classesDict[playerClass]}");
            }
            ShowMessage(stringBuilder.ToString());
        }

        public void ShowPlayerIntroduction(Player player) {
            ShowMessage($"Você é {player.CharacterName}.\nSua Classe é {player.PlayerClass.ClassName}.{player.PlayerClass.Instructions}");
        }

        public void ShowNextPlayerMessage(Player nextPlayer, bool showName) {
            ShowMessage($"(Leia em voz alta)\n{(showName ? nextPlayer.CharacterName : nextPlayer.name)} acorda.\nAs demais pessoas dormem.");
        }

        public void ShowMorningMessage(List<Player> deadPlayers) {
            stringBuilder = new StringBuilder("(Leia em voz alta)\nToda a cidade acorda...");
            if (deadPlayers != null && deadPlayers.Count > 0) {
                foreach (Player deadPlayer in deadPlayers) {
                    stringBuilder.Append($"\n{deadPlayer.CharacterName} não está mais entre nós...");
                }
            } else {
                stringBuilder.Append("\nNada de estranho aconteceu esta noite.");
            }
            ShowMessage(stringBuilder.ToString());
        }

        public void ShowDiscussionMessage() {
            ShowMessage("Decidam em conjunto se alguma pessoa deve ser linchada ou não.");
        }

        public void ShowVoteResult(Player votedPlayer, bool gameEnded) {
            stringBuilder.Clear();
            if (votedPlayer == null)
                stringBuilder.Append("Não houve consenso na votação.");
            else
                stringBuilder.Append($"A cidade decidiu se livrar de {votedPlayer.CharacterName} pelo bem de todos...");
            if (!gameEnded)
                stringBuilder.Append("\nA cidade se prepara para mais uma noite...");
            ShowMessage(stringBuilder.ToString());
        }

        public void ShowVictoryMessage(Team victoriousTeam) {
            ShowMessage($"Fim de jogo!\n{victoriousTeam.VictoryText}");
        }

        public void ShowMessage(string messageDisplayed) {
            displayMessageVariable.Value = messageDisplayed;
            isMessageVisible.Value = true;
        }

        public void HideMessage() {
            isMessageVisible.Value = false;
        }
    }
}
