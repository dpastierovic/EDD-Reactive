using System;
using System.Collections.Generic;

namespace Reactive
{
    public sealed class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables;

        public CompositeDisposable()
        {
            _disposables = new List<IDisposable>();
        }

        public CompositeDisposable(IDisposable disposable)
        {
            _disposables = new List<IDisposable> { disposable };
        }

        public CompositeDisposable(IEnumerable<IDisposable> disposables)
        {
            _disposables = new List<IDisposable>(disposables);
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Add(IEnumerable<IDisposable> disposables)
        {
            _disposables.AddRange(disposables);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }
    }
}