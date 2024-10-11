using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;

public class FornecedorController
{
    // CREATE: Add a new Fornecedor
    public async Task CreateFornecedorAsync()
    {
        Console.WriteLine("Enter Fornecedor Name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter Telefone:");
        var telefone = Console.ReadLine();

        Console.WriteLine("Enter PostalCodeId:");
        var postalCodeId = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(telefone) && !string.IsNullOrWhiteSpace(postalCodeId))
        {
            using var uow = new UnitOfWork();
            var newFornecedor = new Fornecedor
            {
                Name = name,
                Telefone = telefone,
                PostalCodeId = postalCodeId
            };

            uow.FornecedorRepository.Create(newFornecedor);
            await uow.SaveAsync();
            Console.WriteLine($"Fornecedor '{name}' created successfully.");
        }
        else
        {
            Console.WriteLine("All fields must be filled.");
        }
    }

    // READ: Print all Fornecedores
    public async Task PrintFornecedoresAsync()
    {
        Console.WriteLine("All Fornecedores:");
        using var uow = new UnitOfWork();
        var listOfFornecedores = await uow.FornecedorRepository.FindAllAsync();

        if (listOfFornecedores.Count > 0)
        {
            foreach (var fornecedor in listOfFornecedores)
            {
                Console.WriteLine($"-- Name: {fornecedor.Name}, Telefone: {fornecedor.Telefone}, PostalCode: {fornecedor.PostalCodeId}");
            }
        }
        else
        {
            Console.WriteLine("No Fornecedores found.");
        }
    }

    // UPDATE: Update a Fornecedor's details
    public async Task UpdateFornecedorAsync()
    {
        Console.WriteLine("Enter the Fornecedor Name to update:");
        var currentName = Console.ReadLine();

        using var uow = new UnitOfWork();
        var fornecedor = await uow.FornecedorRepository.FindByNameAsync(currentName);

        if (fornecedor != null)
        {
            Console.WriteLine("Enter the new Name:");
            var newName = Console.ReadLine();

            Console.WriteLine("Enter the new Telefone:");
            var newTelefone = Console.ReadLine();

            Console.WriteLine("Enter the new PostalCodeId:");
            var newPostalCodeId = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName) && !string.IsNullOrWhiteSpace(newTelefone) && !string.IsNullOrWhiteSpace(newPostalCodeId))
            {
                fornecedor.Name = newName;
                fornecedor.Telefone = newTelefone;
                fornecedor.PostalCodeId = newPostalCodeId;
                uow.FornecedorRepository.Update(fornecedor);
                await uow.SaveAsync();
                Console.WriteLine($"Fornecedor '{currentName}' updated successfully.");
            }
            else
            {
                Console.WriteLine("All fields must be filled.");
            }
        }
        else
        {
            Console.WriteLine($"Fornecedor with name '{currentName}' not found.");
        }
    }

    // DELETE: Delete a Fornecedor by Name
    public async Task DeleteFornecedorAsync()
    {
        Console.WriteLine("Enter the Fornecedor Name to delete:");
        var name = Console.ReadLine();

        using var uow = new UnitOfWork();
        var fornecedor = await uow.FornecedorRepository.FindByNameAsync(name);

        if (fornecedor != null)
        {
            uow.FornecedorRepository.Delete(fornecedor);
            await uow.SaveAsync();
            Console.WriteLine($"Fornecedor '{name}' deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Fornecedor '{name}' not found.");
        }
    }
}
