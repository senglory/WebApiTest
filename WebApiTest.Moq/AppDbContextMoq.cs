using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebApiTest.Interfaces;

using Moq;

namespace WebApiTest.Moq
{
    public class AppDbContextMoq : IAppDbContext
    {
        public Guid Add(Asset dto)
        {
            var mock = new Mock<IAppDbContext>();


            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Asset FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public QueryResult GetResults(string filterByValue, int start, int length, Dictionary<string, string> orderBy)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public void Update(Asset dto)
        {
            throw new NotImplementedException();
        }
    }
}
