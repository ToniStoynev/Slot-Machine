namespace SlotMachine
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please deposit money you would like to play with:");

            #region Validate Initial Balance UserInput
            var isSuccesfullyParsedInput = decimal.TryParse(Console.ReadLine(), out decimal deposit);

            while (!isSuccesfullyParsedInput)
            {
                Console.WriteLine("The input must be a number !!!");
                Console.WriteLine("Please deposit money you would like to play with:");
                isSuccesfullyParsedInput = decimal.TryParse(Console.ReadLine(), out deposit);
            }
            #endregion

            #region Initialize The Rules Of The Game

            //Determine supported symbols with their type of Sign, coefficients and probability to appear on a cell
            List<Symbol<char>> slotGameSymbols = new List<Symbol<char>>()
            {
                new Symbol<char>('A', 0.4m, 45),
                new Symbol<char>('B', 0.6m, 35),
                new Symbol<char>('P', 0.8m, 15),
                new Symbol<char>('*', 0.0m, 5, true),
            };

            //Initialize a Slot Game with given sizes and supported symbols
            var slotGame = new SlotGame<char>(4, 3, slotGameSymbols);
           
            #endregion

            #region Play The Game! Good Luck :)
            while (deposit > 0)
            {
                Console.WriteLine("Enter stake amount: ");
                decimal stake = decimal.Parse(Console.ReadLine());

                //Check if the player has enough money to bet
                if (stake > deposit)
                {
                    Console.WriteLine("Sorry! You don't have enough credit !!!");
                    Console.WriteLine("Enter stake amount: ");
                    stake = decimal.Parse(Console.ReadLine());
                }

                //Generate The Next Spin Of The Game
                var nextSpin = SlotGame<char>.GenerateGameTable(slotGame);

                //Calculate the sum of the coefficients of the symbols on the winning line(s)
                var tableCoef = SlotGame<char>.CalculateTableCoefficient(nextSpin);

                decimal wonAmount = stake * tableCoef;

                deposit = (deposit - stake) + wonAmount;

                Console.WriteLine($"You have won: {wonAmount:F1}");
                Console.WriteLine($"Current balance is: {deposit:F1}");
            }
            #endregion
        }
    }
}
