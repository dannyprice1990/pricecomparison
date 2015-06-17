using Chetail.API.Data.Factories;
using Chetail.Entities;
using Chetail.API.Helpers;
using Chetail.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web;

namespace Chetail.API.Controllers
{
    public class ProductPricesController : ApiController
    {
         //Data Repo
        private IProductRepository _repo { get; set; }
        //Mapping Factory
        ProductPriceFactory _productPriceFactory = new ProductPriceFactory();

        //Constructor
        public ProductPricesController(IProductRepository repo)
        {
            _repo = repo;
        }

        //Constants
        const int maxPageSize = 100;

        // GET api/Products/1/ProductPrices
        public IHttpActionResult Get(int productId, int pageSize = 50, int page = 0, string sort = "Price")
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
                var results = _repo.GetProductPrices(productId)
                    .ApplySort(sort); //Sort

                var totalResults = results.Count();

                var results2 = results
                    .Skip(pageSize * page) //Paging
                    .Take(pageSize) //Paging
                    .ToList()
                    .Select(p => _productPriceFactory.CreateProductPrice(p))
                    .ToList();

                //Paging Object For Header ------------------
                var pHelper = new PaginationHelper();
                //-- Previous and Next URLs
                var urlHelper = new UrlHelper(Request);
                //Total number of pages
                var totalPages = Convert.ToInt32(Math.Ceiling((double)totalResults / pageSize));

                var prevUrl = page > 0 ? urlHelper.Link("ProductPricesApi",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                    }) : "";
                var nextUrl = page < totalPages - 1 ? urlHelper.Link("ProductPricesApi",
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

        // GET api/Products/1/ProductPrices/1
        public IHttpActionResult Get(int productId, int id)
        {
            try
            {
                //Does product exist?
                Product product = _repo.GetProduct(productId);
                //Does resource exist?
                ProductPrice productPrice = _repo.GetProductPrice(id);
                if (product == null || productPrice==null)
                {
                    return NotFound();
                }

                //Return 200 OK + result
                return Ok(_productPriceFactory.CreateProductPrice(productPrice));
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // POST api/Products/1/ProductPrices/
        public IHttpActionResult Post(int productId,[FromBody]DTO.ProductPrice newProductPrice)
        {
            try
            {
                //Make sure a product price is passed in
                if (newProductPrice == null)
                {
                    return BadRequest();
                }

                //Does product exist?
                Product product = _repo.GetProduct(productId);
                if (product == null)
                {
                    return NotFound();
                }

                //Does product price already exist?
                ProductPrice existingProductPrice = _repo.GetProductPrices(productId)
                    .Where(p => p.WholesalerId == newProductPrice.WholesalerId)
                    .Where(p => p.ProductId == newProductPrice.ProductId)
                    .FirstOrDefault();

                RepositoryActionResult<ProductPrice> result;

                //If already exists then update
                if (existingProductPrice != null)
                {
                    existingProductPrice.Price = newProductPrice.Price;
                    result = _repo.UpdateProductPrice(existingProductPrice);
                }
                else
                {
                   result = _repo.AddProductPrice(_productPriceFactory.CreateProductPrice(newProductPrice));
                }
           
                //If save went okay
                if (result.Status == RepositoryActionStatus.Created | result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var _newProductPrice = _productPriceFactory.CreateProductPrice(result.Entity);

                    //Return new product price with auto generated ID
                    return Created<DTO.ProductPrice>(Request.RequestUri + "/" + newProductPrice.Id.ToString(), _newProductPrice);
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

        // PUT api/Products/5/ProductPrices/1     
        public IHttpActionResult Put(int productId, int id, [FromBody]DTO.ProductPrice updatedProductPrice)
        {
            try
            {
                //Make sure a product price is passed in
                if (updatedProductPrice == null)
                {
                    return BadRequest();
                }

                //Does product exist?
                Product product = _repo.GetProduct(productId);
                if (product == null)
                {
                    return NotFound();
                }

                var result = _repo.UpdateProductPrice(_productPriceFactory.CreateProductPrice(updatedProductPrice));
                //If save went okay
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var _updatedProductPrice = _productPriceFactory
                        .CreateProductPrice(result.Entity);

                    //Return 200 ok + updated resource
                    return Ok(_updatedProductPrice);
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

        // DELETE api/Products/1/ProductPrices/1
        public IHttpActionResult Delete(int productId, int id)
        {
            try
            {
                //Does product exist?
                Product product = _repo.GetProduct(productId);
                if (product == null)
                {
                    return NotFound();
                }

                var result = _repo.DeleteProductPrice(id);

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
