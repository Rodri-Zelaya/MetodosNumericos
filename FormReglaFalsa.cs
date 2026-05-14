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
            // 1. Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtFuncionReglaFalsa.Text) || string.IsNullOrWhiteSpace(txtA.Text) ||
                string.IsNullOrWhiteSpace(txtB.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 🛡️ CAPA 1: Validamos la Función (que no sea basura)
            if (!metodos.EsFuncionValida(txtFuncionReglaFalsa.Text)) return;

            // 🛡️ CAPA 2: Validamos los Valores Numéricos (a, b y tolerancia)
            if (!metodos.SonNumerosValidos(txtA.Text, "el Valor A")) return;
            if (!metodos.SonNumerosValidos(txtB.Text, "el Valor B")) return;
            if (!metodos.SonNumerosValidos(txtTolerancia.Text, "la Tolerancia")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolerancia.Text)) return;

            try
            {
                string funcion = txtFuncionReglaFalsa.Text;

                // 3. Usamos el traductor universal (¡Adiós a los Replace y CultureInfo aquí!)
                double a = metodos.ConvertirADouble(txtA.Text);
                double b = metodos.ConvertirADouble(txtB.Text);
                double tol = metodos.ConvertirADouble(txtTolerancia.Text);

                // 4. Preparamos mXparser para el Teorema de Bolzano
                org.mariuszgromada.math.mxparser.Argument argX = new org.mariuszgromada.math.mxparser.Argument("x");
                org.mariuszgromada.math.mxparser.Expression expr = new org.mariuszgromada.math.mxparser.Expression(funcion, argX);

                argX.setArgumentValue(a);
                double fa = expr.calculate();

                argX.setArgumentValue(b);
                double fb = expr.calculate();

                // 🛡️ ¡NUEVO ESCUDO!: CONTINUIDAD Y DIVISIÓN POR CERO 🛡️
                // Lo ponemos aquí para que atrape el NaN antes de que Bolzano se confunda
                if (!metodos.EsEvaluacionValida(fa, fb)) return;

                // 🛡️ 5. REGLA DE BOLZANO (El Cadenero) 🛡️
                if (fa * fb > 0)
                {
                    MessageBox.Show(
                        "La función no cambia de signo en el intervalo [" + a + ", " + b + "].\n\n" +
                        "f(" + a + ") = " + fa + "\n" +
                        "f(" + b + ") = " + fb + "\n\n" +
                        "Revisa la gráfica y elige un intervalo donde la curva cruce el eje X.",
                        "Error de Intervalo (Bolzano)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return; // Bloquea el paso, no se hace la tabla
                }
                else if (fa == 0 || fb == 0)
                {
                    MessageBox.Show(
                        "¡Uno de los límites ya es la raíz exacta! No hay necesidad de hacer iteraciones.",
                        "Raíz Encontrada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // 6. Si Bolzano da luz verde, llamamos al motor matemático
                string raizEncontrada = metodos.ReglaFalsa(funcion, a, b, tol, dgvReglaFalsa);
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
                metodos.ExportarAExcel(dgvReglaFalsa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla: " + ex.Message);
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            // Solo asegúrate de que el TextBox se llame igual en este form
            string funcion = txtFuncionReglaFalsa.Text;

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
                dgvReglaFalsa, // 1. Tu tabla
                new TextBox[] { txtFuncionReglaFalsa, txtA, txtB, txtTolerancia }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz } // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}
