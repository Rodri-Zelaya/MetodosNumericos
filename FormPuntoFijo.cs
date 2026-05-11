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
            // 1. Validar vacíos (Asegúrate de que los nombres de los txt coincidan con tu diseño)
            if (string.IsNullOrWhiteSpace(txtFuncionPuntoFijo.Text) || string.IsNullOrWhiteSpace(txtX0PuntoFijo.Text) || string.IsNullOrWhiteSpace(txtTolPuntoFijo.Text))
            {
                MessageBox.Show("Llena todos los campos bro.");
                return;
            }

            try
            {
                MetodosNumericos metodos = new MetodosNumericos();
                string funcionG = txtFuncionPuntoFijo.Text;

                // 2. Usamos tu traductor universal
                double x0 = metodos.ConvertirADouble(txtX0PuntoFijo.Text);
                double tol = metodos.ConvertirADouble(txtTolPuntoFijo.Text);

                // 🛡️ 3. REGLA MATEMÁTICA: CRITERIO DE CONVERGENCIA 🛡️
                // Calculamos la derivada de g(x) en el punto inicial x0
                double derivadaG = metodos.CalcularDerivada(funcionG, x0);

                if (Math.Abs(derivadaG) >= 1)
                {
                    // Usamos un MessageBox con botones Yes/No para darle la opción al usuario
                    DialogResult respuesta = MessageBox.Show(
                        "Bro, la derivada de tu g(x) en el punto inicial es mayor o igual a 1 (|g'(x0)| = " + Math.Abs(derivadaG).ToString("F4") + ").\n\n" +
                        "Matemáticamente, esto significa que el método probablemente va a DIVERGER (los números se alejarán de la raíz).\n" +
                        "Lo ideal es despejar la 'x' de tu ecuación original de una forma distinta para obtener otra g(x).\n\n" +
                        "¿Quieres intentar calcular la tabla de todas formas (bajo tu propio riesgo)?",
                        "Alerta de Divergencia (Punto Fijo)",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.No)
                    {
                        return; // Freno de mano si el usuario decide hacer caso a las matemáticas
                    }
                }

                // 4. Arrancamos el motor (ya sea porque la derivada era < 1, o porque el usuario le dio a "Yes")
                string raizEncontrada = metodos.PuntoFijo(funcionG, x0, tol, dgvPuntoFijo);
                lblRaiz.Text = "Raíz: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de cálculo: " + ex.Message);
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
                metodos.ExportarAExcel(dgvPuntoFijo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            // Solo asegúrate de que el TextBox se llame igual en este form
            string funcion = txtFuncionPuntoFijo.Text;

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
