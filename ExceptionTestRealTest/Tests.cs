using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ExceptionTestRealTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            for (int i = 0; i < 20000; i++)
            {
                Func<Task> doWork = async () =>
                {
                    await ((Func<Task>) (async () => await Task.Run(() => throw new NotImplementedException()).ConfigureAwait(false)))().ConfigureAwait(false);
                };
                doWork.ShouldThrowExactly<NotImplementedException>();
            }
        }
        
        [Test]
        public void Test2()
        {
            for (int i = 0; i < 20000; i++)
            {
                Func<Task> doWork = async () =>
                {
                    await ((Func<Task>) (async () => await Task.Run(() => throw new NotImplementedException()).ConfigureAwait(false)))().ConfigureAwait(false);
                };
                doWork.ShouldThrowExactly<NotImplementedException>();
            }
        }
        
        [Test]
        public void Test3()
        {
            for (int i = 0; i < 20000; i++)
            {
                Func<Task> doWork = async () =>
                {
                    await ((Func<Task>) (async () => await Task.Run(() => throw new NotImplementedException()).ConfigureAwait(false)))().ConfigureAwait(false);
                };
                doWork.ShouldThrowExactly<NotImplementedException>();
            }
        }
    }
}