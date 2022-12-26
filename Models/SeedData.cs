using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace FoodStoreAPI.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if(context.Products.Count() == 0)
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Simple Cabbage Salad",
                        Category = "Salad",
                        Description = "A simple cabbage salad",
                        Calories = 104,
                        Price = 7.50m
                    },
                    new Product
                    {
                        Name = "Green Goddess Salad with Chickpeas",
                        Category = "Salad",
                        Description = "A salad with chickpeas",
                        Calories = 304,
                        Price = 14.50m
                    },
                    new Product
                    {
                        Name = "Spaghetti & Spinach with Sun-Dried Tomato Cream Sauce",
                        Category = "Main Dish",
                        Description = "A sun-dried tomato pasta",
                        Calories = 380,
                        Price = 17.5m
                    },
                    new Product
                    {
                        Name = "One-Pot Garlicky Shrimp & Broccoli",
                        Category = "Main Dish",
                        Description = "Chickpea & Spinach Stew",
                        Calories = 401,
                        Price = 10.65m
                    },
                    new Product
                    {
                        Name = "Fruit & Yogurt Smoothie",
                        Category = "Drink",
                        Description = "A simple fruit smoothie",
                        Calories = 279,
                        Price = 5.5m
                    },
                    new Product
                    {
                        Name = "Spinach-Avocado Smoothie",
                        Category = "Drink",
                        Description = "A healthy green smoothie",
                        Calories = 357,
                        Price = 6.5m
                    },
                    new Product
                    {
                        Name = "Sheet-Pan Salmon with Sweet Potatoes & Broccoli",
                        Category = "Baked Dish",
                        Description = "Sheet-Pan Salmon with Sweet Potatoes & Broccoli",
                        Calories = 504,
                        Price = 20.5m
                    },
                    new Product
                    {
                        Name = "Baked Banana-Nut Oatmeal Cups",
                        Category = "Snack",
                        Description = "Muffins meet oatmeal in these moist and tasty grab-and-go oatmeal cups",
                        Calories = 176,
                        Price = 2.5m
                    },
                    new Product
                    {
                        Name = "Sriracha-Buffalo Cauliflower Bites",
                        Category = "Snack",
                        Description = "A great vegetarian alternative to Buffalo wings",
                        Calories = 99,
                        Price = 5.0m
                    }
                    );
            }
            context.SaveChanges();
        }
    }
}
