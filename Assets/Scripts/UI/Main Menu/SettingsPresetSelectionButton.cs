using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMPro;
using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    public class SettingsPresetSelectionButton : MonoBehaviour {
        [SerializeField] private MatchSettings currentSettings;
        [SerializeField] private MatchSettingsPreset preset;
        [SerializeField] private TextMeshProUGUI presetName;
        [SerializeField] private StringVariable presetDescriptionVariable;
        private StringBuilder stringBuilder = new StringBuilder();

        public void OnSelect() {
            ShowPresetOptions();
        }

        private void ShowPresetOptions() {
            presetDescriptionVariable.Value = stringBuilder.ToString();
        }

        public void OnSubmit() {
            currentSettings.LoadPreset(preset);
        }

        void Awake() {
            BuildDescriptionString();
            if (preset == null) {
                currentSettings.AddClassesUpdatedListener(BuildDescriptionString);
                currentSettings.AddPresetLoadListener(BuildDescriptionString);
            }
            SetupButtonAppearance();
        }

        private void BuildDescriptionString() {
            stringBuilder.Clear();
            bool firstLine = true;
            ReadOnlyDictionary<PlayerClass, int> presetClasses = preset != null ? BuildPresetDictionary() : currentSettings.Classes;
            List<PlayerClass> keys = new List<PlayerClass>(presetClasses.Keys);
            keys.Sort((a, b) => a.ClassName.CompareTo(b.ClassName));
            foreach (PlayerClass key in keys) {
                if (presetClasses[key] <= 0)
                    continue;
                if (!firstLine)
                    stringBuilder.Append('\n');
                stringBuilder.Append($"{key.ClassName} X {presetClasses[key]}");
                firstLine = false;
            }
        }

        private ReadOnlyDictionary<PlayerClass, int> BuildPresetDictionary() {
            Dictionary<PlayerClass, int> classes = new Dictionary<PlayerClass, int>();
            foreach (PlayerClass playerClass in preset.AvailableClasses) {
                if (classes.ContainsKey(playerClass))
                    classes[playerClass]++;
                else
                    classes[playerClass] = 1;
            }
            return new ReadOnlyDictionary<PlayerClass, int>(classes);
        }

        private void SetupButtonAppearance() {
            presetName.text = preset != null ? preset.DisplayName : "Configurações atuais";
        }
    }
}
