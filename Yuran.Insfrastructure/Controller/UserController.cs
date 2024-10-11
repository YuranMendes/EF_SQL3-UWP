using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Domain.SeedWork;
using Yuran.Insfrastructure;

namespace Yuran.Insfrastructure.Controller
{
    public class UserController
    {
        // CREATE: Add a new User
        public async Task CreateUserAsync()
        {
            Console.WriteLine("Enter User Name:");
            var userName = Console.ReadLine();

            Console.WriteLine("Enter User Password:");
            var userPassword = Console.ReadLine();

            Console.WriteLine("Enter Postal Code Id:");
            var postalCodeId = Console.ReadLine();

            using var uow = new UnitOfWork();
            var newUser = new User
            {
                Name = userName,
                Password = userPassword,
                PostalCodeId = postalCodeId
            };

            uow.UserRepository.Create(newUser);
            await uow.SaveAsync();
            Console.WriteLine($"User '{userName}' created successfully.");
        }

        // READ: List all Users
        public async Task PrintUsersAsync()
        {
            using var uow = new UnitOfWork();
            var users = await uow.UserRepository.FindAllAsync();

            if (users.Count > 0)
            {
                Console.WriteLine("All Users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"-- User Id: {user.Id}, Name: {user.Name}, PostalCodeId: {user.PostalCodeId}");
                }
            }
            else
            {
                Console.WriteLine("No Users found.");
            }
        }

        // READ: Find User by Id
        public async Task FindUserByIdAsync()
        {
            Console.WriteLine("Enter User Id:");
            var userIdInput = Console.ReadLine();

            if (int.TryParse(userIdInput, out int userId))
            {
                using var uow = new UnitOfWork();
                var user = await uow.UserRepository.FindByIdAsync(userId);

                if (user != null)
                {
                    Console.WriteLine($"User found: Id: {user.Id}, Name: {user.Name}, PostalCodeId: {user.PostalCodeId}");
                }
                else
                {
                    Console.WriteLine($"User with Id '{userId}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid User Id.");
            }
        }

        // UPDATE: Update an existing User
        public async Task UpdateUserAsync()
        {
            Console.WriteLine("Enter User Id to update:");
            var userIdInput = Console.ReadLine();

            if (int.TryParse(userIdInput, out int userId))
            {
                using var uow = new UnitOfWork();
                var userToUpdate = await uow.UserRepository.FindByIdAsync(userId);

                if (userToUpdate != null)
                {
                    // Display current details
                    Console.WriteLine($"Current Name: {userToUpdate.Name}");
                    Console.WriteLine($"Current PostalCodeId: {userToUpdate.PostalCodeId}");

                    // Ask for new data
                    Console.WriteLine("Enter New User Name (leave empty to keep current):");
                    var newUserName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newUserName))
                    {
                        userToUpdate.Name = newUserName;
                    }

                    Console.WriteLine("Enter New User Password (leave empty to keep current):");
                    var newUserPassword = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newUserPassword))
                    {
                        userToUpdate.Password = newUserPassword;
                    }

                    Console.WriteLine("Enter New Postal Code Id (leave empty to keep current):");
                    var newPostalCodeId = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newPostalCodeId))
                    {
                        userToUpdate.PostalCodeId = newPostalCodeId;
                    }

                    await uow.SaveAsync();
                    Console.WriteLine($"User '{userId}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"User with Id '{userId}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid User Id.");
            }
        }

        // DELETE: Delete a User
        public async Task DeleteUserAsync()
        {
            Console.WriteLine("Enter User Id to delete:");
            var userIdInput = Console.ReadLine();

            if (int.TryParse(userIdInput, out int userId))
            {
                using var uow = new UnitOfWork();
                var userToDelete = await uow.UserRepository.FindByIdAsync(userId);

                if (userToDelete != null)
                {
                    Console.WriteLine($"Are you sure you want to delete User '{userToDelete.Name}'? (Y/N)");
                    var confirmation = Console.ReadLine()?.ToUpper();

                    if (confirmation == "Y")
                    {
                        uow.UserRepository.Delete(userToDelete);
                        await uow.SaveAsync();
                        Console.WriteLine($"User '{userId}' deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
                else
                {
                    Console.WriteLine($"User with Id '{userId}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid User Id.");
            }
        }
    }
}