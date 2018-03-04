using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
    class TreeNode
    {
        /// <summary>
        /// Used to show the status of a node, not used in algorithms
        /// </summary>
        public string label { get; set; }
        public string Decision_AttributeType { get; set; }
        public List<TreeBranch> Branches { get; set; }
        public bool isLeaf { get; set; }
        public double StaticGain { get; set; }

        public TreeNode(string label)
        {
            this.label = label;
            this.Decision_AttributeType = null;
            Branches = new List<TreeBranch>();

        }

      


    }
}
