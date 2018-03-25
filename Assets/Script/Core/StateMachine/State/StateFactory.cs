using System;
using System.Collections.Generic;
using Core.StateMachine.Builder;

namespace Core.StateMachine
{
    public class StateFactory
    {
        private Dictionary<Type, Dictionary<string, IBuilder>> builders;

        public StateFactory()
        {
            builders = new Dictionary<Type, Dictionary<string, IBuilder>>();
        }

        public void Bind<T>(string name, IBuilder builder)
        {
            bool isExists = builders.ContainsKey(typeof(T));
            if(!isExists)
            {
                builders[typeof(T)] = new Dictionary<string, IBuilder>();
            }
            builders[typeof(T)].Add(name, builder);
        }

        public T CreateState<T>(string name)
        {
            Dictionary<string, IBuilder> namedBuilders;
            bool res = builders.TryGetValue(typeof(T), out namedBuilders);
            IBuilder concreteBuilder;
            res = namedBuilders.TryGetValue(name, out concreteBuilder);

            return (T) concreteBuilder.Build();
        }
    }
}
