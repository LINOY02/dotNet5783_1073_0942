using System;
using DO;
namespace DAL;

internal class DataSource
{
    
    //Class constructor
    static DataSource()
    {
        s_Intialize();
    }

    // Inner class for a runner variable
    internal static class Config
    {
        // A variable runs to class order 
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        // A variable runs to class orderItems
        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderNumber++; }
    }
    //A variable for drawing a random number
    private static readonly Random s_rand = new();//
    //Defining an array of products and a variable to save the amount of items in the array
    internal static int numOfProducts = 0;
    internal static Product[] Products { get; } = new Product[50];
    //Defining an array of ordersts and a variable to save the amount of items in the array
    internal static int numOfOrders = 0;
    internal static Order[] Orders { get; } = new Order[100];
    //Defining an array of orderitems and a variable to save the amount of items in the array
    internal static int numOfOrderItems = 0;
    internal static OrderItem[] OrderItems { get; } = new OrderItem[200];

    //A function that initializes the three arrays by calling the appropriate functions
    private static void s_Intialize()
    {
        createAndInitProducts();// fill the list with products
        createAndInitOrders();// fill the list with orders
        createAndInitOrderItems();// fill the list with orderItems
    }


    #region FILL PRODUCTS
    //An array for the names of products in the store by categories
    private static string[,] nameOfProducts = new string[5, 3] {/*Tables*/ { "Rimani Table", "Mai Table", "Jambo Table" },
                                                                /*Chairs*/ {"Loas Chair","Tiran Chair","Mai Chair" } ,
                                                                /*Closets*/{"s","s","s" },
                                                                /*Sofas*/ { "a","a","a"},
                                                                /*Beds*/{ "x","a","s"} };
    //Arrays for the price range by categories
    private static int[] priceFrom = new int[5] { 3000, 300, 2000, 4500, 4000 };
    private static int[] priceTo = new int[5] { 12000, 2000, 9000, 20000, 18000 };
    //Arrays to stock items by category
    private static int[] inStock = new int[5] { 15, 100, 30, 20, 35 };
    
    //A function that fills in the first 10 items in products array
    private static void createAndInitProducts()
    {
        for (int i = 0; i < 10; i++)
        {
            int category = s_rand.Next(4);//Category selection by drawingy
            int name = s_rand.Next(2);//Choosing a product name by drawing
            Products[i] =
                new Product
                {
                    ID = i + 100000,
                    Name = nameOfProducts[category, name],//Choosing the name from the matrix according to the numbers drawn
                    Price = s_rand.Next(priceFrom[category], priceTo[category]),//Random price selection from the range of the category
                    Category = (Category)category,
                    InStock = s_rand.Next(inStock[category])//Selecting a quantity in stock randomly from the range of the category
                };
            numOfProducts++;//Increasing the number of items in the array by 1
        }
    }
    #endregion

    #region FILL ORDERS
    //Arrays of first name and last name
    private static string [] firstNames = new string[8] {"Tamar","Linoy","Shira","Avi","Hadar","Rachel" ,"Moshe","Meni"};
    private static string [] lastNames = new string[8] {"Gefner","Yaday","Choen","Levi","Israeli","Revach","Biton", "Fridman"};
    //Arrays of addresses
    private static string[] customerAdress = new string[10] { "Ha-Arasim", "Ha-Gefen","Ha-Goren" ,"Ha-Mitzpe" ,"Chazon Ish","Gordon","Rabi Hakiva","Dakar","Ha-Macabim","Har Sinai"};
    //A function that fills in the first 20 items in orders array
    private static void createAndInitOrders()
    {
        for (int i = 0; i < 20; i++)
        {
            //Creating a customer name by drawing a first name + drawing a last name
            string firstName = firstNames[s_rand.Next(7)]; 
            string lastName =  lastNames[s_rand.Next(7)];
            Orders[i] =
                new Order
                {
                    ID = Config.NextOrderNumber,//Order number according to the serial number
                    CustomerName = firstName + " " + lastName,
                    CustomerAdress = customerAdress[s_rand.Next(9)],//Draw an address from the array
                    CustomerEmail = firstName + lastName + "@gmail.com",//Adding an email extension to the customer's name



                };
            numOfOrders++;//Increasing the number of items in the array by 1
        }
    }
    #endregion

    #region FILL ORDERITEMS
    //Array for quantity ordered by category
    private static int [] amounts = new int[5] { 3, 25, 5, 3, 3 };

    ////A function that fills in the first 40 items in orderitems array
    private static void createAndInitOrderItems()
    {
        int orderNum = 1;//Position in the order array
        for (int i = 0; i < 40; i++)
        {
            int numOfProducts = s_rand.Next(1, 4);//Lottery for the amount of items in the order (1-4)
            for (int j = 0; j < numOfProducts; j++)
            {
                //Selecting an item randomly from the array of products
                Product p = Products[s_rand.Next(numOfProducts)];
                //Lottery of the amount of the item according to the range in the array
                int amount = s_rand.Next(1, amounts[(int)p.Category]);
                OrderItems[i] =
                    new OrderItem
                    {
                        ID = Config.NextOrderItemNumber,//OrderItem number according to the serial number
                        OrderID = Orders[i].ID,//Order number according to the current order
                        ProductID = p.ID,//Product number according to the item we selected
                        Amount = amount,
                        Price = p.Price*amount,//final price
                    };
                numOfOrderItems++;//Increasing the number of items in the array by 1
            }
            orderNum++; //Progress to the next order in the array
        }
    }
    #endregion
}