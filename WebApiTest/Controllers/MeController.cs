using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using Owin;

using DataTables.AspNet.WebApi2;
using DataTables.AspNet.Core;

using WebApiTest.Interfaces;
using WebApiTest;
using System.Web.Http.ModelBinding;

namespace WebApiTest.Controllers
{
    public class MeController : ApiController
    {
        private IAppDbContext _ctx;

        public MeController()
        {
            var container = UnityConfig.GetConfiguredContainer();
            _ctx = container.Resolve<IAppDbContext>();
        }

        public MeController(IAppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/Me")]
        public IHttpActionResult GetSearchResults()
        {
            var totalCount = _ctx.GetTotalCount();

            var orderBy = new Dictionary<string, string>();
            //var sortedColumns = requestModel.Columns.GetSortedColumns();

            //foreach (var column in sortedColumns)
            //{
            //    orderBy[column.Data] = column.SortDirection.ToString();
            //}

            var queryResult = _ctx.GetResults("", 0, 10, orderBy);

            var res = DataTablesResponse.Create(new DataTablesRequestLocal() {
                Draw = 0,
                 Start =0,
                 Length =10,
                 Columns = new List<IColumn>() { new Column("AssetID", "AssetID", true, true, new Search () )},

            }
            , queryResult.TotalCount, queryResult.FilteredCount, queryResult.Data);
            return Json(res);
        }


        public class DataTablesRequestLocal : IDataTablesRequest
        {
            public int Draw { get; set; }

            public int Start { get; set; }

            public int Length { get; set; }

            public ISearch Search { get; set; }

            public IEnumerable<IColumn> Columns { get; set; }

        public IDictionary<string, object> AdditionalParameters { get; set; }
        }

        [HttpPost]
        [Route("api/Me2")]
        public IHttpActionResult GetSearchResults([ModelBinder(typeof(DataTableModelBinder))] IDataTablesRequest requestModel)
        {
            var totalCount = _ctx.GetTotalCount();

            var orderBy = new Dictionary<string, string>();
            //var sortedColumns = requestModel.Columns.GetSortedColumns();

            //foreach (var column in sortedColumns)
            //{
            //    orderBy[column.Data] = column.SortDirection.ToString();
            //}

            var queryResult = _ctx.GetResults(requestModel.Search.Value, requestModel.Start, requestModel.Length, orderBy);

            var res =  DataTablesResponse .Create(requestModel, queryResult.TotalCount, queryResult.FilteredCount, queryResult.Data);
            return Json(res);
        }
    }
}