using System;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;
using System.Collections.Generic;

namespace Yuran.Insfrastructure.Controller { 
public class PostalCodeController
{
    // CREATE: Add a new PostalCode
    public async Task CreatePostalCodeAsync()
    {
        Console.WriteLine("Enter Postal Code (Id):");
        var postalCodeId = Console.ReadLine();

        Console.WriteLine("Enter Localidade:");
        var localidade = Console.ReadLine();

        using var uow = new UnitOfWork();
        var newPostalCode = new PostalCode
        {
            Id = postalCodeId,
            Localidade = localidade,
            Users = new List<User>(),
            Destinos = new List<Destino>(),
            Fornecedores = new List<Fornecedor>()
        };

        uow.PostalCodeRepository.Create(newPostalCode);
        await uow.SaveAsync();
        Console.WriteLine($"Postal Code '{postalCodeId}' created successfully.");
    }

    // READ: List all PostalCodes
    public async Task PrintPostalCodesAsync()
    {
        using var uow = new UnitOfWork();
        var postalCodes = await uow.PostalCodeRepository.FindAllAsync();

        if (postalCodes.Count > 0)
        {
            Console.WriteLine("All Postal Codes:");
            foreach (var postalCode in postalCodes)
            {
                Console.WriteLine($"-- Id: {postalCode.Id}, Localidade: {postalCode.Localidade}");
            }
        }
        else
        {
            Console.WriteLine("No Postal Codes found.");
        }
    }

    // READ: Find PostalCode by Id
    public async Task FindPostalCodeByIdAsync()
    {
        Console.WriteLine("Enter Postal Code Id:");
        var postalCodeId = Console.ReadLine();

        using var uow = new UnitOfWork();
        var postalCode = await uow.PostalCodeRepository.FindByIdAsync(postalCodeId);

        if (postalCode != null)
        {
            Console.WriteLine($"Postal Code found: Id: {postalCode.Id}, Localidade: {postalCode.Localidade}");
        }
        else
        {
            Console.WriteLine($"Postal Code with Id '{postalCodeId}' not found.");
        }
    }

    // UPDATE: Update an existing PostalCode
    public async Task UpdatePostalCodeAsync()
    {
        Console.WriteLine("Enter Postal Code Id to update:");
        var postalCodeId = Console.ReadLine();

        using var uow = new UnitOfWork();
        var postalCodeToUpdate = await uow.PostalCodeRepository.FindByIdAsync(postalCodeId);

        if (postalCodeToUpdate != null)
        {
            // Display current details
            Console.WriteLine($"Current Postal Code Id: {postalCodeToUpdate.Id}");
            Console.WriteLine($"Current Localidade: {postalCodeToUpdate.Localidade}");

            // Ask for new data
            Console.WriteLine("Enter New Localidade:");
            var newLocalidade = Console.ReadLine();

            // Update the postal code properties
            postalCodeToUpdate.Localidade = newLocalidade;

            await uow.SaveAsync();
            Console.WriteLine($"Postal Code '{postalCodeId}' updated successfully.");
        }
        else
        {
            Console.WriteLine($"Postal Code with Id '{postalCodeId}' not found.");
        }
    }

    // DELETE: Delete a PostalCode
    public async Task DeletePostalCodeAsync()
    {
        Console.WriteLine("Enter Postal Code Id to delete:");
        var postalCodeId = Console.ReadLine();

        using var uow = new UnitOfWork();
        var postalCodeToDelete = await uow.PostalCodeRepository.FindByIdAsync(postalCodeId);

        if (postalCodeToDelete != null)
        {
            Console.WriteLine($"Are you sure you want to delete Postal Code '{postalCodeToDelete.Id}'? (Y/N)");
            var confirmation = Console.ReadLine()?.ToUpper();

            if (confirmation == "Y")
            {
                uow.PostalCodeRepository.Delete(postalCodeToDelete);
                await uow.SaveAsync();
                Console.WriteLine($"Postal Code '{postalCodeId}' deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }
        else
        {
            Console.WriteLine($"Postal Code with Id '{postalCodeId}' not found.");
        }
    }
}
}