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
            // 1. Validar vacíos
            if (string.IsNullOrWhiteSpace(txtFuncionNewton.Text) || string.IsNullOrWhiteSpace(txtVl.Text) || string.IsNullOrWhiteSpace(txtTolNewton.Text))
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 🛡️ Validamos Función y en Punto Fijo validamos también g(x)
            if (!metodos.EsFuncionValida(txtFuncionNewton.Text)) return;

            // 🛡️ Validamos que el punto inicial y la tolerancia no tengan letras
            if (!metodos.SonNumerosValidos(txtVl.Text, "el Valor Inicial (x0)")) return;
            if (!metodos.SonNumerosValidos(txtTolNewton.Text, "la Tolerancia")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolNewton.Text)) return;

            try
            {
                string funcion = txtFuncionNewton.Text;

                // 2. Traductor universal
                double x0 = metodos.ConvertirADouble(txtVl.Text);
                double tol = metodos.ConvertirADouble(txtTolNewton.Text);

                // 🛡️ ¡NUEVO ESCUDO 5.1!: CONTINUIDAD DEL PUNTO 🛡️
                // Evaluamos si la función existe en ese punto x0 antes de derivar
                double f0 = metodos.EvaluarFuncion(funcion, x0);
                if (!metodos.EsPuntoValido(f0)) return;

                // 🛡️ 3. REGLA MATEMÁTICA: DERIVADA CERO 🛡️
                double derivadaX0 = metodos.CalcularDerivada(funcion, x0);

                if (Math.Abs(derivadaX0) < 1e-12) // Si es prácticamente 0
                {
                    MessageBox.Show(
                        "La derivada en el punto inicial x0 = " + x0 + " es cero (o casi cero).\n\n" +
                        "Geométricamente, esto significa que la recta tangente es completamente horizontal y jamás cruzará el eje X. " +
                        "El método fallará por división entre cero.\n\n" +
                        "Revisa la gráfica y elige un punto inicial diferente.",
                        "Falla de Newton-Raphson (Derivada 0)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // 4. Si la tangente está bien, arrancamos el motor
                string raizEncontrada = metodos.NewtonRaphson(funcion, x0, tol, dgvNewton);
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
                metodos.ExportarAExcel(dgvNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla: " + ex.Message);
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            // Solo asegúrate de que el TextBox se llame igual en este form
            string funcion = txtFuncionNewton.Text;

            if (string.IsNullOrWhiteSpace(funcion))
            {
                MessageBox.Show("Escribe una función primero.");
                return;
            }

            // Llamamos a tu obra maestra
            FormGraficador visor = new FormGraficador(funcion);
            visor.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Mandas a llamar a la Escoba Mágica (creas el objeto y lo usas en la misma línea)
            new MetodosNumericos().LimpiarPantalla(
                dgvNewton, // 1. Tu tabla
                new TextBox[] { txtFuncionNewton, txtVl, txtTolNewton }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz } // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}
