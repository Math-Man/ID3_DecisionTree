using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment1_MachineLearning
{
    class TreeDrawer
    {

        int[] nodeCount;
        int currentDepth;
        Bitmap image;
        Graphics g;

        public int HorizontalPadding { get; set; }
        public int VerticalPadding { get; set; }
        public int DistanceMult { get; set; }
        public bool signed { get; set; }

        public TreeDrawer()
        {
            HorizontalPadding = 225;
            VerticalPadding = 225;
            DistanceMult = 165;

            nodeCount = new int[99999];
            currentDepth = 1;
            image = new Bitmap(2000, 1500);
            g = Graphics.FromImage(image);
        }

        public void Save()
        {
            image.Save("out.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public void AddNode(TreeNode node)
        {
            int NodesOnThisDepth = nodeCount[currentDepth];
            nodeCount[currentDepth]++;

            g.FillRectangle(Brushes.Gray, 5 + HorizontalPadding + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, 5 + 100 + currentDepth * DistanceMult, 103, 78);
            g.FillRectangle(Brushes.LightGray, HorizontalPadding + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, 100 + currentDepth * DistanceMult, 100, 75);

            Font drawFont = new Font("Arial", 13);
            SolidBrush drawBrush = new SolidBrush(Color.MediumBlue);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(HorizontalPadding + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, 100 + currentDepth * DistanceMult);

            // Draw string to screen.
            g.DrawString(node.label + "\nGain: " + (node.StaticGain).ToString("#0.00"), drawFont, drawBrush, drawPoint);
        }

        public void handleBranches(TreeNode node)
        {
            int NodesOnThisDepth = nodeCount[currentDepth];

            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            foreach (TreeBranch branch in node.Branches)
            {
                if (!branch.drawn)
                {
                    PointF drawPoint = new PointF(
                        ((((nodeCount[currentDepth + 1] * DistanceMult) + nodeCount[currentDepth + 1] * 45) + (NodesOnThisDepth * (DistanceMult)) + NodesOnThisDepth * 20) / 2) + 20 ,
                        (((175 + (currentDepth + 1) * DistanceMult)) + (100 + currentDepth * DistanceMult)) / 2);

                    

                    g.DrawLine(new Pen(Color.SlateGray, 3), HorizontalPadding / 4 + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, (100 + currentDepth * DistanceMult) + 75,
                        HorizontalPadding / 4 + (nodeCount[currentDepth + 1] * DistanceMult) + nodeCount[currentDepth + 1] * 20, 100 + (currentDepth + 1) * DistanceMult);

                    g.DrawString(node.label + " =\n" + branch.label, drawFont, drawBrush, drawPoint);

                    branch.drawn = true;
                }
            }
        }



        public void GoDown()
        {
            currentDepth++;
        }

        public void GoUp()
        {
            currentDepth--;
        }

        public void Sign(string OutcomeType, List<string> attributeTypes)
        {
            Font drawFont = new Font("Arial", 13);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(50,50);

            // Draw string to screen.
            string conc = "";
            foreach (string s in attributeTypes)
            {
                conc += s + ", ";
            }

            g.DrawString("Plot for: " + OutcomeType + "\nAttribute Types: " + conc,  drawFont, drawBrush, drawPoint);
            signed = true;
        }

    }
}
