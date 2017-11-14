using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_UnitTestSample.Helper.Interface;
using NSubstitute;
using NSubstitute.Exceptions;

namespace UnitTest_NSubstitute
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Test_GetStarted_GetSubstitute()
        {
            //透過NSubstitue創建Class, 如 Stub、Mock、Fake、Spy、Test Double 等
            ICalculator calculator = Substitute.For<ICalculator>();
        }


        [TestMethod]
        public void Test_GetStarted_ReturnSpecifiedValue()
        {
            //arrange
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3); //呼叫此方法回回傳值3

            //act
            int actual = calculator.Add(1, 2);

            //assert
            Assert.AreEqual<int>(3, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ReceivedCallsException))]  //可以陳接測試的錯誤
        public void Test_GetStarted_DidNotReceivedSpecificCall()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(5, 7);

            calculator.Received().Add(1, 2);
            calculator.DidNotReceive().Add(5, 7);

        }

        [TestMethod]
        public void Test_GetStarted_SetPropertyValue()
        {
            ICalculator calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC");  //運用Return做Setter
            Assert.AreEqual<string>("DEC", calculator.Mode);

            calculator.Mode = "HEX";  //用原始的方式做Setter
            Assert.AreEqual<string>("HEX", calculator.Mode);
        }

        [TestMethod]
        public void Test_GetStarted_MatchArguments()
        {
            ICalculator calculator = Substitute.For<ICalculator>();

            calculator.Add(10, -5);

            //可以針對傳遞參數給規則
            calculator.Received().Add(10, Arg.Any<int>());
            calculator.Received().Add(10, Arg.Is<int>(x => x < 0));
        }

        [TestMethod]
        public void Test_GetStarted_PassFuncToReturns()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator
               .Add(Arg.Any<int>(), Arg.Any<int>())
               .Returns(x => (int)x[0] + (int)x[1]);

            int actual = calculator.Add(5, 10);

            Assert.AreEqual<int>(15, actual);
        }

        [TestMethod]
        public void Test_GetStarted_MultipleValues()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            //也能夠返回序列來指定多個參數
            calculator.Mode.Returns("HEX", "DEC", "BIN");

            Assert.AreEqual<string>("HEX", calculator.Mode);
            Assert.AreEqual<string>("DEC", calculator.Mode);
            Assert.AreEqual<string>("BIN", calculator.Mode);
        }

        [TestMethod]
        public void Test_GetStarted_RaiseEvents()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            bool eventWasRaised = false;
            //事件內容
            calculator.PoweringUp += (sender, args) =>
            {
                eventWasRaised = true;
            };

            //引發事件
            calculator.PoweringUp += Raise.Event();

            Assert.IsTrue(eventWasRaised);
        }
        //Refer http://www.cnblogs.com/gaochundong/archive/2013/05/21/nsubstitute_setting_a_return_value.html
        [TestMethod]
        public void Test_SettingReturnValue_ReturnsValueWithSpecifiedArguments()
        {
            //為方法設置返回值
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            Assert.AreEqual(calculator.Add(1, 2), 3);
        }

        [TestMethod]
        public void Test_SettingReturnValue_ReturnsDefaultValueWithDifferentArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            // 设置调用返回值为3
            calculator.Add(1, 2).Returns(3);

            Assert.AreEqual(calculator.Add(1, 2), 3);
            Assert.AreEqual(calculator.Add(1, 2), 3);

            // 当使用不同参数调用时,返回值不是3
            Assert.AreNotEqual(calculator.Add(3, 6), 3);
        }

        [TestMethod]
        public void Test_SettingReturnValue_ReturnsValueFromProperty()
        {
            //為屬性給值
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC");
            Assert.AreEqual(calculator.Mode, "DEC");

            calculator.Mode = "HEX";
            Assert.AreEqual(calculator.Mode, "HEX");
        }

        //Refer http://www.cnblogs.com/gaochundong/archive/2013/05/21/nsubstitute_return_for_specific_args.html
        [TestMethod]
        public void Test_ReturnForSpecificArgs_UseArgumentsMatcher()
        {
            //為特定參數測定返回值
            var calculator = Substitute.For<ICalculator>();

            // 当第一个参数是任意int类型的值，第二个参数是5时返回。
            calculator.Add(Arg.Any<int>(), 5).Returns(10);
            Assert.AreEqual(10, calculator.Add(123, 5));
            Assert.AreEqual(10, calculator.Add(-9, 5));
            Assert.AreNotEqual(10, calculator.Add(-9, -9));

            // 当第一个参数是1，第二个参数小于0时返回。
            calculator.Add(1, Arg.Is<int>(x => x < 0)).Returns(345);
            Assert.AreEqual(345, calculator.Add(1, -2));
            Assert.AreNotEqual(345, calculator.Add(1, 2));

            // 当两个参数都为0时返回。
            calculator.Add(Arg.Is(0), Arg.Is(0)).Returns(99);
            Assert.AreEqual(99, calculator.Add(0, 0));
        }

        //Refer http://www.cnblogs.com/gaochundong/archive/2013/05/21/nsubstitute_return_from_a_function.html

        [TestMethod]
        public void Test_ReturnFromFunction_ReturnSum()
        {
            //使用函數來設置返回值
            var calculator = Substitute.For<ICalculator>();

            calculator
              .Add(Arg.Any<int>(), Arg.Any<int>())
              .Returns(x => (int)x[0] + (int)x[1]);

            Assert.AreEqual(calculator.Add(1, 1), 2);
            Assert.AreEqual(calculator.Add(20, 30), 50);
            Assert.AreEqual(calculator.Add(-73, 9348), 9275);
        }

        [TestMethod]
        public void Test_ReturnFromFunction_GetCallbackWhenever()
        {
            var calculator = Substitute.For<ICalculator>();

            var counter = 0;
            calculator
              .Add(Arg.Any<int>(), Arg.Any<int>())
              .ReturnsForAnyArgs(x =>
              {
                  counter++;
                  return 0;
              });

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            calculator.Add(11, -3);
            Assert.AreEqual(counter, 3);
        }

        [TestMethod]
        public void Test_ReturnFromFunction_UseAndDoesAfterReturns()
        {
            var calculator = Substitute.For<ICalculator>();

            var counter = 0;
            calculator
              .Add(0, 0)
              .ReturnsForAnyArgs(x => 0)
              .AndDoes(x => counter++);

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            Assert.AreEqual(counter, 2);
        }

        //Refer http://www.cnblogs.com/gaochundong/archive/2013/05/21/nsubstitute_multiple_return_values.html

        [TestMethod]
        public void Test_MultipleReturnValues_ReturnMultipleValues()
        {
            //設定多個返回值
            var calculator = Substitute.For<ICalculator>();
            
            calculator.Mode.Returns("DEC", "HEX", "BIN");
            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("HEX", calculator.Mode);
            Assert.AreEqual("BIN", calculator.Mode);
        }
        /*待會回來看Exception*/

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_MultipleReturnValues_UsingCallbacks()
        {
            //使用回調來返多個值
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns(x => "DEC", x => "HEX", x => { throw new Exception(); });
            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("HEX", calculator.Mode);

            var result = calculator.Mode;
        }


        //http://www.cnblogs.com/gaochundong/archive/2013/05/22/nsubstitute_replacing_return_values.html

        [TestMethod]
        public void Test_ReplaceReturnValues_ReplaceSeveralTimes()
        {
            //替換返回值
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC,HEX,OCT");
            calculator.Mode.Returns(x => "???");
            calculator.Mode.Returns("HEX");
            calculator.Mode.Returns("BIN");

            Assert.AreEqual(calculator.Mode, "BIN");
        }
    }
}
