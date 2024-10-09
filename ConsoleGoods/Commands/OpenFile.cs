using ClosedXML.Excel;
using ConsoleGoods.Structures;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Commands
{
    internal class OpenFile : ICommand
    {
        public string name => "openFile";

        public string description => "Открывает xlsx файл по указанному пути. Пример: openFile -C:\\files\\file.xlsx";

        public void Execute(string[] parts)
        {
            var path = parts[1].Trim(new Char[] { '"' });
            if (path == null)
            {
                Console.WriteLine("Вы ввели не все атрибуты команды, для получения списка команд введите help.");
                return;
            }
            var success = XlsxFile.Load(path);
            if (success)
            {
                Console.WriteLine("Файл загружен.");
            }
            else
            {
                Console.WriteLine("Не удалось загрузить файл.");
            }
        }
    }
}
