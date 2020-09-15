using DataBaseUpdater.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseUpdater.ConsoleCommander
{
    public class Commander
    {
        /// <summary>
        /// Обработчик команд с кансоли
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Command(UpdateContext context, ConsoleKey key)
        {
            Commands command = (Commands)(Convert.ToInt32(key));
            switch (command)
            {
                case Commands.Esc:
                    {
                        return true;
                    }
                case Commands.Add:
                    {
                        new DataContextTest().Add(context);
                        return true;
                    }
                case Commands.Remove:
                    {
                        new DataContextTest().Remove(context);
                        return true;
                    }
                case Commands.TestFillingOfTables:
                    {
                        new DataContextTest().TestFillingOfTables(context);
                        return true;
                    }
                case Commands.AddTestImages:
                    {
                        new Images().AddTestImages(context);
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }

    enum Commands : int
    { 
        Add = ConsoleKey.D1,
        Remove = ConsoleKey.D2,
        Test = ConsoleKey.D3,
        TestFillingOfTables = ConsoleKey.D9,
        AddTestImages = ConsoleKey.I,
        Esc = ConsoleKey.Escape
    }
}
