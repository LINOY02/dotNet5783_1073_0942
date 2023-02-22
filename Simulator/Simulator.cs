using BlApi;
using BO;
using static Simulator.Simulator;

namespace Simulator;

public static class Simulator
{
    private static readonly IBl bl = Factory.Get();

    //A variable for drawing a random number
    private static readonly Random s_rand = new();

    private static volatile bool activate;

    // report to send information about the current to the PL
    public delegate void Report1(int orderId, DateTime? last, DateTime? now, OrderStatus status, int delay);
    public static Report1? report1;
    // report to make graphic change 
    public delegate void Report2();
    public static Report2? report2;
    // report to tell the PL the simulator is finish
    public delegate void Report3(string mes);
    private static Report3? report3;

    /// <summary>
    /// the function activate the thread of the simulator
    /// </summary>
    /// <exception cref="SimDoesNotExistException"></exception>
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
                        if (orderId == -1)// in case all the orders updated
                        {
                            report3("The order update process has been successfully completed");
                            activate = false;
                            return;
                        }
                        // get the full details of this order
                        var order = bl.Order.GetOrder(orderId);
                        // rand num of seconds to update the order
                        int delay = s_rand.Next(3, 11);
                        DateTime? newTime = DateTime.Now.AddSeconds(delay);
                        // update order from ordered to shipped
                        if (order.ShipDate == null)
                        {
                            report1(orderId, DateTime.Now, newTime, OrderStatus.Ordered, delay);
                            Thread.Sleep(delay * 1000);
                            bl.Order.ShipOrder(orderId, newTime);
                        }
                        else // update order from shipped to delivered
                        {
                            report1(orderId, DateTime.Now, newTime, OrderStatus.Shipped, delay);
                            Thread.Sleep(delay * 1000);
                            bl.Order.DeliveredOrder(orderId, newTime);
                        }
                       report2();// report to change in PL
                    }
                    catch (BlDoesNotExistException ex)
                    {
                        throw new SimDoesNotExistException(ex.Message);
                    }
                    Thread.Sleep(500);
                }
        }).Start();
    }

    /// <summary>
    /// func to stop the thread
    /// </summary>
    public static void StopActivate()
    {
        activate = false;
    }

    /// <summary>
    /// add function to event report1
    /// </summary>
    /// <param name="func"></param>
    public static void RegisterReport1(Report1 func)
    {
        report1 += func;
    }

    /// <summary>
    /// add function to event report2
    /// </summary>
    /// <param name="func"></param>
    public static void RegisterReport2(Report2 func)
    {
        report2 += func;
    }

    /// <summary>
    /// add function to event report3
    /// </summary>
    /// <param name="func"></param>
    public static void RegisterReport3(Report3 func)
    {
        report3 += func;
    }

    /// <summary>
    /// remove function from event report1
    /// </summary>
    /// <param name="func"></param>
    public static void UnRegisterReport1(Report1 func)
    {
        report1 -= func;
    }

    /// <summary>
    /// remove function from event report2
    /// </summary>
    /// <param name="func"></param>
    public static void UnRegisterReport2(Report2 func)
    {
        report2 -= func;
    }

    /// <summary>
    /// remove function from event report3
    /// </summary>
    /// <param name="func"></param>
    public static void UnRegisterReport3(Report3 func)
    {
        report3 -= func;
    }

}