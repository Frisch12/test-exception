﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace ExceptionTestRealTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            for (int i = 0; i < 10000; i++)
            {
                var myCoolMock = Substitute.For<IMyCoolTestInterfaceWhichCantDoMuch>();
                myCoolMock.When(x => x.DoMuchStuff(Arg.Any<string>()))
                    .Do(x => throw new NotImplementedException());
                Func<Task> doWork = async () =>
                {
                    await ((Func<Task>) (async () =>
                    {
                        await Task.Run(() => "Test")
                            .ConfigureAwait(false);
                    }))().ConfigureAwait(false);
                    await ((Func<Task>) (async () =>
                    {
                        await Task.Run(async () => await myCoolMock.DoMuchStuff("").ConfigureAwait(false))
                            .ConfigureAwait(false);
                    }))().ConfigureAwait(false);
                };
                doWork.ShouldThrowExactly<NotImplementedException>();
            }
        }

        [Test]
        public void Test2()
        {
            for (int i = 0; i < 10000; i++)
            {
                var myCoolMock = Substitute.For<IMyCoolTestInterfaceWhichCantDoMuch>();
                myCoolMock.When(x => x.DoMuchStuff(Arg.Any<string>()))
                    .Do(x => throw new NotImplementedException());
                Func<Task> doWork = async () =>
                {
                    await ((Func<Task>) (async () =>
                            await Task.Run(() => myCoolMock.DoMuchStuff("")).ConfigureAwait(false)))()
                        .ConfigureAwait(false);
                };
                doWork.ShouldThrowExactly<NotImplementedException>();
            }
        }
    }
}