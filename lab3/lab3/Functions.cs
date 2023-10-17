using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3
{
    internal class Functions
    {
        public static void Calculating(List<int> listX1, List<int> listX2,
            List<double> density1, List<double> density2, out double ValueOfCrossX, int ChartWidth)
        {
            ValueOfCrossX = 0;
            Random rand = new Random();

            for (int i = 0; i < ProjConst.PointNum; i++)
            {
                listX1.Add(rand.Next(ProjConst.X1BorderLeft, ProjConst.X1BorderRight));
                listX2.Add(rand.Next(ProjConst.X2BorderLeft, ProjConst.X2BorderRight));
            }

            double mathExpect1 = listX1.Sum() / ProjConst.PointNum;
            double mathExpect2 = listX2.Sum() / ProjConst.PointNum;

            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < ProjConst.PointNum; i++)
            {
                sum1 += Math.Pow(listX1[i] - mathExpect1, 2);
                sum2 += Math.Pow(listX2[i] - mathExpect2, 2);
            }

            double sigma1 = Math.Sqrt(sum1 / ProjConst.PointNum);
            double sigma2 = Math.Sqrt(sum2 / ProjConst.PointNum);

            listX1.Sort();
            listX2.Sort();

            for (int x = 0; x < ChartWidth; x++)
            {
                density1.Add(ProjConst.Pc1 * (Math.Exp(-0.5 * Math.Pow((x - mathExpect1) / sigma1, 2)) /
                    (sigma1 * Math.Sqrt(2 * Math.PI))));
                density2.Add(ProjConst.Pc2 * (Math.Exp(-0.5 * Math.Pow((x - mathExpect2) / sigma2, 2)) /
                    (sigma2 * Math.Sqrt(2 * Math.PI))));

                if (Math.Abs(density1[x] - density2[x]) * 500 < 0.002)
                {
                    ValueOfCrossX = x;
                }
            }
        }
    }
}
