using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods.Commands
{
    internal interface ICommand
    {
        public string name { get; }
        public string description { get; }
        void Execute(string[] parts);
    }
}
