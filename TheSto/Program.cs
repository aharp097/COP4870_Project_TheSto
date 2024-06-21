using System.Dynamic;
using System.Security.Cryptography;
using STO.Library.Services;
using STO.Models;
//video 6
namespace TheSto
{
    internal class Sto
    {
        static void Main(string[] args)
        {
            var contactsvc = ContactServerProxy.Current;
            string input = "0";
            List<Product> cart = new List<Product>();
            while (input != "5")
            {
                Console.WriteLine("Welcome to The Sto!");
                Console.WriteLine("Please enter the specified number to select your choice:");
                Console.WriteLine("Option 1: Inventory Management");
                Console.WriteLine("Option 2: Shop");
                Console.WriteLine("Option 5: Exit");
                input = Console.ReadLine() ?? "0";
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        while(input != "5")
                        {
                            Console.WriteLine("---Inventory Management---");
                            Console.WriteLine("1: Create Product Listing");
                            Console.WriteLine("2: Read Product Listing");
                            Console.WriteLine("3: Update Product Listing");
                            Console.WriteLine("4: Delete Product Listing");
                            Console.WriteLine("5: Return to Main Menu");
                            input = Console.ReadLine() ?? "0";
                            if (int.TryParse(input, out result))
                            {
                                if (result == 1)
                                {
                                    Console.WriteLine("Please enter the name of the product: ");
                                    string pname = Console.ReadLine() ?? "0";
                                    Console.WriteLine("Please enter the description of the product: ");
                                    string pdesc = Console.ReadLine() ?? "0";
                                    Console.WriteLine("Please enter the price of the product (do not use '$'): ");
                                    string p = Console.ReadLine() ?? "0";
                                    decimal pprice = 0;
                                    if (decimal.TryParse(p, out decimal dresult))
                                    {
                                        pprice = dresult;
                                    }

                                    Console.WriteLine("Please enter amount of the product available: ");
                                    p = Console.ReadLine() ?? "0";
                                    int pstock = 0;
                                    if (int.TryParse(p, out result))
                                    {
                                        pstock = result;
                                    }
                                    contactsvc?.AddOrUpdate(
                                    new Product
                                    {
                                        Name = pname,
                                        Description = pdesc,
                                        Price = pprice,
                                        Stock = pstock
                                    });
                                    contactsvc?.Products?.ToList()?.ForEach(Console.WriteLine);
                                    Console.WriteLine("Updated Stock List.");
                                }
                                else if (result == 2)
                                {
                                    Console.WriteLine("Please enter the product ID: ");
                                    string p = Console.ReadLine() ?? "0";
                                    int pid = 0;
                                    if (int.TryParse(p, out result))
                                    {
                                        pid = result;
                                    }
                                    var product = contactsvc?.Get(pid);
                                    if (product == null)
                                    {
                                        Console.WriteLine("Product does not exist.");
                                    }
                                    else
                                    {
                                        Console.WriteLine(product);
                                    }

                                }
                                else if (result == 3)
                                {
                                    Console.WriteLine("Please enter the product ID: ");
                                    string p = Console.ReadLine() ?? "0";
                                    int pid = 0;
                                    if (int.TryParse(p, out result))
                                    {
                                        pid = result;
                                    }
                                    var product = contactsvc?.Get(pid);
                                    if (product == null)
                                    {
                                        Console.WriteLine("Product does not exist.");
                                    }
                                    else
                                    {
                                        Console.WriteLine(product);
                                        Console.WriteLine("Please enter the updated name of the product: ");
                                        product.Name = Console.ReadLine() ?? "0";
                                        Console.WriteLine("Please enter the updated description of the product: ");
                                        product.Description = Console.ReadLine() ?? "0";
                                        Console.WriteLine("Please enter the updated price of the product: ");
                                        p = Console.ReadLine() ?? "0";
                                        if (decimal.TryParse(p, out decimal dresult))
                                        {
                                            product.Price = dresult;
                                        }
                                        Console.WriteLine("Please enter amount of the product available: ");
                                        p = Console.ReadLine() ?? "0";
                                        if (int.TryParse(p, out result))
                                        {
                                            product.Stock = result;
                                        }
                                        contactsvc?.AddOrUpdate(product);
                                    }


                                }
                                else if (result == 4)
                                {
                                    Console.WriteLine("Please enter the product ID: ");
                                    string p = Console.ReadLine() ?? "0";
                                    int pid = 0;
                                    if (int.TryParse(p, out result))
                                    {
                                        pid = result;
                                    }
                                    var product = contactsvc?.Get(pid);
                                    if (product == null)
                                    {
                                        Console.WriteLine("Product does not exist.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("About to delete: ");
                                        Console.WriteLine(product);
                                        Console.WriteLine("Are you sure? Y/N");
                                        string i = Console.ReadLine() ?? "0";
                                        if (i == "Y" || i == "y")
                                        {
                                            contactsvc?.Delete(pid);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product not deleted.");
                                        }

                                    }
                                }
                                else if (result == 5)
                                {
                                    input = "5";
                                }

                            }
                        }
                        input = "0";
                        result = 0;
                    }
                    
                    else if (result == 2)
                    {
                        while (input != "5")
                        {
                            Console.WriteLine("---SHOP---");
                            contactsvc?.Products?.ToList()?.ForEach(Console.WriteLine);
                            Console.WriteLine("Would you like to...");
                            Console.WriteLine("1. Add item to cart");
                            Console.WriteLine("2. Remove item from cart");
                            Console.WriteLine("3. Checkout");
                            Console.WriteLine("5: Return to Main Menu");
                            input = Console.ReadLine() ?? "0";
                            if (int.TryParse(input, out result))
                            {
                                if (result == 1)
                                {
                                    Console.WriteLine("Please enter the ID of the product you'd like to add: ");
                                    string p = Console.ReadLine() ?? "0";
                                    int pid = 0;
                                    if (int.TryParse(p, out result))
                                    {
                                        pid = result;
                                    }
                                    var product = contactsvc?.Get(pid);
                                    if (product == null)
                                    {
                                        Console.WriteLine("Product does not exist.");
                                    }
                                    else
                                    {
                                        cart.Add(product);
                                        product.Stock -= 1;
                                        contactsvc?.AddOrUpdate(product);
                                    }

                                }
                                else if (result == 2)
                                {
                                    Console.WriteLine("Please enter the ID of the product you'd like to remove: ");
                                    string p = Console.ReadLine() ?? "0";
                                    int pid = 0;
                                    if (int.TryParse(p, out result))
                                    {
                                        pid = result;
                                    }
                                    var product = contactsvc?.Get(pid);
                                    if (product == null)
                                    {
                                        Console.WriteLine("Product does not exist.");
                                    }
                                    else
                                    {
                                        cart.Remove(product);
                                        product.Stock += 1;
                                        contactsvc?.AddOrUpdate(product);
                                    }
                                }
                                else if (result == 3)
                                {
                                    Console.WriteLine("---CHECKOUT---");
                                    decimal subtotal = 0;
                                    foreach (var product in cart)
                                    {
                                        Console.WriteLine(product.ToString());
                                        subtotal += product.Price;
                                    }
                                    Console.WriteLine($"SUBTOTAL         ${subtotal}");
                                    decimal taxes = subtotal * 0.07m;
                                    Console.WriteLine($"TAXES         ${taxes}");
                                    decimal total = taxes + subtotal;
                                    Console.WriteLine($"TOTAL         ${total}");
                                    Console.WriteLine("Proceed? Y/N");
                                    string i = Console.ReadLine() ?? "0";
                                    if (i == "Y" || i == "y")
                                    {
                                        Console.WriteLine("Purchase Succesful");
                                        cart.Clear();
                                    }
                                    else
                                    {
                                        foreach (var product in cart)
                                        {
                                            product.Stock += 1;
                                        }
                                        cart.Clear();
                                        Console.WriteLine("Products not purchased. Items returned to inventory.");
                                    }

                                }
                                else if (result == 5)
                                {
                                    input = "5";
                                }
                            }
                        }
                        input = "0";
                        result = 0;
                    }
                    input = "0";
                    if (result == 5)
                    {
                        input = "5";
                        Console.WriteLine("Goodbye!");
                    }
                }
            }
            
        }
    }
}