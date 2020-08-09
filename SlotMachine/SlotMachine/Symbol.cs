namespace SlotMachine
{
     public class Symbol<T>
     {
        public T Sign { get; set; }

        public decimal Coefficient { get; set; }

        public int AppearanceProbability { get; set; }

        public bool IsWildCard { get; set; } 

        public Symbol(T sign, decimal coefficient, int appearanceProb, bool isWildCard = false)
        {
            Sign = sign;
            Coefficient = coefficient;
            AppearanceProbability = appearanceProb;
            IsWildCard = isWildCard;
        }
    }
}
