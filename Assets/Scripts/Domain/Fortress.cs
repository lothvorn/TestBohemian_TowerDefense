namespace Domain
{
    public class Fortress
    {
        private int _initialLives;
        public int Lives { get; private set; }

        public Fortress(int initialLives)
        {
            if (initialLives <= 0) throw new System.ArgumentOutOfRangeException(nameof(initialLives), "Lives must be > 0.");

            _initialLives = initialLives;

            Reset();
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

        public void Reset()
        {
            Lives = _initialLives;
        }
    }
}