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

        public string description => "";

        public void Execute(string[] parts)
        {
            throw new NotImplementedException();
        }
    }
}
