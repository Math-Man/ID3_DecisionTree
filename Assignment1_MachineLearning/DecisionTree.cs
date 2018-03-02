using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
    class DecisionTree
    {

        public DecisionTree ID3(List<TreeData> Examples, string TargetAttribute_Type, List<string> Attribute_Types)
        {
            TreeNode Root = new TreeNode();

            //If all Examples are positive
            if (checkForAllPositive(Examples, TargetAttribute_Type))
            {
                Root.label = "+";
            }

            //If all Examples are negative
            if (checkForAllNegative(Examples, TargetAttribute_Type))
            {
                Root.label = "=";
            }
            //If attributes is empty
            if (Attribute_Types.Count == 0)
            {
                Root.label = "=";
            }


            return null;
        }

        private bool checkForAllPositive(List<TreeData> Examples, string TargetAttribute_Type)
        {
            foreach (TreeData data in Examples)
            {
                TreeAttribute attribute = data.GetAttributeByType(TargetAttribute_Type);
                if (attribute.Attribute_Type.Equals(TargetAttribute_Type))
                {
                    if (attribute.Attribute_Value.Equals("loose") || attribute.Attribute_Value.Equals("no")) { return false; }   //TODO: FIX THIS. Find a way to realize outputs as true/false (Get outputType values?)
                }
            }

            return true;
        }
        private bool checkForAllNegative(List<TreeData> Examples, string TargetAttribute_Type)
        {
            foreach (TreeData data in Examples)
            {
                TreeAttribute attribute = data.GetAttributeByType(TargetAttribute_Type);
                if (attribute.Attribute_Type.Equals(TargetAttribute_Type))
                {
                    if (attribute.Attribute_Value.Equals("win") || attribute.Attribute_Value.Equals("yes")) { return false; }   //TODO: FIX THIS. Find a way to realize outputs as true/false (Get outputType values?)
                }
            }
            return true;
        }


        /// <summary>
        /// Calculates Entrophy of a given system. Takes in positive outcome count and negative outcome count as parameters.
        /// </summary>
        /// <param name="positive"></param>
        /// <param name="negative"></param>
        /// <returns>Entrophy between 0 and 1</returns>
        public static double CalculateEntropy(double positive, double negative)
        {
            double total = positive + negative;

            double valueA = -1 * (positive / total) * Math.Log(positive / total, 2);
            double valueB = -1 * (negative / total) * Math.Log(negative / total, 2);

            //Error Check
            if (double.IsNaN(valueA + valueB)) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Detected Nan value (infinite return from log) when calculating entropy. Possibly no failures for given value, returning 0" ); Console.ForegroundColor = ConsoleColor.Gray; return 0.0; }

            return valueA + valueB;
        }

        public static double CalculateGain(List<TreeData> dataList, string AttributeType)
        {
            List<string> PossibleValues = Program.GetPossibleAttributeValues(dataList, AttributeType);

            //TODO: Export into methods
            double DataSetSuccessCount = 0;
            double DataSetFailureCount = 0;

            foreach (TreeData data in dataList)
            {
                if (data.isSuccesful) { DataSetSuccessCount++; }
                else { DataSetFailureCount++; }
            }

            double Gains = CalculateEntropy(DataSetSuccessCount, DataSetFailureCount);   //TODO: Number of positive and negative occurance calculation

            foreach (string possibleValue in PossibleValues)
            {
                double occurances = Program.CountAttributeValueOccurance(dataList, possibleValue, AttributeType);

                //Substract the (number of times this value occured / all data count) * (entropy of the current value of the given type (Wind: Weak, Strong, Mild...))
                double GainForPossibleValue = (occurances / dataList.Count) * CalculateEntropy(Program.CountSuccesesByAttributeValue(dataList, possibleValue, AttributeType), Program.CountFailuresByAttributeValue(dataList, possibleValue, AttributeType));
                Gains -= GainForPossibleValue;

                Console.WriteLine("Entropy Coefficent for: " + possibleValue + " " + AttributeType + " " + GainForPossibleValue);

            }

            return Gains;
        }

        

    }
}
