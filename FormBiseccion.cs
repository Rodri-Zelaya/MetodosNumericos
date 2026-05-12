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
            // 1. Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtFuncionBiseccion.Text) || string.IsNullOrWhiteSpace(txtA.Text) ||
                string.IsNullOrWhiteSpace(txtB.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 🛡️ CAPA 1: Validamos la Función (que no sea basura)
            if (!metodos.EsFuncionValida(txtFuncionBiseccion.Text)) return;

            // 🛡️ CAPA 2: Validamos los Valores Numéricos (a, b y tolerancia)
            if (!metodos.SonNumerosValidos(txtA.Text, "el Valor A")) return;
            if (!metodos.SonNumerosValidos(txtB.Text, "el Valor B")) return;
            if (!metodos.SonNumerosValidos(txtTolerancia.Text, "la Tolerancia")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolerancia.Text)) return;

            try
            {
                string funcion = txtFuncionBiseccion.Text;

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
                string raizEncontrada = metodos.Biseccion(funcion, a, b, tol, dgvBiseccion);
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
                metodos.ExportarAExcel(dgvBiseccion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla: " + ex.Message);
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            string funcion = txtFuncionBiseccion.Text;

            if (string.IsNullOrWhiteSpace(funcion))
            {
                MessageBox.Show("Escribe una función primero.");
                return;
            }

            // 1. Creamos la ventana graficadora y le pasamos la función
            FormGraficador visor = new FormGraficador(funcion);

            // 2. Usamos ShowDialog() para que la ventana se abra como un Pop-up 
            // y el usuario no pueda tocar la tabla hasta que cierre la gráfica
            visor.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Mandas a llamar a la Escoba Mágica (creas el objeto y lo usas en la misma línea)
            new MetodosNumericos().LimpiarPantalla(
                dgvBiseccion, // 1. Tu tabla
                new TextBox[] { txtFuncionBiseccion, txtA, txtB, txtTolerancia }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz } // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}

//Probando que el push funcione bien y suba al git hub los cambios  
//Probando el repositorio secundario New Implementations 