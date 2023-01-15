
using DO;
namespace Dal;

internal static class DataSource
{ 

    
    // Inner class for a runner variable
   // internal static class Config
    
        // A variable runs to class order 
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        // A variable runs to class orderItems
        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }
    /*
        internal static int numOfP = 0;
        internal static int numOfO = 0;
        internal static int numOfOI = 0;
    */
    


    //Class constructor
    static DataSource()
    {
        s_Intialize();
    }

    //A variable for drawing a random number
    private static readonly Random s_rand = new();
    //Defining a list of products 
    
    internal static List<Product?> _products = new List<Product?>();
    //Defining a list of ordersts 

    internal static List<Order?> _orders = new List<Order?>();
    //Defining a list of orderitems 
   
    internal static List<OrderItem?> _orderItems = new List<OrderItem?>();

    //Defining a list of products 

    internal static List<User?> _users = new List<User?>();
    //A function that initializes the three arrays by calling the appropriate functions
    private static void s_Intialize()
    {
        createAndInitProducts();// fill the list with products
        createAndInitOrders();// fill the list with orders
        createAndInitOrderItems();// fill the list with orderItems
        createAndInitUsers();// fill the list with users
    }

    internal static List<Product> Produc { get; }  = new List<Product> { new Product() };
    #region FILL PRODUCTS
    //An array for the names of products in the store by categories
    private static string[] table = new string[5] /*Tables*/ { "Rimani Table", "Mai Table", "Jambo Table", "Troian Table", "Tai Table" };
    private static string[] chair = new string[5] /*Chairs*/ { "Loas Chair", "Tiran Chair", "Mai Chair", "Karamel Chair", "Mango Chair" };
    private static string[] closet = new string[5] /*Closets*/{ "Madrid Closet", "Brazil Closet", "Nikol Closet", "Pariz Closet", "Miami Closet" };
    private static string[] sofa = new string[5] /*Sofas*/ { "Tom Sofa", "Bar Sofa", "Ben Sofa", "Guy Sofa", "Gad Sofa" };
    private static string[] bed = new string[5] /*Beds*/{ "Diamond Bed","Gold Bed","Silver Bed", "Cristal Bed", "King Bed"};
    //Arrays for the price range by categories
    private static int[] priceFrom = new int[5] { 3000, 300, 2000, 4500, 4000 };
    private static int[] priceTo = new int[5] { 12000, 2000, 9000, 20000, 18000 };
    //Arrays to stock items by category
    private static int[] inStock = new int[5] { 30, 200, 60, 40, 70 };

    //A function that fills in the first 10 items in products array
    private static void createAndInitProducts()
    {
        
        for (int i = 0; i < 5; i++)
        {
            int id = 100000 + i;
            Product newProduct =(new Product()
            {
                ID = id,
                Name = table[i],
                Category = Category.TABLE,
                InStock = s_rand.Next(1, 30),
                Price = s_rand.Next(priceFrom[0], priceTo[0]),
                picture = @"\Pics\IMG." + id + ".jpg"
            });
            if (i == 4)
                newProduct.InStock = 0;
            _products.Add(newProduct);  
        }
        for (int i = 0; i < 5; i++)
        {
            int id = 100005 + i;
            Product newProduct =(new Product()
            {
                ID = id,
                Name = chair[i],
                Category = Category.CHAIR,
                InStock = s_rand.Next(1, 200),
                Price = s_rand.Next(priceFrom[1], priceTo[1]),
                picture = @"\Pics\IMG." + id + ".jpg"
            });
            if (i == 4)
                newProduct.InStock = 0;
            _products.Add(newProduct);
        }
        for (int i = 0; i < 5; i++)
        {
            int id = 100010 + i;
            Product newProduct =(new Product()
            {
                ID = id,
                Name = closet[i],
                Category = Category.CLOSET,
                InStock = s_rand.Next(1, 60),
                Price = s_rand.Next(priceFrom[2], priceTo[2]),
                picture = @"\Pics\IMG." + id + ".jpg"
            });
            if (i == 4)
                newProduct.InStock = 0;
            _products.Add(newProduct);
        }
        for (int i = 0; i < 5; i++)
        {
            int id = 100015 + i;
            Product newProduct =(new Product()
            {
                ID = id,
                Name = sofa[i],
                Category = Category.SOFA,
                InStock = s_rand.Next(1, 40),
                Price = s_rand.Next(priceFrom[3], priceTo[3]),
                picture = @"\Pics\IMG." + id + ".jpg"
            });
            if (i == 4)
                newProduct.InStock = 0;
            _products.Add(newProduct);
        }
        for (int i = 0; i < 5; i++)
        {
            int id = 100020 + i;
            Product newProduct =(new Product()
            {
                ID = id,
                Name = bed[i],
                Category = Category.BED,
                InStock = s_rand.Next(1, 70),
                Price = s_rand.Next(priceFrom[4], priceTo[4]),
                picture = @"\Pics\IMG." + id + ".jpg"
            });
            if (i == 4)
                newProduct.InStock = 0;
            _products.Add(newProduct);
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
            //Grill the number of days that have passed since the order date
            int days = s_rand.Next(100,600);
            Order newOrder = new Order
            {
                ID = NextOrderNumber,//Order number according to the serial number
                CustomerName = firstName + " " + lastName,
                CustomerAdress = customerAdress[s_rand.Next(9)],//Draw an address from the array
                CustomerEmail = firstName + lastName + "@gmail.com",//Adding an email extension to the customer's name
                OrderDate = DateTime.Now - new TimeSpan(days, 0, 0, 0),
                ShipDate = null,
                DeliveryDate = null,
            };

            if (i < 0.8*20)//Only 80 percent of the orders were shipped
            {
                //The time for sending an order is between a month and 3 months (30-90 days)
                days = s_rand.Next(30,90);
                TimeSpan shipTime = new TimeSpan(days, 0, 0, 0);
                //Adding the number of days until the shipment leaves to the order date
                newOrder.ShipDate = newOrder.OrderDate + shipTime; 
            }
            if (i < 0.8 *0.6* 20)//Only 60 percent of the orders that went out for delivery were delivered
            {
                //Delivery arrival time is up to a week from the date of ship (1-7 days)
                days = s_rand.Next(1, 7);
                TimeSpan deliverTime = new TimeSpan(days, 0, 0, 0);
                //Adding the number of days until the shipment was delivered to the ship date
                newOrder.DeliveryDate = newOrder.ShipDate + deliverTime;
            }
            _orders.Add(newOrder);
        }
    }
    #endregion

    #region FILL ORDERITEMS

    private static int[] Amount = new int[5] { 15, 100, 30, 20, 35 };


    ////A function that fills in the first 40 items in orderitems array
    private static void createAndInitOrderItems()
    {
        int orderNum = 0;//Position in the order array
        int i = 0;//Position in the orderItem array
        while ( orderNum < 20)
        {
            int numOfProducts = s_rand.Next(1, 4);//Lottery for the amount of items in the order (1-4)
            for (int j = 0; j < numOfProducts; j++)
            {
                //Selecting an item randomly from the array of products
                int x = s_rand.Next(25);
                Product? p = _products[x];
                while (p?.InStock == 0)//Lottery product that is in stock
                {
                    x = s_rand.Next(9);
                    p = _products[x];
                }
                //Lottery of the amount of the item according to the range in the array
                int amount = 100000;
                while (p?.InStock - amount < 0)
                {
                    switch (p?.Category)
                    {
                        case Category.TABLE:
                            amount = s_rand.Next(1, 3);
                            break;
                        case Category.CHAIR:
                            amount = s_rand.Next(1, 20);
                            break;
                        case Category.CLOSET:
                            amount = s_rand.Next(1, 5);
                            break;
                        case Category.SOFA:
                            amount = s_rand.Next(1, 3);
                            break;
                        case Category.BED:
                            amount = s_rand.Next(1, 4);
                            break;
                        default:
                            break;

                    }
                }
                OrderItem newOrderItem = new OrderItem
                {
                    ID = NextOrderItemNumber,//OrderItem number according to the serial number
                    OrderID = (int)_orders[orderNum]?.ID! ,//Order number according to the current order
                    ProductID =(int) p?.ID!,//Product number according to the item we selected
                    Amount = amount,
                    Price =(double) p?.Price!
                };
               _orderItems.Add(newOrderItem);
                i++;
             }
            orderNum++; //Progress to the next order in the array
        }
    }
    #endregion

    private static void createAndInitUsers()
    {
        for (int i = 0; i < _orders.Count(); i++)
        {

            string userName;

            _users.Add(new User
            {
                Name = _orders[i]?.CustomerName,
                Address = _orders[i]?.CustomerAdress,
                Email = _orders[i]?.CustomerEmail,
                userName = _orders[i]?.CustomerName + i,
                password = _orders[i]?.CustomerName + i,
                status = userStatus.CUSTOMER,

            });
        }
        _users.Add(new User
        {
            userName = "tamar",
            password ="1234",
            status = userStatus.MANAGER
        });
        
        _users.Add(new User
        {
            userName = "linoy",
            password = "4321",
            status = userStatus.MANAGER
        });

        


    }
}