using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Insfrastructure;

namespace Yuran.ConsolaAPP
{
    static class Main_Classe
    {
        static void Main(string[] args)
        {
            _ = CreateDemoCategoriesAsync();
            _ = CreateDemoPostalCodesAsync();
            _ = CreateDemoFornecedorsAsync();
            _ = CreateDemoDestinosAsync();
            _ = CreateDemoProductsAsync();
            #region Menu
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("-------- Menu --------");
                Console.WriteLine("1 - Entry Products");
                Console.WriteLine("2 - Out Products");
                Console.WriteLine("3 - Update Product Info");
                Console.WriteLine("4 - Create Fornecedor");
                Console.WriteLine("/////5 - Update Fornecedor");
                Console.WriteLine("6 - Print Fornecedor");
                Console.WriteLine("7 - Delete Fornecedor");
                Console.WriteLine("/////8 - Create Users");
                Console.WriteLine("/////9 - Update Users");
                Console.WriteLine("/////10 - List users");
                Console.WriteLine("/////11- Delete users");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("\nSelect an Option:");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        _ = AddEntryProductAsync();
                        WaitByClick();
                        break;
                    case "2":
                        _ = AddOutProductAsync();
                        WaitByClick();
                        break;
                    case "3":
                        _ = UpdateProductAsync();
                        WaitByClick();
                        break;
                    case "4":
                        _ = CreateFornecedorAsync();
                        WaitByClick();
                        break;
                    case "5":
                        UpdateFornecedorAsync();
                        WaitByClick();
                        break;
                    case "6":
                        PrintFornecedoresAsync();
                        WaitByClick();
                        break;
                    case "7":
                        DeleteFornecedorAsync();
                        WaitByClick();
                        break;
                    case "8":
                        _ = CreateUserAsync();
                        WaitByClick();
                        break;
                    case "9":
                        UpdateUserAsync();
                        WaitByClick();
                        break;
                    case "10":
                        PrintUsersAsync();
                        WaitByClick();
                        break;
                    case "11":
                        DeleteUserAsync();
                        WaitByClick();
                        break;
                    case "12":
                        _ = PrintPostalCodesAsync();
                        WaitByClick();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Option Selected - Use (1-16, 0)");
                        WaitByClick();
                        break;
                }

            }
            #endregion
        }

        static async Task CreateDemoCategoriesAsync()
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

        #region Categories

        static async void CreateCategoriesAsync()
        {
            Console.WriteLine("Enter Category Name:");
            var categoryName = Console.ReadLine();

            using var uow = new UnitOfWork();
            var newCategory = new Category { Name = categoryName };

            uow.CategoryRepository.Create(newCategory);
            await uow.SaveAsync();
        }

        static async void PrintCategoriesAsync()
        {
            Console.WriteLine("All Categories: ");
            using var uow = new UnitOfWork();
            var listOfCategories = await uow.CategoryRepository.FindAllAsync();

            foreach (var category in listOfCategories)
            {
                Console.WriteLine($"-- {category.Name}");
            }
        }
        #endregion

        static async Task CreateDemoProductsAsync()
        {
            using var uow = new UnitOfWork();

            // Define demo products with specific CategoryIds
            var demoProducts = new List<(string Name, double Price, int CategoryId, int Stock)>
    {
        ("Product1", 19.99, 1, 10),
        ("Product2", 29.99, 2, 20),
        ("Product3", 9.99, 3, 0)
    };

            foreach (var (productName, productPrice, categoryId, stock) in demoProducts)
            {
                // Check if the Product already exists
                var existingProduct = await uow.ProductRepository.FindByNameAsync(productName);

                if (existingProduct == null)
                {
                    var category = await uow.CategoryRepository.FindByIdAsync(categoryId);

                    if (category == null)
                    {
                        // Print a warning if the Category is not found
                        Console.WriteLine($"Warning: Category with ID '{categoryId}' not found for Product '{productName}'.");
                    }

                    var newProduct = new Product
                    {
                        Name = productName,
                        Price = productPrice,
                        CategoryId = categoryId,
                        Category = category,
                        Stock = stock
                    };

                    uow.ProductRepository.Create(newProduct);
                }
                else
                {
                    // Print a message if the Product already exists
                    Console.WriteLine($"Product '{productName}' already exists. Skipped creation.");
                }
            }

            await uow.SaveAsync();
        }

        #region Products
        /* nka sabi se mesti
        static async Task CreateProductsAsync(EntryProduct entryProduct)
        {
            Console.WriteLine("Enter Category Name:");
            var categoryName = Console.ReadLine();

            using var uow = new UnitOfWork();
            var category = await uow.CategoryRepository.FindByNameAsync(categoryName);

            if (category != null)
            {
                CreateProduct(uow, entryProduct);
            }
            else
            {
                Console.WriteLine($"Category '{categoryName}' not found. Do you want to create a new category? (Y/N)");

                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    var newCategory = new Category { Name = categoryName };

                    uow.CategoryRepository.Create(newCategory);
                    await uow.SaveAsync();

                    Console.WriteLine($"Category '{categoryName}' created.");

                    CreateProduct(uow, entryProduct);
                }
                else
                {
                    Console.WriteLine("Product creation canceled.");
                }
            }
        }*/

        static async Task<Product?> CreateProductAsync(UnitOfWork uow)
        {
            Console.WriteLine("Enter Category Name:");
            var categoryName = Console.ReadLine();

            var category = await uow.CategoryRepository.FindByNameAsync(categoryName);

            if (category == null)
            {
                Console.WriteLine($"Category '{categoryName}' not found. Do you want to create a new category? (Y/N)");

                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    category = new Category { Name = categoryName };
                    uow.CategoryRepository.Create(category);
                    await uow.SaveAsync();
                    Console.WriteLine($"Category '{categoryName}' created.");
                }
                else
                {
                    Console.WriteLine("Product creation canceled.");
                    return null;
                }
            }
            Console.WriteLine("Enter Product Name:");
            var productName = Console.ReadLine();

            Console.WriteLine("Enter Product Price:");
            var productPrice = Console.ReadLine();

            // Additional input for product properties if needed
            if (double.TryParse(productPrice, out double productPrice_checked))
            {
                // Parsing successful, 'productPrice' contains the parsed value
                // Now you can use the 'productPrice' variable for further processing
                Console.WriteLine($"Product Price: {productPrice}");
            }
            var newProduct = new Product { Name = productName, Category = category, CategoryId = category.Id, Price = productPrice_checked };

            category.Products.Add(newProduct);
            await uow.ProductRepository.FindOrCreateAsync(newProduct);

            return newProduct;
        }

        // mesti kurrigi list 
        //static async Task ListProductsAsync()
        //{
        //    Console.WriteLine("Enter EntryProduct Id:");
        //    var entryProductId = Console.ReadLine();

        //    if (int.TryParse(entryProductId, out int id))
        //    {
        //        using var uow = new UnitOfWork();
        //        var entryProduct = await uow.EntryProductRepository.FindByIdAsync(id);

        //        if (entryProduct != null)
        //        {
        //            Console.WriteLine($"Products in EntryProduct {entryProduct.Id}:");

        //            foreach (var product in entryProduct.Products)
        //            {
        //                Console.WriteLine($"{product.Name}, Price: {product.Price}");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"EntryProduct with Id '{entryProductId}' not found.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid EntryProduct Id.");
        //    }
        //}

        static async Task UpdateProductAsync()
        {
            Console.WriteLine("Enter Product Name to Update:");
            var productNameToUpdate = Console.ReadLine();

            using var uow = new UnitOfWork();
            var productToUpdate = await uow.ProductRepository.FindByNameAsync(productNameToUpdate);

            if (productToUpdate != null)
            {
                // Display current details
                Console.WriteLine($"Current Product Name: {productToUpdate.Name}");
                Console.WriteLine($"Current Product Price: {productToUpdate.Price}");

                // Ask for new data
                Console.WriteLine("Enter New Product Name:");
                var newProductName = Console.ReadLine();

                Console.WriteLine("Enter New Product Price:");
                if (double.TryParse(Console.ReadLine(), out double newProductPrice))
                {
                    // Update the product properties
                    productToUpdate.Name = newProductName;
                    productToUpdate.Price = newProductPrice;

                    await uow.SaveAsync();

                    Console.WriteLine($"Product '{productNameToUpdate}' updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid price format. Please enter a valid double.");
                }
            }
            else
            {
                Console.WriteLine($"Product '{productNameToUpdate}' not found.");
            }
        }


        static async void DeleteProductAsync()
        {
            Console.WriteLine("Enter Product Name to Delete:");
            var productNameToDelete = Console.ReadLine();

            using var uow = new UnitOfWork();
            var productToDelete = await uow.ProductRepository.FindByNameAsync(productNameToDelete);

            if (productToDelete != null)
            {
                Console.WriteLine($"Are you sure you want to delete product '{productToDelete.Name}'? (Y/N)");
                var confirmation = Console.ReadLine()?.ToUpper();

                if (confirmation == "Y")
                {
                    uow.ProductRepository.Delete(productToDelete);
                    await uow.SaveAsync();
                    Console.WriteLine($"Product '{productNameToDelete}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Product deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine($"Product '{productNameToDelete}' not found.");
            }
        }

        static async Task AddEntryProductAsync()
        {
            Console.WriteLine("Enter Product Name:");
            var productName = Console.ReadLine();

            using var uow = new UnitOfWork();
            var product = await uow.ProductRepository.FindByNameAsync(productName);

            if (product == null)
            {
                Console.WriteLine($"Product '{productName}' not found. Do you want to create a new product? (Y/N)");

                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    // Create a new product and use it
                    product = await CreateProductAsync(uow);
                    if (product == null)
                    {
                        Console.WriteLine("Entry creation canceled.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Entry creation canceled.");
                    return;
                }
            }


            Console.WriteLine("Enter Fornecedor Id:");
            var fornecedorId = Console.ReadLine();
            var fornecedor = await uow.FornecedorRepository.FindByIdAsync(Convert.ToInt32(fornecedorId));

            if (fornecedor != null)
            {
                Console.WriteLine("Enter Quantity:");
                var quantityInput = Console.ReadLine();

                if (int.TryParse(quantityInput, out int quantity) && quantity > 0)
                {
                    product.Stock += quantity;
                    var currentDate = DateTime.Now;

                    var entryProduct = new EntryProduct
                    {
                        Date = currentDate,
                        Product = product,
                        ProdutoId = product.Id,
                        Quantity = quantity,
                        Fornecedor = fornecedor,
                        FornecedorId = fornecedor.Id
                    };

                    uow.EntryProductRepository.Create(entryProduct);
                    await uow.SaveAsync();

                    // Update the Product's list of EntryProducts
                    product.EntryProducts.Add(entryProduct);
                    await uow.SaveAsync();

                    // Update the Fornecedor's list of EntryProducts
                    fornecedor.EntryProducts.Add(entryProduct);
                    await uow.SaveAsync();

                    Console.WriteLine($"Entry for Fornecedor '{fornecedor.Name}' and Product '{product.Name}' created successfully with quantity {quantity}.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Quantity must be a positive integer.");
                }
            }
            else
            {
                Console.WriteLine($"Fornecedor with ID '{fornecedorId}' not found. Do you want to create a new Fornecedor? (Y/N)");

                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    // Create a new Fornecedor and use it
                    fornecedor = await CreateFornecedorAsync(uow);
                    if (fornecedor != null)
                    {
                        // Continue with entry creation
                        Console.WriteLine("Enter Quantity:");
                        var quantityInput = Console.ReadLine();

                        if (int.TryParse(quantityInput, out int quantity) && quantity > 0)
                        {
                            var currentDate = DateTime.Now;

                            var entryProduct = new EntryProduct
                            {
                                Date = currentDate,
                                Product = product,
                                ProdutoId = product.Id,
                                Quantity = quantity,
                                Fornecedor = fornecedor,
                                FornecedorId = fornecedor.Id
                            };

                            uow.EntryProductRepository.Create(entryProduct);
                            await uow.SaveAsync();

                            // Update the Product's list of EntryProducts
                            product.EntryProducts.Add(entryProduct);
                            await uow.SaveAsync();

                            // Update the Fornecedor's list of EntryProducts
                            fornecedor.EntryProducts.Add(entryProduct);
                            await uow.SaveAsync();

                            Console.WriteLine($"Entry for Fornecedor '{fornecedor.Name}' and Product '{product.Name}' created successfully with quantity {quantity}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity. Quantity must be a positive integer.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Entry creation canceled.");
                }

            }



        }

        static async Task AddOutProductAsync()
        {
            Console.WriteLine("Enter Product Name:");
            var productName = Console.ReadLine();

            using var uow = new UnitOfWork();
            var product = await uow.ProductRepository.FindByNameAsync(productName);

            if (product == null)
            {
                Console.WriteLine($"Product '{productName}' not found. OutProduct creation canceled.");
                return;
            }

            Console.WriteLine("Enter Destination ID:");
            var DestinoId = Console.ReadLine();
            if (DestinoId == null)
            {
                return;
            }
            int destinoId_checked = int.Parse(DestinoId);

            var destino = await uow.DestinoRepository.FindByIdAsync(destinoId_checked);

            if (destino != null)
            {
                Console.WriteLine("Enter Quantity:");
                var quantityInput = Console.ReadLine();

                if (int.TryParse(quantityInput, out int quantity) && quantity > 0 && quantity <= product.Stock)
                {
                    product.Stock -= quantity;
                    var currentDate = DateTime.Now;

                    var outProduct = new OutProduct
                    {
                        Date = currentDate,
                        Product = product,
                        ProductId = product.Id,
                        Quantity = quantity,
                        Destino = destino,
                        DestinoId = destino.Id
                    };

                    uow.OutProductRepository.Create(outProduct);
                    await uow.SaveAsync();

                    // Update the Product's list of OutProducts
                    product.OutProducts.Add(outProduct);
                    await uow.SaveAsync();

                    Console.WriteLine($"OutProduct for Destination Postal Code '{destino.Id}' and Product '{product.Name}' created successfully with quantity {quantity}.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Quantity must be a positive integer and cannot exceed the available stock.");
                }
            }
            else
            {
                Console.WriteLine($"Destination with the id '{destinoId_checked}' not found. OutProduct creation canceled.");
            }
        }

        #endregion

        #region Users
        static public async Task CreateUserAsync()
        {
            Console.WriteLine("Enter User Name:");
            var userName = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            var pass = Console.ReadLine();

            Console.WriteLine("Enter Postal Code:");
            var postalCodeValue = Console.ReadLine();

            using var uow = new UnitOfWork();

            // Check if the postal code already exists
            var existingPostalCode = await uow.PostalCodeRepository.FindByNameAsync(postalCodeValue);

            if (existingPostalCode == null)
            {
                Console.WriteLine("Enter Postal Code Location:");
                var postalCodeLocation = Console.ReadLine();

                var newPostalCode = new PostalCode { Id = postalCodeValue, Localidade = postalCodeLocation };

                uow.PostalCodeRepository.Create(newPostalCode);
                await uow.SaveAsync();

                var newUser = new User { Name = userName, PostalCodeId = postalCodeValue, PostalCode = newPostalCode, Password = pass };

                var createdUser = await uow.UserRepository.FindOrCreateAsync(newUser);
                await uow.SaveAsync();

                if (createdUser != null)
                {
                    Console.WriteLine($"User '{userName}' created.");

                    // Add the newly created user to the Users collection of the PostalCode
                    newPostalCode.Users ??= new List<User> { createdUser };
                    newPostalCode.Users.Add(createdUser);
                    await uow.SaveAsync();
                }
                else
                {
                    Console.WriteLine($"Failed to create or find user '{userName}'.");
                }
            }
            else
            {
                var newUser = new User { Name = userName, PostalCodeId = postalCodeValue, PostalCode = existingPostalCode };

                var createdUser = await uow.UserRepository.FindOrCreateAsync(newUser);
                await uow.SaveAsync();

                if (createdUser != null)
                {
                    Console.WriteLine($"User '{userName}' created or already exists with ID {createdUser.Id}");

                    // Add the newly created user to the Users collection of the existing PostalCode
                    existingPostalCode.Users ??= new List<User>();
                    existingPostalCode.Users.Add(createdUser);
                    await uow.SaveAsync();
                }
                else
                {
                    Console.WriteLine($"Failed to create or find user '{userName}'.");
                }
            }
        }

        static async void PrintUsersAsync()
        {
            Console.WriteLine("All User: ");
            using var uow = new UnitOfWork();
            var listOfUsers = await uow.UserRepository.FindAllAsync(include => include.Include(u => u.PostalCode));


            foreach (var user in listOfUsers)
            {
                if (user != null)
                {
                    Console.WriteLine($"-- {user}");
                }
            }
        }

        static async void UpdateUserAsync()
        {
            Console.WriteLine("Enter User Id to Update:");
            var userIdToUpdate = Console.ReadLine();

            using var uow = new UnitOfWork();
            var userToUpdate = await uow.UserRepository.FindByIdAsync(Convert.ToInt32(userIdToUpdate));

            if (userToUpdate != null)
            {
                Console.WriteLine("Enter New User Name:");
                var newUserName = Console.ReadLine();

                // Update the user properties
                userToUpdate.Name = newUserName;

                await uow.SaveAsync();
                Console.WriteLine($"User '{userIdToUpdate}' updated successfully.");
            }
            else
            {
                Console.WriteLine($"User '{userIdToUpdate}' not found.");
            }
        }

        static async void DeleteUserAsync()
        {
            Console.WriteLine("Enter User Id to Delete:");
            var userIdToDelete = Console.ReadLine();

            using var uow = new UnitOfWork();
            var userToDelete = await uow.UserRepository.FindByIdAsync(Convert.ToInt32(userIdToDelete));

            if (userToDelete != null)
            {
                Console.WriteLine($"Are you sure you want to delete user '{userToDelete.Name}'? (Y/N)");
                var confirmation = Console.ReadLine()?.ToUpper();

                if (confirmation == "Y")
                {
                    uow.UserRepository.Delete(userToDelete);
                    await uow.SaveAsync();
                    Console.WriteLine($"User '{userIdToDelete}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"User deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine($"User '{userIdToDelete}' not found.");
            }
        }
        #endregion

        static async Task CreateDemoPostalCodesAsync()
        {
            using var uow = new UnitOfWork();
            var demoPostalCodes = new List<string> { "PostalCode1", "PostalCode2", "PostalCode3" };

            foreach (var postalCodeId in demoPostalCodes)
            {
                var postalCode = await uow.PostalCodeRepository.FindByNameAsync(postalCodeId);

                if (postalCode == null)
                {
                    var postalCodeLocation = "";

                    // Assign specific locations for demo PostalCodes
                    switch (postalCodeId)
                    {
                        case "PostalCode1":
                            postalCodeLocation = "Location1";
                            break;
                        case "PostalCode2":
                            postalCodeLocation = "Location2";
                            break;
                        case "PostalCode3":
                            postalCodeLocation = "Location3";
                            break;
                    }

                    postalCode = new PostalCode { Id = postalCodeId, Localidade = postalCodeLocation };
                    uow.PostalCodeRepository.Create(postalCode);
                }
            }

            await uow.SaveAsync();
        }

        #region PostalCode

        public static async Task CreatePostalCodeAsync()
        {
            Console.WriteLine("Enter Postal Code:");
            var postalCodeValue = Console.ReadLine();

            Console.WriteLine("Enter Postal Code Location:");
            var postalCodeLocation = Console.ReadLine();

            using var uow = new UnitOfWork();
            var newPostalCode = new PostalCode { Id = postalCodeValue, Localidade = postalCodeLocation };

            uow.PostalCodeRepository.Create(newPostalCode);
            await uow.SaveAsync();
        }

        static public async Task PrintPostalCodesAsync()
        {
            Console.WriteLine("All Postal Codes: ");
            using var uow = new UnitOfWork();
            var listOfPostalCodes = await uow.PostalCodeRepository.FindAllAsync();

            foreach (var postalCode in listOfPostalCodes)
            {
                Console.WriteLine($"-- {postalCode}");
            }
        }
        #endregion

        static async Task CreateDemoFornecedorsAsync()
        {
            using var uow = new UnitOfWork();

            // Define demo fornecedors with specific PostalCodeIds
            var demoFornecedors = new List<(string Name, string Telefone, string PostalCodeId)>
    {
        ("Fornecedor1", "123456789", "PostalCode1"),
        ("Fornecedor2", "987654321", "PostalCode2"),
        ("Fornecedor3", "456789123", "PostalCode3")
    };

            foreach (var (fornecedorName, fornecedorTelefone, postalCodeId) in demoFornecedors)
            {
                // Check if the Fornecedor already exists
                var existingFornecedor = await uow.FornecedorRepository.FindByNameAsync(fornecedorName);

                if (existingFornecedor == null)
                {
                    var postalCode = await uow.PostalCodeRepository.FindByNameAsync(postalCodeId);

                    var newFornecedor = new Fornecedor
                    {
                        Name = fornecedorName,
                        Telefone = fornecedorTelefone,
                        PostalCodeId = postalCodeId,
                        PostalCode = postalCode
                    };

                    uow.FornecedorRepository.Create(newFornecedor);
                }
            }

            await uow.SaveAsync();
        }

        #region Fornecedor

        static async Task<Fornecedor> CreateFornecedorAsync(UnitOfWork uow)
        {
            Console.WriteLine("Enter Fornecedor Name:");
            var fornecedorName = Console.ReadLine();
            Console.WriteLine("Enter Fornecedor Telefone:");
            var fornecedorTelefone = Console.ReadLine();
            Console.WriteLine("Enter Postal Code:");
            var postalCodeValue = Console.ReadLine();

            // using var uow = new UnitOfWork();

            // Check if the postal code already exists
            var existingPostalCode = await uow.PostalCodeRepository.FindByNameAsync(postalCodeValue);

            if (existingPostalCode == null)
            {
                Console.WriteLine("Enter Postal Code Location:");
                var postalCodeLocation = Console.ReadLine();

                var newPostalCode = new PostalCode { Id = postalCodeValue, Localidade = postalCodeLocation };

                uow.PostalCodeRepository.Create(newPostalCode);
                await uow.SaveAsync();

                var newFornecedor = new Fornecedor { Name = fornecedorName, Telefone = fornecedorTelefone, PostalCodeId = postalCodeValue, PostalCode = newPostalCode };

                uow.FornecedorRepository.Create(newFornecedor);
                await uow.SaveAsync();

                Console.WriteLine($"Fornecedor '{fornecedorName}' created.");
                return newFornecedor;
            }
            else
            {
                var newFornecedor = new Fornecedor { Name = fornecedorName, Telefone = fornecedorTelefone, PostalCodeId = postalCodeValue, PostalCode = existingPostalCode };

                uow.FornecedorRepository.Create(newFornecedor);
                await uow.SaveAsync();

                Console.WriteLine($"Fornecedor '{fornecedorName}' created with existing Postal Code '{postalCodeValue}'.");
                return newFornecedor;
            }
        }
        static async Task<Fornecedor> CreateFornecedorAsync()
        {
            using var uow = new UnitOfWork();
            Console.WriteLine("Enter Fornecedor Name:");
            var fornecedorName = Console.ReadLine();
            Console.WriteLine("Enter Fornecedor Telefone:");
            var fornecedorTelefone = Console.ReadLine();
            Console.WriteLine("Enter Postal Code:");
            var postalCodeValue = Console.ReadLine();

            // using var uow = new UnitOfWork();

            // Check if the postal code already exists
            var existingPostalCode = await uow.PostalCodeRepository.FindByNameAsync(postalCodeValue);

            if (existingPostalCode == null)
            {
                Console.WriteLine("Enter Postal Code Location:");
                var postalCodeLocation = Console.ReadLine();

                var newPostalCode = new PostalCode { Id = postalCodeValue, Localidade = postalCodeLocation };

                uow.PostalCodeRepository.Create(newPostalCode);
                await uow.SaveAsync();

                var newFornecedor = new Fornecedor { Name = fornecedorName, Telefone = fornecedorTelefone, PostalCodeId = postalCodeValue, PostalCode = newPostalCode };

                uow.FornecedorRepository.Create(newFornecedor);
                await uow.SaveAsync();

                Console.WriteLine($"Fornecedor '{fornecedorName}' created.");
                return newFornecedor;
            }
            else
            {
                var newFornecedor = new Fornecedor { Name = fornecedorName, Telefone = fornecedorTelefone, PostalCodeId = postalCodeValue, PostalCode = existingPostalCode };

                uow.FornecedorRepository.Create(newFornecedor);
                await uow.SaveAsync();

                Console.WriteLine($"Fornecedor '{fornecedorName}' created with existing Postal Code '{postalCodeValue}'.");
                return newFornecedor;
            }
        }

        static async void PrintFornecedoresAsync()
        {
            Console.WriteLine("All Fornecedores: ");
            using var uow = new UnitOfWork();
            var listOfFornecedores = await uow.FornecedorRepository.FindAllAsync(include => include.Include(f => f.PostalCode));

            foreach (var fornecedor in listOfFornecedores)
            {
                if (fornecedor != null)
                {
                    Console.WriteLine($"-- {fornecedor}");
                }
            }
        }

        static async void UpdateFornecedorAsync()
        {
            Console.WriteLine("Enter Fornecedor Id to Update:");
            var fornecedorIdToUpdate = Console.ReadLine();

            using var uow = new UnitOfWork();
            var fornecedorToUpdate = await uow.FornecedorRepository.FindByIdAsync(Convert.ToInt32(fornecedorIdToUpdate));

            if (fornecedorToUpdate != null)
            {
                Console.WriteLine("Enter New Fornecedor Name:");
                var newFornecedorName = Console.ReadLine();
                Console.WriteLine("Enter New Fornecedor Telefone:");
                var newFornecedorTelefone = Console.ReadLine();

                // Update the fornecedor properties
                fornecedorToUpdate.Name = newFornecedorName;
                fornecedorToUpdate.Telefone = newFornecedorTelefone;

                await uow.SaveAsync();
                Console.WriteLine($"Fornecedor '{fornecedorIdToUpdate}' updated successfully.");
            }
            else
            {
                Console.WriteLine($"Fornecedor '{fornecedorIdToUpdate}' not found.");
            }
        }

        static async void DeleteFornecedorAsync()
        {
            Console.WriteLine("Enter Fornecedor Id to Delete:");
            var fornecedorIdToDelete = Console.ReadLine();

            using var uow = new UnitOfWork();
            var fornecedorToDelete = await uow.FornecedorRepository.FindByIdAsync(Convert.ToInt32(fornecedorIdToDelete));

            if (fornecedorToDelete != null)
            {
                Console.WriteLine($"Are you sure you want to delete fornecedor '{fornecedorToDelete.Name}'? (Y/N)");
                var confirmation = Console.ReadLine()?.ToUpper();

                if (confirmation == "Y")
                {
                    uow.FornecedorRepository.Delete(fornecedorToDelete);
                    await uow.SaveAsync();
                    Console.WriteLine($"Fornecedor '{fornecedorIdToDelete}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Fornecedor deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine($"Fornecedor '{fornecedorIdToDelete}' not found.");
            }
        }
        #endregion

        static async Task CreateDemoDestinosAsync()
        {
            using var uow = new UnitOfWork();

            // Define demo destinos with specific PostalCodeIds
            var demoDestinos = new List<(string Description, string PostalCodeId)>
    {
        ("Destination1", "PostalCode1"),
        ("Destination2", "PostalCode2"),
        ("Destination3", "PostalCode3")
    };

            foreach (var (destinoDescription, postalCodeId) in demoDestinos)
            {
                // Check if the Destino already exists
                var existingDestino = await uow.DestinoRepository.FindByNameAsync(destinoDescription);

                if (existingDestino == null)
                {
                    var postalCode = await uow.PostalCodeRepository.FindByNameAsync(postalCodeId);

                    var newDestino = new Destino
                    {
                        Description = destinoDescription,
                        PostalCodeId = postalCodeId,
                        PostalCode = postalCode
                    };

                    uow.DestinoRepository.Create(newDestino);
                }
            }

            await uow.SaveAsync();
        }

        static void WaitByClick()
        {
            Console.WriteLine("Press any key to proceed");
            Console.ReadLine();
        }
    }
}