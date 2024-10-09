using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Commands
{
    internal class Help : ICommand
    {
        public string name => "help";

        public string description => "Выводит все команды в консоль.";

        public void Execute(string[] parts)
        {
            var type = typeof(ICommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => type.IsAssignableFrom(t));
            Console.WriteLine("Список команд:");
            foreach (var commandType in types)
            {
                if (commandType.IsClass)
                {
                    var command = Activator.CreateInstance(commandType);
                    var nameProperty = commandType.GetProperty("name");
                    var descriptionProperty = commandType.GetProperty("description");
                    var name = nameProperty != null ? nameProperty.GetValue(command) : string.Empty;
                    var description = descriptionProperty != null ? descriptionProperty.GetValue(command) : string.Empty;
                    Console.WriteLine("     " + name + " - " + description);
                }
            }
        }
    }
}
