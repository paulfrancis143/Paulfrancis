using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly Dictionary<Type, Type> _bindings = new();
        public void Bind(Type interfaceType, Type implementationType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));
            if (implementationType == null) throw new ArgumentNullException(nameof(implementationType));
            if (!interfaceType.IsInterface) throw new ArgumentException("interfaceType must be an interface.", nameof(interfaceType));
            if (!interfaceType.IsAssignableFrom(implementationType)) throw new ArgumentException("implementationType does not implement interfaceType.", nameof(implementationType));

            _bindings[interfaceType] = implementationType;
        }
        
        public T Get<T>()
        {
            var type = typeof(T);
            if (!_bindings.TryGetValue(type, out var implementationType))
            {
                throw new InvalidOperationException($"Type {type} not registered.");
            }

            return (T)CreateInstance(implementationType);
        }

        // Create an instance of the type using reflection
        private object CreateInstance(Type type)
        {
            var constructor = type.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            var parameterInstances = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                var parameterInstance = GetService(parameterType);
                parameterInstances[i] = parameterInstance;
            }

            return Activator.CreateInstance(type, parameterInstances);
        }
        
        private object GetService(Type serviceType)
        {
            if (!_bindings.TryGetValue(serviceType, out var implementationType))
            {
                throw new InvalidOperationException($"Service {serviceType} not registered.");
            }

            return CreateInstance(implementationType);
        }
    }
}
