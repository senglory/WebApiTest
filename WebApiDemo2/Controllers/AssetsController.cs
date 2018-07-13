using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using WebApiTest.Interfaces;
using Microsoft.Practices.Unity;
using WebApiDemo2.App_Start;



namespace WebApiDemo2.Controllers
{
    public class AssetsController : ApiController
    {
        private IAppDbContext _dbContext;

        public AssetsController()
        {
            var container = UnityConfig.GetConfiguredContainer();

            _dbContext = container.Resolve<IAppDbContext>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("api/assets/{id?}")]
        public IHttpActionResult Get(Guid id)
        {
            var res = _dbContext.FindById(id);
            return Json(res);
        }

        [Route("api/assets")]
        public IHttpActionResult Get()
        {
            var res = new Asset();
            return Json(res);
        }

        [Route("api/assets")]
        public IHttpActionResult Post([FromBody]Asset obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = _dbContext.Add(obj);
            return Json(res);
        }

        [Route("api/assets")]
        public IHttpActionResult Put([FromBody]Asset obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Update(obj);
            return Ok();
        }

        [Route("api/assets/{id?}")]
        public IHttpActionResult Delete(Guid id)
        {
            _dbContext.Delete(id);
            return Ok();
        }


        [Route("api/assets-filter")]
        public IHttpActionResult Post([FromBody]DataTableRequest requestModel)
        {
            var orderBy = new Dictionary<string, string>();
            var sortedColumns = requestModel.Order;

            foreach (var column in sortedColumns)
            {
                orderBy[requestModel.Columns[column.Column].Data] = column.Dir;
            }

            var queryResult = _dbContext.GetResults(requestModel.Search.Value, requestModel.Start, requestModel.Length, orderBy);

            var response = new CustomerSearchResponse
            {
                data = queryResult.Data,
                draw = requestModel.draw,
                recordsFiltered = queryResult.FilteredCount,
                recordsTotal = queryResult.TotalCount
            };
            return Json(response);
        }
    }
}
