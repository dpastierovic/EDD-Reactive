using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NUnit.Framework;
// ReSharper disable InvokeAsExtensionMethod

namespace Reactive
{
    public class T07ConnectableObservableTests
    {
        [Test]
        public void NotConnectedExample()
        {
            var observable = new Subject<int>();
            observable.OnNext(1);
            observable.OnNext(2);
            observable.OnNext(3);

            var publishedObservable = observable.Publish();

            publishedObservable.Subscribe(value => TestContext.WriteLine($"Subscription 1 received {value}"));
            publishedObservable.Subscribe(value => TestContext.WriteLine($"Subscription 2 received {value}"));
        }

        [Test]
        public void ConnectedBeforeSubscribers()
        {
            var observable = new ReplaySubject<int>(3);
            observable.OnNext(1);
            observable.OnNext(2);
            observable.OnNext(3);

            var publishedObservable = observable.Publish();
            publishedObservable.Connect();

            publishedObservable.Subscribe(value => TestContext.WriteLine($"Subscription 1 received {value}"));
            publishedObservable.Subscribe(value => TestContext.WriteLine($"Subscription 2 received {value}"));
        }

        [Test]
        public void ConnectedBetweenSubscribers()
        {
            var observable = new Subject<int>();
            observable.OnNext(1);
            observable.OnNext(2);
            var publishedObservable = observable.Publish();

            publishedObservable.Subscribe(value => TestContext.WriteLine($"Subscription 1 received {value}"));

            publishedObservable.Connect();
            observable.OnNext(3);

            publishedObservable.Subscribe(value => TestContext.WriteLine($"Subscription 2 received {value}"));
        }
    }
}