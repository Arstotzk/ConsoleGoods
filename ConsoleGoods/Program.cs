using ConsoleGoods.Commands;
using System.Linq;

namespace ConsoleGoods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commands = new List<ICommand> { new Help() };
            Console.WriteLine("Нажмите Ctrl + c чтобы выйти из приложения.");
            Console.WriteLine("Для получения списка команд введите help");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null) 
                {
                    continue;
                }
                var inputParts = input.Split(' ');
                var command = commands.FirstOrDefault(c => c.name.ToLower().Equals(inputParts[0].ToLower()));
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
