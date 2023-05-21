using UnityEngine;

namespace CidadeDorme {
    [System.Serializable]
    [CreateAssetMenu(menuName = "CidadeDorme/TurnWaitInfo")]
    public class TurnWaitInfo : ScriptableObject {
        [SerializeField] private float duration;
        public float Duration => duration;
        [SerializeField] private float threshold;
        public float Thresold => threshold;
    }
}
