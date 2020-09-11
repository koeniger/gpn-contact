using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseUpdater.ConsoleCommander
{
    public class Price
    {
        static public decimal GetRandomPrice()
        {
            return Convert.ToDecimal(Math.Round(new Random().Next(100, 10000) * new Random().NextDouble(), 2));
        }
    }
}
