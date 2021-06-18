using System;
using System.Reactive.Subjects;
using NUnit.Framework;
// ReSharper disable ConvertClosureToMethodGroup

namespace Reactive
{
    public class T03ReplaySubjectTests
    {
        [Test]
        public void ReplaySubjectSubscribeBeforeSequence()
        {
            var subject = new ReplaySubject<string>(2);

            var observer = subject.Subscribe
            (
                message => TestContext.WriteLine(message),
                () => TestContext.WriteLine("Sequence completed")
            );

            subject.OnNext("Message 1");
            subject.OnNext("Message 2");
            subject.OnNext("Message 3");
            subject.OnCompleted();

            observer.Dispose();
            subject.Dispose();
        }

        [Test]
        public void ReplaySubjectSubscribeDuringSequence()
        {
            var subject = new ReplaySubject<string>(2);

            subject.OnNext("Message 1");
            subject.OnNext("Message 2");

            var observer = subject.Subscribe
            (
                message => TestContext.WriteLine(message),
                () => TestContext.WriteLine("Sequence completed")
            );

            subject.OnNext("Message 3");
            subject.OnCompleted();

            observer.Dispose();
            subject.Dispose();
        }

        [Test]
        public void ReplaySubjectSubscribeAfterCompletion()
        {
            var subject = new ReplaySubject<string>(2);

            subject.OnNext("Message 1");
            subject.OnNext("Message 2");
            subject.OnNext("Message 3");
            subject.OnCompleted();

            var observer = subject.Subscribe
            (
                message => TestContext.WriteLine(message),
                () => TestContext.WriteLine("Sequence completed")
            );

            observer.Dispose();
            subject.Dispose();
        }
    }
}