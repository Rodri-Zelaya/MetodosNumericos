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
    public partial class FormSecante : Form
    {
        public FormSecante()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Validar vacíos (Aquí usamos x0 y x1)
            if (string.IsNullOrWhiteSpace(txtFuncionSecante.Text) || string.IsNullOrWhiteSpace(txtVI.Text) ||
                string.IsNullOrWhiteSpace(txtV2.Text) || string.IsNullOrWhiteSpace(txtTolSecante.Text))
            {
                MessageBox.Show("Llena todos los campos bro.");
                return;
            }

            try
            {
                MetodosNumericos metodos = new MetodosNumericos();
                string funcion = txtFuncionSecante.Text;

                // 2. Traductor universal
                double x0 = metodos.ConvertirADouble(txtVI.Text);
                double x1 = metodos.ConvertirADouble(txtV2.Text);
                double tol = metodos.ConvertirADouble(txtTolSecante.Text);

                // 🛡️ 3. REGLA MATEMÁTICA: SECANTE HORIZONTAL 🛡️
                double f0 = metodos.EvaluarFuncion(funcion, x0);
                double f1 = metodos.EvaluarFuncion(funcion, x1);

                if (Math.Abs(f0 - f1) < 1e-12) // Si dan el mismo resultado
                {
                    MessageBox.Show(
                        "Bro, la función evaluada en x0 y x1 da el mismo resultado (f(x) = " + f0.ToString("G4") + ").\n\n" +
                        "Esto crea una recta secante horizontal que nunca cruzará el eje X, provocando una división entre cero.\n\n" +
                        "Ingresa valores iniciales diferentes.",
                        "Falla de Secante (Recta Horizontal)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // 4. Si hay inclinación, arrancamos el motor
                string raizEncontrada = metodos.Secante(funcion, x0, x1, tol, dgvSecante);
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
                metodos.ExportarAExcel(dgvSecante);
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
                dgvSecante, // 1. Tu tabla
                new TextBox[] { txtFuncionSecante, txtVI, txtV2, txtTolSecante }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz } // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}
