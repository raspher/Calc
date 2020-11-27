using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Calc
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Display.Text = "0";
        }

        public static class Calc
        {
            public static double result = 0, helper = 0;
            public static bool lastButtonWasAction = true;
            public static string action = "=";
        }

        private void Button_Click_Num(object sender, RoutedEventArgs e)
        {
            if (Display.Text == "0" || Calc.lastButtonWasAction)
                Display.Text = (sender as Button).Content.ToString();
            else
                Display.Text += (sender as Button).Content.ToString();

            Calc.lastButtonWasAction = false;
        }

        private void Button_Click_Dot(object sender, RoutedEventArgs e)
        {
            if (!Display.Text.Contains(".") && !Calc.lastButtonWasAction)
                Display.Text += (sender as Button).Content.ToString();
        }

        private void Button_Click_Clean(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            Calc.result = 0;
            Calc.helper = 0;
            Calc.lastButtonWasAction = true;
            Calc.action = "=";
        }

        private void Read()
        {
            if (Calc.action == "=")
                Calc.result = Convert.ToDouble(Display.Text.ToString());
            else
                Calc.helper = Convert.ToDouble(Display.Text.ToString());
        }

        private void Calculate()
        {
            Calc.lastButtonWasAction = true;

            if (Calc.action == "+")
            {
                Calc.result += Calc.helper;
            }
            if (Calc.action == "-")
            {
                Calc.result -= Calc.helper;
            }
            if (Calc.action == "×")
            {
                Calc.result *= Calc.helper;
            }
            if (Calc.action == "÷")
            {
                if (Calc.helper != 0)
                    Calc.result /= Calc.helper;
                else
                {
                    DivideByZeroException divideByZeroException = new DivideByZeroException();
                    throw divideByZeroException;
                }
            }

            Calc.lastButtonWasAction = true;
        }

        private void Button_Click_Action(object sender, RoutedEventArgs e)
        {
            try
            {
                Read();
                Calculate();
                Display.Text = Calc.result.ToString();
                Calc.action = (sender as Button).Content.ToString();
            }
            
            catch (Exception ex)
            {
                Display.Text = ex.Message;
                Calc.action = "=";
                Calc.lastButtonWasAction = true;
            }

            
        }

        private void Button_Click_Special_Action(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as Button).Content.ToString() == "√")
                {
                    double P = Convert.ToDouble(Display.Text);
                    if (P < 0)
                    {
                        ArgumentException argumentException = new ArgumentException();
                        throw argumentException;
                    }

                    if (P == 0)
                    {
                        Display.Text = "0";
                        return;
                    }

                    double a = 1, b = P;
                    while (Math.Abs(a - b) >= 0.000000000000001)
                    {
                        a = (a + b) / 2;
                        b = P / a;
                    }

                    Display.Text = a.ToString();
                }
                if ((sender as Button).Content.ToString() == "x²")
                {

                    Display.Text = (Convert.ToDouble(Display.Text) * Convert.ToDouble(Display.Text)).ToString();
                }
                if ((sender as Button).Content.ToString() == "±")
                {
                    Display.Text = (-Convert.ToDouble(Display.Text)).ToString();
                }
            }

            catch (Exception ex)
            {
                Display.Text = ex.Message;
                Calc.lastButtonWasAction = true;
            }
        }
    }
}