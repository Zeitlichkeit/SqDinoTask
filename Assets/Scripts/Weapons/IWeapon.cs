using UnityEngine;
using Weapons.Bullets;

namespace Weapons
{
    public interface IWeapon
    {
        void Shoot(Vector3 direction);
        Vector3 GetShootPosition();
    }
}