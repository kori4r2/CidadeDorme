using System;
using System.Collections.Generic;

namespace CidadeDorme {
    public class PlayerQueue {
        private List<Player> players = new List<Player>();
        private int currentIndex = 0;
        public bool IsEmpty => currentIndex >= players.Count;

        public PlayerQueue(List<Player> players) {
            this.players = new List<Player>(players);
            FindNextValidPlayer();
        }

        private void FindNextValidPlayer() {
            while (currentIndex < players.Count && !players[currentIndex].IsAlive) {
                currentIndex++;
            }
        }

        public void ResetQueue() {
            currentIndex = 0;
            FindNextValidPlayer();
        }

        public Player Dequeue() {
            FindNextValidPlayer();
            Player dequeued = players[currentIndex++];
            if (dequeued == null)
                throw new Exception("[PlayerQueue]: Invalid dequeue result");
            FindNextValidPlayer();
            return dequeued;
        }
    }
}
