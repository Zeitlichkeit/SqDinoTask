using System;
using Player;
using UnityEngine;

namespace Stages
{
    public class Waypoint : MonoBehaviour
    {
        public delegate void PlayerWaypointEvent(Waypoint waypoint, PlayerController playerController);

        public event PlayerWaypointEvent PlayerReachedWaypoint;
        
        private void OnTriggerEnter(Collider other)
        {
            var playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                PlayerReachedWaypoint?.Invoke(this, playerController);
            }
        }
    }
}