using System;
using System.Collections.Generic;

using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Oracle.Example.Mappings;

namespace NHibernate.Oracle.Example.Infrastructure
{
    public class Repository
    {
        private readonly Configuration _configuration;
        private readonly ISessionFactory _sessionFactory;

        public Repository()
        {
            _configuration = new Configuration();

            _configuration.Configure();

            var mapper = new ModelMapper();
            mapper.AddMapping<LocalizationSettingsDataMapping>();
            mapper.AddMapping<LocalizationDataMapping>();
            
            _configuration.AddDeserializedMapping(mapper.CompileMappingForAllExplicitlyAddedEntities(), null);

            _sessionFactory = _configuration.BuildSessionFactory();
        }

        protected TResult Execute<TResult>(Func<ISession, TResult> action)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return action(session);
            }
        }

        protected void Execute(Action<ISession> action)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                action(session);
            }
        }

        protected void ExecuteTransaction(Action<ISession> action)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        action(session);

                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }
        }
    }
}
