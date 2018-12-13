using Microsoft.EntityFrameworkCore;
using Project0.DataAccess;
using Lib = Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project0.App
{
    class Program
    {
        static DbContextOptions<Project0Context> options;

        static void Main(string[] args)
        {
            var connectionString = SecretConfiguration.ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(connectionString);
            options = optionsBuilder.Options;

            var repo = new DataRepository(new Project0Context(options));

            Lib.User currentUser;

            Console.WriteLine("Welcome to Pizza Manager!");
            //Console.WriteLine("Press L to (L)oad data");
            //Console.WriteLine("Press N for (N)ew data");
            string input; //= Console.ReadLine().ToUpper();
            //if (input.StartsWith('N'))
            //{
            //Console.WriteLine("You selected New Data");
            PrintMainMenu();
            input = Console.ReadLine().ToUpper();
            while (!input.StartsWith('Q'))
            {
                List<Lib.Location> posLoc = repo.GetLocations();
                if (input.StartsWith('C') || input.StartsWith('U'))
                {
                    
                    Console.WriteLine("Enter First Name");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Enter Last Name");
                    string lastName = Console.ReadLine();
                    if (input.StartsWith('C'))
                    {
                        Console.WriteLine("Please enter default location");
                        foreach (var l in posLoc)
                        {
                            Console.WriteLine(l.Name);
                        }
                        input = Console.ReadLine();
                        Lib.Location defaultLocation = posLoc.Where(a => a.Name.Equals(input)).First();
                        currentUser = new Lib.User() { FirstName = firstName, LastName = lastName, DefaultLocation = defaultLocation  };
                        //adding to database
                        repo.AddUser(currentUser);
                        //Users.Add(currentUser);
                    }
                    else
                    {
                        currentUser = repo.GetUser(firstName, lastName);
                    }
                    
                    Console.WriteLine($"Welcome {currentUser.FirstName} {currentUser.LastName}");
                    Console.WriteLine("Press A to (A)dd Order");
                    Console.WriteLine("Press F to order (F)avorite");
                    Console.WriteLine("Press H to view Order History");
                    Console.WriteLine("Press B to go (B)ack to Main Menu");
                    input = Console.ReadLine().ToUpper();
                    while (!input.StartsWith('B'))
                    {
                        if (input.StartsWith('A'))
                        {
                            //List<Lib.Location> posLoc = repo.GetLocations();
                            Console.WriteLine("Possible Locations: ");
                            foreach (var l in posLoc)
                            {
                                Console.Write(l.Name);
                                if(currentUser.DefaultLocation.LocationId == l.LocationId)
                                {
                                    Console.Write(" (default) ");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine("Enter the name of the restaurant you would like to order from");

                            input = Console.ReadLine();
                            Lib.Location chosenLocation = posLoc.Where(a => a.Name.Equals(input)).First();
                            //List < KeyValuePair<Pizza, int> > menu = chosenLocation.Inventory.ToList();
                            List<Lib.Pizza> menu = repo.GetPizzas();
                            Dictionary<Lib.Pizza, int> chosenPizzas = new Dictionary<Lib.Pizza, int>();

                            int numPressed;
                            bool again = true;
                            Console.WriteLine("Choose your pizza(s) to order");
                            for (int i = 0; i < menu.Count; i++)
                            {
                                Console.WriteLine($"Press {i} to choose {menu[i].Name}, Price: {menu[i].Price}");
                            }
                            input = Console.ReadLine();
                            if (!int.TryParse(input, out numPressed))
                            {
                                numPressed = 0;
                            }
                            while (again)
                            {
                                if (chosenPizzas.ContainsKey(menu[numPressed]))
                                {
                                    chosenPizzas[menu[numPressed]] += 1;
                                }
                                else
                                {
                                    chosenPizzas[menu[numPressed]] = 1;
                                }
                                Console.WriteLine("Add another pizza(s) to order or Press Q to finish order");
                                for (int i = 0; i < menu.Count; i++)
                                {
                                    Console.WriteLine($"Press {i} to choose {menu[i].Name}, Price: {menu[i].Price}");
                                }
                                input = Console.ReadLine();
                                again = int.TryParse(input, out numPressed);
                            }
                            //Console.WriteLine($"placing order for User {currentUser.UserId}");
                            //Console.WriteLine($"placing order for Location {chosenLocation.LocationId}");
                            Console.WriteLine("how many hours from now would you like to place the order");
                            input = Console.ReadLine();
                            int numHours = 0;
                            int.TryParse(input, out numHours);
                            Lib.Order ChosenOrder = new Lib.Order(chosenLocation, currentUser, DateTime.Now.AddHours(numHours), chosenPizzas);
                            try
                            {
                                repo.AddOrder(ChosenOrder);
                                Console.WriteLine("Order has been Placed");
                            } catch(Lib.BadOrderException e)
                            {
                                Console.WriteLine($"A problem has occured with your order: {e.Message}");
                            }
                            //Console.WriteLine("number of pepperoni: "+ chosenLocation.Inventory[chosenLocation.Inventory.Keys.Where(a=>a.Name == "Pepperoni").First()]);
                            
                            repo.Save();
                        }
                        else if (input.StartsWith('H'))
                        {
                            List<Lib.Order> orderHistory = repo.GetUserOrderHistory(currentUser);
                            //Console.WriteLine(orderHistory.Count());
                            input = "A";
                            while (!input.StartsWith('Q')) {
                                foreach (var o in orderHistory)
                                {
                                    Console.WriteLine($"{o.User.FirstName},{o.User.LastName} \t {o.Location.Name} \t {o.OrderTime}");
                                    foreach (var p in o.Contents)
                                    {
                                        Console.Write($"{p.Key.Name} - {p.Value}, ");
                                    }
                                    Console.WriteLine($"Total Cost: {o.Price()} \n");
                                }
                                Console.WriteLine("Order History by (E)arliest, (L)atest, (C)heapest, (P)riciest, or Q to (Q)uit");
                                input = Console.ReadLine().ToUpper();
                                if (input.StartsWith('E'))
                                {
                                    orderHistory = Lib.OrderManager.EarliestOrderedHistory(orderHistory);
                                }
                                else if (input.StartsWith('L'))
                                {
                                    orderHistory = Lib.OrderManager.LatestOrderedHistory(orderHistory);
                                }
                                else if (input.StartsWith('C'))
                                {
                                    orderHistory = Lib.OrderManager.CheapestOrderedHistory(orderHistory);
                                }
                                else if (input.StartsWith('P'))
                                {
                                    orderHistory = Lib.OrderManager.ExpensiveOrderedHistory(orderHistory);
                                }
                            }
                        }
                        else if (input.StartsWith('F'))
                        {
                            Lib.Order o = currentUser.SuggestedOrder(repo.GetUserOrderHistory(currentUser));
                            try
                            {
                                repo.AddOrder(o);
                                Console.WriteLine("Order has been Placed");
                            }
                            catch (Lib.BadOrderException e)
                            {
                                Console.WriteLine($"A problem has occured with your order: {e.Message}");
                            }
                        }

                        Console.WriteLine("Press A to (A)dd Order");
                        Console.WriteLine("Press F to order (F)avorite");
                        Console.WriteLine("Press H to view Order History");
                        Console.WriteLine("Press B to go (B)ack to Main Menu");
                        input = Console.ReadLine().ToUpper();
                    }
                }
                else if (input.StartsWith('L') || input.StartsWith('O'))
                {
                    Lib.Location currentLocation;
                    if (input.StartsWith('O'))
                    {
                        Console.WriteLine("Enter the name of your location");
                        input = Console.ReadLine();
                        string locName = input;
                        Console.WriteLine("choose your inventory package plan");
                        Console.WriteLine("press 1 for beginner package: 5 pepperoni, 5 sausage, 5 Bell Pepper, 5 Olives");
                        Console.WriteLine("press 2 for medium package: 10 pepperoni, 10 sausage, 10 Bell Pepper, 10 Olives");
                        Console.WriteLine("press 3 advanced package: 15 pepperoni, 15 sausage, 15 Bell Pepper, 15 Olives");
                        input = Console.ReadLine();
                        int level = int.Parse(input);
                        Dictionary<Lib.Ingredient, int> tempInv = new Dictionary<Lib.Ingredient, int>();
                        tempInv[new Lib.Ingredient("Pepperoni")] = 5 * level;
                        tempInv[new Lib.Ingredient("Sausage")] = 5 * level;
                        tempInv[new Lib.Ingredient("Bell Pepper")] = 5 * level;
                        tempInv[new Lib.Ingredient("Olives")] = 5 * level;
                        currentLocation = new Lib.Location() { Name = locName, Inventory = tempInv };
                        repo.AddLocation(currentLocation);
                    } else
                    {
                        Console.WriteLine("enter the name of the location");
                        input = Console.ReadLine();
                        currentLocation = repo.GetLocation(input);
                    }
                    Console.WriteLine($"Welcome {currentLocation.Name}");
                    Console.WriteLine("Press H to view Order History");
                    Console.WriteLine("Press I to view current Inventory");
                    Console.WriteLine("Press B to go back to Main Menu");
                    input = Console.ReadLine().ToUpper();
                    while(!input.StartsWith('B'))
                    {
                        if(input.StartsWith('I'))
                        {
                            foreach (var pair in currentLocation.Inventory)
                            {
                                Console.WriteLine($"Ingredient: {pair.Key.Name}, Remaining: {pair.Value}");
                            }
                        }
                        else if(input.StartsWith('H'))
                        {
                            //List<Lib.Order> orderHistory = repo.GetLocationOrderHistory(currentLocation);
                            ////Console.WriteLine(orderHistory.Count());
                            //foreach (var o in orderHistory)
                            //{
                            //    Console.WriteLine($"{o.User.FirstName},{o.User.LastName} \t {o.Location.Name}");
                            //    foreach (var p in o.Contents)
                            //    {
                            //        Console.Write($"{p.Key.Name} - {p.Value}, ");
                            //    }
                            //    Console.WriteLine($"Total Cost: {o.Price()} \n");
                            //}

                            List<Lib.Order> orderHistory = repo.GetLocationOrderHistory(currentLocation);
                            //Console.WriteLine(orderHistory.Count());
                            input = "A";
                            while (!input.StartsWith('Q'))
                            {
                                foreach (var o in orderHistory)
                                {
                                    Console.WriteLine($"{o.User.FirstName},{o.User.LastName} \t {o.Location.Name} \t {o.OrderTime}");
                                    foreach (var p in o.Contents)
                                    {
                                        Console.Write($"{p.Key.Name} - {p.Value}, ");
                                    }
                                    Console.WriteLine($"Total Cost: {o.Price()} \n");
                                }
                                Console.WriteLine("Order History by (E)arliest, (L)atest, (C)heapest, (P)riciest, or Q to (Q)uit");
                                input = Console.ReadLine().ToUpper();
                                if (input.StartsWith('E'))
                                {
                                    orderHistory = Lib.OrderManager.EarliestOrderedHistory(orderHistory);
                                }
                                else if (input.StartsWith('L'))
                                {
                                    orderHistory = Lib.OrderManager.LatestOrderedHistory(orderHistory);
                                }
                                else if (input.StartsWith('C'))
                                {
                                    orderHistory = Lib.OrderManager.CheapestOrderedHistory(orderHistory);
                                }
                                else if (input.StartsWith('P'))
                                {
                                    orderHistory = Lib.OrderManager.ExpensiveOrderedHistory(orderHistory);
                                }
                            }



                        }

                        Console.WriteLine("Press H to view Order History");
                        Console.WriteLine("Press I to view current Inventory");
                        Console.WriteLine("Press B to go back to Main Menu");
                        input = Console.ReadLine().ToUpper();
                    }




                }
                PrintMainMenu();
                input = Console.ReadLine().ToUpper();
            }
        }

        public static void PrintMainMenu()
        {
            Console.WriteLine("Press C to (C)reate User Account");
            Console.WriteLine("Press U to Log into (U)ser Account");
            Console.WriteLine("Press O to Add a new L(o)cation");
            Console.WriteLine("Press L to Log into (L)ocation");
            Console.WriteLine("Press Q to (Q)uit");
        }

    }
}
