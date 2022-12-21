using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    static public class Factory
    {
        public static IBl? Get() { return default(IBl); }   
    }
}
