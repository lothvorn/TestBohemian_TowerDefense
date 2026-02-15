using Core;
using UnityEngine;

namespace Framework.Runtime
{
    public sealed class WaypointPath : MonoBehaviour, IPathProvider
    {
        [SerializeField] private Transform[] waypoints = new Transform[0];

        public int PointCount => waypoints.Length;
        public Transform[] Waypoints => waypoints;
        
        public Vector3 GetPoint(int pointIndex)
        {
            if (waypoints.Length == 0)
                return transform.position;

            int clampedIndex = Mathf.Clamp(pointIndex, 0, waypoints.Length - 1);

            Transform waypointTransform = waypoints[clampedIndex];
            if (waypointTransform == null)
                return transform.position;

            return waypointTransform.position;
        }

        
    }
}