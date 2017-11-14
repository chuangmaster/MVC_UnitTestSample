using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_NSubstitute.Test.Interface;

namespace UnitTest_NSubstitute.Test
{
    public class OnceOffCommandRunner
    {
        ICommand command;
        public OnceOffCommandRunner(ICommand command)
        {
            this.command = command;
        }
        public void Run()
        {
            if (command == null) return;
            command.Execute();
            command = null;
        }
    }
}
