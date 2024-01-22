using System.Collections.Generic;
using Toblerone.Toolbox;
using UnityEngine;
using UnityEngine.Events;

namespace CidadeDorme {
    public class RaiseEventsOnStart : MonoBehaviour {
        [SerializeField] private List<EventSO> eventsToRaise;
        [SerializeField] private UnityEvent unityEvent;

        void Start() {
            unityEvent?.Invoke();
            foreach (EventSO eventToRaise in eventsToRaise) {
                if (eventToRaise != null)
                    eventToRaise.Raise();
            }
        }
    }
}
