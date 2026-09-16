using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace szamologep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            InitializeCalc();
        }
        private bool isLastOperator=false;
        private List<string> szamok = new List<string>();
        private List<string> operatorok = new List<string>();

        private void InitializeCalc()
        {
            for (int i = 0; i < 4; i++)
            {
                ButtonGrid.RowDefinitions.Add(new RowDefinition());
                ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }
            string[,] feliratok = new string[4, 4]
            {
                { "7", "8", "9", "/" },
                { "4", "5", "6", "*" },
                { "1", "2", "3", "-" },
                { "C", "0", "=", "+" }
            };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string label = feliratok[i, j];
                    Button gomb = new Button
                    {
                        Content = feliratok[i, j],
                        FontSize = 20,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(3)
                    };
                    if (char.IsDigit(label[0]))
                    {
                        gomb.Background = Brushes.WhiteSmoke;
                    }
                    else if (label == "C")
                    {
                        gomb.Background = Brushes.IndianRed;
                        gomb.Foreground = Brushes.White;
                    }
                    else
                    {
                        gomb.Background = Brushes.CornflowerBlue;
                        gomb.Foreground = Brushes.White;
                    }
                    gomb.Click += Btn_Click;
                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb, j);
                    ButtonGrid.Children.Add(gomb);
                }
            }
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string output = "";
            Button btn = sender as Button;

            string label = btn.Content.ToString();
            bool isOperator = label == "C" || label == "+" || label == "-" || label == "/" || label == "*" || label == "=";

            if (!isOperator)
            {
                output += label;
                tb_kijelzo.Text += label;
                isLastOperator=false;
            }
            else if (isOperator && !isLastOperator)
            {
                isLastOperator = true;
                switch (label)
                {
                    case "C":
                        tb_kijelzo.Text = "";
                        isLastOperator=false;
                        szamok.Clear();
                        operatorok.Clear();
                        break;
                    case "=":
                        eredmeny();
                        break;
                    default:
                        szamok.Add(output);
                        operatorok.Add(label);
                        output = "";
                        tb_kijelzo.Text += label;
                        break;
                }
            }
        }
        private void eredmeny()
        {
            int eredmeny = 0;
            int a = 0;
            int b = 0;
            for (int i = 1; i < szamok.Count; i++)
            {
                a = Convert.ToInt32(szamok[i - 1]);
                b = Convert.ToInt32(szamok[i]);
                switch (operatorok[i - 1])
                {
                    case "+":
                        eredmeny = a + b;
                        break;
                    case "-":
                        eredmeny = a - b;
                        break;
                    case "*":
                        eredmeny = a * b;
                        break;
                    case "/":
                        eredmeny = a / b;
                        break;
                }
            }
        }
    }
}