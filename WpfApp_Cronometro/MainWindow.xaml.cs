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

namespace WpfApp_Cronometro
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        Cronometro _crn = new Cronometro();
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _crn.Start();
            }
            catch (Exception ex)
            {
                MessaggioErrore(ex);
            }
            
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblTempo.Content = _crn.LeggiTempo();
            }
            catch (Exception ex)
            {
                MessaggioErrore(ex);
            }

            
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _crn.Reset();
            }
            catch
            {
                // Non crashare, ma non mostrare nessun messaggio
            }

            lblTempo.Content = "00:00:00";
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _crn.Stop();
            }
            catch (Exception ex)
            {
                MessaggioErrore(ex);
            }
            
        }

        private void btnResetConTempo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _crn.Reset(txtTempo.Text);
            }
            catch (Exception ex)
            {
                MessaggioErrore(ex);
            }
            
        }


        void MessaggioErrore(Exception ex)
        {
            MessageBox.Show("Errore: " +ex.Message);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.R)
                {
                    _crn.Reset();
                }
                if (e.Key == Key.S)
                {
                    _crn.Start();
                }
                if (e.Key == Key.Q)
                {
                    Close();
                }
                if (e.Key == Key.L)
                {
                    btnRead_Click(sender, e);
                }
                if (e.Key == Key.F)
                {
                    btnStop_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                MessaggioErrore(ex);
            }
        }
    }
}
