using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Name List")]
    public class NameList : ScriptableObject {
        [SerializeField] private List<string> nameList = new List<string>() { "Vitor", "Fabiane", "Tiago", "Júlia", "Gabriel", "Aline", "Guilherme", "Maria", "Arthur", "Caroline", "Cris", "Luca", "Rafa" };
        private List<string> nameListCopy;

        private void OnEnable() {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += ResetNameListCopyOnPlayModeStart;
#endif
            ResetNameListCopy();
        }

#if UNITY_EDITOR
        private void ResetNameListCopyOnPlayModeStart(PlayModeStateChange newState) {
            if (newState != PlayModeStateChange.EnteredPlayMode)
                return;
            Debug.Log("Name list has been reset");
            ResetNameListCopy();
        }
#endif

        [ContextMenu("Reset Name List Copy")]
        private void ResetNameListCopy() {
            nameListCopy = new List<string>(nameList);
        }

        public void ReturnName(string name) {
            if (!string.IsNullOrEmpty(name) && nameList.Contains(name))
                nameListCopy.Add(name);
        }

        public string GetNewName() {
            int index = Random.Range(0, nameListCopy.Count);
            string newName = nameListCopy[index];
            nameListCopy.RemoveAt(index);
            return newName;
        }
    }
}
