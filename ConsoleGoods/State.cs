using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGoods
{
    internal static class State
    {
        private static StateType currentState = StateType.None;
        public static StateType GetState()
        {
            return currentState;
        }
        public static void SetState(StateType state)
        {
            currentState = state;
        }
    }
    public enum StateType
    {
        None,
        FileLoaded
    }
}
