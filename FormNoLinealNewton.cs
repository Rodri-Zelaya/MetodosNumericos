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
    public partial class FormNoLinealNewton : Form
    {
        public FormNoLinealNewton()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                string funcF = txtF.Text;
                string funcG = txtG.Text;

                double x0 = double.Parse(txtX0.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double y0 = double.Parse(txtY0.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTol.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                string resultado = metodos.NewtonRaphsonNoLineal(funcF, funcG, x0, y0, tol, dgvNoLinealNewton);
                lblRespuesta.Text = "Intersección: " + resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Revisa la sintaxis de tus funciones bro: " + ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciamos tu clase cerebro donde metimos el código universal
                MetodosNumericos metodos = new MetodosNumericos();

                // 2. Llamamos al método y le mandamos la tabla de esta ventana
                // OJO: Si tu tabla se llama distinto (ej. dgvNewton o dgvBairstow), cámbiale el nombre aquí
                metodos.ExportarAExcel(dgvNoLinealNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }
    }
}
