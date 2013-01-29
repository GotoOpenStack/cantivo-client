using System;
using System.Collections.Generic;

namespace Adell.ItClient.Common.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services;

        static ServiceLocator()
        {
            Services = new Dictionary<Type, object>();
        }

        public static bool IsRegistered<T>()
        {
            return Services.ContainsKey(typeof(T));
        }

        public static T GetService<T>()
        {
            return (T)Services[typeof (T)];
        }

        public static void RegisterService<T>(T service)
        {
            Services[typeof (T)] = service;
        }
    }
}
