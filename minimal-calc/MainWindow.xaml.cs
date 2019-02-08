using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace minimal_calc
{
    public partial class MainWindow : Window
    {

        Double Result = 0;
        String Operation = "";
        bool enterValue = false;


        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Content = DateTime.Now.ToShortTimeString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Method to make form movable or mouse left click
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void clickButton(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            /*
             * On first button click event,
             * To Concatenate clicked button string,
             * Initialized 0 should have to be removed
             */
            if ((lowerBlock.Text == "0") || (enterValue))
            {
                lowerBlock.Text = "";
                enterValue = false;
            }

            /*
             * if clicked button is a decimal point,
             * then concatenate that if and only if 
             * lowerBlock text doesn't contain any decimal point
             * being clicked previously
             */
            if ((textTrimmer(b.ToolTip.ToString()) == "."))
            {
                if (!lowerBlock.Text.Contains("."))
                {
                    lowerBlock.Text += textTrimmer(b.ToolTip.ToString());
                }
            }
            else
            {
                lowerBlock.Text += textTrimmer(b.ToolTip.ToString());
            }

        }

        private string textTrimmer(String iconToolTipValue)
        {
            return iconToolTipValue[iconToolTipValue.Length - 1].ToString();
        }

        private void operatorClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            /*
             * L95: Operation = previousPressed, Result = previousCalculated
             * L96: to Make Lower text box null, else it will concat with existing Result text
             * L97: Get newly clicked operation from b object
             * L98: Set upperBlock Text
             */

            if (Result != 0)
            {
                BtnEquals_Click(this, null);
                enterValue = true;
                Operation = textTrimmer(b.ToolTip.ToString());
                upperBlock.Text = System.Convert.ToString(Result) + "   " + Operation;
            }

            /*
             * L110: When Result = 0, Get newly clicked operation
             * L111: Send existing lowerTextbox value to Result as Result = 0
             * L112: Make Lower Text box null to concat new text
             * L113: Set upper block text
             */

            else
            {
                Operation = textTrimmer(b.ToolTip.ToString());
                Result = (Double.Parse(lowerBlock.Text));
                lowerBlock.Text = "";
                upperBlock.Text = System.Convert.ToString(Result) + "   " + Operation;
            }
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            switch (Operation)
            {
                case "+":
                    lowerBlock.Text = (Result + Double.Parse(lowerBlock.Text)).ToString();
                    break;
                case "-":
                    lowerBlock.Text = (Result - Double.Parse(lowerBlock.Text)).ToString();
                    break;
                case "*":
                    lowerBlock.Text = (Result * Double.Parse(lowerBlock.Text)).ToString();
                    break;
                case "/":
                    lowerBlock.Text = (Result / Double.Parse(lowerBlock.Text)).ToString();
                    break;
            }
            Result = Double.Parse(lowerBlock.Text);
            Operation = "";
            upperBlock.Text = "";
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            lowerBlock.Text = "";
            upperBlock.Text = "";
            Operation = "";
            enterValue = false;
            Result = 0;
            lowerBlock.Text = "0";
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (lowerBlock.Text.Length > 0)
            {
                lowerBlock.Text = lowerBlock.Text.Remove(lowerBlock.Text.Length - 1, 1);
            }

            if (lowerBlock.Text == "")
            {
                lowerBlock.Text = "0";
            }
        }
    }
}
