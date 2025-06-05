using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

public class AiComponentsContainer
{
    private Dictionary<Type, object> _bindedServices = new Dictionary<Type, object>();

    public void Bind(Type serviceType, object service)
    {
        if (_bindedServices.ContainsKey(serviceType))
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(" ");

            foreach (KeyValuePair<Type, object> current in _bindedServices)
            {
                stringBuilder.AppendLine($"Service -> {current.Key} : {current.Value}");
            }

            Debug.LogError($"Can`t bind same service: {serviceType}" + stringBuilder.ToString());
            return;
        }

        if (serviceType != service.GetType() && service.GetType().IsAssignableFrom(serviceType))
        {
            Debug.LogError($"Critical error -> can`t bind service of not assignable type: {serviceType} Service type: {service.GetType()}");
            return;
        }

        _bindedServices.Add(serviceType, service);
    }

    public void Remove(Type serviceType)
    {
        if (!_bindedServices.ContainsKey(serviceType))
        {
            Debug.LogError($"Can`t remove not existing service: {serviceType}");
            return;
        }

        _bindedServices.Remove(serviceType);
    }

    public T Pop<T>() where T : class
    {
        if (!_bindedServices.ContainsKey(typeof(T)))
        {
            Debug.LogError($"Can`t locate not registred service: {typeof(T)}");
            return null;
        }

        return _bindedServices[typeof(T)] as T;
    }
}