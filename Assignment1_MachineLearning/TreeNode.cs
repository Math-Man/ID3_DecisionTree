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
        /// <summary>
        /// Attribute type this node uses as rule
        /// </summary>
        public string Decision_AttributeType { get; set; }
        /// <summary>
        /// Branches coming out of this node
        /// </summary>
        public List<TreeBranch> Branches { get; set; }
        public bool isLeaf { get; set; }
        /// <summary>
        /// Gain of the attribute type on this node
        /// </summary>
        public double StaticGain { get; set; }

        public TreeNode(string label)
        {
            this.label = label;
            this.Decision_AttributeType = null;
            Branches = new List<TreeBranch>();

        }

      


    }
}
