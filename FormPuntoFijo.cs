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
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 🛡️ Validamos Función y en Punto Fijo validamos también g(x)
            if (!metodos.EsFuncionValida(txtFuncionPuntoFijo.Text)) return;

            // 🛡️ Validamos que el punto inicial y la tolerancia no tengan letras
            if (!metodos.SonNumerosValidos(txtX0PuntoFijo.Text, "el Valor Inicial (x0)")) return;
            if (!metodos.SonNumerosValidos(txtTolPuntoFijo.Text, "la Tolerancia")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolPuntoFijo.Text)) return;

            try
            {
                string funcionG = txtFuncionPuntoFijo.Text;

                // 2. Usamos tu traductor universal
                double x0 = metodos.ConvertirADouble(txtX0PuntoFijo.Text);
                double tol = metodos.ConvertirADouble(txtTolPuntoFijo.Text);

                // 🛡️ 3. REGLA MATEMÁTICA: CRITERIO DE CONVERGENCIA 🛡️
                // Calculamos la derivada de g(x) en el punto inicial x0
                double derivadaG = metodos.CalcularDerivada(funcionG, x0);

                if (Math.Abs(derivadaG) >= 1)
                {
                    // Freno automático con mensaje directo
                    MessageBox.Show(
                        "La derivada de tu g(x) en el punto inicial es mayor o igual a 1 (|g'(x0)| = " + Math.Abs(derivadaG).ToString("F4") + ").\n\n" +
                        "Matemáticamente, esto garantiza que el método va a DIVERGER (los valores explotarán y se alejarán de la raíz).\n\n" +
                        "Para solucionar esto, despeja la 'x' de tu ecuación original de una forma distinta para obtener una nueva g(x) y vuelve a intentar.",
                        "Divergencia Detectada (Punto Fijo)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error); // Cambiado a ícono de Error porque no lo vamos a dejar pasar

                    return; // Freno de mano absoluto, no sigue calculando nada
                }

                // 4. Arrancamos el motor (Solo llegará aquí si la derivada es < 1)
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
                dgvPuntoFijo, // 1. Tu tabla
                new TextBox[] { txtFuncionPuntoFijo, txtX0PuntoFijo, txtTolPuntoFijo }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz } // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}
