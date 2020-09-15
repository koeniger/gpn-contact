using System;
using DataBaseUpdater.ConsoleCommander;
using DataBaseUpdater.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataBaseUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("База данных Contact");
            UpdateContext context = GetContext();

            Commander cmd = new Commander();

            ConsoleKeyInfo k;
            do
            {
                Console.WriteLine();
                ConsoleWriter.WarningLine($"{Commands.Add} Product press {(char)Commands.Add}");
                ConsoleWriter.WarningLine($"{Commands.Remove} Product from Products press {(char)Commands.Remove}");
                ConsoleWriter.WarningLine($"{Commands.TestFillingOfTables} Test filling of tables press {(char)Commands.TestFillingOfTables}");
                Console.WriteLine("Для выхода нажмите {0}", ConsoleKey.Escape);
                k = Console.ReadKey(true);          //Ожидание нажатия кнопки пользователем

                cmd.Command(context, k.Key);

            } while (k.Key != ConsoleKey.Escape);   //Проверка нажатия кнопки Escape
        }

        private static UpdateContext GetContext()
        {
            var configuration = Helpers.ReadConfigFromAppconfig();
            var connectionString = configuration.GetConnectionString(StringResources.ConnectionStringName);
            ConsoleWriter.InfoLine($"Строка подключения: {connectionString}");
            var optionsBuilder = new DbContextOptionsBuilder<UpdateContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new UpdateContext(optionsBuilder.Options);
        }

      
    }
}
