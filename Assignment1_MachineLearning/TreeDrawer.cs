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

        public TreeDrawer()
        {
            nodeCount = new int[9999];
            currentDepth = 1;
            image = new Bitmap(1500,1500);
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


            g.FillRectangle(Brushes.Yellow, 100 + (NodesOnThisDepth*100) + NodesOnThisDepth*20 , 100 + currentDepth*100, 80, 50);

           

            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Aquamarine);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(100+(NodesOnThisDepth * 100) + NodesOnThisDepth * 20, 100 + currentDepth * 100);

            // Draw string to screen.
            g.DrawString(node.label, drawFont, drawBrush, drawPoint);


            Save();

        }

        public void handleBranches(TreeNode node)
        {
            int NodesOnThisDepth = nodeCount[currentDepth];

            Font drawFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Aquamarine);

            

           
            foreach (TreeBranch branch in node.Branches)
            {
                // Create point for upper-left corner of drawing.
                PointF drawPoint = new PointF(  
                    ((((nodeCount[currentDepth + 1] * 100) + nodeCount[currentDepth + 1] * 20) + (NodesOnThisDepth * 100) + NodesOnThisDepth * 20)/2),
                    (((100 + (currentDepth + 1) * 100)) + (100 + currentDepth * 100))/2);

                // Draw string to screen.
                g.DrawString(node.label + " = " +branch.label, drawFont, drawBrush, drawPoint);

                g.DrawLine(Pens.Chartreuse, (NodesOnThisDepth * 100) + NodesOnThisDepth * 20, 100 + currentDepth * 100,
                    (nodeCount[currentDepth + 1] * 100) + nodeCount[currentDepth + 1] * 20, 100 + (currentDepth + 1) * 100);
                //Console.WriteLine((NodesOnThisDepth * 100) + NodesOnThisDepth * 20 + " " + 100 + currentDepth * 100 + " " + (nodeCount[currentDepth + 1] * 100) + nodeCount[currentDepth + 1] * 20 + " " + 100 + (currentDepth + 1) * 100);

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

    }
}
