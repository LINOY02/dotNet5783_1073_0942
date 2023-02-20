using BlApi;
using BO;
namespace Simulator;

public static class Simulator
{
    private static readonly IBl bl = Factory.Get();

    //A variable for drawing a random number
    private static readonly Random s_rand = new();

    private static volatile bool activate;


    public delegate void Report1(int orderId, DateTime? last, DateTime? now, OrderStatus status, int delay);
    public static Report1? report1;
    public delegate void Report2();
    public static Report2? report2;
    public delegate void Report3(string mes);
    private static Report3? report3;

    public static void Activate()
    {
        new Thread(() =>
            {
                activate = true;
                while (activate)
                {
                    try
                    {
                        // find the oldext order
                        var orderId = bl.Order.OrderOldest();
                        if (orderId == -1)
                        {
                            report3("The order update process has been successfully completed");
                            activate = false;
                            return;
                        }
                        // get the full details of this order
                        var order = bl.Order.GetOrder(orderId);

                        int delay = s_rand.Next(3, 11);
                        DateTime? newTime = DateTime.Now.AddSeconds(delay);
                        if (order.ShipDate == null)
                        {
                            report1(orderId, DateTime.Now, newTime, OrderStatus.Ordered, delay);
                            Thread.Sleep(delay * 1000);
                            bl.Order.ShipOrder(orderId, newTime);
                        }
                        else
                        {
                            report1(orderId, DateTime.Now, newTime, OrderStatus.Shipped, delay);
                            Thread.Sleep(delay * 1000);
                            bl.Order.DeliveredOrder(orderId, newTime);
                        }
                       report2();
                    }
                    catch (BlDoesNotExistException ex)
                    {
                        throw new SimDoesNotExistException(ex.Message);
                    }
                    Thread.Sleep(500);
                }
        }).Start();
    }

    public static void StopActivate()
    {
        activate = false;
    }

    public static void RegisterReport1(Report1 func)
    {
        report1 += func;
    }

    public static void RegisterReport2(Report2 func)
    {
        report2 += func;
    }
    public static void RegisterReport3(Report3 func)
    {
        report3 += func;
    }

    public static void UnRegisterReport1(Report1 func)
    {
        report1 -= func;
    }

    public static void UnRegisterReport2(Report2 func)
    {
        report2 -= func;
    }

    public static void UnRegisterReport3(Report3 func)
    {
        report3 -= func;
    }

}