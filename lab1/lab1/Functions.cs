using SFML.Graphics;
using SFML.System;

namespace lab1
{
    internal class Functions
    {
        public static void CircleColoring(List<CircleShape> circles, List<CircleShape> cores)
        {
            int temp;
            for (int i = 0; i < ProjConst.CircleNumber; i++)
            {
                int tempMin = 10000;
                int numMin = 0;

                for (int j = 0; j < ProjConst.CoreNumber; j++)
                {
                    temp = (int)Math.Sqrt(Math.Pow(circles[i].Position.X - cores[j].Position.X, 2) +
                        Math.Pow(circles[i].Position.Y - cores[j].Position.Y, 2));
                    if (temp < tempMin)
                    {
                        tempMin = temp;
                        numMin = j;
                    }
                }

                circles[i].FillColor = Program.colors[numMin];
            }
        }

        public static void CoresMoving(List<CircleShape> circles, List<CircleShape> cores)
        {
            int temp;
            bool neadLoop = true;
            while (neadLoop)
            {
                neadLoop = false;
                for (int i = 0; i < ProjConst.CoreNumber; i++)
                {
                    float sumX = 0;
                    float sumY = 0;
                    int count = 0;
                    for (int j = 0; j < ProjConst.CircleNumber; j++)
                    {
                        if (Program.colors[i] == circles[j].FillColor)
                        {
                            count++;
                            sumX += circles[j].Position.X;
                            sumY += circles[j].Position.Y;
                        }
                    }
                    if (Math.Abs(cores[i].Position.X - sumX / count) > 1 || 
                        Math.Abs(cores[i].Position.Y - sumY / count) > 1)
                    {
                        neadLoop = true;
                    }
                    cores[i].Position = new Vector2f(sumX / count, sumY / count);
                }
                CircleColoring(circles, cores);
            }
        }
    }
}
