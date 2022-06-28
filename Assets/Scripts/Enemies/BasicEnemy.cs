using System;
using UnityEngine;
using Utils;
using Weapons.Bullets;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class BasicEnemy : MonoBehaviour, IEnemy, IRagdollActivable
    {

        public bool useSlowmoEffect = true;
        public float slowmoTime = 0.1f;
        public bool enableRagdollOnDeath = true;
        [SerializeField] private int health;
        [SerializeField] private Animator _animator;

        private Rigidbody _rigidbody;
        private Collider _collider;
        
        public event HealthEvent HealthChanged;
        public event HealthEvent EnemyDied;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            DisableRagdoll();
        }

        public int GetHealth()
        {
            return health;
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        private void OnCollisionEnter(Collision other)
        {
            var bullet = other.gameObject.GetComponent<IBullet>();
            if (bullet != null && !IsDead())
            {
                DecreaseHealth(bullet.GetDamage());
                if (useSlowmoEffect)
                {
                    SlowMotionManager.Instance.ActivateSlowMotionFor(slowmoTime);
                }
            }
        }

        private void DecreaseHealth(int value)
        {
            health -= value;
            if (IsDead())
            {
                health = 0;
                EnemyDied?.Invoke(this, health);
                if (enableRagdollOnDeath)
                {
                    EnableRagdoll();
                }
            }
            else
            {
                HealthChanged?.Invoke(this, health);
            }
        }

        public void EnableRagdoll()
        {
            SetRigidbodiesKinematic(true);
        }

        public void DisableRagdoll()
        {
            SetRigidbodiesKinematic(false);
        }
        
        void SetRigidbodiesKinematic(bool state)
        {
            _animator.enabled = !state;
            _rigidbody.isKinematic = !state;
            _collider.enabled = !state;
            Rigidbody[] bodies = _animator.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in bodies)
            {
                rb.isKinematic = !state;
            }
        }
    }
}
