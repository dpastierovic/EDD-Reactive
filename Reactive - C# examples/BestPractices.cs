using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

// ReSharper disable ArrangeAccessorOwnerBody

namespace Reactive
{
    public class BestPractices
    {
        private readonly Subject<int> _mySubject;

        public BestPractices()
        {
            _mySubject = new Subject<int>();
            MyObservable5 = _mySubject.AsObservable();
        }

        // incorrect - anyone can call OnNext, OnCompleted, OnError
        public Subject<int> MyObservable1 = new Subject<int>();

        // incorrect - MyObservable2 can be cast back to Subject<int> and we are in the same situation as above
        public IObservable<int> MyObservable2 = new Subject<int>();

        // incorrect - New Observable is created each time this is accessed
        public IObservable<int> MyObservable3 => _mySubject.AsObservable();
        public IObservable<int> MyObservable4
        {
            get => _mySubject.AsObservable();
        }

        // correct
        public IObservable<int> MyObservable5 { get; }
    }
}