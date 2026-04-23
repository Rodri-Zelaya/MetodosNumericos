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
    public partial class FormReglaFalsa : Form
    {
        public FormReglaFalsa()
        {
            InitializeComponent();
        }

        private void FormReglaFalsa_Load(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFuncionReglaFalsa.Text) || string.IsNullOrWhiteSpace(txtA.Text) ||
        string.IsNullOrWhiteSpace(txtB.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Llena todos los campos bro.");
                return;
            }

            try
            {
                string funcion = txtFuncionReglaFalsa.Text;
                double a = double.Parse(txtA.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double b = double.Parse(txtB.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                // Atrapamos la raíz y la mandamos al Label
                string raizEncontrada = metodos.ReglaFalsa(funcion, a, b, tol, dgvReglaFalsa);
                lblRaiz.Text = "Raíz: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en los datos o en la función: " + ex.Message);
            }
        }
    }
}
