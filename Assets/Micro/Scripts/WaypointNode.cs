using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class WaypointNode : MonoBehaviour
    {
        [SerializeField]
        private WaypointNode prev;

        [SerializeField]
        private WaypointNode next;
    }
}
