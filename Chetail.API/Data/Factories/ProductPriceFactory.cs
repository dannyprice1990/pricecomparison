using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.API.Data.Factories
{
    public class ProductPriceFactory
    {
        WholesalerFactory wholesalerFactory = new WholesalerFactory();

        public DTO.ProductPrice CreateProductPrice(ProductPrice productPrice)
        {
            return new DTO.ProductPrice()
            {
                Id = productPrice.Id,
                Created = productPrice.Created,
                Modified = productPrice.Modified,
                Price = productPrice.Price,
                ProductId = productPrice.ProductId,
                WholesalerId = productPrice.WholesalerId,
                Wholesaler = productPrice.Wholesaler == null ? new DTO.Wholesaler() :
                            wholesalerFactory.CreateWholesaler(productPrice.Wholesaler)
            };
        }
        public ProductPrice CreateProductPrice(DTO.ProductPrice productPrice)
        {
            return new ProductPrice()
            {
                Id = productPrice.Id,
                Created = productPrice.Created,
                Modified = productPrice.Modified,
                Price = productPrice.Price,
                ProductId = productPrice.ProductId,
                WholesalerId = productPrice.WholesalerId,
                Wholesaler = productPrice.Wholesaler == null ? new Wholesaler() :
                            wholesalerFactory.CreateWholesaler(productPrice.Wholesaler)
            };
        }
    }
}