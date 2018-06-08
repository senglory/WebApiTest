namespace WebApiTest.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Linq.Dynamic;

    using WebApiTest.Interfaces;

    public class WebApiTestDB : DbContext, IAppDbContext
    {
        // Your context has been configured to use a 'WebApiTestDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApiTest.MSSQL.WebApiTestDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WebApiTestDB' 
        // connection string in the application configuration file.
        public WebApiTestDB()
            : base("WebApiTestDB")
        {
        }

        public Guid Add(Asset dto)
        {
            this.Assets.Add(dto);
            this.SaveChanges();
            return dto.AssetID;
        }

        public void Delete(Guid id)
        {
            var dto = this.Assets.Find (id);
            this.Assets.Remove(dto);
            this.SaveChanges();
        }

        public Asset FindById(Guid id)
        {
            var dto = this.Assets.Find(id);
            return dto;
        }

        public void Update(Asset dto)
        {
            this.Entry(dto).State = EntityState.Modified;
            this.SaveChanges();
        }

        public QueryResult GetResults(string filterByValue, int start, int length, Dictionary<string, string> orderBy)
        {
            IQueryable<Asset> query = this.Assets;
            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (filterByValue != string.Empty)
            {
                var value = filterByValue.Trim();

                query = query.Where(p => p.FirstName.Contains(value) ||
                                         p.LastName.Contains(value) ||
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
                orderByString += (column.Key) + (column.Value == "Ascendant" ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "AssetNumber asc" : orderByString);

            #endregion Sorting

            // Paging
            var query2 = query.Skip(start).Take(length).ToList ();

            var queryResult = new QueryResult(query2, query.Count(), totalCount);
            return queryResult;
        }

        public int GetTotalCount()
        {
            IQueryable<Asset> query = this.Assets;
            var totalCount = query.Count();
            return totalCount;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Asset> Assets { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}