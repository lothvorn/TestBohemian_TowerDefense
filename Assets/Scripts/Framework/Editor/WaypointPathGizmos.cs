using Framework.Runtime;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    public static class WaypointPathGizmos
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawSelectedGizmos(WaypointPath waypointPath, GizmoType gizmoType)
        {
            Transform[] waypoints = waypointPath.Waypoints;
            if (waypoints == null || waypoints.Length < 2)
                return;

            for (int waypointIndex = 0; waypointIndex < waypoints.Length - 1; waypointIndex++)
            {
                Transform currentWaypoint = waypoints[waypointIndex];
                Transform nextWaypoint = waypoints[waypointIndex + 1];

                if (currentWaypoint == null || nextWaypoint == null)
                    continue;

                Gizmos.DrawLine(currentWaypoint.position, nextWaypoint.position);
                Gizmos.DrawSphere(currentWaypoint.position, 0.1f);
            }

            Transform lastWaypoint = waypoints[waypoints.Length - 1];
            if (lastWaypoint != null)
                Gizmos.DrawSphere(lastWaypoint.position, 0.12f);
        }
    }
}