using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerNavigation : MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints;

        private int _currentWaypoint;

        public Transform GetNextWaypoint()
        {
            return waypoints[_currentWaypoint++];
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            waypoints?.ForEach(tr => {
                if (tr != null)
                {
                    Gizmos.DrawSphere(tr.position, 0.2f);
                }
            });
        }
        #endif
    }
}