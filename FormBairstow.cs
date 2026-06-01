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
    public partial class FormBairstow : Form
    {
        public FormBairstow()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCoeficientes.Text))
            {
                MessageBox.Show("Pon los coeficientes, separados por espacios.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // Validamos que los coeficientes sean solo números
            if (!metodos.SonNumerosValidos(txtCoeficientes.Text, "los Coeficientes")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolerancia.Text)) return;

            try
            {
                // 1. Preparamos los datos
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1;

                // 🛡️ REGLA MATEMÁTICA 1: Grado del polinomio
                if (n < 3)
                {
                    MessageBox.Show("Matemáticamente Bairstow es para ligas mayores. Solo funciona para polinomios de grado 3 o superior. Si tienes uno de grado 2, usa la fórmula cuadrática.", "Error de Grado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double[] a = new double[n + 1];
                for (int i = 0; i <= n; i++)
                {
                    a[n - i] = double.Parse(partes[i].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                }

                // 🛡️ REGLA MATEMÁTICA 2: Coeficiente principal no nulo
                if (a[n] == 0)
                {
                    MessageBox.Show("El primer coeficiente (el principal) no puede ser cero, bro. Revisa tus números.", "Error de Coeficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double tol = double.Parse(txtTolerancia.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

                // 2. Preparamos variables vacías para atrapar r0 y s0
                double r0_calculado, s0_calculado;

                // 3. Ejecutamos el método
                string resultado = metodos.Bairstow(a, tol, dgvBairstow, out r0_calculado, out s0_calculado);

                // 4. Mostramos resultados en la pantalla
                lblRaiz.Text = "Raíces encontradas: " + resultado;

                // Asegúrate de tener creados estos dos Labels en tu diseño:
                lblR0.Text = "r0 automático: " + r0_calculado.ToString("F4");
                lblS0.Text = "s0 automático: " + s0_calculado.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Revisa la sintaxis: " + ex.Message);
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
                metodos.ExportarAExcel(dgvBairstow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo falló al intentar mandar la tabla: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Mandas a llamar a la Escoba Mágica (creas el objeto y lo usas en la misma línea)
            new MetodosNumericos().LimpiarPantalla(
                dgvBairstow, // 1. Tu tabla
                new TextBox[] { txtCoeficientes, txtTolerancia }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz, lblS0, lblR0 } // 3. (Opcional) Tus labels de resultado
            );
        }

        private void AplicarEstiloTuani(Control.ControlCollection controles)
        {
            this.BackColor = Color.FromArgb(243, 244, 246);
            Color azulOscuro = Color.FromArgb(17, 24, 39);
            Color azulHover = Color.FromArgb(55, 65, 81);

            foreach (Control control in controles)
            {
                if (control is Button btn)
                {
                    // 🛡️ EL FIX PARA LOS BOTONES BLANCOS: Apagamos el estilo nativo de Windows
                    btn.UseVisualStyleBackColor = false;

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = azulOscuro;
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.FlatAppearance.MouseOverBackColor = azulHover;
                    btn.Height = 40;
                }
                else if (control is Label lbl)
                {
                    lbl.ForeColor = Color.Black;
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (control is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }

                // Magia recursiva: Si tienes controles agrupados dentro de un panel, esto los encuentra
                if (control.HasChildren)
                {
                    AplicarEstiloTuani(control.Controls);
                }
            }
        }
    }
}
