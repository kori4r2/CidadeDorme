using TMPro;
using Toblerone.Toolbox;
using UnityEngine;
using UnityEngine.UI;

namespace CidadeDorme {
    public class PlayerClassSelectionButton : MonoBehaviour {
        [SerializeField] private MatchSettings currentSettings;
        [SerializeField] private PlayerClass playerClass;
        [SerializeField] private Button increaseCountButton;
        [SerializeField] private Button decreaseCountButton;
        [SerializeField] private TextMeshProUGUI className;
        [SerializeField] private TextMeshProUGUI classCount;
        [SerializeField] private StringVariable classDescriptionVariable;

        public void OnSelect() {
            ShowPresetOptions();
        }

        private void ShowPresetOptions() {
            classDescriptionVariable.Value = playerClass.Instructions;
        }

        public void IncreasePlayerClassCount() {
            currentSettings.AddClass(playerClass);
            if (!increaseCountButton.interactable)
                decreaseCountButton.Select();
        }

        public void DecreasePlayerClassCount() {
            currentSettings.RemoveClass(playerClass);
            if (!decreaseCountButton.interactable)
                increaseCountButton.Select();
        }

        void Start() {
            SetupButtonAppearance();
            currentSettings.AddClassesUpdatedListener(UpdateButtonsStateAndCount);
            currentSettings.AddPresetLoadListener(UpdateButtonsStateAndCount);
            UpdateButtonsState();
        }

        private void SetupButtonAppearance() {
            className.text = playerClass.ClassName;
            UpdateClassCount();
        }

        private void UpdateClassCount() {
            classCount.text = $"{currentSettings.GetClassCount(playerClass)}";
        }

        private void UpdateButtonsStateAndCount() {
            UpdateButtonsState();
            UpdateClassCount();
        }

        private void UpdateButtonsState() {
            increaseCountButton.interactable = currentSettings.CanAddClass(playerClass);
            decreaseCountButton.interactable = currentSettings.CanRemoveClass(playerClass);
        }
    }
}
