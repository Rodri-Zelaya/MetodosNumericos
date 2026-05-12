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
    public partial class FormHornerNewton : Form
    {
        public FormHornerNewton()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Verificamos que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtCoeficientes.Text) || string.IsNullOrWhiteSpace(txtR0.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Pon los coeficientes bro, separados por espacios, y llena todos los campos.");
                return;
            }

            try
            {
                // Instanciamos tu clase cerebro PRIMERO para poder usar el traductor
                MetodosNumericos metodos = new MetodosNumericos();

                // 2. Usamos el traductor universal (adiós a los Replace raros)
                double r0 = metodos.ConvertirADouble(txtR0.Text);
                double tol = metodos.ConvertirADouble(txtTolerancia.Text);

                // Partimos el texto en pedacitos cada vez que haya un espacio
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1; // Calculamos el grado del polinomio

                // 🛡️ REGLA MATEMÁTICA 1: Grado mínimo
                if (n < 1)
                {
                    MessageBox.Show("Necesitas al menos un polinomio de grado 1 bro.");
                    return;
                }

                // Acomodamos los números de izquierda a derecha (an hasta a0)
                double[] a = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    a[n - i] = metodos.ConvertirADouble(partes[i]);
                }

                // 🛡️ REGLA MATEMÁTICA 2: Coeficiente principal no nulo
                if (a[n] == 0)
                {
                    MessageBox.Show("El primer coeficiente no puede ser cero bro.");
                    return;
                }

                // 🛡️ REGLA MATEMÁTICA 3: DERIVADA CERO EN HORNER (El Escudo) 🛡️
                // Simulamos la primera división sintética rápida para sacar la derivada en r0
                double[] b = new double[n + 1];
                b[n] = a[n];
                for (int i = n - 1; i >= 0; i--) b[i] = a[i] + r0 * b[i + 1];

                double[] c = new double[n + 1];
                c[n] = b[n];
                for (int i = n - 1; i >= 1; i--) c[i] = b[i] + r0 * c[i + 1];

                // c[1] es la derivada. Si es cero, la tangente es horizontal y explota.
                if (Math.Abs(c[1]) < 1e-12)
                {
                    MessageBox.Show(
                        "Bro, la derivada del polinomio en el punto inicial r0 = " + r0 + " es cero.\n\n" +
                        "Geométricamente, la tangente es horizontal y causará una división entre cero en la primera iteración.\n" +
                        "Intenta con un punto inicial diferente.",
                        "Falla (Tangente Horizontal)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Si pasó todos los escudos, mandamos la info al motor matemático
                string raizEncontrada = metodos.HornerNewton(a, r0, tol, dgvHornerNewton);
                lblRaiz.Text = "Raíz encontrada: " + raizEncontrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica los datos. Recuerda poner los coeficientes separados por espacios: " + ex.Message);
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
                metodos.ExportarAExcel(dgvHornerNewton);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla, bro: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Mandas a llamar a la Escoba Mágica (creas el objeto y lo usas en la misma línea)
            new MetodosNumericos().LimpiarPantalla(
                dgvHornerNewton, // 1. Tu tabla
                new TextBox[] { txtCoeficientes, txtTolerancia, txtR0 }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz} // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}
