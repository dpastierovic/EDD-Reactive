using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NUnit.Framework;

namespace Reactive
{
    public class T05CreatingObservables
    {
        [Test]
        public void Create()
        {
            var observable = Observable.Create<int>(o =>
                {
                    o.OnNext(1);
                    o.OnNext(2);
                    o.OnNext(3);
                    return Disposable.Create(() => {});
                }
            );

            observable.Subscribe(i => TestContext.WriteLine($"Subscriber 1 received: {i}"));
            observable.Subscribe(i => TestContext.WriteLine($"Subscriber 2 received: {i}"));
        }

        [Test]
        public void From()
        {
            var observable = new[] {1, 2, 3}.ToObservable();
            observable.Subscribe(i => TestContext.WriteLine($"Subscriber 1 received: {i}"));
            observable.Subscribe(i => TestContext.WriteLine($"Subscriber 2 received: {i}"));
        }

        private delegate void EventHandlerForObservable(object source, EventArgs args);
        private event EventHandlerForObservable Event;
        [Test]
        public void FromEvent()
        {
            var hotObservable = Observable.FromEventPattern<EventHandlerForObservable, EventArgs>(
                handler => (a, b) =>
                {
                    TestContext.WriteLine("FromEventPattern emitted value");
                    handler(a, b);
                },
                h => Event += h,
                h => Event -= h);

            hotObservable.Subscribe(value => TestContext.WriteLine($"Subscription 1 received value"));
            hotObservable.Subscribe(value => TestContext.WriteLine($"Subscription 2 received value"));

            Event?.Invoke(this, EventArgs.Empty);
        }
    }
}