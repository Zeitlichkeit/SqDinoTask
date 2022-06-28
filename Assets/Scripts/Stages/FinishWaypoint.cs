using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stages
{
    public class FinishWaypoint : Waypoint
    {
        public event PlayerWaypointEvent PlayerReachedFinish;

        private void Awake()
        {
            PlayerReachedFinish += ReloadScene;
        }

        private void OnTriggerEnter(Collider other)
        {
            var playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                PlayerReachedFinish?.Invoke(this, playerController);
            }
        }

        private void ReloadScene(Waypoint waypoint, PlayerController playerController)
        {
            SceneManager.LoadScene(0);
        }
    }
}