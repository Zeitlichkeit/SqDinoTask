using UnityEngine;
using Weapons.Bullets;
using Utils.ObjectPool;

namespace Weapons
{
    public class BasicWeapon : MonoBehaviour, IWeapon
    {
        public bool collectionChecks = true;
        public int maxPoolSize = 5;

        public IObjectPool<GameObject> Bullets
        {
            get
            {
                if (_bullets == null)
                {
                    _bullets = new ObjectPool<GameObject>(BulletInstance,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        collectionChecks,
                        maxPoolSize);
                }

                return _bullets;
            }
        }

        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bulletPrefab;
        private IObjectPool<GameObject> _bullets;

        public void Shoot(Vector3 direction)
        {
            var bulletObject = Bullets.Get();
            var bullet = bulletObject.GetComponent<IBullet>();
            bullet.SetParentPool(Bullets);
            bulletObject.transform.position = shootPoint.position;
            var bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(direction * bullet.GetSpeed());
        }

        public Vector3 GetShootPosition()
        {
            return shootPoint.position;
        }

        private GameObject BulletInstance()
        {
            var bullet = Instantiate(bulletPrefab);
            return bullet;
        }

        void OnReturnedToPool(GameObject obj)
        {
            obj.SetActive(false);
            var bulletRigidbody = obj.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
        }

        void OnTakeFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        void OnDestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }
    }
}