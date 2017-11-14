using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_NSubstitute.Test.Interface;

namespace UnitTest_NSubstitute.Test
{
    public class CommandWatcher
    {
        ICommand command;
        public CommandWatcher(ICommand command)
        {
            this.command = command;
            this.command.Executed += OnExecuted;
        }
        public bool DidStuff { get; private set; }
        public void OnExecuted(object o, EventArgs e)
        {
            DidStuff = true;
        }
    }
}
