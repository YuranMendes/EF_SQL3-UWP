using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;

namespace Yuran.Insfrastructure.Controller {
    public class CategoryController
    {
        // CREATE: Create a new category with user input
        public async Task CreateCategoryAsync()
        {
            Console.WriteLine("Enter Category Name:");
            var categoryName = Console.ReadLine();

            Console.WriteLine("Enter Category Description:");
            var categoryDescription = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(categoryName) && !string.IsNullOrWhiteSpace(categoryDescription))
            {
                using var uow = new UnitOfWork();
                var newCategory = new Category { Name = categoryName, Description = categoryDescription };

                uow.CategoryRepository.Create(newCategory);
                await uow.SaveAsync();
                Console.WriteLine($"Category '{categoryName}' created successfully.");
            }
            else
            {
                Console.WriteLine("Category name and description cannot be empty.");
            }
        }

        // READ: Print all categories
        public async Task PrintCategoriesAsync()
        {
            Console.WriteLine("All Categories:");
            using var uow = new UnitOfWork();
            var listOfCategories = await uow.CategoryRepository.FindAllAsync();

            if (listOfCategories.Count > 0)
            {
                foreach (var category in listOfCategories)
                {
                    Console.WriteLine($"-- {category.Name} (Description: {category.Description})");
                }
            }
            else
            {
                Console.WriteLine("No categories found.");
            }
        }

        // UPDATE: Update a category's name and description
        public async Task UpdateCategoryAsync()
        {
            Console.WriteLine("Enter the current category name to update:");
            var currentCategoryName = Console.ReadLine();

            using var uow = new UnitOfWork();
            var category = await uow.CategoryRepository.FindByNameAsync(currentCategoryName);

            if (category != null)
            {
                Console.WriteLine("Enter the new category name:");
                var newCategoryName = Console.ReadLine();

                Console.WriteLine("Enter the new category description:");
                var newCategoryDescription = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newCategoryName) && !string.IsNullOrWhiteSpace(newCategoryDescription))
                {
                    category.Name = newCategoryName;
                    category.Description = newCategoryDescription;
                    uow.CategoryRepository.Update(category);
                    await uow.SaveAsync();
                    Console.WriteLine($"Category '{currentCategoryName}' updated to '{newCategoryName}' with description '{newCategoryDescription}'.");
                }
                else
                {
                    Console.WriteLine("New category name and description cannot be empty.");
                }
            }
            else
            {
                Console.WriteLine($"Category '{currentCategoryName}' not found.");
            }
        }

        // DELETE: Delete a category
        public async Task DeleteCategoryAsync()
        {
            Console.WriteLine("Enter the category name to delete:");
            var categoryName = Console.ReadLine();

            using var uow = new UnitOfWork();
            var category = await uow.CategoryRepository.FindByNameAsync(categoryName);

            if (category != null)
            {
                uow.CategoryRepository.Delete(category);
                await uow.SaveAsync();
                Console.WriteLine($"Category '{categoryName}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Category '{categoryName}' not found.");
            }
        }
    }

}


