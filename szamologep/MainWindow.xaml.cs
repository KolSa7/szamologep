using System.Reflection.Emit;
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
    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            InitializeCalc();
        }
        private bool isLastOperator=true;
        private string temp = "";

        private List<string> szamok=new List<string>();

        private void InitializeCalc()
        {
            for (int i = 0; i < 4; i++)
            {
                ButtonGrid.RowDefinitions.Add(new RowDefinition());
                ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }
            string[,] feliratok = new string[4, 4]
            {
                { "7", "8", "9", "*" },
                { "4", "5", "6", "+" },
                { "1", "2", "3", "-" },
                { "C", "0", "/", "=" }
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
            
            Button btn = sender as Button;

            string label = btn.Content.ToString();
            bool isOperator = label == "C" || label == "+" || label == "-" || label == "/" || label == "*" || label == "=";

            if (!isOperator)
            {
                if (temp.Length != 0 && label != "0")
                {
                    temp += label;
                    tb_kijelzo.Text += label;
                    isLastOperator = false;
                }
            }
            else if (isOperator && !isLastOperator)
            {
                szamok.Add(temp);
                
                switch (label)
                {
                    case "C":
                        szamok.Clear();
                        tb_kijelzo.Text = "";
                        temp = "";
                        break;
                    case "=":
                        if (temp.Length!=0) 
                        { 
                            eredmeny();
                        }
                        break;
                    default:
                        isLastOperator = true;
                        szamok.Add(label);
                        temp = "";
                        tb_kijelzo.Text += label;
                        break;
                }
            }
        }
        private void eredmeny()
        {
            string ops = "+-*/";
            int eredmeny = int.Parse(szamok[0]);
            for (int i=1; i< szamok.Count-1; i++)
            {
                if (ops.Contains(szamok[i]))
                {
                    switch (szamok[i])
                    {
                        case "+":
                            eredmeny += int.Parse(szamok[i+1]);
                            break;
                        case "-":
                            eredmeny -= int.Parse(szamok[i+1]);
                            break;
                        case "*":
                            eredmeny *= int.Parse(szamok[i+1]);
                            break;
                        case "/":
                            eredmeny /= int.Parse(szamok[i+1]);
                            break;
                    }
                }
            }
            tb_kijelzo.Text =eredmeny.ToString();
            temp = "";
            szamok.Clear();
        }
    }
}