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



namespace SimpleCalculator
{
    //
    // Code behind for the main Calculator WIndow
    // This window takes input values and a list value to determine how much you could save by making regular contributions to an account
    // It also has a radio button that controls an image only (for now)
    // A help button brings up a help window and an exit button ends execution.
    //
    public partial class CalculatorWindow : Window
    {
        decimal _BegBal;
        decimal _Contrib;
        double _Freq;
        decimal _Rate;
        bool _validinput;
        string _errormsg;

        public CalculatorWindow()
        {
            InitializeComponent();
            InitializeWindow();
        }
        private void InitializeWindow()
        {
            //
            // set initial value for radio box selection
            //
            RadioButton_Yen.IsChecked = true;
            RadioButton_USD.IsChecked = false;
        }


        private void Get_Operands()
        {
            //
            // take all the inputs from the screen and validate them according to their data type
            //
            
            _validinput = true;
            _errormsg = "";
            if (decimal.TryParse(TextBox_BegBal.Text, out _BegBal))
            { }
            else
            {
                _validinput = false;
                _errormsg = "Beginning Balance must be an integer";
            };
            if (decimal.TryParse(TextBox_Contrib.Text, out _Contrib))
            { }
            else
            {
                _validinput = false;
                _errormsg = "Contribution must be an integer";
            };
            if (double.TryParse(TextBox_Freq.Text, out _Freq))
            { }
            else
            {
                _validinput = false;
                _errormsg = "Frequency must be an integer";
            };
            if (decimal.TryParse(TextBox_Rate.Text, out _Rate))
            { }
            else
            {
                _validinput = false;
                _errormsg = "Rate is not a valid decimal";
            };

            //
            // If any data is invalid, show an error message in a dialogue box. Input is all verified; however, the error message only shows 
            // what the last error found was. Next version will show all errors
            //

            if (!_validinput)
            {
                
                 MessageBox.Show(_errormsg, "Input Error");

            }

         }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            Get_Operands();

            if (_validinput)
            {
                //
                // The user provides annual interest rate. It is changed to a monthly rate (/ 12). 1 is added to avoid an extra calculation 
                // in another line. If you earn 1% in a month on $100, you'd have $100 * 1.01 after a month.
                //
                decimal _ratemult = (1 + (_Rate / 100/12));
                decimal compoundbal = System.Convert.ToDecimal(_BegBal);
                decimal sumcontrib = compoundbal;
                double periods=_Freq;
                string frequency = ComboBox_Freq.SelectionBoxItem as string;
                //
                // the combobox allows months or years to be selected. Count=2 for years so multiply frequency by 12
                //
                if (frequency == "Years") 
                {
                    periods = _Freq * 12;
                }
                
                for (int i = 0; i < periods; i++)
                {
                    sumcontrib = sumcontrib + _Contrib;
                    compoundbal = compoundbal + _Contrib;
                    compoundbal = (compoundbal * _ratemult);
                }
                
                Label_Saved_Amt.Content = sumcontrib.ToString("###,###,###.##");
                Label_Balance_Amt.Content = compoundbal.ToString("###,###,###.##");
                

            }
        }

        private void Button_HelpButton_Click(object sender, RoutedEventArgs e)
        {
             
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();



        }

        private void Button_ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    


        private void RadioButton_Currency_Checked(object sender, RoutedEventArgs e)
        {
            
            RadioButton radioButton = sender as RadioButton;
            string imageChoice = radioButton.Name;

            switch (imageChoice)
            {
                case "RadioButton_USD":
                    CurrencyImage.Source = new BitmapImage(new Uri(@"DollarImage.png", UriKind.Relative));
                    break;

                case "RadioButton_Yen":
                    CurrencyImage.Source = new BitmapImage(new Uri(@"YenImage.png", UriKind.Relative));
                    break;

                default:
                    throw new Exception("Unknown Image Choice");
            }


        }
    }
}

