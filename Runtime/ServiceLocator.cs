using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TigerMoon;

namespace TigerMoon
{
    public interface IServiceLocator
    {
        static IServiceLocator Instance { get; }
        void Register<TService>(TService serviceInstance);
        TService GetService<TService>();
    }

    public sealed class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _serviceRegistry = new();

        private static readonly Lazy<IServiceLocator> _instance = new(() =>
        {
            return new ServiceLocator();
        });

        public static IServiceLocator Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private ServiceLocator()
        {

        }

        public void Register<TService>(TService serviceInstance)
        {
            var serviceType = typeof(TService);

            if (_serviceRegistry.ContainsKey(serviceType))
            {
                _serviceRegistry[serviceType] = serviceInstance;
                return;
            }

            _serviceRegistry.Add(serviceType, serviceInstance);
        }

        public TService GetService<TService>()
        {
            var serviceType = typeof(TService);
            if (_serviceRegistry.ContainsKey(serviceType))
            {
                return (TService)_serviceRegistry[serviceType];
            }

            throw new Exception($"{serviceType} is unregistered.");
        }
    }
}