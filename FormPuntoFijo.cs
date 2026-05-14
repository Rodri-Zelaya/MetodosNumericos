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
            // 1. Validar que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtFuncionOriginal.Text) ||
                string.IsNullOrWhiteSpace(txtDespejeG.Text) ||
                string.IsNullOrWhiteSpace(txtX0PuntoFijo.Text) ||
                string.IsNullOrWhiteSpace(txtTolPuntoFijo.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos, incluyendo la función original y tu despeje.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 🛡️ CAPA 1: Validar que lo que ingresó en ambas cajas sean funciones matemáticas reales
            if (!metodos.EsFuncionValida(txtFuncionOriginal.Text)) return;
            if (!metodos.EsFuncionValida(txtDespejeG.Text)) return;

            // 🛡️ CAPA 2: Validar los números
            if (!metodos.SonNumerosValidos(txtX0PuntoFijo.Text, "el Valor Inicial (x0)")) return;
            if (!metodos.SonNumerosValidos(txtTolPuntoFijo.Text, "la Tolerancia")) return;
            if (!metodos.EsToleranciaValida(txtTolPuntoFijo.Text)) return;

            try
            {
                // Guardamos las variables. La función original solo se guarda pero no se opera.
                string funcionOriginal = txtFuncionOriginal.Text;
                string funcionG = txtDespejeG.Text; // Este es el despeje que sí vamos a usar

                double x0 = metodos.ConvertirADouble(txtX0PuntoFijo.Text);
                double tol = metodos.ConvertirADouble(txtTolPuntoFijo.Text);

                // 🛡️ ESCUDO 5.1: Verificamos que el punto exista en el despeje
                double g0 = metodos.EvaluarFuncion(funcionG, x0);
                if (!metodos.EsPuntoValido(g0)) return;

                // 🧠 LA MAGIA DEL SISTEMA: Calcular la derivada del despeje y evaluar convergencia
                double derivadaG = metodos.CalcularDerivada(funcionG, x0);

                // Validamos la regla de oro: |g'(x0)| < 1
                if (Math.Abs(derivadaG) >= 1)
                {
                    // Freno automático si no converge
                    MessageBox.Show(
                        "¡Ese despeje no sirve!\n\n" +
                        "El sistema calculó la derivada de tu g(x) y el resultado absoluto es " + Math.Abs(derivadaG).ToString("F4") + ".\n\n" +
                        "Como es mayor o igual a 1, este despeje NO CONVERGE y los números explotarán hacia el infinito.\n" +
                        "Por favor, despeja la 'x' de otra forma en tu función original e ingresa la nueva opción.",
                        "Divergencia Detectada (No Converge)",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return; // Bloquea la ejecución, no arma la tabla
                }

                // 🚀 Si el código llega hasta aquí, significa que la derivada dio < 1 y sí converge.
                MessageBox.Show(
                    "¡Despeje válido!\n\nLa derivada evaluada da " + Math.Abs(derivadaG).ToString("F4") + " (menor a 1). El sistema procederá a iterar.",
                    "Convergencia Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Arrancamos el motor de iteraciones usando el despeje validado
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
            string funcion = txtFuncionOriginal.Text;

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
                new TextBox[] { txtFuncionOriginal, txtX0PuntoFijo, txtTolPuntoFijo }, // 2. Tus cajas de texto (pon de primera la que quieras que tome el cursor)
                new Label[] { lblRaiz } // 3. (Opcional) Tus labels de resultado
            );
        }
    }
}
