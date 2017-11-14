using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_NSubstitute.Test
{
    public class SomeClassWithCtorArgs : IDisposable
    {
        public SomeClassWithCtorArgs(int arg1, string arg2)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
