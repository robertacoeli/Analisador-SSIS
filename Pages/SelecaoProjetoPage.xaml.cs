using AnalisadorSSIS.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace AnalisadorSSIS.Pages
{
    /// <summary>
    /// Interaction logic for SelecaoProjetoPage.xaml
    /// </summary>
    public partial class SelecaoProjetoPage : Page
    {
        Solucao solucao;

        public SelecaoProjetoPage()
        {
            InitializeComponent();
        }

        public SelecaoProjetoPage(object data) : this()
        {
            DataContext = data;
            solucao = (Solucao)data;
            ListaProjetos.ItemsSource = solucao.projetos.Select(x => System.IO.Path.GetFileName(x));
        }
    }
}
