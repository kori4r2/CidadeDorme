using UnityEngine;
using Toblerone.Toolbox;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Turn Timer")]
    public class TurnTimer : ScriptableObject {
        [SerializeField] private FloatVariable timerVariable;
        [SerializeField] private FloatVariable maxTimerVariable;
        [SerializeField] private FloatVariable hurryThresholdVariable;
        [SerializeField] private BoolVariable timerUIVisible;
        [SerializeField] private EventSO startTimerEvent;
        [SerializeField] private EventSO timerEndedEvent;
        private bool isActive = false;

        public void StartTimer(TurnWaitInfo turnWaitInfo) {
            maxTimerVariable.Value = turnWaitInfo.Duration;
            hurryThresholdVariable.Value = turnWaitInfo.Thresold;
            timerVariable.Value = maxTimerVariable.Value;
            timerUIVisible.Value = true;
            isActive = true;
            startTimerEvent.Raise();
        }

        public void Update() {
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
