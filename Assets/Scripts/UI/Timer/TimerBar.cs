using UnityEngine;
using UnityEngine.UI;
using Toblerone.Toolbox;

namespace CidadeDorme {
    public class TimerBar : TimerUIElement {
        [Space]
        [SerializeField] private Slider slider;
        [SerializeField] private FloatVariable maxTimerValueVariable;

        protected override void UpdatedTimer(float newTimerValue) {
            newTimerValue = Mathf.Clamp(newTimerValue, 0f, maxTimerValueVariable.Value);
            slider.value = newTimerValue / maxTimerValueVariable.Value;
        }

        protected override void Hide() {
            slider.value = 1.0f;
            slider.gameObject.SetActive(false);
            base.Hide();
        }

        protected override void Show() {
            slider.value = 1.0f;
            slider.gameObject.SetActive(true);
            base.Show();
        }
    }
}
