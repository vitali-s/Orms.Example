using System;
using System.Collections.Generic;

using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Oracle.Example.Models;

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

            mapper.Class<LocalizationSettingsData>(ca =>
            {
                ca.Table("LOCALIZATION_SETTINGS");
                ca.Id(x => x.Id, m => m.Column("ID"));
                ca.Property(x => x.EntityId, m => m.Column("ENTITY_ID"));
                ca.Property(x => x.ParentId, m => m.Column("PARENT_ID"));
                ca.Property(x => x.Language, m => m.Column("LANGUAGE"));
                ca.Property(x => x.Formatting, m => m.Column("FORMATTING"));
            });

            mapper.Class<LocalizationData>(ca =>
            {
                ca.Table("LOCALE_OVERRIDES");
                ca.Id(x => x.Id, m => m.Column("ID"));
                ca.Property(x => x.LocalizationSettingsId, m => m.Column("LOCALIZATION_SETTINGS_ID"));
                ca.Property(x => x.Key, m => m.Column("KEY"));
                ca.Property(x => x.Value, m => m.Column("VALUE"));
            });

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
