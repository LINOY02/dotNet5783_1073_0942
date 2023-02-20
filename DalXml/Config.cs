
using System.Xml.Linq;

namespace Dal
{
    static internal class Config
    {
        static string s_config = "configuration";
        internal static int GetNextOrderId()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config)?.Element("NextOrderId")!;
        }
        internal static void SaveNextOrderID(int orderNumber)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderId")!.SetValue(orderNumber.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }
        internal static int GetOrderItemId()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config)?.Element("NextOrderItemId")!;
        }
        internal static void SaveNextOrderItemId(int orderItemNumber)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderItemId")!.SetValue(orderItemNumber.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }
    }
}
