using System;
using System.IO;

using NHibernate;
using NHibernate.Cfg;



namespace WebApiTest.NHibernate
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var configurationPath = Path.Combine (dir, @"bin\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var bookConfigurationFile = Path.Combine(dir, @"bin\Asset.hbm.xml");
            configuration.AddFile(bookConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
