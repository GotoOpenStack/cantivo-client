using System;
using System.Reactive.Linq;

namespace Adell.Convenience.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<T> Assert<T>(
            this IObservable<T> source,
            Func<T, bool> assertion,
            Func<T, Exception> exFunc)
        {
            //TODO: fix unsubscirbe
            IDisposable subs;
            return Observable.Create<T>(
                obs =>
                {
                    var failed = false;
                    subs = source.Subscribe(
                            t =>
                            {
                                if (assertion(t) && !failed)
                                    obs.OnNext(t);
                                else
                                {
                                    failed = true;
                                    obs.OnError(exFunc(t));
                                }
                            },
                            obs.OnError,
                            obs.OnCompleted);
                    return () => { };
                });
        }
    }
}
