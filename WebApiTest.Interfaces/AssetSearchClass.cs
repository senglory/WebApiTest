using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;



namespace WebApiTest.Interfaces
{
    public class AssetSearchClass
    {
        public virtual QueryResult GetResults(IQueryable<Asset> query, string filterByValue, int start, int length, Dictionary<string, string> orderBy)
        {
            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (filterByValue != string.Empty)
            {
                var value = filterByValue.Trim();

                query = query.Where(p => p.FirstName.Contains(value) ||
                                         p.LastName.Contains(value) ||
                                         p.AssetDate.ToString().Contains(value) ||
                                         p.OrgName.Contains(value) ||
                                         p.Position.Contains(value) ||
                                         p.EMail.Contains(value) ||
                                         p.AssetNumber.Contains(value)
                                   );
            }

            var filteredCount = query.Count();

            #endregion Filtering

            #region Sorting
            // Sorting
            var orderByString = String.Empty;

            foreach (var column in orderBy)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Key) + (string.IsNullOrEmpty(column.Value) ? " asc" : " " + column.Value);
            }

            query = query.OrderBy(orderByString == string.Empty ? "AssetNumber asc" : orderByString);

            #endregion Sorting

            // Paging
            var query2 = query.Skip(start).Take(length).ToList();

            var queryResult = new QueryResult(query2, query.Count(), totalCount);
            return queryResult;
        }
    }
}
