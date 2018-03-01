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
                TreeAttribute attribute = data.GetValuebyType(TargetAttribute_Type);
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
                TreeAttribute attribute = data.GetValuebyType(TargetAttribute_Type);
                if (attribute.Attribute_Type.Equals(TargetAttribute_Type))
                {
                    if (attribute.Attribute_Value.Equals("win") || attribute.Attribute_Value.Equals("yes")) { return false; }   //TODO: FIX THIS. Find a way to realize outputs as true/false (Get outputType values?)
                }
            }
            return true;
        }



        public static double CalculateEntropy(int positive, int negative)
        {
            return ((positive / (positive + negative)) * Math.Log((positive / (positive + negative)), 2) - (negative / (positive + negative)) * Math.Log((negative / (positive + negative))));
        }

        public static double CalculateGain(List<TreeData> dataList, string AttributeType)
        {
            List<string> PossibleValues = Program.GetPossibleAttributeValues(dataList, AttributeType);

            double Gains = CalculateEntropy(1,1);   //TODO: Number of positive and negative occurance calculation

            foreach (string possibleValue in PossibleValues)
            {
                int occurances = Program.CountAttributeValueOccurance(dataList, possibleValue, AttributeType);

                Gains -= (occurances / dataList.Count) * CalculateEntropy(1,1); //Success count of any attribute type, failure count of any attribute type
            }

            return Gains;
        }

    }
}
