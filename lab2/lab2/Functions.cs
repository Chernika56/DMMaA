using SFML.Graphics;

namespace lab2
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

                for (int j = 0; j < cores.Count; j++)
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
    }
}
