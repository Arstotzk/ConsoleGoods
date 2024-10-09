using ConsoleGoods.Commands;
using System.Linq;

namespace ConsoleGoods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commands = new List<ICommand>();
            var type = typeof(ICommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => type.IsAssignableFrom(t));
            foreach (var commandType in types)
            {
                if (commandType.IsClass)
                {
                    var command = (ICommand)Activator.CreateInstance(commandType);
                    if (command != null)
                        commands.Add(command);
                }
            }
            Console.WriteLine("Нажмите Ctrl + c чтобы выйти из приложения.");
            Console.WriteLine("Для получения списка команд введите help");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null) 
                {
                    continue;
                }
                var inputParts = input.Split(" -");
                var command = commands.FirstOrDefault(c => c.name.ToLower().Equals(inputParts[0].Trim().ToLower()));
                if (command != null)
                {
                    command.Execute(inputParts);
                }
                else
                {
                    Console.WriteLine("Вы ввели неверную команду, для получения списка команд введите help.");
                }
            }
        }
    }
}
