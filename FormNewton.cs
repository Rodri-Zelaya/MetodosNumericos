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
    public partial class FormNewton : Form
    {
        public FormNewton()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFuncionNewton.Text) || string.IsNullOrWhiteSpace(txtVI.Text) || string.IsNullOrWhiteSpace(txtTolNewton.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos de Newton-Raphson.");
                return;
            }

            try
            {
                string funcion = txtFuncionNewton.Text;
                double x0 = double.Parse(txtVI.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                double tol = double.Parse(txtTolNewton.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                MetodosNumericos metodos = new MetodosNumericos();

                // Atrapamos la raíz y la mandamos al Label
                string raizEncontrada = metodos.NewtonRaphson(funcion, x0, tol, dgvNewton);
                lblRaiz.Text = "Raíz: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Revisa la sintaxis de la función o los valores: " + ex.Message);
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
                metodos.ExportarAExcel(dgvNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            // Solo asegúrate de que el TextBox se llame igual en este form
            string funcion = txtFuncionNewton.Text;

            if (string.IsNullOrWhiteSpace(funcion))
            {
                MessageBox.Show("Bro, escribe una función primero.");
                return;
            }

            // Llamamos a tu obra maestra
            FormGraficador visor = new FormGraficador(funcion);
            visor.ShowDialog();
        }
    }
}
