namespace Domain
{
    public class Score
    {
        public int Value { get; private set; }
        public bool GameWon { get;  set; }

        public void Add (int amount)
        {
            if (amount <= 0) return;
            Value += amount;
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}