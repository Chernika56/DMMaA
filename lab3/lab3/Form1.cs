using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public static double ValueOfCrossX;
        public static List<double> density1 = new List<double>();
        public static List<double> density2 = new List<double>();

        public Form1()
        {
            InitializeComponent();

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();

            List<int> X1 = new List<int>();
            List<int> X2 = new List<int>();
            int ChartWidth = chart1.Width;
            
            Functions.Calculating(X1, X2, density1, density2, out ValueOfCrossX, ChartWidth);

            for (int i = 0; i < ChartWidth; i++)              
            {
                chart1.Series[0].Points.AddXY(i, density1[i]);
                chart1.Series[1].Points.AddXY(i, density2[i]);
            }

            chart1.Series[2].Points.AddXY(ValueOfCrossX, 0);
            chart1.Series[2].Points.AddXY(ValueOfCrossX, 0.0015);
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            double falseAlarmZone = 0;
            double missZone = 0;
            double sumOfErrors = 0;

            falseAlarmZone = density2.Take((int)ValueOfCrossX).Sum();
            missZone = ProjConst.Pc1 > ProjConst.Pc2 ? density2.Skip((int)ValueOfCrossX).Sum() :
                density1.Skip((int)ValueOfCrossX).Sum();

            MessageBox.Show($"False alarm zone: {Math.Round(falseAlarmZone, 5)}\n" +
                                $"Detection pass zone: {Math.Round(missZone, 5)}\n" +
                                $"Sum of errors: {Math.Round(missZone + falseAlarmZone, 5)}");
        }
    }
}
