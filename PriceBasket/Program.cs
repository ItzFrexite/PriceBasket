using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PriceBasket
{
    internal class Program
    {

        public class Product
        {
            // Variables for the product name and price using get and set
            public string product { get; set; }
            public double price { get; set; }

            // Parameterless constructor for a list created in the main method
            public Product()
            {
            }

        }

        static void Main(string[] args)
        {
            // These are variables for the subtotal of each item, this is used later on in the code.
            double subsoup = 0;
            double subbread = 0;
            double submilk = 0;
            double subapple = 0;

            // I have created a list to hold the current items for sale and the price
            List<Product> productList = new List<Product>
            {
                new Product{ product = "Soup", price = 0.65 },
                new Product{ product = "Bread", price = 0.8 },
                new Product{ product = "Milk", price = 1.3 },
                new Product{ product = "Apples", price = 1 }
            };

            // Displays the current items for sale 
            Console.WriteLine("Welcome to the store. The current items for sale are: ");

            // I have used a for loop to go through the product list and then display the price based on the current list index.
            for (int i = 0; i < productList.Count; i++)
            {
                Product product = productList[i];
                if (i == 0)
                {
                    Console.WriteLine("Can of Soup - £" + product.price.ToString("0.00"));
                }
                if (i == 1)
                {
                    Console.WriteLine("Loaf of Bread - £" + product.price.ToString("0.00"));
                }
                if (i == 2)
                {
                    Console.WriteLine("Carton of Milk - £" + product.price.ToString("0.00"));
                }
                if (i == 3)
                {
                    Console.WriteLine("Bag of Apples - £" + product.price.ToString("0.00"));
                }
            }


            // A new list to store user input
            List<string> productBasket = new List<string>{};

            // Variables for the amount of each item the user has.
            int soup = 0;
            int bread = 0;
            int milk = 0;
            int apple = 0;

            void UserInput()
            {
                // Re-declaring the amount of each item so that invalid attempts with correct items included wont be included.
                soup = 0;
                bread = 0;
                milk = 0;
                apple = 0;

                // This part allows the user to enter their input in the format apple, bread and then removes the space so it can be added to the list.
                Console.WriteLine("\nPlease enter the items you wish to buy (eg. Soup Bread Milk Apples)");
                string input = Console.ReadLine();
                // This part splits the string where "," is present and adds it to the list.
                productBasket = input.Split(' ').ToList();

                // This loop is very similar to the one above and loops for the same amount of times as the amount of items the user has entered.
                for (int i = 0; i < productBasket.Count; i++)
                {
                    // This variable is just used to say if the number is equal to the amount of products in productList then run UserInput() again. This is a method I have used to check if the user has entered an invalid item.
                    int no = 0;
                    // A nested for loop which will run for the same amount of times as the amount of products in productList.
                    for (int j = 0; j < productList.Count; j++)
                    {
                        Product product = productList[j];
                        //Console.WriteLine(product.product);
                        string prodBas = productBasket[i];
                        string prodList = product.product;

                        // This part uses the Regular Expressions library and checks if the current variable of prodList, based of the index of the list, matches the value of prodBas, also based off the index of the corresponding list.
                        // the @"\b" either side of prodBas set word boundaries to make the word match exactly, not only part of the word, eg. "app" for apples and also makes it none case sensitive.
                        if (Regex.Match(prodList, @"\b" + prodBas + @"\b", RegexOptions.IgnoreCase).Success)
                        {

                            //Console.WriteLine("Success"); (USED FOR TESTING)
                            // These IF statements are checking to see if j is euqals to the index value of each item, for example 0 is soup and if the index value is 0 then +1 to the amount of soup the user has.
                            if (j == 0)
                            {
                                soup++;
                                subsoup = product.price;
                            }
                            if (j == 1)
                            {
                                bread++;
                                subbread = product.price;
                            }
                            if (j == 2)
                            {
                                milk++;
                                submilk = product.price;
                            }
                            if (j == 3)
                            {
                                apple++;
                                subapple = product.price;
                            }
                            break;
                        }
                        else
                        {
                            //Console.WriteLine("Failed"); (USED FOR TESTING)
                            // This is where the value of no is increased if the current item has failed the check to see if it is valid.
                            no++;
                            // When the amount of checks equals the amount of products available then it will recall UserInput() as the item intered is invalid and doesn't match any of the products available
                            if (no == productList.Count)
                            {
                                UserInput();
                            }
                        }

                    }
                }


                Console.WriteLine("\nYour Item(s) Are:");

                // These IF statments check to see if the user has 1 or more of each item and if so display the correct amount, if the user doesnt have 1 of an item it won't be displayed.
                if (soup > 1)
                {
                    Console.WriteLine(soup + " Cans of Soup");
                }
                else if (soup == 1) { Console.WriteLine(soup + " Can of Soup"); }

                if (bread > 1)
                {
                    Console.WriteLine(bread + " Loaves of Bread");
                }
                else if (bread == 1) { Console.WriteLine(bread + " Load of Bread"); }

                if (milk > 1)
                {
                    Console.WriteLine(milk + " Cartons of Milk");
                }
                else if (milk == 1) { Console.WriteLine(milk + " Carton of Milk"); }

                if (apple >= 1)
                {
                    Console.WriteLine(apple + " Bag of Apples");
                }

                // These variables are created here and used later.
                double disapple = 0;
                double disbread = 0;

                // Here I have created variables for each item to work out the total price of each item the user has. I have done this by multiplying the price of one by the amount the user has.
                double totsoup = subsoup * soup;
                double totbread = subbread * bread;
                double totmilk = submilk * milk;
                double totapple = subapple * apple;

                // This variable works out the subtotal
                double subtotal = totsoup + totbread + totmilk + totapple;
                // These IF statements calculate if the user has the required amount of each item for a discount. For example at least 1 apple or 2 soup and 1 bread.
                // The variables created before are now used for work out the amount of discount that needs to be appled onto the discouned item. With bread being half price I divide the subtotal bread cost by 2 and for apples I divide by 10 to get 10%
                if (soup > 1 && bread >= 1)
                {
                    disbread = totbread / 2;
                }
                if (apple > 0)
                {
                    disapple = totapple / 10;
                }

                // This variable is for the subtotal - discounts
                double discountsubtotal = subtotal - (disapple + disbread);

                // Here I am displaying the users subtotal before discounts
                Console.WriteLine("\nSubtotal: £" + subtotal.ToString("0.00"));

                // These IF statements are used to figure out which discounts the user has if they do have any. If not then "(no offers available)" will be displayed
                if (soup >= 2 && bread >= 1)
                {
                    Console.WriteLine("Bread Half Price: -£" + disbread.ToString("0.00"));
                }
                if (apple >= 1)
                {
                    Console.WriteLine("Apples 10% Off: -£" + disapple.ToString("0.00"));
                }
                if (apple == 0 && soup < 2)
                {
                    Console.WriteLine("(no offers available)");
                }

                // Finally I display the users subtotal with discounts applied.
                Console.WriteLine("Total: £" + discountsubtotal.ToString("0.00") + "\n");

                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();


            }

            // This part of the code runs before most of the code before as it is in a method, the method for the code above is used here.
            UserInput();
        }

    }
}