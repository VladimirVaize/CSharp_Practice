using System;
using System.Collections.Generic;

namespace EntryPoint.Server
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service) where T : class
        {
            _services[typeof(T)] = service;
        }

        public static T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Сервис {typeof(T)} не зарегистрирован");
        }

        public static bool Has<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }
    }
}
