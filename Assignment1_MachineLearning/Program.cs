using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/**
 * Goktug Kayacan 
 * ID3 - Decision Tree implementation.
 * Capable of handling non-binary outcomes.  
 */

namespace Assignment1_MachineLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            Console.WriteLine("Program will scan root directory of this application for .txt files...\n");
            while (true)
            {
                List<string> options = new List<string>();
                int counter = 0;
                Console.WriteLine("Enter index of one of the following text files as the dataset: ");
                foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.txt"))
                {
                    string[] split = file.Split('\\');
                    Console.WriteLine(counter + ") " + split[split.Length-1]);
                    counter++;
                    options.Add(split[split.Length - 1]);
                }

                if (counter == 0) return;

                Console.WriteLine(counter + ") Exit");

                int decisionIndex = Int32.Parse(Console.ReadLine());
                if (decisionIndex == counter) { return; }

                while (decisionIndex > options.Count - 1 || decisionIndex < 0) { decisionIndex = Int32.Parse(Console.ReadLine()); }

                Console.WriteLine("Enter name of the outcome (playtennis, Game etc..)");
                string outcomeType = Console.ReadLine();

                Console.WriteLine("Enter the value of outcome success value (yes, win etc...)");
                string outcomeSuccessValue = Console.ReadLine();

                List<TreeData> data;
                List<string> possibleTypes;

                data = ExtractData(
                    options[decisionIndex],
                    outcomeType,
                    outcomeSuccessValue,
                    out possibleTypes);

                possibleTypes.Remove(outcomeType);

                DecisionTree Tree = new DecisionTree();
                Console.WriteLine("----------------------------------------");
                DecisionTree.ID3Alt(data, outcomeType, possibleTypes, false);
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Completed!, Outcome Chart is saved at root as 'out.txt'\n");
            }
        }

        /// <summary>
        /// Extracts data from the given file.
        /// </summary>
        /// <param name="FileName">Path of the file</param>
        /// <param name="outcomeTypeStringName">Outcome type of this dataset (example: playtennis)</param>
        /// <param name="succesfullOutcomeName">Outcome type's value upon succesful result (yes, win etc.)</param>
        /// <param name="failedOutcomeName">Outcome type's value upon failed result (no, loose)</param>
        /// <returns></returns>
        public static List<TreeData> ExtractData(string FileName, string outcomeTypeStringName, string succesfullOutcomeName, out List<string> possibleTypes)
        {
            List<TreeData> output = new List<TreeData>();

            StreamReader reader = new StreamReader(FileName);
            string line = "";
            possibleTypes = new List<string>();

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
                    TreeData data = new TreeData(attList, outcomeTypeStringName, succesfullOutcomeName);
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
                TreeAttribute attribute =  data.GetAttributeByType(attributeType);
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
        public static double CountAttributeValueOccurance(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            List<string> ValuesList = new List<string>();

            foreach (TreeData data in dataList)
            {
                TreeAttribute attribute = data.GetAttributeByType(attributeType);
                ValuesList.Add(attribute.Attribute_Value);
            }
            int count = ((from temp in ValuesList where temp.Equals(attributeValue) select temp).Count());
            return count;
        }

        public static double CountFailuresByAttributeValue(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            double count = 0;
            foreach (TreeData data in dataList)
            {
                TreeAttribute attribute = data.GetAttributeByType(attributeType);   //Get attribute from the given data
                if (attribute.Attribute_Value.Equals(attributeValue))   //Is the attribute's value equals to the value we are counting?
                {
                    if (!data.isSuccesful)  //if this data failed with the given attribute value, increment count
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static double CountSuccesesByAttributeValue(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            double count = 0;
            foreach (TreeData data in dataList)
            {
                TreeAttribute attribute = data.GetAttributeByType(attributeType);   //Get attribute from the given data
                if (attribute.Attribute_Value.Equals(attributeValue))   //Is the attribute's value equals to the value we are counting?
                {
                    if (data.isSuccesful)  //if this data failed with the given attribute value, increment count
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the data with given value of the given attribute type
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="attributeValue"></param>
        /// <param name="attributeType"></param>
        /// <returns>A list of the data with given credentials</returns>
        public static List<TreeData> GetAttributeValueOccurances(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            List<TreeData> FilteredDataList = new List<TreeData>();

            foreach (TreeData data in dataList)
            {
                if (data.GetAttributeByType(attributeType).Attribute_Value.Equals(attributeValue))
                {
                    FilteredDataList.Add(data);
                }
            }
            return FilteredDataList;
        }

    }
}
