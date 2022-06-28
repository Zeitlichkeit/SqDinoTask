using System;
using UnityEngine;
using Utils.ObjectPool;

namespace Weapons.Bullets
{
    public class BasicBullet : MonoBehaviour, IBullet
    {
        public int damage;
        public float speed;

        private IObjectPool<GameObject> _pool;

        public int GetDamage()
        {
            return damage;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public void Release()
        {
            try
            {
                _pool?.Release(this.gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetParentPool(IObjectPool<GameObject> pool)
        {
            _pool = pool;
        }

        private void OnCollisionEnter(Collision other)
        {
            Release();
        }
    }
}