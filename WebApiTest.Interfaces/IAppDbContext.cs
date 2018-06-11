using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.Interfaces
{
    public interface IAppDbContext
    {
        int GetTotalCount();
        QueryResult GetResults(string filterByValue, int start, int length, Dictionary<string, string> orderBy);

        Asset FindById(Guid id);
        Guid Add(Asset dto);
        void Update(Asset dto);
        bool Delete(Guid id);
    }
}
