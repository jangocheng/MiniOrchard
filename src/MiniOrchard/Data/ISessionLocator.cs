using System;
using NHibernate;

namespace MiniOrchard.Data
{
    public interface ISessionLocator : IDependency {        
        ISession For(Type entityType);
    }
}