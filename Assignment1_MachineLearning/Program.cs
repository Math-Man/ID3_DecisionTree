using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Assignment1_MachineLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Take data from console about outcomes and success/fail keywords
            List<TreeData> data;
            data = ExtractData(
                @"c:\users\mathman\documents\visual studio 2015\Projects\Assignment1_MachineLearning\Assignment1_MachineLearning\Datasets\dataset1.txt",
                "playtennis",
                "yes",
                "no");

            Console.WriteLine(data[0].AttributesList[0].Attribute_Type + " " + data[0].AttributesList[0].Attribute_Value);

            List<string> distinctList =  GetPossibleAttributeValues(data, "outlook");

            Console.WriteLine("!"+distinctList.Count);

            Console.WriteLine(CountAttributeValueOccurance(data, "high", "humidity"));

            Console.ReadKey();
        }

        public static List<TreeData> ExtractData(string FileName, string outcomeTypeStringName, string succesfullOutcomeName, string failedOutcomeName)
        {
            List<TreeData> output = new List<TreeData>();

            StreamReader reader = new StreamReader(FileName);
            string line = "";
            List<string> possibleTypes = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                line = line.Replace(",", string.Empty); //Remove Commas
                string[] words = line.Split(' ');

                //Only true for the first loop
                if (possibleTypes.Count == 0)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        possibleTypes.Add(words[i]);
                    }
                }
                else
                {
                    List<TreeAttribute> attList = new List<TreeAttribute>();
                    for (int i = 0; i < words.Length; i++)
                    {
                        TreeAttribute attribute = new TreeAttribute(words[i], possibleTypes[i]);
                        attList.Add(attribute);
                    }
                    TreeData data = new TreeData(attList, outcomeTypeStringName, succesfullOutcomeName, failedOutcomeName);
                    output.Add(data);
                }
            }
            return output;
        }


        /// <summary>
        /// Returns a list of possible values for a given attributeType from the given data list
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="attributeType"></param>
        /// <returns>string list of possible values</returns>
        public static List<string> GetPossibleAttributeValues(List<TreeData> dataList, string attributeType) 
        {
            List<string> ValuesList = new List<string>();

            foreach (TreeData data in dataList)
            {
                TreeAttribute attribute =  data.GetValuebyType(attributeType);
                ValuesList.Add(attribute.Attribute_Value);
            }

            List<string> distinct_valuesList = ValuesList.Distinct().ToList();
            return distinct_valuesList;
        }

        /// <summary>
        /// Returns the number of occurances of the given value of given given type within the given data list
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="attributeValue"></param>
        /// <param name="attributeType"></param>
        /// <returns>The number of occurances of the given</returns>
        public static int CountAttributeValueOccurance(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            List<string> ValuesList = new List<string>();

            foreach (TreeData data in dataList)
            {
                TreeAttribute attribute = data.GetValuebyType(attributeType);
                ValuesList.Add(attribute.Attribute_Value);
            }
            int count = ((from temp in ValuesList where temp.Equals(attributeValue) select temp).Count());
            return count;
        }


        public static int CountFailuresByAttributeValue(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            foreach (TreeData data in dataList)
            {
                TreeAttribute attribute = data.GetValuebyType(attributeType);
                //Basarlili mi basariz mi?
            }
            return -1;
        }

        public static int CountSuccesesByAttributeValue(List<TreeData> dataList, string attributeValue)
        {

            return -1;
        }


    }
}
