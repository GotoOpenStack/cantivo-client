using System;
using System.Reactive.Linq;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface IDispatcher
    {
        void Dispatch(Action a);
    }
    //public static class DispatcherExtensions
    //{
    //    public delegate void VoidDelegate();
    //    public static void Dispatch(this IDispatcher disp, VoidDelegate func)
    //    {
    //        disp.Dispatch(() => func());
    //    }
    //}

}
