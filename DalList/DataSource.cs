using DO;
namespace DAL;

internal class DataSource
{
    static DataSource()
    {
        s_Intialize();
    }

    private static readonly Random s_rand = new();

    internal static class Config
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumbe = s_startOrderNumber;
        internal static int NextOrderNumb { get => s_nextOrderNumbe++; }
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumbe = s_startOrderNumber;
        internal static int NextOrderItemNumb { get => s_nextOrderNumbe++; }
    }
    internal static List<Product> Products { get; } = new List<Product>();
    internal static List<Order> Orders { get; } = new List<Order>();
    internal static List<OrderItem> OrderItems { get; } = new List<OrderItem>();
    private static void s_Intialize()
    {
        createAndInitProducts();// fill the list with products
        createAndInitOrders();// fill the list with orders
        createAndInitOrderItems();// fill the list with orderItems
    }

    private static void createAndInitProducts()
    {
        string [] productsName = { "a", "b", "c", "d", "e", "f", "g" };
        for (int i = 0; i < 10; i++)
        {
            Products.Add(
                new Product
                {
                    ID = i,
                    Name = productsName[i],
                    Price = s_rand.Next(25000),
                    Category = (Category)s_rand.Next(4),
                    InStock = s_rand.Next(50)
                });
        }
    }

    private static void createAndInitOrders()
    {

    }

    private static void createAndInitOrderItems()
    {

    }
}