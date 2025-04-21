using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        protected Dictionary<Type, IEntityCompo> _components;

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityCompo>();
            GetComponentsInChildren<IEntityCompo>(true).ToList().ForEach((x) => _components.Add(x.GetType(), x));

            InitComp();
            AfterInit();
        }

        protected virtual void InitComp()
        {
            _components.Values.ToList().ForEach((x) => x.Initialize(this));
        }

        protected virtual void AfterInit()
        {
            _components.Values.ToList().ForEach((x) =>
            {
                if (x is IAfterInit afterInit)
                {
                    afterInit.AfterInit();
                }
            });
        }

        public T GetCompo<T>(bool isdDerived = false) where T : IEntityCompo
        {
            if (_components.TryGetValue(typeof(T), out IEntityCompo comp))
            {
                return (T)comp;
            }

            if (isdDerived == false) return default;

            Type findType = _components.Keys.FirstOrDefault((t) => t.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_components[findType];

            return default;
        }
    }
}