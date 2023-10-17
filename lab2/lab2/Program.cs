using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Diagnostics.CodeAnalysis;

namespace lab2
{
    internal class Program
    {
        public static List<Color> colors = new List<Color>() { Color.Red, Color.Magenta, Color.Cyan, Color.Blue,
                Color.Yellow, Color.Green, new Color(125, 125, 125), new Color(125, 125, 0), new Color(125, 0, 0) };

        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(ProjConst.WinWidth, ProjConst.WinHight),
                ProjConst.WinTitle);

            window.SetVerticalSyncEnabled(true);
            window.Closed += (sender, args) => window.Close();

            List<CircleShape> circles = new List<CircleShape>();
            List<CircleShape> cores = new List<CircleShape>();

            Random random = new Random();
            int x, y;

            for (int i = 0; i < ProjConst.CircleNumber; i++)
            {
                CircleShape Circle = new CircleShape(ProjConst.CircleR);
                x = (ushort)random.Next(ProjConst.WinWidth);
                y = (ushort)random.Next(ProjConst.WinHight);
                Circle.FillColor = Color.Blue;
                Circle.Position = new Vector2f(x, y);

                circles.Add(Circle);
            }

            //Find first core
            int newCore = random.Next(ProjConst.CircleNumber);
            cores.Add(circles[newCore]);

            int maxCount = 0;
            int maxNum = 0;

            //Find second core
            for (int i = 0; i < ProjConst.CircleNumber; i++)
            {
                int temp = (int)Math.Sqrt(Math.Pow(circles[i].Position.X - cores[0].Position.X, 2) +
                        Math.Pow(circles[i].Position.Y - cores[0].Position.Y, 2));

                if (temp > maxCount)
                {
                    maxNum = i;
                    maxCount = temp;
                }
            }

            cores.Add(circles[maxNum]);

            bool flag = true;
            while (flag)
            {
                flag = false;
                Functions.CircleColoring(circles, cores);

                int deltaCount = 0;
                int deltaNum = 0;

                //Find delta in classes
                for (int i = 0; i < cores.Count; i++)
                {
                    maxCount = 0;
                    maxNum = 0;

                    for (int j = 0; j < ProjConst.CircleNumber; j++)
                    {
                        if (colors[i] == circles[j].FillColor)
                        {
                            int temp = (int)Math.Sqrt(Math.Pow(circles[j].Position.X - cores[i].Position.X, 2) +
                            Math.Pow(circles[j].Position.Y - cores[i].Position.Y, 2));

                            if (temp > maxCount)
                            {
                                maxNum = j;
                                maxCount = temp;
                            }
                        }
                    }

                    if (maxCount > deltaCount)
                    {
                        deltaCount = maxCount;
                        deltaNum = maxNum;
                    }
                }

                int sum = 0;
                int sumcount = 0;

                for (int i = 0; i < cores.Count - 1; i++)
                {
                    for (int j = i + 1; j < cores.Count; j++)
                    {
                        sumcount++;
                        sum += (int)Math.Sqrt(Math.Pow(cores[j].Position.X - cores[i].Position.X, 2) +
                            Math.Pow(cores[j].Position.Y - cores[i].Position.Y, 2));
                    }
                }

                if (deltaCount > sum / sumcount / 2)
                {
                    cores.Add(circles[deltaNum]);
                    flag = true;
                }
            }

            window.Clear(Color.White);

            foreach (var circle in circles)
            {
                window.Draw(circle);
            }

            foreach (var circle in cores)
            {
                circle.FillColor = Color.Black;
                circle.Radius = ProjConst.CoreR;
                window.Draw(circle);
            }

            window.Display();

            while (window.IsOpen)
            {
                window.DispatchEvents();
            }
        }
    }
}