using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Repository
{
    public interface IProductRepository
    {
        /// Gets Products as IQueryable
        IQueryable<Product> GetProducts();
        /// Gets Product by ID
        Product GetProduct(int id);
        /// Adds a Product
        RepositoryActionResult<Product> AddProduct(Product newProduct);
        /// Updates a Product
        RepositoryActionResult<Product> UpdateProduct(Product updatedProduct);
        ///  Deletes a Product
        RepositoryActionResult<Product> DeleteProduct(int id);


        /// Gets Product Categories as IQueryable
        IQueryable<ProductCategory> GetProductCategories();
        /// Gets Product Category by ID
        ProductCategory GetProductCategory(int id);
        /// Adds a Product Category
        RepositoryActionResult<ProductCategory> AddProductCategory(ProductCategory newProductCategory);
        /// Updates a Product Category
        RepositoryActionResult<ProductCategory> UpdateProductCategory(ProductCategory updatedProductCategory);
        ///  Deletes a Product Category
        RepositoryActionResult<ProductCategory> DeleteProductCategory(int id);


        /// Gets ProductPrices as IQueryable
        IQueryable<ProductPrice> GetProductPrices(int productId);
        /// Gets ProductPrice by ID
        ProductPrice GetProductPrice(int id);
        /// Adds a Product Price
        RepositoryActionResult<ProductPrice> AddProductPrice(ProductPrice newProductPrice);
        /// Updates a Product Price
        RepositoryActionResult<ProductPrice> UpdateProductPrice(ProductPrice updatedProductPrice);
        ///  Deletes a Product Price
        RepositoryActionResult<ProductPrice> DeleteProductPrice(int id);

    }
}

