using UnityEngine;

namespace Enemies
{
    public class BasicEnemyUI : EnemyUI
    {
        public BasicEnemy basicEnemy;

        protected override void Start()
        {
            enemy = basicEnemy;
            base.Start();
        }
    }
}