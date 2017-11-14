using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_NSubstitute.Test.Interface
{
    public interface IFoo
    {
        string Bar(int a, string b);

        string Append(string a, string b);

        void SayHello(string to);
    }
}
