
using DO;
namespace DAL;

internal class DataSource
{
    static DataSource()
    {
        s_Intialize();
    }
   
    private static readonly Random NAME = new();
    internal static List<Product> Products { get; } = new List<Product>();
    internal static List<Order> Orders { get; } = new List<Order>();
    internal static List<OrderItem> OrderItems { get; } = new List<OrderItem>();
    private static void s_Intialize()
    {

    }
}