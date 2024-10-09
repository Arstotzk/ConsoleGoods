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

        public string description => "Открывает xlsx файл по указанному пути. Пример: openFile C:\\files\\file.xlsx";

        public void Execute(string[] parts)
        {
            var path = parts[1];
            if (path == null)
            {
                Console.WriteLine("Вы ввели не все атрибуты команды, для получения списка команд введите help.");
                return;
            }
            var workbook = new XLWorkbook(path);
            var goodsWorksheet = workbook.Worksheet(1);
            var countersWorksheet = workbook.Worksheet(2);
            var requestsWorksheet = workbook.Worksheet(3);

            var goods = new List<Good>();
            var counterparties = new List<Counterparty>();
            var requests = new List<Request>();
            foreach (var row in goodsWorksheet.Rows())
            {
                var good = new Good();
                good.id = (int) row.Cell(0).Value.GetNumber();
                good.name = row.Cell(1).Value.GetText();
                good.unit = row.Cell(2).Value.GetText();
                good.price = (float) row.Cell(3).Value.GetNumber();
            }
            foreach (var row in countersWorksheet.Rows())
            {
                var counterparty = new Counterparty();
                counterparty.id = (int)row.Cell(0).Value.GetNumber();
                counterparty.name = row.Cell(1).Value.GetText();
                counterparty.address = row.Cell(2).Value.GetText();
                counterparty.person = row.Cell(3).Value.GetText();
            }
            foreach (var row in requestsWorksheet.Rows())
            {
                var request = new Request();
                request.id = (int)row.Cell(0).Value.GetNumber();
                request.goodId = (int)row.Cell(1).Value.GetNumber();
                request.counterpartyId = (int)row.Cell(2).Value.GetNumber();
                request.number = (int)row.Cell(3).Value.GetNumber();
                request.amount = (int)row.Cell(4).Value.GetNumber();
                request.date = row.Cell(5).Value.GetDateTime();
            }
            throw new NotImplementedException();
        }
    }
}
