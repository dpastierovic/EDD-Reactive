using System;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using Microsoft.Reactive.Testing;
using NUnit.Framework;

namespace Reactive
{
    public class T09Tests : ReactiveTest
    {
        [Test]
        public void SubjectOnNextBeforeSubscription()
        {
            var scheduler = new TestScheduler();
            var subject = new Subject<int>();
            var observedValue = 0;
            var tick = 1;

            scheduler.Schedule(TimeSpan.FromTicks(tick++), () => subject.OnNext(1));
            scheduler.Schedule(TimeSpan.FromTicks(tick++), () => subject.Subscribe(_ => observedValue = _));

            scheduler.AdvanceTo(tick);

            Assert.AreEqual(0, observedValue);
        }

        [Test]
        public void SubjectOnNextAfterSubscription()
        {
            var scheduler = new TestScheduler();
            var subject = new Subject<int>();
            var observedValue = 0;
            var tick = 1;

            scheduler.Schedule(TimeSpan.FromTicks(tick++), () => subject.OnNext(1));
            scheduler.Schedule(TimeSpan.FromTicks(tick++), () => subject.Subscribe(_ => observedValue = _));
            scheduler.Schedule(TimeSpan.FromTicks(tick++), () => subject.OnNext(2));

            scheduler.AdvanceTo(tick);

            Assert.AreEqual(2, observedValue);
        }

        [Test]
        public void BehaviourSubjectSubscription()
        {
            var scheduler = new TestScheduler();
            var subject = new BehaviorSubject<int>(1);
            var observedValue = 0;
            var tick = 1;

            scheduler.Schedule(TimeSpan.FromTicks(tick++), () => subject.Subscribe(_ => observedValue = _));

            scheduler.AdvanceTo(tick);

            Assert.AreEqual(1, observedValue);
        }

        [Test]
        public void TestTest()
        {
            var scheduler = new TestScheduler();

            var input = scheduler.CreateColdObservable(
                OnNext(100, 1),
                OnNext(200, 2),
                OnNext(250, 3),
                OnCompleted<int>(500)
            );

            ReactiveAssert.AreElementsEqual(
                new[]
                {
                    OnNext(100, 1),
                    OnNext(200, 2),
                    OnNext(250, 3),
                    OnCompleted<int>(500)
                },
                input.Messages
            );
        }
    }
}