using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Insfrastructure;

namespace Yuran.ConsolaAPP.Controller
{
    public class CategoryController
    {
        public async Task CreateDemoCategoriesAsync()
        {
            using var uow = new UnitOfWork();
            var demoCategories = new List<string> { "Category1", "Category2", "Category3" };

            foreach (var categoryName in demoCategories)
            {
                var category = await uow.CategoryRepository.FindByNameAsync(categoryName);

                if (category == null)
                {
                    category = new Category { Name = categoryName };
                    uow.CategoryRepository.Create(category);
                }
            }

            await uow.SaveAsync();
        }

        public async Task CreateCategoryAsync()
        {
            Console.WriteLine("Enter Category Name:");
            var categoryName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                using var uow = new UnitOfWork();
                var newCategory = new Category { Name = categoryName };

                uow.CategoryRepository.Create(newCategory);
                await uow.SaveAsync();
                Console.WriteLine($"Category '{categoryName}' created successfully.");
            }
            else
            {
                Console.WriteLine("Category name cannot be empty.");
            }
        }

        public async Task PrintCategoriesAsync()
        {
            Console.WriteLine("All Categories:");
            using var uow = new UnitOfWork();
            var listOfCategories = await uow.CategoryRepository.FindAllAsync();

            if (listOfCategories.Count > 0)
            {
                foreach (var category in listOfCategories)
                {
                    Console.WriteLine($"-- {category.Name}");
                }
            }
            else
            {
                Console.WriteLine("No categories found.");
            }
        }

    }
}
