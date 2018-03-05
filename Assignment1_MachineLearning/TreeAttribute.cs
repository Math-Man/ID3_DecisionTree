using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
    /// <summary>
    /// Attribute_Type:Attribute_Value pair
    /// </summary>
    class TreeAttribute
    {

        public string Attribute_Value { get; set; }
        public string Attribute_Type { get; set; }
       

        public TreeAttribute(string value, string type)
        {
            this.Attribute_Value = value;
            this.Attribute_Type = type;
        }

    }
}
