using System;
using System.Reactive.Subjects;
using NUnit.Framework;

namespace Reactive
{
    // ReSharper disable ConvertClosureToMethodGroup
    class T02BehaviorSubjectTests
    {

        [Test]
        public void BehaviorSubjectSubscribeBeforeSequence()
        {
            var subject = new BehaviorSubject<string>("Initial message");

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
        public void BehaviorSubjectSubscribeDuringSequence()
        {
            var subject = new BehaviorSubject<string>("Initial message");

            subject.OnNext("Message 1");

            var observer = subject.Subscribe
            (
                message => TestContext.WriteLine(message),
                () => TestContext.WriteLine("Sequence completed")
            );

            subject.OnNext("Message 2");
            subject.OnNext("Message 3");
            subject.OnCompleted();

            observer.Dispose();
            subject.Dispose();
        }

        [Test]
        public void BehaviorSubjectSubscribeAfterCompletion()
        {
            var subject = new BehaviorSubject<string>("Initial message");

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
