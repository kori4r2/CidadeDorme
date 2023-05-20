using UnityEngine;
using Toblerone.Toolbox;

namespace CidadeDorme {
    public class TurnTimer : MonoBehaviour {
        [SerializeField] private FloatVariable timerVariable;
        [SerializeField] private FloatVariable maxTimerVariable;
        [SerializeField] private FloatVariable hurryThresholdVariable;
        [SerializeField] private BoolVariable timerUIVisible;
        [SerializeField] private EventSO timerEndedEvent;
        private bool isActive = false;

        public void DebugStartTimer() {
            StartTimer(maxTimerVariable.Value, hurryThresholdVariable.Value);
        }

        public void StartTimer(float maxTime, float hurryThreshold) {
            maxTimerVariable.Value = maxTime;
            timerVariable.Value = maxTime;
            hurryThresholdVariable.Value = hurryThreshold;
            timerUIVisible.Value = true;
            isActive = true;
        }

        private void Update() {
            if (!isActive)
                return;

            timerVariable.Value -= Time.deltaTime;
            if (timerVariable.Value >= 0f)
                return;

            isActive = false;
            timerUIVisible.Value = false;
            timerEndedEvent.Raise();
        }
    }
}
