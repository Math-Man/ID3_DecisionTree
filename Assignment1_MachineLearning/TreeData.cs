using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
  
    class TreeData
    {
        public List<TreeAttribute> AttributesList { get; set; }
        public bool isSuccesful { get; set; }
        public string OutComeValue { get; set; }

        public TreeData(List<TreeAttribute> attributes, string outcomeAttributeType, string SuccessKeyWord)
        {
            //TODO: Add error
            AttributesList = attributes;

            this.OutComeValue = GetAttributeByType(outcomeAttributeType).Attribute_Value;

            //Determine if this data is succesful
            if (GetAttributeByType(outcomeAttributeType).Attribute_Value.Equals(SuccessKeyWord)) { isSuccesful = true; }
            else { isSuccesful = false; } // (GetAttributeByType(outcomeAttributeType).Attribute_Value.Equals(FailureKeyWord)) { isSuccesful = false; }
            //else { Console.ForegroundColor = ConsoleColor.Red;  Console.WriteLine("Somethings broken"); Console.ForegroundColor = ConsoleColor.Gray; }//Its broken Error message
        }

        /// <summary>
        /// Finds the attribute with given Attribute Type and returns it
        /// </summary>
        /// <param name="Attribute_Type"></param>
        /// <returns>Attribute with given Attribute Type</returns>
        public TreeAttribute GetAttributeByType(string Attribute_Type)
        {
            foreach (TreeAttribute att in AttributesList)
            {
                if (att.Attribute_Type.Equals(Attribute_Type))
                {
                    return att;
                }
            }
            return null;
        }
    }
}
