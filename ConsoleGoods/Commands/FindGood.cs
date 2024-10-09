using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Commands
{
    internal class FindGood : ICommand
    {
        public string name => "findGood";

        public string description => "Отображает заказы по указанному товару. Пример: findGood -Вода";

        public void Execute(string[] parts)
        {
            if (State.GetState() != StateType.FileLoaded)
            {
                Console.WriteLine("Сначала загрузите файл командой openFile.");
                return;
            }
            var good = parts[1];
            var success = XlsxFile.FindGood(good);
            if (!success) 
            {
                Console.WriteLine("Ошибка при получении заказов по товару.");
            }
        }
    }
}
