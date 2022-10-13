using Color = Microsoft.Maui.Graphics.Color;

namespace Tip4Calculator
{
    public partial class MainPage : ContentPage
    {
        private readonly Color _colorNavy = Colors.Navy;
        private readonly Color _colorSilver = Colors.Silver;

        public MainPage()
        {
            InitializeComponent();
            billInput.TextChanged += (s, e) => CalculateTip();
        }

        void CalculateTip()
        {

            if (double.TryParse(billInput.Text, out double bill) && bill > 0)
            {
                double tip = Math.Round(bill * 0.15, 2);
                double final = bill + tip;

                tipOutput.Text = tip.ToString("C");
                totalOutput.Text = final.ToString("C");
            }
        }

        void OnLight(object sender, EventArgs e)
        {
            Resources["fgColor"] = _colorNavy;
            Resources["bgColor"] = _colorSilver;
        }

        void OnDark(object sender, EventArgs e)
        {
            Resources["fgColor"] = _colorSilver;
            Resources["bgColor"] = _colorNavy;
        }

        async void GotoCustom(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CustomTipPage));
        }
    }
}