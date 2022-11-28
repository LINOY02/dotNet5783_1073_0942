
 
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus status { get; set; }
   public List <Tuple<DateTime, string>> Tracking { set; get; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
