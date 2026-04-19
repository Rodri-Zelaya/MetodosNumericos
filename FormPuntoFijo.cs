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
    public partial class FormPuntoFijo : Form
    {
        public FormPuntoFijo()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                string g_x = txtFuncionPuntoFijo.Text;
                double x0 = double.Parse(txtX0PuntoFijo.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolPuntoFijo.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                int maxIter = int.Parse(txtMaxIterPuntoFijo.Text);

                MetodosNumericos metodos = new MetodosNumericos();
                metodos.PuntoFijo(g_x, x0, tol, maxIter, dgvPuntoFijo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
