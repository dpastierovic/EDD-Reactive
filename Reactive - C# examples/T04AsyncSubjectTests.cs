using System;
using System.Reactive.Subjects;
using NUnit.Framework;
// ReSharper disable ConvertClosureToMethodGroup

namespace Reactive
{
    public class T04AsyncSubjectTests
    {
        [Test]
        public void AsyncSubjectSubscribeBeforeSequence()
        {
            var subject = new AsyncSubject<string>();

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
        public void AsyncSubjectSubscribeDuringSequence()
        {
            var subject = new AsyncSubject<string>();

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
        public void AsyncSubjectSubscribeAfterCompletion()
        {
            var subject = new AsyncSubject<string>();

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