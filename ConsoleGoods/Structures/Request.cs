using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Structures
{
    internal struct Request
    {
        public int id;
        public int goodId;
        public int counterpartyId;
        public int number;
        public int amount;
        public DateTime date;
    }
}
