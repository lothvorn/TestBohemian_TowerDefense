namespace Domain
{
    public class Enemy
    {
        public int Strength { get; }
        public float Speed { get; }
        public int MaxHealth { get; }
        public int Reward { get; }
        
        public int CurrentHealth { get; private set; }

        public Enemy(float speed, int maxHealth, int reward, int strength)
        {
            if (speed <= 0) throw new System.ArgumentOutOfRangeException(nameof(speed), "Speed must be greater than zero.");
            if (maxHealth <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxHealth), "MaxHealth must be greater than zero.");
            if (reward < 0) throw new System.ArgumentOutOfRangeException(nameof(reward), "Reward must be greater or equal than zero.");
            if (strength <= 0) throw new System.ArgumentOutOfRangeException(nameof(speed), "Strength must be greater than zero.");

            Speed = speed;
            MaxHealth = maxHealth;
            Reward = reward;
            Strength = strength;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead()) 
                return;
            
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                CurrentHealth = 0;

        }
        
        
        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
    }
}