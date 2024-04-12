using System;
using System.Collections.Generic;

namespace PROG6221
{
    //creating class for storing the ingredients
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double OriginalQuantity { get; }

        public Ingredient(string name, double quantity, string unit)
        {
            //data will be stored
            Name = name;
            Quantity = quantity;
            OriginalQuantity = quantity;
            Unit = unit;
        }

        public Ingredient ScaleIngredient(double factor)
        {
            //creating a method that will scale the ingredients
            return new Ingredient(Name, Quantity * factor, Unit);
        }

        public void ResetQuantity()
        {
            //method for reseting the quantity to original value
            Quantity = OriginalQuantity;
        }

        public override string ToString()
        {
            return $"{Quantity} {Unit} of {Name}";
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Recipe(string name)
        {
            //storing the ingredients in a new array list
            Name = name;
            Ingredients = new List<Ingredient>();
        }

        public void AddIngredient(string name, double quantity, string unit)
        {
            Ingredients.Add(new Ingredient(name, quantity, unit));
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                //calling the quantity method to scale the ingredients
                ingredient.Quantity *= factor;
            }
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                //calling the reset method 
                ingredient.ResetQuantity();
            }
        }

        public override string ToString()
        {
            string recipeString = $"Recipe: {Name}\nIngredients:\n";

            foreach (Ingredient ingredient in Ingredients)
            {
                recipeString += $"{ingredient}\n";
            }

            return recipeString;
        }
    }

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>(); // Store recipes in a list

        static void Main(string[] args)
        {
            
            bool running = true;

            while (running)
            {
                //creating menu to pop up for user to interact with the program
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. Scale a recipe");
                Console.WriteLine("3. Reset quantities of a recipe");
                Console.WriteLine("4. Clear all recipes data");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                //user inputed will be inputed in the system to access the switch case

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }

                switch (choice)
                {
                    case 1:
                        AddNewRecipe();
                        break;
                    case 2:
                        ScaleRecipe();
                        break;
                    case 3:
                        ResetQuantities();
                        break;
                    case 4:
                        ClearAllRecipesData();
                        break;
                    case 5:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please choose a number between 1 and 5.");
                        break;
                }
            }
        }

        static void AddNewRecipe()
        {
            Console.WriteLine("\nEnter the name of the recipe:");
            string recipeName = Console.ReadLine();

            //creating a new recipe 
            Recipe myRecipe = new Recipe(recipeName);

            //callign the add method 
            bool addingIngredients = true;


            while (addingIngredients)
            {
                Console.WriteLine("Enter the name of the ingredient (or type 'done' to finish adding ingredients):");
                string ingredientName = Console.ReadLine();

                if (ingredientName.ToLower() == "done")
                {
                    addingIngredients = false;
                    break;
                }

                //calling method for adding quantity
                double quantity;
                Console.WriteLine("Enter the quantity:");
                while (!double.TryParse(Console.ReadLine(), out quantity))
                {
                    Console.WriteLine("Invalid input! Please enter a valid quantity:");
                }

                Console.WriteLine("Enter the unit of measurement:");
                string unit = Console.ReadLine();

                myRecipe.AddIngredient(ingredientName, quantity, unit);
            }

            recipes.Add(myRecipe);
            Console.WriteLine("\nRecipe added successfully!");
        }

        //scailing recipes
        static void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available to scale.");
                return;
            }

            Console.WriteLine("\nAvailable recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.WriteLine("\nEnter the number of the recipe you want to scale:");
            int recipeNumber;
            while (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid input! Please enter a valid recipe number:");
            }

            Recipe selectedRecipe = recipes[recipeNumber - 1];

            Console.WriteLine("\nEnter the scale factor:");
            double scaleFactor;
            while (!double.TryParse(Console.ReadLine(), out scaleFactor))
            {
                Console.WriteLine("Invalid input! Please enter a valid scale factor:");
            }

            selectedRecipe.ScaleRecipe(scaleFactor);
            Console.WriteLine($"\nRecipe '{selectedRecipe.Name}' scaled successfully!");
            Console.WriteLine(selectedRecipe);
        }

        //calling the reset method
        static void ResetQuantities()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available to reset quantities.");
                return;
            }

            Console.WriteLine("\nAvailable recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.WriteLine("\nEnter the number of the recipe you want to reset quantities for:");
            int recipeNumber;
            while (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid input! Please enter a valid recipe number:");
            }

            Recipe selectedRecipe = recipes[recipeNumber - 1];
            selectedRecipe.ResetQuantities();
            Console.WriteLine($"\nQuantities of recipe '{selectedRecipe.Name}' reset to original values!");
            Console.WriteLine(selectedRecipe);
        }

        //calling the clear recipe data method

        static void ClearAllRecipesData()
        {
            recipes.Clear();
            Console.WriteLine("\nAll recipes data cleared successfully!");
        }
    }
}
