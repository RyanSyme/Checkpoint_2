

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Welcome to the Grocery pricing app, Please :-");
Console.WriteLine("Press 'F' to add a food");
Console.WriteLine("Press 'S' to search for a food");
Console.WriteLine("or press 'Q' to Quit");

var foodList = new FoodList();
var userInput = Console.ReadLine().ToLower().Trim();

while(true)
{
    switch(userInput)
    {
        case "f":
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter a food category");
            Console.ForegroundColor = ConsoleColor.White;
            string category = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter the name of the food item:");
            Console.ForegroundColor = ConsoleColor.White;
            string name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter the price of the food item:");
            bool isDouble = false;
            string userPrice = "";
            double price; 
            while(!isDouble)
            { 
                Console.ForegroundColor = ConsoleColor.White;
                userPrice = Console.ReadLine(); 
                if(double.TryParse(userPrice, out price))
                { 
                    isDouble = true; 
                } 
                else
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please use a number for this entry.");
                } 
            } 
            price = double.Parse(userPrice);
            var newFood = new Food(category, name, price);
            foodList.AddFood(newFood);

            break;
        case "d":
            foodList.DisplayAllFoods();
            break;
        case "s":
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Search food by name: ");
            Console.ForegroundColor = ConsoleColor.White;
            string searchName = Console.ReadLine();
            foodList.searchFoodName(searchName);
            break;
        case "q":
            return;
            break;
        default :
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Select one of the available options");
            Console.WriteLine("Please press 'F' to add a food, 'S' to search, 'Q' to Quit");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine().ToLower().Trim();
            break;
    }
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Operation completed successfully");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Please press 'F' to add a food, 'D' to display the list, 'S' to search, 'Q' to Quit");
    Console.ForegroundColor = ConsoleColor.White;
    userInput = Console.ReadLine().ToLower().Trim();
}


public class Food
    {
        public Food(string category, string name, double price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

public class FoodList
    {
        public List<Food> foodList {get; set;} = new List<Food>();
        

        public void displayFoodInfo(Food food)
        {
            Console.WriteLine($"{food.Category.PadRight(15)} {food.Name.PadRight(15)} {food.Price}");
        }

        public void AddFood(Food food)
        {
            foodList.Add(food);
        }

        public void DisplayAllFoods()
        {
            List<Food> foodByPrice = foodList.OrderBy(item => item.Price).ToList();
            Console.WriteLine("-----------------------------------");
            foreach (var food in foodByPrice)
            {
                displayFoodInfo(food);
            }
            double sum = foodByPrice.Sum(food => food.Price);
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Total".PadRight(32) + sum.ToString());
        }
        public void DisplayFood(string name)
        {
            var foodName = foodList.FirstOrDefault(item => item.Name == name);
            if(foodName == null)
            {
                Console.WriteLine("Food item not found");
            }
            else
            {
                displayFoodInfo(foodName);
            }
        }

        public void searchFoodName(string userSearch)
        {
            var matchingFoods = foodList.Where(item => item.Name.Contains(userSearch)).ToList();
            if(matchingFoods == null || !matchingFoods.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No foods match your search");
            }
            else{
                foreach (var food in matchingFoods)
                {
                    displayFoodInfo(food);
                }
            }
        }
    }