using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_MachineLearning
{
    class TreeBranch
    {
        public string label { get; set; }
        public TreeNode ConnectionNode { get; set; }
        public bool drawn { get; set; }

        public TreeBranch(string label)
        {
            drawn = false;
            this.label = label;
        }



    }

}
