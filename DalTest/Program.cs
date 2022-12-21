
using DO;
using DalApi;

namespace DalTest
{
    public class Program
    {
        public IDal? dalList = DalApi.Factory.Get();

        static void Main(string[] args)
        {
            Console.WriteLine(@"
 enter a number between 0 to 3:
 0- exsit
 1- order
 2-order item
 3- product");
            int number;
            int.TryParse(Console.ReadLine(), out number);
            while (number != 0)
            {
                Program program = new Program();
                switch (number)
                {
                    //case 0:
                    //    Console.WriteLine("out");
                    //    return;
                    case 1:
                        program.ORDER();
                        break;
                    case 2:
                        program.ORDERITEM();
                        break;
                    case 3:
                        program.PRODUCT();
                        break;
                    default:
                        return;
                        break;
                }
                Console.WriteLine(@"
 enter a number between 0 to 3:
 0- exsit
 1- order
 2-order item
 3- product");
                int.TryParse(Console.ReadLine(), out number);
            }
        }


        #region product
        //The function receives data for a new product
        void createProduct(ref Product product1)
        {
            int id;
            Console.WriteLine("enter the id of the product");
            int.TryParse(Console.ReadLine(), out id);
            Category Category;
            Console.WriteLine("enter the Category of the product");
            Enum.TryParse(Console.ReadLine(), out Category);
            string name;
            Console.WriteLine("enter the name of the product");
            name = Console.ReadLine();
            int price;
            Console.WriteLine("enter the price of the product");
            int.TryParse(Console.ReadLine(), out price);
            int inStock;
            Console.WriteLine("enter the amount of the product in stock");
            int.TryParse(Console.ReadLine(), out inStock);
            product1.ID = id;
            product1.Price = price;
            product1.Name = name;
            product1.InStock = inStock;
            product1.Category = Category;
        }
        //The function performs CRUD operations according to the user's request
        void PRODUCT()
        {
            try
            {
                Console.WriteLine(@"
enter 'a' for add a product
enter 'b' for show a product
enter 'c' for show the list
enter 'd' for update the product
enter 'e' for delete the product");
                Product product1 = new Product();
                string ch = Console.ReadLine();
                switch (ch)
                {
                    case "a":
                        createProduct(ref product1);
                        Console.WriteLine(dalList?.Product.Add(product1));
                        break;
                    case "b":
                        Console.WriteLine("Enter the id of the product");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalList?.Product.GetById(id));
                        break;
                    case "c":
                        foreach (var p in dalList?.Product.GetAll())
                            Console.WriteLine(p);
                        break;
                    case "d":
                        Console.WriteLine("Enter the id of the product for updating");
                        int id2;
                        int.TryParse(Console.ReadLine(), out id2);
                        Console.WriteLine(dalList?.Product.GetById(id2));
                        createProduct(ref product1);
                        product1.ID = id2;
                        dalList?.Product.Update(product1);
                        break;
                    case "e":
                        Console.WriteLine("Enter the id of the product for delete");
                        int id1;
                        int.TryParse(Console.ReadLine(), out id1);
                        dalList?.Product.Delete(id1);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception str)
            {
                Console.WriteLine(str);
            }
        }
        #endregion
        #region orderItem
        //The function receives data for a new order item
        void createOrderItem(ref OrderItem orderItem1)
        {
            int orderId;
            Console.WriteLine("enter the ID of the order");
            int.TryParse(Console.ReadLine(), out orderId);
            int productId;
            Console.WriteLine("enter the ID of the product");
            int.TryParse(Console.ReadLine(), out productId);
            int amount;
            Console.WriteLine("enter the amount of the product");
            int.TryParse(Console.ReadLine(), out amount);
            orderItem1.OrderID = orderId;
            orderItem1.ProductID = productId;
            orderItem1.Amount = amount;
            orderItem1.Price = amount * dalList.Product.GetById(productId).Price;
        }
        void ORDERITEM()
        {
            try
            {
                Console.WriteLine(@"
enter 'a' for add
enter 'b' for show a order item
enter 'c' for show the list
enter 'd' for update the order item
enter 'e' for delete
enter 'f' for show by order's & product's id
enter 'g' for show the list of order");
                OrderItem orderItem1 = new OrderItem();
                string ch = Console.ReadLine();
                switch (ch)
                {
                    case "a":
                        createOrderItem(ref orderItem1);
                        Console.WriteLine(dalList?.OrderItem.Add(orderItem1));
                        break;
                    case "b":
                        Console.WriteLine("Enter the id of the ordr item");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalList?.OrderItem.GetById(id));
                        break;
                    case "c":
                        foreach (var OIt in dalList?.OrderItem.GetAll())
                            Console.WriteLine(OIt);
                        break;
                    case "d":
                        Console.WriteLine("Enter the id of the order item for updating");
                        int id1;
                        int.TryParse(Console.ReadLine(), out id1);
                        createOrderItem(ref orderItem1);
                        orderItem1.ID = id1;
                        dalList?.OrderItem.Update(orderItem1);
                        break;
                    case "e":
                        Console.WriteLine("Enter the id of the order for delete");
                        int id2;
                        int.TryParse(Console.ReadLine(), out id2);
                        dalList?.OrderItem.Delete(id2);
                        break;
                    case "f":
                        Console.WriteLine("Enter the id of the order");
                        int idOrder;
                        int.TryParse(Console.ReadLine(), out idOrder);
                        Console.WriteLine("Enter the id of the product");
                        int idProduct;
                        int.TryParse(Console.ReadLine(), out idProduct);
                        Console.WriteLine(dalList?.OrderItem.GetItem((OrderItem? x) => { return (x?.ProductID == idProduct && x?.OrderID == idOrder); }));
                        break;
                    case "g":
                        Console.WriteLine("Enter the id of the order");
                        int idOrder1;
                        int.TryParse(Console.ReadLine(), out idOrder1);

                        foreach (var item in dalList.OrderItem.GetAll((OrderItem? x) => { return x?.OrderID == idOrder1; }))
                            Console.WriteLine(item);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception str)
            {
                Console.WriteLine(str);
            }
        }
        #endregion
        #region order
        //The function receives data for a new order item
        void createOrder(ref Order order1)
        {
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
            Console.WriteLine("enter the date of the order");
            DateTime.TryParse(Console.ReadLine(), out orderDate);
            DateTime shipDate;
            Console.WriteLine("enter the ship date");
            DateTime.TryParse(Console.ReadLine(), out shipDate);
            DateTime deliveryDate;
            Console.WriteLine("enter the delivery date");
            DateTime.TryParse(Console.ReadLine(), out deliveryDate);
            order1.CustomerName = customerName;
            order1.CustomerEmail = customerEmail;
            order1.CustomerAdress = customerAdress;
            order1.OrderDate = orderDate;
            order1.ShipDate = shipDate;
            order1.DeliveryDate = deliveryDate;
        }
        void ORDER()
        {
            try
            {
                Console.WriteLine(@"
 enter 'a' for add order
 enter 'b' for show a order
 enter 'c' for show the list
 enter 'd' for update the order
 enter 'e' for delete order");
                Order order1 = new Order();
                string ch = Console.ReadLine();
                switch (ch)
                {
                    case "a":
                        createOrder(ref order1);
                        Console.WriteLine(dalList?.Order.Add(order1));
                        break;
                    case "b":
                        Console.WriteLine("Enter the id of the order");
                        int id;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dalList?.Order.GetById(id));
                        break;
                    case "c":
                        foreach (var o in dalList.Order.GetAll())
                            Console.WriteLine(o);
                        break;
                    case "d":
                        Console.WriteLine("Enter the id of the order for updating");
                        int id1;
                        int.TryParse(Console.ReadLine(), out id1);
                        createOrder(ref order1);
                        order1.ID = id1;
                        dalList?.Order.Update(order1);
                        break;
                    case "e":
                        Console.WriteLine("Enter the id of the order for delete:");
                        int id2;
                        int.TryParse(Console.ReadLine(), out id2);
                        dalList?.Order.Delete(id2);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception str)
            {
                Console.WriteLine(str);
            }
        }
        #endregion
    }
}
