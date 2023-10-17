using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace lab1
{
    class Program
    {
        public static List<Color> colors = new List<Color>() { Color.Red, Color.Magenta, Color.Cyan, Color.Blue,
                Color.Yellow, Color.Green, new Color(125, 125, 125), new Color(125, 125, 0) };

        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(ProjConst.WinWidth, ProjConst.WinHight),
                ProjConst.WinTitle);

            window.Position = new Vector2i(0, (int)(1080 / 2 - ProjConst.WinHight / 2));
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

            for (int i = 0; i < ProjConst.CoreNumber; i++)
            {
                CircleShape Circle = new CircleShape(ProjConst.CoreR);
                x = (ushort)random.Next(ProjConst.WinWidth);
                y = (ushort)random.Next(ProjConst.WinHight);
                Circle.FillColor = Color.Black;
                Circle.Position = new Vector2f(x, y);

                cores.Add(Circle);
            }

            Functions.CircleColoring(circles, cores);

            window.Clear(Color.White);

            foreach (var circle in circles)
            {
                window.Draw(circle);
            }

            foreach (var circle in cores)
            {
                window.Draw(circle);
            }

            window.Display();

            Functions.CoresMoving(circles, cores);

            RenderWindow resultWindow = new RenderWindow(new VideoMode(ProjConst.WinWidth, ProjConst.WinHight),
                ProjConst.ResultWinTitle);
            resultWindow.Position = new Vector2i(window.Position.X + ProjConst.WinWidth, window.Position.Y);
            resultWindow.Clear(Color.White);

            foreach (var circle in circles)
            {
                resultWindow.Draw(circle);
            }

            foreach (var circle in cores)
            {
                resultWindow.Draw(circle);
            }

            resultWindow.Display();

            while (window.IsOpen && resultWindow.IsOpen)
            {
                window.DispatchEvents();
                resultWindow.DispatchEvents();
            }
        }
    }
}