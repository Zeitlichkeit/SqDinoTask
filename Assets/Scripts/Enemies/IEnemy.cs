namespace Enemies
{
    public delegate void HealthEvent(IEnemy enemy, int value);
    
    public interface IEnemy
    {
        public event HealthEvent HealthChanged;
        public event HealthEvent EnemyDied;
        int GetHealth();
        bool IsDead();
    }
}