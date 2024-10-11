using System;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;

namespace Yuran.Insfrastructure.Controller
{
    public class OutProductController
    {
        // CREATE: Add a new OutProduct
        public async Task CreateOutProductAsync()
        {
            Console.WriteLine("Enter Product ID:");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product ID.");
                return;
            }

            Console.WriteLine("Enter Quantity:");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid Quantity.");
                return;
            }

            Console.WriteLine("Enter Destino ID:");
            if (!int.TryParse(Console.ReadLine(), out int destinoId))
            {
                Console.WriteLine("Invalid Destino ID.");
                return;
            }

            Console.WriteLine("Enter Date (YYYY-MM-DD) or leave blank for current date:");
            var dateInput = Console.ReadLine();
            DateTime? date = string.IsNullOrWhiteSpace(dateInput) ? DateTime.Now : DateTime.Parse(dateInput);

            using var uow = new UnitOfWork();
            var product = await uow.ProductRepository.FindByIdAsync(productId);
            var destino = await uow.DestinoRepository.FindByIdAsync(destinoId);

            if (product == null || destino == null)
            {
                Console.WriteLine($"Product ID '{productId}' or Destino ID '{destinoId}' not found.");
                return;
            }

            var newOutProduct = new OutProduct
            {
                ProductId = productId,
                Product = product,
                Quantity = quantity,
                DestinoId = destinoId,
                Destino = destino,
                Date = date
            };

            uow.OutProductRepository.Create(newOutProduct);
            await uow.SaveAsync();
            Console.WriteLine($"OutProduct created successfully for Product ID '{productId}' and Destino ID '{destinoId}'.");
        }

        // READ: List all OutProducts
        public async Task PrintOutProductsAsync()
        {
            using var uow = new UnitOfWork();
            var outProducts = await uow.OutProductRepository.FindAllAsync();

            if (outProducts.Count > 0)
            {
                Console.WriteLine("All OutProducts:");
                foreach (var outProduct in outProducts)
                {
                    Console.WriteLine($"-- Product ID: {outProduct.ProductId}, Quantity: {outProduct.Quantity}, Destino: {outProduct.Destino?.Description}, Date: {outProduct.Date}");
                }
            }
            else
            {
                Console.WriteLine("No OutProducts found.");
            }
        }

        // UPDATE: Update an existing OutProduct
        public async Task UpdateOutProductAsync()
        {
            Console.WriteLine("Enter OutProduct ID to update:");
            if (!int.TryParse(Console.ReadLine(), out int outProductId))
            {
                Console.WriteLine("Invalid OutProduct ID.");
                return;
            }

            using var uow = new UnitOfWork();
            var outProduct = await uow.OutProductRepository.FindByIdAsync(outProductId);

            if (outProduct != null)
            {
                Console.WriteLine($"Current Product ID: {outProduct.ProductId}");
                Console.WriteLine("Enter New Product ID:");
                if (int.TryParse(Console.ReadLine(), out int newProductId))
                {
                    outProduct.ProductId = newProductId;
                }

                Console.WriteLine($"Current Quantity: {outProduct.Quantity}");
                Console.WriteLine("Enter New Quantity:");
                if (int.TryParse(Console.ReadLine(), out int newQuantity))
                {
                    outProduct.Quantity = newQuantity;
                }

                Console.WriteLine($"Current Destino ID: {outProduct.DestinoId}");
                Console.WriteLine("Enter New Destino ID:");
                if (int.TryParse(Console.ReadLine(), out int newDestinoId))
                {
                    outProduct.DestinoId = newDestinoId;
                }

                Console.WriteLine($"Current Date: {outProduct.Date}");
                Console.WriteLine("Enter New Date (YYYY-MM-DD) or leave blank for no change:");
                var newDateInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDateInput) && DateTime.TryParse(newDateInput, out DateTime newDate))
                {
                    outProduct.Date = newDate;
                }

                await uow.SaveAsync();
                Console.WriteLine($"OutProduct '{outProductId}' updated successfully.");
            }
            else
            {
                Console.WriteLine($"OutProduct with ID '{outProductId}' not found.");
            }
        }

        // DELETE: Delete an OutProduct
        public async Task DeleteOutProductAsync()
        {
            Console.WriteLine("Enter OutProduct ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int outProductId))
            {
                Console.WriteLine("Invalid OutProduct ID.");
                return;
            }

            using var uow = new UnitOfWork();
            var outProduct = await uow.OutProductRepository.FindByIdAsync(outProductId);

            if (outProduct != null)
            {
                Console.WriteLine($"Are you sure you want to delete OutProduct ID '{outProductId}'? (Y/N)");
                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    uow.OutProductRepository.Delete(outProduct);
                    await uow.SaveAsync();
                    Console.WriteLine($"OutProduct '{outProductId}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine($"OutProduct with ID '{outProductId}' not found.");
            }
        }
    }
}