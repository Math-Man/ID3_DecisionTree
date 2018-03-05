using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
    class DecisionTree
    {

        public static TreeDrawer drawer { get; set; }

        public DecisionTree()
        {
            drawer = new TreeDrawer();
        }

        /// <summary>
        /// Recursive ID3 Algorithm
        /// </summary>
        /// <param name="Examples"></param>
        /// <param name="TargetAttribute_Type"></param>
        /// <param name="Attribute_Types"></param>
        /// <param name="ExtraLogging"></param>
        /// <returns></returns>
        public static TreeNode ID3Alt(List<TreeData> Examples, string TargetAttribute_Type, List<string> Attribute_Types, bool ExtraLogging)
        {
            TreeNode Root = new TreeNode("Unlabeled");

            if(!drawer.signed) drawer.Sign(TargetAttribute_Type, Attribute_Types);

            string typestring;
            if (checkForAllSameOutcome(Examples, TargetAttribute_Type, out typestring))
            {
                Console.WriteLine(" Outcome : " + typestring);
                Root.label = typestring;
                Root.Decision_AttributeType = TargetAttribute_Type;
                Root.isLeaf = true;
                drawer.AddNode(Root);
                drawer.GoUp();
                if (ExtraLogging) Console.WriteLine("\nTree Finalized for Type: " + Examples[0].AttributesList[0].Attribute_Type + " With general value of: " + Examples[0].AttributesList[0].Attribute_Value + " With outcome of: " + typestring);
                return Root;
            }

            if (Attribute_Types.Count == 0)
            {
                List<string> possible_TargetAttribute_Types = Program.GetPossibleAttributeValues(Examples, TargetAttribute_Type);

                //taken from: https://stackoverflow.com/questions/355945/find-the-most-occurring-number-in-a-listint
                var most = possible_TargetAttribute_Types.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                Root.isLeaf = true;
                Root.Decision_AttributeType = TargetAttribute_Type;
                Root.label = most;
                Console.WriteLine(" Outcome : " + most);
                drawer.AddNode(Root);
                drawer.GoUp();
                if (ExtraLogging) Console.WriteLine("\nTree Finalized for: " + typestring);
                return Root;
            }
            //Begin

            //Find the  Attribute with the best gain
            string attributeType_WithBestGain = "";
            double bestGain = -1;

            foreach (string AttributeType in Attribute_Types)
            {
                double calculatedGain = CalculateGain(Examples, AttributeType, TargetAttribute_Type);     //Finds the gain for the given attribute type in examples
                if (ExtraLogging) Console.WriteLine("\nInformation Gain : " + calculatedGain + " from attribute: " + AttributeType);
                if (calculatedGain > bestGain)
                {
                    bestGain = calculatedGain;
                    attributeType_WithBestGain = AttributeType;
                    
                }
            }
            if (ExtraLogging) Console.WriteLine("Selected Attribute : '" + attributeType_WithBestGain + "' with information gain of :" + bestGain);
            Root.label = attributeType_WithBestGain;
            Root.StaticGain = bestGain;
            drawer.AddNode(Root);
            Root.Decision_AttributeType = attributeType_WithBestGain;

            //For each possible value vi of attributeType_WithBestGain
            List<string> PossibleValues = Program.GetPossibleAttributeValues(Examples, attributeType_WithBestGain);
            foreach (string vi in PossibleValues)
            {
                
                //Create a new Branch below Root, corresponding to the test attributeType_WithBestGain = vi
                TreeBranch branch = new TreeBranch(vi);

                //Let Examplesvi be the subset of examples that have value vi for attributeType_WithBestGain
                List<TreeData> Examplesvi = Program.GetAttributeValueOccurances(Examples, vi, attributeType_WithBestGain); //EXAMPLESvi subset list

                //if Examplesvi is empty
                if (Examplesvi.Count == 0)
                {
                    //Below this new branch add a leaf node with label = most common value of Target_attribute in Examples
                    List<string> possible_TargetAttribute_Types = Program.GetPossibleAttributeValues(Examples, TargetAttribute_Type);
                    //taken from: https://stackoverflow.com/questions/355945/find-the-most-occurring-number-in-a-listint
                    var most = possible_TargetAttribute_Types.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();

                    Root.Branches.Add(branch);
                    drawer.handleBranches(Root);
                }
                //Else below this new branch add the subtree
                else
                {
                    Console.Write(attributeType_WithBestGain + "= " + vi + "->");

                    if(ExtraLogging) Console.WriteLine("\nRemoving current Attribute for next subtree: " + attributeType_WithBestGain);

                    List<string> Attribute_TypesCopy = new List<string>(Attribute_Types);
                    Attribute_TypesCopy.Remove(attributeType_WithBestGain);  //Remove A(attribute with best gain ) from the list

                    if (ExtraLogging) Console.WriteLine("\nStarting new Sub-tree with rule: " + attributeType_WithBestGain + " = "+ vi);
                    drawer.GoDown();
                    TreeNode subtree = ID3Alt(Examplesvi, TargetAttribute_Type, Attribute_TypesCopy, ExtraLogging);

                    branch.ConnectionNode = subtree;
                    Root.Branches.Add(branch);
                    drawer.handleBranches(Root);
                }
            }
            drawer.GoUp();
            drawer.Save();
            return Root;
            
        }

        private static bool checkForAllSameOutcome(List<TreeData> Examples, string TargetAttribute_Type, out string outcome)
        {
            bool allSame = !Examples.Select(item => item.OutComeValue)  //If all outcome values are the same in the given list, return true
                      .Where(x => !string.IsNullOrEmpty(x))
                      .Distinct()
                      .Skip(1)
                      .Any();

            if (allSame)
            {
                outcome = Examples[0].OutComeValue;
                return true;
            }
            else
            {
                outcome = null;
                return false;
            }
        }

        /// <summary>
        /// Calculates Entrophy of a given system. Takes in positive outcome count and negative outcome count as parameters.
        /// </summary>
        /// <param name="positive"></param>
        /// <param name="negative"></param>
        /// <returns>Entrophy between 0 and 1</returns>
        public static double CalculateEntropy(double[] outcomeCounts)//(double positive, double negative)
        {
            double total = 0;
            foreach (double i in outcomeCounts)
            {
                total += i;
            }

            double outputValue = 0;
            foreach (double i in outcomeCounts)
            {
                double val = -1 * (i / total) * Math.Log(i / total, 2);

                if (double.IsNaN(val)) { val = 0.0; }

                outputValue += val;
            }

            //NaN / absolute success check
            if (double.IsNaN(outputValue))
            {
                return 0.0;
            }
            return outputValue;
        }

        public static double CalculateGain(List<TreeData> dataList, string AttributeType, string outcomeType)
        {
            List<string> possibleOutcomeValues = new List<string>();
            possibleOutcomeValues = Program.GetPossibleAttributeValues(dataList, outcomeType);

            double[] outcomeCounts = new double[possibleOutcomeValues.Count];

            for (int i = 0; i < possibleOutcomeValues.Count; i++)
            {
                outcomeCounts[i] = Program.CountAttributeValueOccurance(dataList, possibleOutcomeValues[i], outcomeType);
            }

            double Gains = CalculateEntropy(outcomeCounts);
            List<string> PossibleValues = Program.GetPossibleAttributeValues(dataList, AttributeType);

            foreach (string possibleValue in PossibleValues) //Possible value : A, B, C,... for stadium
            {
                List<TreeData> occurances =  Program.GetAttributeValueOccurances(dataList, possibleValue, AttributeType);

                double[] occuranceOutcomeCounts = new double[possibleOutcomeValues.Count];

                foreach (TreeData data in occurances)   //For every treedata object where possiblevalue exists (for every treedata where A is for stadium)
                {
                    for (int i = 0; i < possibleOutcomeValues.Count; i++)
                    {
                        if (data.OutComeValue.Equals(possibleOutcomeValues[i]))
                        {
                            occuranceOutcomeCounts[i]++;
                            break;
                        }
                    }
                }

                
                double entropyValue = CalculateEntropy(occuranceOutcomeCounts);
                double GainForPossibleValue = (Convert.ToDouble(occurances.Count) / Convert.ToDouble(dataList.Count)) * entropyValue;
                Gains -= GainForPossibleValue;

            }

            //Console.WriteLine("Gain for " + AttributeType +" : " + Gains);
            return Gains;
        }



    }
}
