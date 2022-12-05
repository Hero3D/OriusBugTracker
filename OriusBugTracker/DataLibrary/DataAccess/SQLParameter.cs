using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public class SQLParameter
    {
        public string Name { get; set; }
        public string Data { get; set; }

        public SQLParameter(string name, string data)
        {
            Name = name;
            Data = data;
        }
    }
}
