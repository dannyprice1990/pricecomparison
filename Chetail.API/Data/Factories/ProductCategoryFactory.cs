using Chetail.Entities;
using Chetail.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.API.Data.Factories
{
    public class ProductCategoryFactory
    {
        ProductFactory productFactory = new ProductFactory();

          //Data Repo
        private IProductRepository _repo { get; set; }

        //Constructor
        public ProductCategoryFactory(IProductRepository repo)
        {
            _repo = repo;
        }

        public ProductCategory CreateProductCategory(DTO.ProductCategory productCategory)
        {
            return new ProductCategory()
            {
                Id=productCategory.Id,
                Created = productCategory.Created,
                Modified = productCategory.Modified,
                Name = productCategory.Name,
                Products = productCategory.Products == null ? new List<Product>() : 
                            productCategory.Products.Select(p => productFactory.CreateProduct(p)).ToList()              
            };
        }

        public DTO.ProductCategory CreateProductCategory(ProductCategory productCategory)
        {
            //Get count of products for each product Category
            var count = _repo.GetProducts().Where(p => p.ProductCategoryId == productCategory.Id).Count();

            return new DTO.ProductCategory()
            {
                Id = productCategory.Id,
                Created = productCategory.Created,
                Modified = productCategory.Modified,
                Name = productCategory.Name,
                Products = productCategory.Products == null ? new List<DTO.Product>() :
                           productCategory.Products.Select(p => productFactory.CreateProduct(p)).ToList() ,
                ProductCount=count
            };
        }
    }
}