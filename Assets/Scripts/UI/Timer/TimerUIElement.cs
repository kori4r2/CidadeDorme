using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    public abstract class TimerUIElement : MonoBehaviour {
        [SerializeField] protected FloatVariable timerVariable;
        protected VariableObserver<float> timeObserver;
        [SerializeField] protected BoolVariable isVisibleVariable;
        protected VariableObserver<bool> isVisibleObserver;
        protected bool isVisible = false;

        protected virtual void Awake() {
            timeObserver = new VariableObserver<float>(timerVariable, UpdatedTimer);
            isVisibleObserver = new VariableObserver<bool>(isVisibleVariable, ChangeVisibleState);
            Hide();
        }

        protected abstract void UpdatedTimer(float newTimerValue);

        protected void ChangeVisibleState(bool newIsVisibleValue) {
            if (isVisible == newIsVisibleValue)
                return;

            isVisible = newIsVisibleValue;
            if (isVisible)
                Show();
            else
                Hide();
        }

        protected virtual void Show() {
            timeObserver.StartWatching();
        }

        protected virtual void Hide() {
            timeObserver.StopWatching();
        }

        protected virtual void OnEnable() {
            isVisibleObserver.StartWatching();
        }

        protected virtual void OnDisable() {
            isVisibleObserver.StopWatching();
            timeObserver.StopWatching();
        }
    }
}
