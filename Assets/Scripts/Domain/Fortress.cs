namespace Domain
{
    public class Fortress
    {
        public int Lives { get; private set; }

        public Fortress(int lives)
        {
            if (lives <= 0) throw new System.ArgumentOutOfRangeException(nameof(lives), "Lives must be > 0.");
            
            Lives = lives;
        }

        public void ReceiveDamage(int damage)
        {
            if (damage <= 0) return;

            Lives -= damage;
            if (Lives < 0) Lives = 0;
        }

        public bool IsDestroyed()
        {
            return Lives <= 0;
        }
    }
}
