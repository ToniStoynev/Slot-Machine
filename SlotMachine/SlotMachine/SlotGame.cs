namespace SlotMachine
{
    using System;
    using System.Collections.Generic;

    public class SlotGame<T>
    {
        public int Rows { get; set; }

        public int Cols { get; set; }

        List<Symbol<T>> Symbols { get; set; }

        List<WeightedItem<Symbol<T>>> WeightedItems { get; set; } = new List<WeightedItem<Symbol<T>>>();

        public SlotGame(int rows, int cols, List<Symbol<T>> symbols)
        {
            Rows = rows;
            Cols = cols;
            Symbols = symbols;
            SetWeightedItem(symbols);
        }

        public static Symbol<T>[,] GenerateGameTable(SlotGame<T> slotGame)
        {
            var rndTable = new Symbol<T>[slotGame.Rows, slotGame.Cols];

            for (int row = 0; row < rndTable.GetLength(0); row++)
            {
                for (int col = 0; col < rndTable.GetLength(1); col++)
                {
                    rndTable[row, col] = WeightedItem<Symbol<T>>.Choose(slotGame.WeightedItems);
                    Console.Write(rndTable[row, col].Sign);
                }
                Console.WriteLine();
            }
            return rndTable;
        }

        public static decimal CalculateTableCoefficient(Symbol<T>[,] currentTableSpin)
        {
            decimal tableCoefficient = 0;

            for (int row = 0; row < currentTableSpin.GetLength(0); row++)
            {
                var rowFirstSymbol = currentTableSpin[row, 0];

                decimal rowCoefficient = rowFirstSymbol.Coefficient;

                if (rowFirstSymbol.IsWildCard)
                {
                    for (int col = 1; col < currentTableSpin.GetLength(1); col++)
                    {
                        if (currentTableSpin[row, col].IsWildCard)
                        {
                            continue;
                        }
                        var startSymbol = currentTableSpin[row, col];
                        int index = col;
                        for (int j = index; j < currentTableSpin.GetLength(1); j++)
                        {
                            if (currentTableSpin[row, j] != startSymbol && !currentTableSpin[row, j].IsWildCard)
                            {
                                rowCoefficient = 0;
                                break;
                            }
                            rowCoefficient += currentTableSpin[row, j].Coefficient;
                        }
                        break;
                    }
                }
                else
                {
                    for (int col = 1; col < currentTableSpin.GetLength(1); col++)
                    {
                        if (currentTableSpin[row, col] != rowFirstSymbol && !currentTableSpin[row, col].IsWildCard)
                        {
                            rowCoefficient = 0;
                            break;
                        }
                        rowCoefficient += currentTableSpin[row, col].Coefficient;
                    }
                }

                tableCoefficient += rowCoefficient;
            }

            return tableCoefficient;
        }

        private void SetWeightedItem(List<Symbol<T>> symbols)
        {
            foreach (var symbol in symbols)
            {
                this.WeightedItems.Add(new WeightedItem<Symbol<T>>(symbol, symbol.AppearanceProbability));
            }
        }

    }
}
