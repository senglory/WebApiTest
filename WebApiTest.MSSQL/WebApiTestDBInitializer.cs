using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.MSSQL
{
    public class WebApiTestDBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WebApiTestDB>
    {
        public override void InitializeDatabase(WebApiTestDB context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(WebApiTestDB context)
        {
            base.Seed(context);
            var sqlFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.sql");
            foreach (var sql in sqlFiles)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(sql));
            }
        }
    }
}
