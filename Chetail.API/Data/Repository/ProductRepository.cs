using Chetail.API.Data;
using Chetail.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Repository
{
    public class ProductRepository:IProductRepository
    {
        AppDBContext _ctx;
        public ProductRepository(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        // Products Specific
        public IQueryable<Entities.Product> GetProducts()
        {
            return _ctx.Products
                .Include("ProductPrices");
        }     
        public Product GetProduct(int id)
        {
            return _ctx.Products.Include("ProductPrices").SingleOrDefault(p => p.Id == id);
        }
        public RepositoryActionResult<Product> AddProduct(Product newProduct)
        {
            try
            {
                //Adds new product
                _ctx.Products.Add(newProduct);
                //Saves Changes
                var result = _ctx.SaveChanges();

                //Returns Product & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<Product>(newProduct, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Product>(newProduct, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<Product> UpdateProduct(Product updatedProduct)
        {
            try
            {
                // Only update when product already exists
                var existingProduct = _ctx.Products.FirstOrDefault(b => b.Id == updatedProduct.Id);
                if (existingProduct == null)
                {
                    return new RepositoryActionResult<Product>(updatedProduct, RepositoryActionStatus.NotFound);
                }

                // Change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet
                // set original entity state to detached
                _ctx.Entry(existingProduct).State = EntityState.Detached;

                // attach & save
                _ctx.Products.Attach(updatedProduct);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(updatedProduct).State = EntityState.Modified;

                // Save Changes
                var result = _ctx.SaveChanges();

                //Returns Book & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<Product>(updatedProduct, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Product>(updatedProduct, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<Product> DeleteProduct(int id)
        {
            try
            {
                //Finds existing based on ID
                var existing = _ctx.Products.Where(b => b.Id == id).FirstOrDefault();
                if (existing != null)
                {
                    _ctx.Products.Remove(existing);
                    _ctx.SaveChanges();
                    //All went okay
                    return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Deleted);
                }
                //Entity was not found
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Product>(null, RepositoryActionStatus.Error, ex);
            }
        }


        // Product Category Specific
        public IQueryable<ProductCategory> GetProductCategories()
        {
            return _ctx.ProductCategories;
        }
        public IQueryable<ProductCategory> GetProductCategoriesIncludingProducts()
        {
            return _ctx.ProductCategories.Include("Products");
        }
        public ProductCategory GetProductCategory(int id)
        {
            return _ctx.ProductCategories.SingleOrDefault(p => p.Id == id);
        }
        public ProductCategory GetProductCategoryIncludingProducts(int id)
        {
            return _ctx.ProductCategories.Include("Products").SingleOrDefault(p => p.Id == id);
        }
        public RepositoryActionResult<ProductCategory> AddProductCategory(ProductCategory newProductCategory)
        {
            try
            {
                //Adds new product category
                _ctx.ProductCategories.Add(newProductCategory);
                //Saves Changes
                var result = _ctx.SaveChanges();

                //Returns Product Category & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<ProductCategory>(newProductCategory, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ProductCategory>(newProductCategory, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ProductCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<ProductCategory> UpdateProductCategory(ProductCategory updatedProductCategory)
        {
            try
            {
                // Only update when product category already exists
                var existingProduct = _ctx.Products.FirstOrDefault(b => b.Id == updatedProductCategory.Id);
                if (existingProduct == null)
                {
                    return new RepositoryActionResult<ProductCategory>(updatedProductCategory, RepositoryActionStatus.NotFound);
                }

                // Change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet
                // set original entity state to detached
                _ctx.Entry(existingProduct).State = EntityState.Detached;

                // attach & save
                _ctx.ProductCategories.Attach(updatedProductCategory);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(updatedProductCategory).State = EntityState.Modified;

                // Save Changes
                var result = _ctx.SaveChanges();

                //Returns Book & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<ProductCategory>(updatedProductCategory, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ProductCategory>(updatedProductCategory, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ProductCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<ProductCategory> DeleteProductCategory(int id)
        {
            try
            {
                //Finds existing based on ID
                var existing = _ctx.ProductCategories.Where(b => b.Id == id).FirstOrDefault();
                if (existing != null)
                {
                    _ctx.ProductCategories.Remove(existing);
                    _ctx.SaveChanges();
                    //All went okay
                    return new RepositoryActionResult<ProductCategory>(null, RepositoryActionStatus.Deleted);
                }
                //Entity was not found
                return new RepositoryActionResult<ProductCategory>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ProductCategory>(null, RepositoryActionStatus.Error, ex);
            }
        }


        // Product Price Specific
        public IQueryable<ProductPrice> GetProductPrices(int productId)
        {
            return _ctx.ProductPrices
                .Include("Wholesaler")
                .Where(p => p.ProductId == productId);
        }
        public ProductPrice GetProductPrice(int id)
        {
            return _ctx.ProductPrices.Find(id);
        }
        public RepositoryActionResult<ProductPrice> AddProductPrice(ProductPrice newProductPrice)
        {
            try
            {

                newProductPrice.Wholesaler=_ctx.Wholesalers.Find(newProductPrice.WholesalerId);
                //Adds new product price
                _ctx.ProductPrices.Add(newProductPrice);         

                //Saves Changes
                var result = _ctx.SaveChanges();

                //Returns Product Price & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<ProductPrice>(newProductPrice, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<ProductPrice>(newProductPrice, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ProductPrice>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<ProductPrice> UpdateProductPrice(ProductPrice updatedProductPrice)
        {
            try
            {
                // Only update when product price already exists
                var existingProductPrice = _ctx.ProductPrices.FirstOrDefault(b => b.Id == updatedProductPrice.Id);
                if (existingProductPrice == null)
                {
                    return new RepositoryActionResult<ProductPrice>(updatedProductPrice, RepositoryActionStatus.NotFound);
                }

                // Change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet
                // set original entity state to detached
                _ctx.Entry(existingProductPrice).State = EntityState.Detached;

                // attach & save
                _ctx.ProductPrices.Attach(updatedProductPrice);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(updatedProductPrice).State = EntityState.Modified;

                // Save Changes
                var result = _ctx.SaveChanges();

                //Returns Entity & Status
                if (result > 0)
                {
                    return new RepositoryActionResult<ProductPrice>(updatedProductPrice, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<ProductPrice>(updatedProductPrice, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ProductPrice>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<ProductPrice> DeleteProductPrice(int id)
        {
            try
            {
                //Finds existing based on ID
                var existing = _ctx.ProductPrices.Where(b => b.Id == id).FirstOrDefault();
                if (existing != null)
                {
                    _ctx.ProductPrices.Remove(existing);
                    _ctx.SaveChanges();
                    //All went okay
                    return new RepositoryActionResult<ProductPrice>(null, RepositoryActionStatus.Deleted);
                }
                //Entity was not found
                return new RepositoryActionResult<ProductPrice>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ProductPrice>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}