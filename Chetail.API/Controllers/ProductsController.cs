using Chetail.API.Data.Factories;
using Chetail.API.Helpers;
using Chetail.Entities;
using Chetail.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Chetail.API.Controllers
{
    public class ProductsController : ApiController
    {
        //Data Repo
        private IProductRepository _repo { get; set; }
        //Mapping Factory
        ProductFactory _productFactory = new ProductFactory();

        //Constructor
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        //Constants
        const int maxPageSize = 100;

        // GET api/Products
        public IHttpActionResult Get(int pageSize = 50, int page = 0, string sort = "Code", int productCategoryId = 0)
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
                var results = _repo.GetProducts()
                    .Where(p => (productCategoryId == 0 || p.ProductCategoryId == productCategoryId)) //Filter 
                    .ApplySort(sort); //Sort

                var totalResults = results.Count();

                var results2 = results
                    .Skip(pageSize * page) //Paging
                    .Take(pageSize) //Paging
                    .ToList()
                    .Select(p => _productFactory.CreateProduct(p))
                    .ToList();   

                //Paging Object For Header ------------------
                var pHelper = new PaginationHelper();
                //-- Previous and Next URLs
                var urlHelper = new UrlHelper(Request);
                //Total number of pages
                var totalPages = Convert.ToInt32(Math.Ceiling((double)totalResults / pageSize));

                var prevUrl = page > 0 ? urlHelper.Link("ProductsApi",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                    }) : "";
                var nextUrl = page < totalPages - 1 ? urlHelper.Link("ProductsApi",
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

        // GET api/Products/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                //Does resource exist?
                Product product = _repo.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }

                //Return 200 OK + result
                return Ok(_productFactory.CreateProduct(product));
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // POST api/Products/
        public IHttpActionResult Post([FromBody]DTO.Product newProduct)
        {
            try
            {
                //Make sure a product is passed in
                if (newProduct == null)
                {
                    return BadRequest();
                }

                //Does Product Code already exist?
                var existingProduct = _repo.GetProducts()
                    .Where(p => p.Code == newProduct.Code)
                    .FirstOrDefault();

                //Code already exists
                if (existingProduct != null)
                {
                    return BadRequest("Product Code already exists");
                }

                var result = _repo.AddProduct(_productFactory.CreateProduct(newProduct));
                //If save went okay
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto
                    var _newProduct = _productFactory.CreateProduct(result.Entity);

                    //Return new product with auto generated ID
                    return Created<DTO.Product>(Request.RequestUri + "/" + newProduct.Id.ToString(), _newProduct);
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

        // PUT api/Products/5
        public IHttpActionResult Put(int id, [FromBody]DTO.Product updatedProduct)
        {
            try
            {
                //Make sure a product is passed in
                if (updatedProduct == null)
                {
                    return BadRequest();
                }

                var result = _repo.UpdateProduct(_productFactory.CreateProduct(updatedProduct));
                //If save went okay
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var _updatedProduct = _productFactory
                        .CreateProduct(result.Entity);

                    //Return 200 ok + updated resource
                    return Ok(_updatedProduct);
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

        // DELETE api/Products/5
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