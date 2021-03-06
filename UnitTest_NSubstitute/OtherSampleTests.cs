﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        //Refer http://www.cnblogs.com/gaochundong/archive/2013/05/22/nsubstitute_checking_received_calls.html

        [TestMethod]
        public void Test_CheckReceivedCalls_CallReceived()
        {
            //檢查接收到的呼叫
            //Arrange
            var command = Substitute.For<ICommand>();
            var something = new SomethingThatNeedsACommand(command);

            //Act
            something.DoSomething();

            //Assert
            command.Received(1).Execute();
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_CallDidNotReceived()
        {
            //檢查沒有收到的呼叫
            //Arrange
            var command = Substitute.For<ICommand>();
            var something = new SomethingThatNeedsACommand(command);

            //Act
            something.DontDoAnything();

            //Assert
            command.DidNotReceive().Execute();
        }

        
        [TestMethod]
        public void Test_CheckReceivedCalls_CallReceivedNumberOfSpecifiedTimes()
        {
            //檢查接收到次數
            // Arrange
            var command = Substitute.For<ICommand>();
            var repeater = new CommandRepeater(command, 3);

            // Act
            repeater.Execute();

            // Assert
            // 如果仅接收到2次或者4次，这里会失败。
            command.Received(3).Execute();

            // 表示有接收到 >0次
            command.Received().Execute();

            /*
             Received(1) 会检查该调用收到并且仅收到一次。这与默认的 Received() 不同，
             其检查该调用至少接收到了一次。Received(0) 的行为与 DidNotReceive() 相同。
             
             */
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_CheckingCallsToIndexers()
        {
            //檢查索引器
            var dictionary = Substitute.For<IDictionary<string, int>>();
            dictionary["test"] = 1;

            dictionary.Received()["test"] = 1;
            dictionary.Received()["test"] = Arg.Is<int>(x => x < 5);
        }


        [TestMethod]
        public void Test_CheckReceivedCalls_CheckingEventSubscriptions()
        {
            //檢查事件訂閱
            var command = Substitute.For<ICommand>();
            var watcher = new CommandWatcher(command);

            command.Executed += Raise.Event();

            Assert.IsTrue(watcher.DidStuff);
        }

        [TestMethod]
        public void Test_CheckReceivedCalls_MakeSureWatcherSubscribesToCommandExecuted()
        {
            //檢查事件訂閱
            var command = Substitute.For<ICommand>();
            var watcher = new CommandWatcher(command);

            // 不推荐这种方法。
            // 更好的办法是测试行为而不是具体实现。
            command.Received().Executed += watcher.OnExecuted;
            // 或者有可能事件处理器是不可访问的。
            command.Received().Executed += Arg.Any<EventHandler>();
        }

        //http://www.cnblogs.com/gaochundong/archive/2013/05/22/nsubstitute_clearing_received_calls.html


        [TestMethod]
        public void Test_ClearReceivedCalls_ForgetPreviousCalls()
        {
            //清理以收到的呼叫
            var command = Substitute.For<ICommand>();
            var runner = new OnceOffCommandRunner(command);

            // 第一次运行
            runner.Run();
            command.Received().Execute();

            // 忘记前面对command的调用
            command.ClearReceivedCalls();

            // 第二次运行
            runner.Run();
            command.DidNotReceive().Execute();


            /*
             執行過ClearReceivedCalls就會把實體給null，執行此段可以嘗試用偵錯去看實體，
             會發現建構子傳入的ICommand實體已經不存在
             */
        }

        //Refer http://www.cnblogs.com/gaochundong/archive/2013/05/22/nsubstitute_callbacks_void_calls_and_when_do.html


        [TestMethod]
        public void Test_CallbacksWhenDo_UseWhenDo()
        {
            //为无返回值调用创建回调
            /*Returns() 可以被用于为成员设置产生返回值的回调函数，但是对于 void 类型的成员，
             * 我们需要不同的方式，因为我们无法调用一个 void 并返回一个值。对于这种情况，
             * 我们可以使用 When..Do 语法。*/
            var counter = 0;
            var foo = Substitute.For<IFoo>();

            foo.When(x => x.SayHello("World"))
              .Do(x => counter++);

            foo.SayHello("World");
            foo.SayHello("World");
            Assert.AreEqual(2, counter);
        }

        

    }
}
