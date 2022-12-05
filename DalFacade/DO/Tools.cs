

namespace DO
{
    static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (var item in t.GetType().GetProperties())
                str += item.Name + ":  " + item.GetValue(t, null) + "\n";
            return str += "\n";

        }
        
    }
}
