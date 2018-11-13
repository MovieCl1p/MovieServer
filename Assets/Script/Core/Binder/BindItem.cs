using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Binder
{
    public class BindItem
    {
        private readonly Dictionary<Type, BindItem> _bindDictionary;
        private readonly Dictionary<object, Type> _classTypeDictionary = new Dictionary<object, Type>();
        private readonly Dictionary<object, object> _instanceDictionary = new Dictionary<object, object>();
        private bool _isSingleton;
        
        public BindItem(Dictionary<Type, BindItem> bindDictionary)
        {
            _bindDictionary = bindDictionary;
        }
        
        public BindItem Bind<T>()
        {
            var type = typeof(T);
            _bindDictionary.Add(type, this);
            return this;
        }

        public T GetInstance<T>(object key = null)
        {
            if (key == null)
            {
                key = this;
            }

            if (!_isSingleton)
            {
                return CreateInstance<T>(key);
            }
            
            object result;
            if (_instanceDictionary.TryGetValue(key, out result))
            {
                return (T)result;
            }
            
            result = CreateInstance<T>(key);
            _instanceDictionary.Add(key, result);
            return (T)result;
        }

        public BindItem To<T>(object key = null)
        {
            if (key == null)
            {
                key = this;
            }
            
            if (!_classTypeDictionary.ContainsKey(key))
            {
                _classTypeDictionary.Add(key, typeof(T));
            }
            else
            {
                _classTypeDictionary[key] = typeof(T);
                _instanceDictionary.Remove(key);
            }

            return this;
        }
        
        public BindItem ToSingleton()
        {
            _isSingleton = true;

            return this;
        }

        private T CreateInstance<T>(object key)
        {
            if(_isSingleton)
            {
                if (!_classTypeDictionary.ContainsKey(this))
                {
                    _classTypeDictionary.Add(this, typeof(T));
                }
            }
            
            var keyName = key == this ? "Default" : key.ToString();
            
            Type targetType;
            if (!_classTypeDictionary.TryGetValue(key, out targetType))
            {
                Debug.LogError("BindManager : " + typeof(T).Name + " at Key: " + keyName + " inject not found!!!");
                return default(T);
            }
            
            return (T)Activator.CreateInstance(targetType);
        }
    }
}
