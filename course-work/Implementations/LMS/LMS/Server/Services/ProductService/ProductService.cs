
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BibliotekBoklusen.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = _context.Products.Include(p => p.Creators)
                .Include(c => c.Category).ToList();
            if (products != null)
            {
                return products;
            }
            else
            {
                return null;
            }

        }

        public async Task<Product> GetProductById(int id)
        {
            var product = _context.Products.Include(p => p.Creators).Include(c => c.Category).FirstOrDefault(p => p.Id == id);
            return product;
        }

        public async Task<ActionResult<string>> CreateProduct(Product productToAdd)
        {
            // Kollar om produkten redan finns i db genom titel och typ.
            var productExists = _context.Products.FirstOrDefault(p => p.Title.ToLower() == productToAdd.Title.ToLower() && p.Type == productToAdd.Type);

            if (productExists == null)
            {
                var categoryList = new List<Category>();

                // Kollar om kategorin redan finns i databasen. Lägger då till den i en lista.
                foreach (var category in productToAdd.Category)
                {
                    var categoryExists = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
                    if (categoryExists != null)
                        categoryList.Add(categoryExists);
                    else
                        categoryList.Add(category);
                }
                productToAdd.Category = categoryList;

                var creatorList = new List<Creator>();

                // Kollar om författaren redan finns. Lägger då till den i en ny lista.
                foreach (var creator in productToAdd.Creators)
                {
                    var creatorExists = _context.Creators.FirstOrDefault(c => c.FirstName.ToLower() == creator.FirstName.ToLower() && c.LastName.ToLower() == creator.LastName.ToLower());

                    if (creatorExists != null)
                        creatorList.Add(creatorExists);
                    else
                        creatorList.Add(creator);
                }
                productToAdd.Creators = creatorList;

                _context.Products.Add(productToAdd);
                await _context.SaveChangesAsync();

                var getProduct = _context.Products.FirstOrDefault(p => p.Title.ToLower() == productToAdd.Title.ToLower() && p.Type == productToAdd.Type);
                CreateProductCopies(getProduct);

                return "Product has been added";
            }
            return "Product already exists";
        }

        public async Task<string> UpdateProduct(int id, Product productToUpdate)
        {
            var productForCount = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var nrOfCopies = productToUpdate.NumberOfCopiesOwned - productForCount.NumberOfCopiesOwned;
            ProductCopy productCopy = new();
            productCopy.ProductId = productToUpdate.Id;

            if (nrOfCopies > 0)
            {
                for (int add = 1; add <= nrOfCopies; add++)
                {
                    productCopy.Id = 0;
                    productCopy.CopyId = productForCount.NumberOfCopiesOwned + add;
                    await _context.productCopies.AddAsync(productCopy);
                    await _context.SaveChangesAsync();
                }
            }

            else if (nrOfCopies < 0)
            {
                int nrOfCopiesToRemovePos = Math.Abs(nrOfCopies);

                for (int count = 0; count < nrOfCopiesToRemovePos; count++)
                {
                    var postToRemove =
                    await _context.productCopies.Where(pc => pc.ProductId == productToUpdate.Id)
                    .OrderByDescending(q => q.CopyId)
                    .FirstOrDefaultAsync();
                    _context.productCopies.Remove(postToRemove);
                    await _context.SaveChangesAsync();
                }
            }

            var product = await _context.Products.Include(p => p.Creators).Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                var creatorList = new List<Creator>();
                var categoryList = new List<Category>();
                var newCreator = new Creator();

                foreach (var creator in productToUpdate.Creators)
                {
                    var creatorExists = await _context.Creators.FirstOrDefaultAsync(c => c.FirstName.ToLower() == creator.FirstName.ToLower() && c.LastName.ToLower() == creator.LastName.ToLower());

                    if (creatorExists != null)
                    {
                        creatorList.Add(creatorExists);
                    }
                    else
                    {
                        newCreator.FirstName = creator.FirstName;
                        newCreator.LastName = creator.LastName;

                        await _context.Creators.AddAsync(newCreator);
                        await _context.SaveChangesAsync();
                        creatorList.Add(newCreator);
                    }
                }

                foreach (var category in productToUpdate.Category)
                {
                    var categoryExists = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
                    if (categoryExists != null)
                        categoryList.Add(categoryExists);
                    else
                        categoryList.Add(category);
                }

                productToUpdate.Category = categoryList;
                productToUpdate.Creators = creatorList;
                product.Title = productToUpdate.Title;
                product.Creators = productToUpdate.Creators;
                product.Type = productToUpdate.Type;
                product.Published = productToUpdate.Published;
                product.Category = productToUpdate.Category;
                product.NumberOfCopiesOwned = productToUpdate.NumberOfCopiesOwned;
                _context.Update(product);
                await _context.SaveChangesAsync();

                return "Product has been updated";
            }
            return "Failed to update product";
        }

        public async Task<string> DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return "There is no product with that ID";
            }
            else
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

            }

            var list = _context.productCopies.Where(p => p.ProductId == id).ToList();
            _context.productCopies.RemoveRange(list);
            _context.SaveChanges();
            return "Product has been deleted";
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await FindProductsBySearchText(searchText)
            };
            return response;

        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                        .Include(c => c.Creators)
                        .Include(c => c.Category)
                        .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                        p.Creators.Any(cr => cr.FirstName.ToLower().Contains(searchText.ToLower()) ||
                        p.Creators.Any(cr => cr.LastName.ToLower().Contains(searchText.ToLower()) ||
                        p.Category.Any(c => c.CategoryName.ToLower().Contains(searchText.ToLower()))))
                        ).ToListAsync();
        }

        public async Task CreateProductCopies(Product product)
        {
            ProductCopy pc = new();
            pc.ProductId = product.Id;

            for (int copyId = 1; copyId <= product.NumberOfCopiesOwned; copyId++)
            {
                pc.CopyId = copyId;
                pc.Id = 0;
                await _context.productCopies.AddAsync(pc);
                _context.SaveChanges();
            }

        }

    }
}
