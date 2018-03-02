using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
    class TreeNode
    {
        public string label { get; set; }
        public string Decision_AttributeType { get; set; }
        public List<NodeBranch> BranchesList { get; }


        public TreeNode(string label, string Decision_AttributeType)
        {
            this.label = label;
            this.Decision_AttributeType = Decision_AttributeType;

            BranchesList = new List<NodeBranch>();

        }

        /// <summary>
        /// Adds a branch to this node, creating tests for other nodes
        /// </summary>
        /// <param name="ConnectionNode"></param>
        public void AddBranch(List<TreeData> data, string Decision_AttributeValue)
        {
           double occurance = Program.CountAttributeValueOccurance(data, Decision_AttributeValue, Decision_AttributeType);

            List<TreeData> DataSubset = Program.GetAttributeValueOccurances(data, Decision_AttributeValue, Decision_AttributeType); //EXAMPLESvi
        }


    }
}
