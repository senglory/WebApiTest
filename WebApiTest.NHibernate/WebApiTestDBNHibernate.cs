using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

using WebApiTest.Interfaces;



namespace WebApiTest.NHibernate
{
    class WebApiTestDBNHibernate : IAppDbContext
    {
        public Guid Add(Asset dto)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(dto);
                    transaction.Commit();
                    return dto.AssetID;
                }
            }
        }

        public bool Delete(Guid id)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                var dto = session.Get<Asset >(id);
                if (dto == null)
                    return false;
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(dto);
                    trans.Commit();
                    return true;
                }
            }
        }

        public void Dispose()
        {
            
        }

        public Asset FindById(Guid id)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                var query = session.Query<Asset>();

                var dto = query.First( x => x.AssetID == id);
                return dto;
            }
        }

        public QueryResult GetResults(string filterByValue, int start, int length, Dictionary<string, string> orderBy)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                var query = session.Query<Asset>();
                var sc = new AssetSearchClass();
                return sc.GetResults(query, filterByValue, start, length, orderBy);
            }
        }

        public int GetTotalCount()
        {
            using (var session = NHibernateSession.OpenSession())
            {
                var query = session.Query<Asset>();
                var totalCount = query.Count();

                return totalCount;
            }
        }

        public void Update(Asset dto)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(dto);
                    transaction.Commit();
                }
            }
        }
    }
}
