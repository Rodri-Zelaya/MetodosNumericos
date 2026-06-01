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
            AplicarEstiloTuani(this.Controls);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Verificamos que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtFunciones.Text) || string.IsNullOrWhiteSpace(txtValoresIniciales.Text) || string.IsNullOrWhiteSpace(txtTol.Text))
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 1. Partimos las funciones para revisarlas una por una
            string[] funciones = txtFunciones.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // 🛡️ ESCUDO DE FUNCIONES (Para que no metan basura como "sdfw35356")
            if (!metodos.EsSistemaNoLinealValido(funciones)) return;

            // 🛡️ ESCUDO DE VALORES INICIALES (Para que no metan letras en X0, Y0, etc.)
            if (!metodos.SonNumerosValidos(txtValoresIniciales.Text, "los Valores Iniciales")) return;

            if (!metodos.SonNumerosValidos(txtTol.Text, "la Tolerancia")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTol.Text)) return;

            try
            {

                // 3. Leemos los valores iniciales (uno por línea según tu diseño)
                string[] strValores = txtValoresIniciales.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // 🛡️ REGLA MATEMÁTICA 1: SISTEMA CUADRADO (NxN)
                if (funciones.Length != strValores.Length)
                {
                    MessageBox.Show($"Pusiste {funciones.Length} funciones pero {strValores.Length} valores iniciales.\n\nMatemáticamente el sistema debe ser cuadrado (NxN). La cantidad debe ser igual.", "Error de Dimensión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Traducimos los valores iniciales con el método seguro
                double[] X0 = new double[strValores.Length];
                for (int i = 0; i < strValores.Length; i++)
                {
                    X0[i] = metodos.ConvertirADouble(strValores[i]);
                }

                // 5. Traducimos la tolerancia
                double tol = metodos.ConvertirADouble(txtTol.Text);

                // 6. Arrancamos el motor
                metodos.NewtonRaphsonGeneral(funciones, X0, tol, dgvNoLinealNewton);

                // Un mensajito de éxito nunca está de más cuando el método es tan pesado
                MessageBox.Show("¡Cálculo terminado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // 🛡️ REGLA MATEMÁTICA 2: MATRIZ SINGULAR (Atrapada desde el motor)
                if (ex.Message.Contains("Matriz singular"))
                {
                    MessageBox.Show(
                        "El método falló matemáticamente.\n\nMotivo: " + ex.Message + "\n\nIntenta con otros valores iniciales para evitar que la matriz Jacobiana se vuelva cero (divisiones por cero en el espacio multidimensional).",
                        "Alerta de Sistema (Jacobiano)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Verifica la sintaxis. Recuerda escribir cada función en una línea nueva: " + ex.Message);
                }
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
                MessageBox.Show("Algo falló al intentar mandar la tabla: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Mandas a llamar a la Escoba Mágica (creas el objeto y lo usas en la misma línea)
            new MetodosNumericos().LimpiarPantalla(
                dgvNoLinealNewton, // 1. Tu tabla
                new TextBox[] { txtFunciones, txtValoresIniciales, txtTol }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] {  } // 3. (Opcional) Tus labels de resultado
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
                    // 🛡️ EL FIX: Apagamos el estilo nativo de Windows
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

                // Magia recursiva para encontrar controles anidados
                if (control.HasChildren)
                {
                    AplicarEstiloTuani(control.Controls);
                }
            }
        }
    }
}
