using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;

namespace Yuran.Insfrastructure.Controller
{
    public class DestinoController
    {
        // CREATE: Add a new Destino
        public async Task CreateDestinoAsync()
        {
            Console.WriteLine("Enter Destino Description:");
            var description = Console.ReadLine();

            Console.WriteLine("Enter PostalCodeId:");
            var postalCodeId = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(postalCodeId))
            {
                using var uow = new UnitOfWork();
                var newDestino = new Destino
                {
                    Description = description,
                    PostalCodeId = postalCodeId
                };

                uow.DestinoRepository.Create(newDestino);
                await uow.SaveAsync();
                Console.WriteLine($"Destino '{description}' created successfully.");
            }
            else
            {
                Console.WriteLine("Description and PostalCodeId cannot be empty.");
            }
        }

        // READ: Print all Destinos
        public async Task PrintDestinosAsync()
        {
            Console.WriteLine("All Destinos:");
            using var uow = new UnitOfWork();
            var listOfDestinos = await uow.DestinoRepository.FindAllAsync();

            if (listOfDestinos.Count > 0)
            {
                foreach (var destino in listOfDestinos)
                {
                    Console.WriteLine($"-- {destino.Description} (Postal Code ID: {destino.PostalCodeId})");
                }
            }
            else
            {
                Console.WriteLine("No destinos found.");
            }
        }

        // UPDATE: Update a Destino's description and postal code
        public async Task UpdateDestinoAsync()
        {
            Console.WriteLine("Enter the current Destino description to update:");
            var currentDescription = Console.ReadLine();

            using var uow = new UnitOfWork();
            var destino = await uow.DestinoRepository.FindByDescriptionAsync(currentDescription);

            if (destino != null)
            {
                Console.WriteLine("Enter the new description:");
                var newDescription = Console.ReadLine();

                Console.WriteLine("Enter the new PostalCodeId:");
                var newPostalCodeId = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newDescription) && !string.IsNullOrWhiteSpace(newPostalCodeId))
                {
                    destino.Description = newDescription;
                    destino.PostalCodeId = newPostalCodeId;
                    uow.DestinoRepository.Update(destino);
                    await uow.SaveAsync();
                    Console.WriteLine($"Destino '{currentDescription}' updated to '{newDescription}' with PostalCodeId '{newPostalCodeId}'.");
                }
                else
                {
                    Console.WriteLine("New description and PostalCodeId cannot be empty.");
                }
            }
            else
            {
                Console.WriteLine($"Destino with description '{currentDescription}' not found.");
            }
        }

        // DELETE: Delete a Destino
        public async Task DeleteDestinoAsync()
        {
            Console.WriteLine("Enter the description of the Destino to delete:");
            var description = Console.ReadLine();

            using var uow = new UnitOfWork();
            var destino = await uow.DestinoRepository.FindByDescriptionAsync(description);

            if (destino != null)
            {
                uow.DestinoRepository.Delete(destino);
                await uow.SaveAsync();
                Console.WriteLine($"Destino '{description}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Destino '{description}' not found.");
            }
        }
    }
}