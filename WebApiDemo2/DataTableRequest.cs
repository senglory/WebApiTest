using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using WebApiTest.Interfaces;

namespace WebApiDemo2
{
    [ModelBinder(typeof(DataTableModelBinder))]
    public class DataTableRequest
    {
        public int draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTableOrder[] Order { get; set; }
        public DataTableColumn[] Columns { get; set; }
        public DataTableSearch Search { get; set; }
    }

    public class DataTableOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class DataTableSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class DataTableColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }

        public DataTableSearch Search { get; set; }
    }

    //public abstract class SearchDetail
    //{
    //}

    //public class CustomerSearchDetail : SearchDetail
    //{
    //    public string CompanyName { get; set; }
    //    public string Address { get; set; }
    //    public string Postcode { get; set; }
    //    public string Telephone { get; set; }
    //}

    public abstract class SearchResponse<T>// where T : SearchDetail
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public IList<T> data { get; set; }
    }

    public class CustomerSearchResponse : SearchResponse<Asset>
    {
    }

    public class CustomerData
    {
        public IList<Asset> Data { get; set; }
    }

}