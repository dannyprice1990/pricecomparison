using Chetail.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chetail.API.Controllers
{
    public class CartController : ApiController
    {
        //Data Repo
        private ICartRepository _repo { get; set; }
        //Mapping Factory
       
        //Constructor
        public CartController(ICartRepository repo)
        {
            _repo = repo;
        }

        // POST api/Cart
        public IHttpActionResult Post([FromBody]DTO.Cart cart)
        {
            try
            {
                //Make sure a cart is passed in
                if (cart == null)
                {
                    return BadRequest();
                }

                var result = _repo.GetCartResult(cart);

                if (result.Status == RepositoryActionStatus.Created)
                {
                    //Return result 
                    return Ok<DTO.CartResult>(result.Entity);
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
