using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Commands
{
    internal class RewritePerson : ICommand
    {
        public string name => "rewritePerson";

        public string description => "Перезаписывает контактное лицо у организации в xlsx файле. Пример: rewritePerson -ООО Одуванчик -Иванов Иван Иваныч";

        public void Execute(string[] parts)
        {
            if (State.GetState() != StateType.FileLoaded)
            {
                Console.WriteLine("Сначала загрузите файл командой openFile.");
                return;
            }
            var counterpatyName = parts[1];
            var person = parts[2];
            var success = XlsxFile.RewritePerson(counterpatyName, person);
            if (!success)
            {
                Console.WriteLine("Ошибка при изменении контатного лица.");
            }
        }
    }
}
