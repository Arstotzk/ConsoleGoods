using ClosedXML.Excel;
using ConsoleGoods.Structures;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods
{
    internal static class XlsxFile
    {
        public static string path = "";
        public static List<Good> goods = new List<Good>();
        public static List<Counterparty> counterparties = new List<Counterparty>();
        public static List<Request> requests = new List<Request>();

        public static bool Load(string _path) 
        {
            try
            {
                goods = new List<Good>();
                counterparties = new List<Counterparty>();
                requests = new List<Request>();

                path = _path;
                var workbook = new XLWorkbook(path);
                var goodsWorksheet = workbook.Worksheet(1);
                var countersWorksheet = workbook.Worksheet(2);
                var requestsWorksheet = workbook.Worksheet(3);

                foreach (var row in goodsWorksheet.Rows())
                {
                    if (row.Cell(1).Value.IsText || row.Cell(1).Value.IsBlank)
                        continue;

                    var good = new Good();
                    good.id = (int)row.Cell(1).Value.GetNumber();
                    good.name = row.Cell(2).Value.GetText();
                    good.unit = row.Cell(3).Value.GetText();
                    good.price = (float)row.Cell(4).Value.GetNumber();
                    goods.Add(good);
                }

                foreach (var row in countersWorksheet.Rows())
                {
                    if (row.Cell(1).Value.IsText || row.Cell(1).Value.IsBlank)
                        continue;

                    var test = row.Cell(1).Value;
                    var counterparty = new Counterparty();
                    counterparty.id = (int)row.Cell(1).Value.GetNumber();
                    counterparty.name = row.Cell(2).Value.GetText();
                    counterparty.address = row.Cell(3).Value.GetText();
                    counterparty.person = row.Cell(4).Value.GetText();
                    counterparties.Add(counterparty);
                }

                foreach (var row in requestsWorksheet.Rows())
                {
                    if (row.Cell(1).Value.IsText || row.Cell(1).Value.IsBlank)
                        continue;

                    var request = new Request();
                    request.id = (int)row.Cell(1).Value.GetNumber();
                    request.goodId = (int)row.Cell(2).Value.GetNumber();
                    request.counterpartyId = (int)row.Cell(3).Value.GetNumber();
                    request.number = (int)row.Cell(4).Value.GetNumber();
                    request.amount = (int)row.Cell(5).Value.GetNumber();
                    request.date = row.Cell(6).Value.GetDateTime();
                    requests.Add(request);
                }
                State.SetState(StateType.FileLoaded);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool FindGood(string goodName)
        {
            try
            {
                var goodLinq = goods.Where(g => g.name.Equals(goodName));
                if (!goodLinq.Any())
                {
                    Console.WriteLine("Товар с таким именем не найден.");
                    return true;
                }
                var good = goodLinq.FirstOrDefault();
                var foundRequests = requests.Where(r => r.goodId.Equals(good.id)).ToList();
                if (foundRequests.Count == 0)
                    Console.WriteLine("Заказов не найдено.");
                foreach (var request in foundRequests)
                {
                    var counterpartyLinq = counterparties.Where(c => c.id.Equals(request.counterpartyId));
                    if (!counterpartyLinq.Any())
                        continue;
                    var counterparty = counterpartyLinq.FirstOrDefault();
                    Console.WriteLine(counterparty.name + " заказал " + request.amount + " " + good.unit + " по цене: " + good.price + ". Дата заказа: " + request.date.ToString("dd.MM.yyyy"));
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool RewritePerson(string counterpartyName, string newPersonName)
        {
            try
            {
                var counterpartyLinq = counterparties.Where(c => c.name.Equals(counterpartyName));
                if (!counterpartyLinq.Any())
                {
                    Console.WriteLine("Контрагент с таким именем не найден.");
                    return true;
                }
                var counterparty = counterpartyLinq.FirstOrDefault();
                counterparty.person = newPersonName;
                var workbook = new XLWorkbook(path);
                var countersWorksheet = workbook.Worksheet(2);
                var cellLinq = countersWorksheet.Cells().Where(c => c.Value.IsText).Where(c => c.Value.GetText().Equals(counterpartyName));
                if (!cellLinq.Any())
                {
                    Console.WriteLine("Контрагент с таким именем не найден.");
                    return true;
                }
                var cellCounterparty = cellLinq.FirstOrDefault();
                var cellperson = countersWorksheet.Cell(cellCounterparty.Address.RowNumber, 4);
                cellperson.Value = newPersonName;
                workbook.Save();
                Console.WriteLine("У контрагента " + counterparty.name + " изменилось контактное лицо на: " + newPersonName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public static bool FindGoldCounterparty(int year)
        {
            try
            {
                var startDate = new DateTime(year, 1, 1);
                var endDate = startDate.AddYears(1).AddDays(-1);
                return FindGoldCounterparty(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool FindGoldCounterparty(int year, int month) 
        {
            try
            {
                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                return FindGoldCounterparty(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private static bool FindGoldCounterparty(DateTime startDate, DateTime endDate)
        {
            try
            {
                var requestLinq = requests.Where(r => r.date >= startDate && r.date <= endDate);
                if (!requestLinq.Any())
                {
                    Console.WriteLine("На выбранную дату не было заказов");
                    return true;
                }
                var counterpartyId = requestLinq.GroupBy(r => r.counterpartyId).Select(g => new { counterpartyId = g.Key, Count = g.Count() })
                    .OrderByDescending(c => c.Count).FirstOrDefault().counterpartyId;
                var counterparty = counterparties.FirstOrDefault(c => c.id.Equals(counterpartyId));
                Console.WriteLine("Золотой клиент: " + counterparty.name);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
