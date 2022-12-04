using BlApi;
using BO;
namespace BlTest
{
    public class Program
    {
        private IBl Bl = new Bl();
        static void Main(string[] args)
        {
                 
        Console.WriteLine(@"
 enter a number between 0 to 3:
 0- exsit
 1- order
 2- cart
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
                            program.CART();
                            break;
                        case 3:
                            program.PRODUCT();
                            break;
                        default:
                            return;
                    }
                    Console.WriteLine(@"
 enter a number between 0 to 3:
 0- exsit
 1- order
 2- cart
 3- product");
                    int.TryParse(Console.ReadLine(), out number);
                }
        }


            #region product
            //The function receives data for a new product
            void createProduct(ref BO.Product product1)
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
Actions for the manager:
enter 'a' for add a product
enter 'b' for show a product by ID
enter 'c' for show the list of products
enter 'd' for update the product
enter 'e' for delete the product

Actions for the customer:
enter 'f' for show a product by ID
enter 'g' for show the product catalog") ;
                    BO.Product product1 = new BO.Product();
                    string ch = Console.ReadLine();
                    switch (ch)
                    {
                        case "a":
                            createProduct(ref product1);
                            Bl.Product.AddProduct(product1);
                            break;
                        case "b":
                            Console.WriteLine("Enter the id of the product");
                            int id;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(Bl.Product.GetProduc(id));
                            break;
                        case "c":
                            foreach (var p in Bl.Product.GetListedProducts())
                                Console.WriteLine(p);
                            break;
                        case "d":
                            Console.WriteLine("Enter product for updating");
                            createProduct(ref product1);
                            Bl.Product.UpdateProduct(product1);
                            break;
                        case "e":
                            Console.WriteLine("Enter the id of the product for delete");
                            int id1;
                            int.TryParse(Console.ReadLine(), out id1);
                            Bl.Product.DeleteProduct(id1);
                            break;
                        case "f":
                            Console.WriteLine("Enter the id of the product");
                            int id2;
                            int.TryParse(Console.ReadLine(), out id2);
                            Console.WriteLine(Bl.Product.GetItem(id2));
                            break;
                        case "g":
                            foreach (var p in Bl.Product.GetProducts())
                                Console.WriteLine(p);
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
            #region Cart
            //The function receives data for a new order item
            void createCart(ref Cart cart)
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
                
            }
            void CART()
            {
                try
                {
                    Console.WriteLine(@"
enter 'a' for add product to the cart
enter 'b' for update the cart
enter 'c' for order the cart");
                    Cart cart = new Cart();
                    string ch = Console.ReadLine();
                    switch (ch)
                    {
                        case "a":
                            createCart(ref cart);
                            Console.WriteLine("Enter the id of the added product");
                            int productID;
                            int.TryParse(Console.ReadLine(), out productID);
                            Console.WriteLine(Bl.Cart.AddProductToCart(cart, productID));
                            break;
                        case "b":
                            createCart(ref cart);
                            Console.WriteLine("Enter the id of the added product");
                            int productID1;
                            int.TryParse(Console.ReadLine(), out productID1);
                            Console.WriteLine("Enter the new amount");
                            int amount;
                            int.TryParse(Console.ReadLine(), out amount);
                            Console.WriteLine(Bl.Cart.UpdateCart(cart, productID1, amount));
                            break;
                        case "c":
                            createCart(ref cart);
                            Console.WriteLine("Enter the costumer details(name, email, adress)");
                            string name, email, adress;
                            name = Console.ReadLine();  
                            email = Console.ReadLine();
                            adress = Console.ReadLine();
                           Bl.Cart.OrderCart(cart, name, email, adress);
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

            void ORDER()
            {
                try
                {
                    Console.WriteLine(@"
 enter 'a' for show a order
 enter 'b' for show the list of ordrs
 enter 'c' for Order ship date update
 enter 'd' for Order tracking
 enter 'e' for Order delivery date update");
                    Order order1 = new Order();
                    string ch = Console.ReadLine();
                    switch (ch)
                    {
                        case "a":
                            Console.WriteLine("Enter the id of the order");
                            int id;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(Bl.Order.GetOrder(id));
                            break;
                        case "b":
                            foreach (var o in Bl.Order.GetListedOrders())
                                Console.WriteLine(o);
                            break;
                        case "c":
                            Console.WriteLine("Enter the id of the order for updating the ship date");
                            int id1;
                            int.TryParse(Console.ReadLine(), out id1);
                           Console.WriteLine(Bl.Order.ShipOrder(id1));
                            break;
                        case "d":
                            Console.WriteLine("Enter the id of the order for trucking");
                            int id2;
                            int.TryParse(Console.ReadLine(), out id2);
                            Console.WriteLine(Bl.Order.UpdateOrder(id2));
                            break;
                        case "e":
                            Console.WriteLine("Enter the id of the order for updating the delivery date");
                            int id3;
                            int.TryParse(Console.ReadLine(), out id3);
                            Console.WriteLine(Bl.Order.DeliveredOrder(id3));
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