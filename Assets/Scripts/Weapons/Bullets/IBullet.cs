using UnityEngine;
using Utils.ObjectPool;

namespace Weapons.Bullets
{
    public interface IBullet
    {
        int GetDamage();
        float GetSpeed();
        void Release();
        void SetParentPool(IObjectPool<GameObject> pool);
    }
}