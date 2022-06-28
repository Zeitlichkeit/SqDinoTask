using System;
using System.Collections.Generic;
using Enemies;
using Player;
using UnityEngine;

namespace Stages
{
    public class StageControl : MonoBehaviour
    {
        [SerializeField] private Waypoint waypoint;
        [SerializeField] private List<GameObject> enemies;

        private PlayerController _playerController;

        private void Start()
        {
            enemies.ForEach(enemy => enemy.GetComponent<IEnemy>().EnemyDied += EnemyDied);
            waypoint.PlayerReachedWaypoint += PlayerReachedStage;
        }

        private void EnemyDied(IEnemy enemy, int health)
        {
            Transform nearest = NearestTo(_playerController.transform.position);
            _playerController.SetHitPlanePosition(nearest);
            if (nearest == null)
            {
                _playerController.GetComponent<PlayerMovement>().MoveToNext();
            }
        }

        private void PlayerReachedStage(Waypoint waypoint, PlayerController playerController)
        {
            _playerController = playerController;
            Transform nearest = NearestTo(playerController.transform.position);
            if (nearest != null)
            {
                playerController.SetHitPlanePosition(nearest);
            }
        }

        private Transform NearestTo(Vector3 position)
        {
            float minMagnitude = float.PositiveInfinity;
            int minIndex = -1;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetComponent<IEnemy>().IsDead())
                {
                    continue;
                }
                float sqrMagnitude = (position - enemies[i].transform.position).sqrMagnitude;
                if (sqrMagnitude < minMagnitude)
                {
                    minMagnitude = sqrMagnitude;
                    minIndex = i;
                }
            }

            return minIndex >= 0 ? enemies[minIndex].transform : null;
        }
    }
}