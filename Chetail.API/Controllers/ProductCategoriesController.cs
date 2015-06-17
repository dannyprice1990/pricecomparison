using Chetail.API.Data.Factories;
using Chetail.API.Helpers;
using Chetail.Entities;
using Chetail.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Chetail.API.Controllers
{
    [RoutePrefix("api")]
    public class ProductCategoriesController : ApiController
    {
        //Data Repo
        private IProductRepository _repo { get; set; }

        //Mapping Factory
        ProductCategoryFactory _productCategoryFactory;
        ProductFactory _productFactory;

        //Constructor
        public ProductCategoriesController(IProductRepository repo)
        {
            _repo = repo;
            _productCategoryFactory = new ProductCategoryFactory(_repo);
            _productFactory= new ProductFactory();
        }

        //Constants
        const int maxPageSize = 100;

        // GET api/ProductCategories
        public IHttpActionResult Get(int pageSize = 50, int page = 0, string sort = "Name", bool includeProducts = false)
        {
            try
            {
                //Paging
                //-- Do not exceed max page size
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                //Return object
                var results = _repo.GetProductCategories()
                    .ApplySort(sort); //Sort

                var totalResults = results.Count();

                var results2 = results
                    .Skip(pageSize * page) //Paging
                    .Take(pageSize) //Paging
                    .ToList()
                    .Select(p => _productCategoryFactory.CreateProductCategory(p))
                    .ToList();

                //Paging Object For Header ------------------
                var pHelper = new PaginationHelper();
                //-- Previous and Next URLs
                var urlHelper = new UrlHelper(Request);
                //Total number of pages
                var totalPages = Convert.ToInt32(Math.Ceiling((double)totalResults / pageSize));

                var prevUrl = page > 0 ? urlHelper.Link("ProductCategoriesApi",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                    }) : "";
                var nextUrl = page < totalPages - 1 ? urlHelper.Link("ProductCategoriesApi",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort,
                    }) : "";

                //-- Pagination Object
                var pagination = pHelper
                    .getPaginationObject(Request, totalResults, totalPages, pageSize, page, prevUrl, nextUrl);

                //-- Add pagination to the response header
                HttpContext.Current.Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(pagination));
                //-------------------------------------------

                //Return 200 OK + result
                return Ok(results2);
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // GET api/ProductCategories/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                //Does resource exist?
                ProductCategory productCategory = _repo.GetProductCategory(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                //Return 200 OK + result
                return Ok(_productCategoryFactory.CreateProductCategory(productCategory));
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // POST api/ProductCategories/
        public IHttpActionResult Post([FromBody]DTO.ProductCategory newProductCategory)
        {
            try
            {
                //Make sure a product is passed in
                if (newProductCategory == null)
                {
                    return BadRequest();
                }

                var result = _repo.AddProductCategory(_productCategoryFactory.CreateProductCategory(newProductCategory));
                //If save went okay
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto
                    var _newProductCategory = _productCategoryFactory.CreateProductCategory(result.Entity);

                    //Return new product with auto generated ID
                    return Created<DTO.ProductCategory>(Request.RequestUri + "/" + newProductCategory.Id.ToString(), _newProductCategory);
                }
                //Otherwise 400 Bad Request
                return BadRequest();
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // PUT api/ProductCategories/5
        public IHttpActionResult Put(int id, [FromBody]DTO.ProductCategory updatedProductCategory)
        {
            try
            {
                //Make sure a product category is passed in
                if (updatedProductCategory == null)
                {
                    return BadRequest();
                }

                var result = _repo.UpdateProductCategory(_productCategoryFactory.CreateProductCategory(updatedProductCategory));

                //If save went okay
                if (result.Status == RepositoryActionStatus.Updated)
                { 
                    // map to dto
                    var _updatedProductCategory = _productCategoryFactory
                        .CreateProductCategory(result.Entity);

                    //Return 200 ok + updated resource
                    return Ok(_updatedProductCategory);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                //Otherwise 400 Bad Request
                return BadRequest();
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // DELETE api/ProductCategories/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repo.DeleteProduct(id);

                //If delete went okay
                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    //Return 204 - No Content
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                //Otherwise 400 Bad Request
                return BadRequest();
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }

        }
    }
}
