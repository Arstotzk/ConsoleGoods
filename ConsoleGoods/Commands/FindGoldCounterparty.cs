using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Commands
{
    internal class FindGoldCounterparty : ICommand
    {
        public string name => "findGoldCounterparty";

        public string description => "Выводит клиента с наибольшим колличеством заказов за указанный год, месяц. Примеры: findGoldCounterparty -2024; findGoldCounterparty -2024 -01";

        public void Execute(string[] parts)
        {
            if (State.GetState() != StateType.FileLoaded)
            {
                Console.WriteLine("Сначала загрузите файл командой openFile.");
                return;
            }
            bool success;
            int year, month; 
            if(!int.TryParse(parts[1], out year))
                Console.WriteLine("Не удалось распознать год.");
            if (parts.Length == 2)
            {
                success = XlsxFile.FindGoldCounterparty(year);
            }
            else
            {
                if (!int.TryParse(parts[2], out month))
                    Console.WriteLine("Не удалось распознать месяц.");
                success = XlsxFile.FindGoldCounterparty(year, month);
            }
            if (!success)
            {
                Console.WriteLine("Ошибка при получении заказов по товару.");
            }
        }
    }
}
