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

namespace Calculator
{
    /// <summary>
    /// floateraction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Result { get; set; }
        public string Operator { get; set; } = "";

        public string LastValue { get; set; }

        bool clicked = false;
        bool consecutiveEqual = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonClick(object sender, RoutedEventArgs e)
        {
            float num = -1;

            Button tmpButton = e.Source as Button;
            if(float.TryParse(tmpButton.Content.ToString(), out num))
            {
                // Operand
                if (clicked == false)
                {
                    Result = $"{Result}{num.ToString()}";
                    labelResult.Content = Result;
                } else
                {
                    LastValue = Result;
                    Result = num.ToString();
                    labelResult.Content = Result;
                    clicked = false;   
                }
            }
            else
            {
                clicked = true;
                if (!string.IsNullOrEmpty(Result))
                {
                    if (consecutiveEqual == true && tmpButton.Content.ToString() == "=")
                    {
                        Result = PerformAction();
                        labelResult.Content = Result;
                    }
                    else if (tmpButton.Content.ToString() == "=")
                    {
                        var tmp = Result;
                        Result = PerformAction();
                        labelResult.Content = Result;
                        LastValue = tmp;
                        consecutiveEqual = true;
                    }
                    else
                    {
                        Operator = tmpButton.Content.ToString();
                        consecutiveEqual = false;
                    }
                }
            }
        }


        private string PerformAction()
        {
            if(Operator == "+") return (float.Parse(LastValue) + float.Parse(Result)).ToString();
            if(Operator == "x") return (float.Parse(LastValue) * float.Parse(Result)).ToString();
            if (consecutiveEqual == false)
            {
                if (Operator == "-") return (float.Parse(LastValue) - float.Parse(Result)).ToString();
                if (Operator == "/") return (float.Parse(LastValue) / float.Parse(Result)).ToString();
            } else
            {
                if (Operator == "-") return (float.Parse(Result) - float.Parse(LastValue)).ToString();
                if (Operator == "/") return (float.Parse(Result) / float.Parse(LastValue)).ToString();
            }

            return "";
        }

        private void buttonClear(object sender, RoutedEventArgs e)
        {
            Result = "";
            LastValue = "";
            Operator = "";
            consecutiveEqual= false;
            clicked= false;
            labelResult.Content= Result;
        }
    }
}
