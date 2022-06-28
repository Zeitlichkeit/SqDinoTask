using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(PlayerNavigation), typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsMove { get; private set; }

        [SerializeField] private Animator animator;
        private PlayerNavigation _playerNavigation;
        private NavMeshAgent _navMeshAgent;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerNavigation = GetComponent<PlayerNavigation>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.1f && IsMove)
            {
                IsMove = false;
                animator.SetBool("Run", false);
                _playerController.AbleToShoot = true;
            }
        }

        public void MoveToNext()
        {
            Transform waypoint = _playerNavigation.GetNextWaypoint();
            _navMeshAgent.SetDestination(waypoint.position);
            IsMove = true;
            animator.SetBool("Run", true);
            _playerController.AbleToShoot = false;
        }
    }
}