using System.Collections.Generic;
using Toblerone.Toolbox;
using UnityEngine;

namespace CidadeDorme {
    [CreateAssetMenu(menuName = "CidadeDorme/Event/PlayerListEvent")]
    public class PlayerListEvent : GenericEvent<List<Player>> { }
}
