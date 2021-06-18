using System;
using System.Reactive.Subjects;
using NUnit.Framework;
// ReSharper disable ConvertClosureToMethodGroup

namespace Reactive
{
    public class T08CompositeDisposableTests
    {
        [Test]
        public void DisposeWithCompositeDisposable()
        {
            var subject = new Subject<int>();
            var compositeDisposable = new CompositeDisposable();

            subject.OnNext(1);

            subject.Subscribe(i => TestContext.WriteLine($"Subscription 1 received {i}")).DisposeWith(compositeDisposable);
            subject.Subscribe(i => TestContext.WriteLine($"Subscription 2 received {i}")).DisposeWith(compositeDisposable);

            subject.OnNext(2);
            compositeDisposable.Dispose();

            subject.OnNext(3);
        }
    }
}