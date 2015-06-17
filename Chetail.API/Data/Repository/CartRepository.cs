using Chetail.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.Repository
{
    public class CartRepository:ICartRepository
    {
        AppDBContext _ctx;
        public CartRepository(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        public RepositoryActionResult<DTO.CartResult> GetCartResult(DTO.Cart cart)
        {
            try
            {
                var result = new DTO.CartResult();
                result.CartResultItems = new List<DTO.CartResultItem>();

                //Get and loop through Wholesalers
                if (cart.Wholesalers.Count != 1)
                {
                    foreach (var wholesaler in cart.Wholesalers)
                    {
                        //Generate a cart result for each wholesaler
                        var newItem = new DTO.CartResultItem();

                        //Adds wholesaler
                        newItem.Wholesaler = wholesaler;
                        newItem.WholesalerId = wholesaler.Id;
                        newItem.Products = new List<DTO.WholesalerProduct>();

                        decimal wholeSalerTotal = 0;
                        int ProductsAvailCount = 0;
                       int ProductsInStockCount=0;

                        //Loop through products
                        foreach (var product in cart.Products)
                        {
                            var newProduct = new DTO.WholesalerProduct();
                            newProduct.Code = product.Code;
                            newProduct.Created = product.Created;
                            newProduct.Desc = product.Desc;
                            newProduct.Id = product.Id;
                            newProduct.Modified = product.Modified;
                            newProduct.ProductCategoryId = product.ProductCategoryId;
                            newProduct.ProductPrices = product.ProductPrices;
                            newProduct.Qty = product.Qty;                       

                            //Set default of in stock to true for now
                            newProduct.InStock = true;
                            ProductsInStockCount += 1;

                            //Sets wholesaler price for this product
                            var _prod  = _ctx.ProductPrices
                                .Where(p => p.WholesalerId == wholesaler.Id)
                                .Where(p => p.ProductId == product.Id)
                                .FirstOrDefault();
                           
                            //Product is sold by this wholesaler
                            if (_prod !=null){
                                newProduct.Price = _prod.Price * newProduct.Qty;

                                //Wholesaler sells product
                                newProduct.SellsProduct = true;

                                ProductsAvailCount += 1;          
                            }
                            else               
                            {
                                //Wholesaler does not sell this product
                                newProduct.SellsProduct = false;
                            }

                            //Adds product
                            newItem.Products.Add(newProduct);
                            wholeSalerTotal += (newProduct.Price * newProduct.Qty);
                        }

                        //Adds wholesaler total
                        newItem.Price = wholeSalerTotal;
                        newItem.ProductsAvailCount = ProductsAvailCount;
                        newItem.ProductsInStockCount = ProductsInStockCount;

                        //Calculate various fields
                        //-- Are all items available
                        if (newItem.ProductsAvailCount == newItem.Products.Count)
                        {
                            newItem.AllProductsAvail = true;
                        }               
                        //-- What % of items are available
                        newItem.ProductsAvailPct = newItem.ProductsAvailCount / newItem.Products.Count();
                        //-- Are all items in stock
                        if (newItem.ProductsInStockCount == newItem.Products.Count)
                        {
                            newItem.AllProductsInStock = true;
                        }
                        //-- What % of items are in stock
                        newItem.ProductsInStockPct = newItem.ProductsInStockCount / newItem.Products.Count();

                        //Adds new cart item to total set of results
                        result.CartResultItems.Add(newItem);
                    }

                    //Sort list of results by price
                    result.CartResultItems = result.CartResultItems
                        .OrderByDescending(p => p.AllProductsAvail)
                        .ThenByDescending(p => p.ProductsAvailCount)
                        .ThenBy(p => p.Price)
                        .ToList();

                    //Adds top result
                    if (result.CartResultItems.Count != 0)
                    {
                        result.TopResult = result.CartResultItems
                        .OrderByDescending(p => p.AllProductsAvail)
                        .ThenByDescending(p => p.ProductsAvailCount)
                        .ThenBy(p => p.Price)
                        .FirstOrDefault();
                    }
                   
                    return new RepositoryActionResult<DTO.CartResult>(result, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<DTO.CartResult>(null, RepositoryActionStatus.Error,
                        new Exception("No Wholesalers Selected"));
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<DTO.CartResult>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}