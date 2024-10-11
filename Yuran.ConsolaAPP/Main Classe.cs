using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuran.Domain.Models;
using Yuran.Insfrastructure.Controller;

namespace Yuran.ConsolaAPP
{
    static class Main_Classe { 
    
     
        static void Main(string[] args)
        {
          
            


        }

        static string Menu()
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
            return option!;
        }
        static void WaitByClick()
        {
            Console.WriteLine("Press any key to proceed");
            Console.ReadLine();
        }
    }
}