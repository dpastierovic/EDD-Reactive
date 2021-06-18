using System;
using NUnit.Framework;

namespace Reactive
{
    public static class Helpers
    {
        public static int LogAndEmit(this int value)
        {
            TestContext.WriteLine($"OnNext {value}");
            return value;
        }

        public static IDisposable DisposeWith(this IDisposable d, CompositeDisposable compositeDisposable)
        {
            compositeDisposable.Add(d);
            return d;
        }
    }
}