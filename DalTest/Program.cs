using DAL;
using DO;

namespace DalTest
{
    public class Program
    {
        private static DalOrder order = new DalOrder();
        private static DalOrderItem orderItem = new DalOrderItem();
        private static DalProduct product = new DalProduct();

        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("enter a number between 0 to 3");
            int number;
            int.TryParse(Console.ReadLine(), out number);
            switch (number)
            {
                case 0:
                    Console.WriteLine("out");
                    return;
                case 1:
                    void createOrder (ref Order order1)
                    {
                        int id;
                        Console.WriteLine("enter the id of the order");
                        int.TryParse(Console.ReadLine(), out id);
                        string customerName;
                        Console.WriteLine("enter your name");
                        customerName = Console.ReadLine();
                        string customerEmail;
                        Console.WriteLine("enter your Email");
                        customerEmail = Console.ReadLine();
                        string customerAdress;
                        Console.WriteLine("enter your adress");
                        customerAdress = Console.ReadLine();
                        DateTime orderDate;
                        Console.WriteLine("enter the id of the order");
                        DateTime.TryParse(Console.ReadLine(), out orderDate);
                        DateTime shipDate;
                        Console.WriteLine("enter the id of the order");
                        DateTime.TryParse(Console.ReadLine(), out shipDate);
                        DateTime deliveryDate;
                        Console.WriteLine("enter the id of the order");
                        DateTime.TryParse(Console.ReadLine(), out deliveryDate);
                        order1.ID = id;
                        order1.CustomerName = customerName;
                        order1.CustomerEmail = customerEmail;
                        order1.CustomerAdress = customerAdress;
                        order1.OrderDate = orderDate;
                        order1.ShipDate = shipDate;
                        order1.DeliveryDate = deliveryDate;
                    }
                    void ORDER(DalOrder order)
                    {

                        Console.WriteLine("enter 'a' for add");
                        Console.WriteLine("enter 'b' for show a order");
                        Console.WriteLine("enter 'c' for show the list");
                        Console.WriteLine("enter 'd' for update the order");
                        Console.WriteLine("enter 'e' for delete order");
                        Order order1 = new Order();
                        
                        string ch = Console.ReadLine();
                        while (ch != "0")
                        {
                            switch (ch)
                            {
                                case "a":
                                    createOrder(ref order1);
                                    Console.WriteLine(order.Add(order1));
                                    break;
                                case "b":
                                    Console.WriteLine("Enter the id of the order");
                                    int id;
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(order.GetById(id));
                                    break;
                                case "c":
                                    foreach (var o in order.())
                                        Console.WriteLine(o);
                                    break;
                                case "d":
                                    Console.WriteLine("Enter the id of the order for updating");
                                    int id1;
                                    int.TryParse(Console.ReadLine(), out id1);
                                    Console.WriteLine(order.GetById(id1));
                                    order1.ID = id1;
                                    createOrder(ref order1);
                                    order.Update(order1);
                                    break;
                                case "e":
                                    Console.WriteLine("Enter the id of the order for delete:");
                                    int id2;
                                    int.TryParse(Console.ReadLine(), out id2);
                                    order.Delete(id2);
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    break;
                case 2:
                    void createOrderItem(ref OrderItem orderItem1)
                    {
                        int id;
                        Console.WriteLine("enter the id");
                        int.TryParse(Console.ReadLine(), out id);
                        int price;
                        Console.WriteLine("enter the price of the product");
                        int.TryParse(Console.ReadLine(), out price);
                        int orderId;
                        Console.WriteLine("enter the ID of the order");
                        int.TryParse(Console.ReadLine(), out orderId);
                        int productId;
                        Console.WriteLine("enter the ID of the product");
                        int.TryParse(Console.ReadLine(), out productId);
                        int amount;
                        Console.WriteLine("enter the amount of the product");
                        int.TryParse(Console.ReadLine(), out amount);
                        orderItem1.ID = id;
                        orderItem1.Price = price;
                        orderItem1.OrderID = orderId;
                        orderItem1.ProductID = productId;
                        orderItem1.Amount = amount;
                    }
                    void ORDERITEM(OrderItem orderItem)
                    {

                        Console.WriteLine("enter 'a' for add");
                        Console.WriteLine("enter 'b' for show a order item");
                        Console.WriteLine("enter 'c' for show the list");
                        Console.WriteLine("enter 'd' for update the order item");
                        Console.WriteLine("enter 'e' for delete");
                        OrderItem orderItem1 = new OrderItem();
                        string ch = Console.ReadLine();
                        switch (ch)
                        {
                            case "a":
                                createOrderItem(ref orderItem1);
                                Console.WriteLine(orderItem.Add(orderItem1));
                                break;
                            case "b":
                                Console.WriteLine("Enter the id of the ordr item");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(orderItem.GetById(id));
                                break;
                            case "c":
                                foreach (var OIt in orderItem.RequestAll())
                                    Console.WriteLine(OIt);
                                break;
                            case "d":
                                Console.WriteLine("Enter the id of the order item for updating");
                                int id1;
                                int.TryParse(Console.ReadLine(), out id1);
                                Console.WriteLine(orderItem.GetById(id1));
                                orderItem1.ID = id1;
                                createOrderItem(ref orderItem1);
                                orderItem.Update(orderItem1);
                                break;
                            case "e":
                                Console.WriteLine("Enter the id of the order for delete");
                                int id2;
                                int.TryParse(Console.ReadLine(), out id2);
                                orderItem.Delete(id2);
                            default:
                                break;
                        }
                    }
                    break;
                case 3:
                    void createProduct(ref Product product1)
                    {
                        int id;
                        Console.WriteLine("enter the id of the product");
                        int.TryParse(Console.ReadLine(), out id);
                        int price;
                        Console.WriteLine("enter the price of the product");
                        int.TryParse(Console.ReadLine(), out price);
                        string name;
                        Console.WriteLine("enter the name of the product");
                        name = Console.ReadLine();
                        int inStock;
                        Console.WriteLine("enter the amount of the product");
                        int.TryParse(Console.ReadLine(), out inStock);
                        Category Category;
                        Console.WriteLine("enter the Category of the product");
                        Category.TryParse(Console.ReadLine(), out Category);
                        product1.ID = id;
                        product1.Price = price;
                        product1.Name = name;
                        product1.InStock = inStock;
                        product1.Category = Category;
                    }
                    void PRODUCT(DalProduct product)
                    {
                        Console.WriteLine("enter 'a' for add a product");
                        Console.WriteLine("enter 'b' for show a product");
                        Console.WriteLine("enter 'c' for show the list");
                        Console.WriteLine("enter 'd' for update the product");
                        Console.WriteLine("enter 'e' for delete the product");
                        Product product1 = new Product();
                        
                        string ch = Console.ReadLine();
                        switch (ch)
                        {
                            case "a":
                                createProduct(ref product1);
                                Console.WriteLine(product.Add(product1));
                                break;
                            case "b":
                                Console.WriteLine("Enter the id of the product");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(product.GetById(id));
                                break;
                            case "c":
                                foreach (var p in product.)
                                    Console.WriteLine(p);
                                break;
                            case "d":
                                Console.WriteLine("Enter the id of the product for updating");
                                int id2;
                                int.TryParse(Console.ReadLine(), out id2);
                                Console.WriteLine(product.GetById(id2));
                                product1.ID = id2;
                                createProduct(ref product1);
                                product.Update(product1);
                                break;
                            case "e":
                                Console.WriteLine("Enter the ID of the product that you wants to delete:");
                                int id1;
                                int.TryParse(Console.ReadLine(), out id1);
                                product.Delete(id1);
                                break;
                            default: 
                                break;
                        }
                    }
                    break;
                default: Console.WriteLine("end");
                    break;
            }
        
        }
    }
}