using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Micro
{
    [InitializeOnLoad()]
    public class WaypointEditor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
        public static void OnDrawSceneGizmo(WaypointNode waypoint, GizmoType gizmoType)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.grey * 0.25f;
            }

            Gizmos.DrawSphere(waypoint.transform.position, 0.25f);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(waypoint.transform.position, waypoint.width * 0.5f);

            if (waypoint.prev != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(waypoint.transform.position, waypoint.prev.transform.position);
            }

            if (waypoint.next != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(waypoint.transform.position, waypoint.next.transform.position);
            }
        }
    }
}
