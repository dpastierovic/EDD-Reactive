using System;
using System.Reactive.Subjects;
using NUnit.Framework;
// ReSharper disable ConvertClosureToMethodGroup

namespace Reactive
{
    public class T01SubjectTests
    {
        [Test]
        public void SubjectSubscribeBeforeSequence()
        {
            var subject = new Subject<string>();

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
        public void SubjectSubscribeDuringSequence()
        {
            var subject = new Subject<string>();

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
        public void SubjectSubscribeAfterCompletion()
        {
            var subject = new Subject<string>();

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