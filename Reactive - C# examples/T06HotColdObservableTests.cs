using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;
// ReSharper disable InvokeAsExtensionMethod

namespace Reactive
{
    public class T06HotColdObservableTests
    {
        private delegate void EventHandlerForObservable(object source, EventArgs args);
        private event EventHandlerForObservable Event;

        [Test]
        public void ColdObservableSubscribe()
        {
            var coldObservable = Observable.Create<int>(observable =>
            {
                observable.OnNext(Helpers.LogAndEmit(1));
                observable.OnNext(Helpers.LogAndEmit(2));
                observable.OnNext(Helpers.LogAndEmit(3));

                return Disposable.Create(() =>
                {
                    TestContext.WriteLine("Observable disposed");
                });
            });

            coldObservable.Subscribe(value => TestContext.WriteLine($"Subscription 1 received {value}"));
            Thread.Sleep(1000);
            coldObservable.Subscribe(value => TestContext.WriteLine($"Subscription 2 received {value}"));
        }

        [Test]
        public void HotObservableSubscribe()
        {
            var hotObservable = Observable.FromEventPattern<EventHandlerForObservable, EventArgs>(
                handler => (a, b) =>
                {
                    TestContext.WriteLine("FromEventPattern emitted value");
                    handler(a, b);
                },
                h => Event += h,
                h => Event -= h);

            Event?.Invoke(this, EventArgs.Empty);

            hotObservable.Subscribe(value => TestContext.WriteLine($"Subscription 1 received value"));

            Event?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);

            hotObservable.Subscribe(value => TestContext.WriteLine($"Subscription 2 received value"));

            Event?.Invoke(this, EventArgs.Empty);
        }
    }
}