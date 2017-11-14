using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_NSubstitute.Test;
using UnitTest_NSubstitute.Test.Interface;

namespace UnitTest_NSubstitute
{
    /// <summary>
    /// Reffer http://www.cnblogs.com/gaochundong/archive/2013/05/21/nsubstitute_creating_a_substitute.html
    /// </summary>
    [TestClass]
    public class OtherSampleTests
    {
        [TestMethod]
        public void Test_CreatingSubstitute_MultipleInterfaces()
        {
            //當類別擁有一個建構子參數
            var command = Substitute.For<ICommand, IDisposable>();

            var runner = new CommandRunner(command);
            runner.RunCommand();

            command.Received().Execute(); //ICommand的方法
            ((IDisposable)command).Received().Dispose();  //IDisposable的方法 (IDisposable)亦可拿掉
        }

        [TestMethod]
        public void Test_CreatingSubstitute_SpecifiedOneClassType()
        {
            //當類別擁有多個建構子參數 ex: SomeClassWithCtorArgs(int arg1, string arg2)
            var substitute = Substitute.For(
                  new[] { typeof(ICommand), typeof(IDisposable), typeof(SomeClassWithCtorArgs) }, //需產生的class實體
                  new object[] { 5, "hello world" }  //參數
                );
            Assert.IsInstanceOfType(substitute, typeof(ICommand));
            Assert.IsInstanceOfType(substitute, typeof(IDisposable));
            Assert.IsInstanceOfType(substitute, typeof(SomeClassWithCtorArgs));
        }

        [TestMethod]
        public void Test_CreatingSubstitute_ForDelegate()
        {
            //針對委派型別的測試
            var func = Substitute.For<Func<string>>();
            func().Returns("hello");
            Assert.AreEqual<string>("hello", func());
        }

        //------------------


        [TestMethod]
        public void Test_ReturnFromFunction_CallInfo()
        {
            var foo = Substitute.For<IFoo>();
            foo.Bar(0, "").ReturnsForAnyArgs(x => "Hello " + x.Arg<string>());
            Assert.AreEqual("Hello World", foo.Bar(1, "World"));

            //參數列會略過參數f前後的空白
            foo.Append("", "").ReturnsForAnyArgs(x => (string)x[0] + (string)x[1]);
            Assert.AreEqual("str1 str2", foo.Append("str1 ", "str2"));
            Assert.AreEqual("str1str2", foo.Append("str1", "str2"));
        }


    }
}
