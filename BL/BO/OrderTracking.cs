

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus status { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
