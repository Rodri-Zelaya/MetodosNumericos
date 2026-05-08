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
                // Leemos las funciones línea por línea ignorando las vacías
                string[] funciones = txtFunciones.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] strValores = txtValoresIniciales.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (funciones.Length != strValores.Length)
                {
                    MessageBox.Show("Bro, la cantidad de funciones debe ser igual a la cantidad de valores iniciales.");
                    return;
                }

                double[] X0 = new double[strValores.Length];
                for (int i = 0; i < strValores.Length; i++)
                {
                    X0[i] = double.Parse(strValores[i].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                }

                double tol = double.Parse(txtTol.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();
                metodos.NewtonRaphsonGeneral(funciones, X0, tol, dgvNoLinealNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica la sintaxis bro. Recuerda escribir cada función en una línea nueva: " + ex.Message);
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
