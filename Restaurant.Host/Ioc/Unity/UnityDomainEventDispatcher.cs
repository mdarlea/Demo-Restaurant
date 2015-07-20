using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Swaksoft.Domain.Seedwork.Events;

namespace Restaurant.Host.Ioc.Unity
{
    public class UnityDomainEventDispatcher : IHandleDomainEvents
    {
        private readonly IUnityContainer _container;

        public UnityDomainEventDispatcher(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }

        public void Handle<T>(T domainEvent) where T : IDomainEvent
        {
            var subscribers = _container.ResolveAll<IHandle<T>>().ToList();
            subscribers.ForEach(s =>
            {
                try
                {
                    s.Handle(domainEvent);
                }
                finally
                {
                    if (s != null)
                    {
                        s.Dispose();
                    }
                }
            });
        }
    }
}