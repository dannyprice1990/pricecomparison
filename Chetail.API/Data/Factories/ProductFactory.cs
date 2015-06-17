using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.API.Data.Factories
{
    public class ProductFactory
    {
        ProductPriceFactory productPriceFactory = new ProductPriceFactory();
        WholesalerFactory wholesalerFactory = new WholesalerFactory();

        public DTO.Product CreateProduct(Product product)
        {
            return new DTO.Product()
            {
                Id=product.Id,
                Code = product.Code,
                Created = product.Created,
                Desc = product.Desc,
                Modified = product.Modified,
                ProductCategoryId = product.ProductCategoryId,
                ProductPrices = product.ProductPrices == null ? new List<DTO.ProductPrice>() :
                            product.ProductPrices.Select(p => productPriceFactory.CreateProductPrice(p)).ToList(),  
            };
        }
        public Product CreateProduct(DTO.Product product)
        {
            return new Product()
            {
                Id=product.Id,
                Code = product.Code,
                Created = product.Created,
                Desc = product.Desc,
                Modified = product.Modified,
                ProductCategoryId = product.ProductCategoryId,
                ProductPrices = product.ProductPrices == null ? new List<ProductPrice>() :
                            product.ProductPrices.Select(p => productPriceFactory.CreateProductPrice(p)).ToList(),
            };
        }
    }
}