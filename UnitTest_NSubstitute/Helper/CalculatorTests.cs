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
    }
}
