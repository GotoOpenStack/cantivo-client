using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace Adell.ItClient.Common.Extensions
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
