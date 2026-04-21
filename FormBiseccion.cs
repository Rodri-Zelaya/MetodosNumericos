using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormBiseccion : Form
    {
        public FormBiseccion()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFuncionBiseccion.Text) || string.IsNullOrWhiteSpace(txtA.Text) ||
        string.IsNullOrWhiteSpace(txtB.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos.");
                return;
            }

            try
            {
                string funcion = txtFuncionBiseccion.Text;
                double a = double.Parse(txtA.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double b = double.Parse(txtB.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();
                metodos.Biseccion(funcion, a, b, tol, dgvBiseccion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de cálculo: " + ex.Message);
            }
        }

        private void FormBiseccion_Load(object sender, EventArgs e)
        {

        }
    }
}

//Probando que el push funcione bien y suba al git hub los cambios  
//Probando el repositorio secundario New Implementations 