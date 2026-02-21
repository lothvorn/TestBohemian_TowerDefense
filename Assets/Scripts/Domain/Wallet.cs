namespace Domain
{
    public class Wallet
    {
        public int Gold { get; private set; }
        private int _initialGold;
        public Wallet(int initialGold)
        {
            if (initialGold < 0) throw new System.ArgumentOutOfRangeException(nameof(initialGold));
            _initialGold = initialGold;

            Reset();
        }

        public bool CanSpend (int amount)
        {
            return Gold >= amount;
        }

        public void SpendGold (int amount)
        {
            if (!CanSpend(amount))
                return;
            Gold -= amount;
        }

        public void AddGold (int amount)
        {
            if (amount <= 0) return;
            Gold += amount;
        }

        public void Reset()
        {
            Gold = _initialGold;
        }
    }
}