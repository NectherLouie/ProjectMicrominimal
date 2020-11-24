using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class WaypointNode : MonoBehaviour
    {
        public WaypointNode prev;

        public WaypointNode next;

        [Range(0f, 5f)]
        public float width = 1.0f;
        
        public WaypointNode GetNextNode()
        {
            return next;
        }

        public Vector3 GetPosition()
        {
            Vector3 min = transform.position + transform.right * (width * 0.5f);
            Vector3 max = transform.position + transform.right * (width * 0.5f);

            return Vector3.Lerp(min, max, Random.Range(0f, 1f));
        }
    }
}
