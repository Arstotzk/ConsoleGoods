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

        public string description => "";

        public void Execute(string[] parts)
        {
            throw new NotImplementedException();
        }
    }
}
