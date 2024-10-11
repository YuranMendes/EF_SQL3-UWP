using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;

namespace Yuran.Insfrastructure.Controller
{
    public class ProductController
    {
        // CREATE: Add a new Product
        public async Task CreateProductAsync()
        {
            Console.WriteLine("Enter Product Name:");
            var productName = Console.ReadLine();

            Console.WriteLine("Enter Product Price:");
            var productPrice = Console.ReadLine();

            Console.WriteLine("Enter Stock Quantity:");
            var stockQuantity = Console.ReadLine();

            Console.WriteLine("Enter Category Id:");
            var categoryIdInput = Console.ReadLine();

            using var uow = new UnitOfWork();
            if (int.TryParse(categoryIdInput, out int categoryId))
            {
                var newProduct = new Product
                {
                    Name = productName,
                    Price = double.TryParse(productPrice, out double price) ? price : 0,
                    Stock = int.TryParse(stockQuantity, out int stock) ? stock : 0,
                    CategoryId = categoryId
                };

                uow.ProductRepository.Create(newProduct);
                await uow.SaveAsync();
                Console.WriteLine($"Product '{productName}' created successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Category Id.");
            }
        }

        // READ: List all Products
        public async Task PrintProductsAsync()
        {
            using var uow = new UnitOfWork();
            var products = await uow.ProductRepository.FindAllAsync();

            if (products.Count > 0)
            {
                Console.WriteLine("All Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"-- Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}, CategoryId: {product.CategoryId}");
                }
            }
            else
            {
                Console.WriteLine("No Products found.");
            }
        }

        // READ: Find Product by Id
        public async Task FindProductByIdAsync()
        {
            Console.WriteLine("Enter Product Id:");
            var productIdInput = Console.ReadLine();

            if (int.TryParse(productIdInput, out int productId))
            {
                using var uow = new UnitOfWork();
                var product = await uow.ProductRepository.FindByIdAsync(productId);

                if (product != null)
                {
                    Console.WriteLine($"Product found: Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}, CategoryId: {product.CategoryId}");
                }
                else
                {
                    Console.WriteLine($"Product with Id '{productId}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Product Id.");
            }
        }

        // UPDATE: Update an existing Product
        public async Task UpdateProductAsync()
        {
            Console.WriteLine("Enter Product Id to update:");
            var productIdInput = Console.ReadLine();

            if (int.TryParse(productIdInput, out int productId))
            {
                using var uow = new UnitOfWork();
                var productToUpdate = await uow.ProductRepository.FindByIdAsync(productId);

                if (productToUpdate != null)
                {
                    // Display current details
                    Console.WriteLine($"Current Name: {productToUpdate.Name}");
                    Console.WriteLine($"Current Price: {productToUpdate.Price}");
                    Console.WriteLine($"Current Stock: {productToUpdate.Stock}");

                    // Ask for new data
                    Console.WriteLine("Enter New Product Name (leave empty to keep current):");
                    var newProductName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newProductName))
                    {
                        productToUpdate.Name = newProductName;
                    }

                    Console.WriteLine("Enter New Product Price (leave empty to keep current):");
                    var newPriceInput = Console.ReadLine();
                    if (double.TryParse(newPriceInput, out double newPrice))
                    {
                        productToUpdate.Price = newPrice;
                    }

                    Console.WriteLine("Enter New Stock Quantity (leave empty to keep current):");
                    var newStockInput = Console.ReadLine();
                    if (int.TryParse(newStockInput, out int newStock))
                    {
                        productToUpdate.Stock = newStock;
                    }

                    await uow.SaveAsync();
                    Console.WriteLine($"Product '{productId}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Product with Id '{productId}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Product Id.");
            }
        }

        // DELETE: Delete a Product
        public async Task DeleteProductAsync()
        {
            Console.WriteLine("Enter Product Id to delete:");
            var productIdInput = Console.ReadLine();

            if (int.TryParse(productIdInput, out int productId))
            {
                using var uow = new UnitOfWork();
                var productToDelete = await uow.ProductRepository.FindByIdAsync(productId);

                if (productToDelete != null)
                {
                    Console.WriteLine($"Are you sure you want to delete Product '{productToDelete.Name}'? (Y/N)");
                    var confirmation = Console.ReadLine()?.ToUpper();

                    if (confirmation == "Y")
                    {
                        uow.ProductRepository.Delete(productToDelete);
                        await uow.SaveAsync();
                        Console.WriteLine($"Product '{productId}' deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
                else
                {
                    Console.WriteLine($"Product with Id '{productId}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Product Id.");
            }
        }
    }
}
