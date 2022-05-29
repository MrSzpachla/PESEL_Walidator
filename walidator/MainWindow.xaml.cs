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

namespace walidator
{
    public partial class MainWindow : Window
    {
        
        String tak = "141541653";
        PESELWalidator pESELWalidator;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Enter_Click(object sender, RoutedEventArgs e)
        {
            pESELWalidator = new PESELWalidator(Input_Pesel.Text);
            if (pESELWalidator.poprawny == true)
            {   
                Wynik.Content = pESELWalidator.ZwrocPesel();
                Data.Content = pESELWalidator.DataUrodzenia();
                Plec.Content = pESELWalidator.Plec();
                Suma.Content = pESELWalidator.SumaKontrolna();
            }
            else
            {
                MessageBox.Show("Pesel jest niepoprawny!", "PESEL Walidator", MessageBoxButton.OK, MessageBoxImage.Warning);
                Input_Pesel.Text = "";
            }
        }

        private void Input_Pesel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    public class PESELWalidator
    {
        protected int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        protected int[] pesel;
        protected string[] miesiac = { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
        public bool poprawny;


        public PESELWalidator(String wynik)
        {
            WczytajPesel(wynik);
        }

        public void WczytajPesel(String wynik)
        {
            
            char[] characters = wynik.ToCharArray();
            if(SprawdzPesel(characters) == true)
                {
                    pesel = characters.Select(c => Convert.ToInt32(c.ToString())).ToArray();
                }
           
        }
        public string ZwrocPesel()
        {
            string a = "";
            for(int i = 0; i < pesel.Length; i++)
            {
                a += pesel[i].ToString();
            }
            return a;
        }

        public int SumaKontrolna()
        {
            int kontrolna = 0;
            int num = 0;
            for (int i = 0; i <= 9; i++)
            {
                int suma = pesel[num] * wagi[num];
                if (suma >= 10)
                {
                    kontrolna += suma % 10;
                }
                else
                {
                    kontrolna += suma;
                }
                num++;
            }
            if (kontrolna >= 10)
            {
                kontrolna = 10 - (kontrolna % 10);
            }
            else
            {
                kontrolna = 10 - kontrolna;
            }
            return kontrolna;
        }

        public String DataUrodzenia()
        {
            String calosc = "";
            String mies = "";
            calosc += pesel[4] + "" + pesel[5] + " ";
            calosc += miesiac[pesel[2]+pesel[3]-1]+" ";
            if (pesel[2] == 8 || pesel[2] == 9)
            {
                calosc += "18";
            }
            else if (pesel[2] == 0 || pesel[2] == 1)
            {
                calosc += "19";
            }
            else if(pesel[2] == 2 || pesel[2] == 3)
            {
                calosc += "20";
            }
            calosc += pesel[0] +""+ pesel[1]+" r.";
            return calosc;
        }

        public String Plec()
        {
            if (pesel[9] % 2 == 1)
            {
                return "Mężczyzna";
            }
            else
            {
                return "Kobieta";
            }
        }

        public Boolean SprawdzPesel(Array cpesel)
        {
            if (cpesel.Length == 11)
            {
                foreach (char c in cpesel)
                {
                    if (c < '0' || c > '9')
                    {
                        poprawny = false;
                        return false;
                    }
                }
                poprawny = true;
                return true;
            }
            poprawny = false;
            return false;
        }

    }
}
