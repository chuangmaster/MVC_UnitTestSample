using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_NSubstitute.Test.Interface;

namespace UnitTest_NSubstitute.Test
{
    public class CommandRunner
    {
        private ICommand _command;

        public CommandRunner(ICommand command)
        {
            _command = command;
        }

        public void RunCommand()
        {
            _command.Execute();
            _command.Dispose();
        }
    }

}
