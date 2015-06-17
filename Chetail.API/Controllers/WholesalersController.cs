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
    public class WholesalersController : ApiController
    {
        //Data Repo
        private IWholesalerRepository _repo { get; set; }
        //Mapping Factory
        WholesalerFactory _wholesalerFactory = new WholesalerFactory();

        //Constructor
        public WholesalersController(IWholesalerRepository repo)
        {
            _repo = repo;
        }

        //Constants
        const int maxPageSize = 100;

        // GET api/Wholesalers
        public IHttpActionResult Get(int pageSize = 50, int page = 0, string sort = "Name")
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
                var results = _repo.GetWholesalers()
                    .ApplySort(sort); //Sort

                var totalResults = results.Count();

                var results2 = results
                    .Skip(pageSize * page) //Paging
                    .Take(pageSize) //Paging
                    .ToList()
                    .Select(w => _wholesalerFactory.CreateWholesaler(w))
                    .ToList();

                //Paging Object For Header ------------------
                var pHelper = new PaginationHelper();
                //-- Previous and Next URLs
                var urlHelper = new UrlHelper(Request);
                //Total number of pages
                var totalPages = Convert.ToInt32(Math.Ceiling((double)totalResults / pageSize));

                var prevUrl = page > 0 ? urlHelper.Link("WholesalersApi",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                    }) : "";
                var nextUrl = page < totalPages - 1 ? urlHelper.Link("WholesalersApi",
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

        // GET api/Wholesalers/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                //Does resource exist?
                Wholesaler wholesaler = _repo.GetWholesaler(id);
                if (wholesaler == null)
                {
                    return NotFound();
                }

                //Return 200 OK + result
                return Ok(_wholesalerFactory.CreateWholesaler(wholesaler));
            }
            catch (Exception)
            {
                //If failed 500 error
                return InternalServerError();
            }
        }

        // POST api/Wholesalers/
        public IHttpActionResult Post([FromBody]DTO.Wholesaler newWholesaler)
        {
            try
            {
                //Make sure a Wholesaler is passed in
                if (newWholesaler == null)
                {
                    return BadRequest();
                }

                var result = _repo.AddWholesaler(_wholesalerFactory.CreateWholesaler(newWholesaler));
                //If save went okay
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto
                    var _newWholesaler = _wholesalerFactory.CreateWholesaler(result.Entity);

                    //Return new Wholesaler with auto generated ID
                    return Created<DTO.Wholesaler>(Request.RequestUri + "/" + newWholesaler.Id.ToString(), _newWholesaler);
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

        // PUT api/Wholesalers/5
        public IHttpActionResult Put(int id, [FromBody]DTO.Wholesaler updatedWholesaler)
        {
            try
            {
                //Make sure a Wholesaler is passed in
                if (updatedWholesaler == null)
                {
                    return BadRequest();
                }

                var result = _repo.UpdateWholesaler(_wholesalerFactory.CreateWholesaler(updatedWholesaler));
                //If save went okay
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var _updatedWholesaler = _wholesalerFactory
                        .CreateWholesaler(result.Entity);

                    //Return 200 ok + updated resource
                    return Ok(_updatedWholesaler);
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

        // DELETE api/Wholesalers/5
 
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repo.DeleteWholesaler(id);

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
