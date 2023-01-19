using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class Config
    {
        static readonly string s_config = "configXml";
        public static int GetNextOrderID()
        {
            int orderID, nextOrderID;
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            orderID = XMLTools.ToIntNullable(root, "NextOrderId") ?? throw new Exception("null next order id");
            nextOrderID = orderID + 1;
            root.Element("NextOrderId").SetValue(nextOrderID.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
            return orderID;
        }
        public static int GetNextOrderItemID()
        {
            int orderItemID, nextOrderItemID;
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            orderItemID = XMLTools.ToIntNullable(root, "NextOrderItemId") ?? throw new Exception("null next orderItem id");
            nextOrderItemID = orderItemID + 1;
            root.Element("NextOrderItemId").SetValue(nextOrderItemID.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
            return orderItemID;
        }
    }
}
