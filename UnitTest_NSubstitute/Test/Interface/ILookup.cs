using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_NSubstitute.Test.Interface
{
    public interface ILookup
     {
       bool TryLookup(string key, out string value);
    }
}
