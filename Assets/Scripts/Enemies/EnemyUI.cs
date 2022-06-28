using System;
using TMPro;
using UnityEngine;

namespace Enemies
{
    public class EnemyUI : MonoBehaviour
    {
        public TextMeshProUGUI healthText;
        public IEnemy enemy;

        protected virtual void Start()
        {
            enemy.HealthChanged += HealthChanged;
            enemy.EnemyDied += HealthChanged;
            ShowHealth();
        }

        public void ShowHealth()
        {
            if (!enemy.IsDead())
            {
                healthText.text = enemy.GetHealth().ToString();
            }
            else
            {
                healthText.gameObject.SetActive(false);
            }
        }

        protected virtual void HealthChanged(IEnemy enemy, int value)
        {
            ShowHealth();
        }
    }
}
