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


        public TreeDrawer()
        {
            HorizontalPadding = 150;
            VerticalPadding = 150;
            DistanceMult = 120;

            nodeCount = new int[9999];
            currentDepth = 1;
            image = new Bitmap(1500, 1500);
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


            g.FillRectangle(Brushes.LightGray, HorizontalPadding + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, 100 + currentDepth * DistanceMult, 80, 50);



            Font drawFont = new Font("Arial", 13);
            SolidBrush drawBrush = new SolidBrush(Color.MediumBlue);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(HorizontalPadding + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, 100 + currentDepth * DistanceMult);

            // Draw string to screen.
            g.DrawString(node.label + "\nGain: " + (node.StaticGain).ToString("#0.00"), drawFont, drawBrush, drawPoint);


            //Save();

        }

        public void handleBranches(TreeNode node)
        {
            int NodesOnThisDepth = nodeCount[currentDepth];

            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            foreach (TreeBranch branch in node.Branches)
            {
                if (!branch.drawn)
                {

                    // Create point for upper-left corner of drawing.
                    PointF drawPoint = new PointF(
                        ((((nodeCount[currentDepth + 1] * DistanceMult) + nodeCount[currentDepth + 1] * 45) + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20) / 2),
                        (((100 + (currentDepth + 1) * DistanceMult)) + (100 + currentDepth * DistanceMult)) / 2);

                    // Draw string to screen.
                    g.DrawString(node.label + " = " + branch.label, drawFont, drawBrush, drawPoint);

                    g.DrawLine(Pens.Chartreuse, HorizontalPadding / 4 + (NodesOnThisDepth * DistanceMult) + NodesOnThisDepth * 20, (100 + currentDepth * DistanceMult) + 50,
                        HorizontalPadding / 4 + (nodeCount[currentDepth + 1] * DistanceMult) + nodeCount[currentDepth + 1] * 20, 100 + (currentDepth + 1) * DistanceMult);
                    //Console.WriteLine((NodesOnThisDepth * 100) + NodesOnThisDepth * 20 + " " + 100 + currentDepth * 100 + " " + (nodeCount[currentDepth + 1] * 100) + nodeCount[currentDepth + 1] * 20 + " " + 100 + (currentDepth + 1) * 100);
                    branch.drawn = true;
                }
            }
            //Save();
        }



        public void GoDown()
        {
            currentDepth++;
        }

        public void GoUp()
        {
            currentDepth--;
        }

    }
}
