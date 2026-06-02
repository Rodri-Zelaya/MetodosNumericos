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
        // 🚀 Variable global para nuestro panel de espera
        private Panel pnlEspera;
        public FormPuntoFijo()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            // 🚀 Inyectamos el panel de espera con su descripción
            ConfigurarEmptyState("Método de Punto Fijo", "Método abierto que transforma la ecuación original f(x) = 0 en una forma equivalente x = g(x) para iterar sucesivas aproximaciones.");
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
                // 🚀 EL CAMBIAZO: Mostramos la tabla con datos y ocultamos la bienvenida
                pnlEspera.Visible = false;
                dgvPuntoFijo.Visible = true;
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
            // 🚀 REGRESAR AL ESTADO VACÍO
            dgvPuntoFijo.Visible = false;
            pnlEspera.Visible = true;
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
        // 🚀 MOTOR DEL EMPTY STATE (SPLIT CARD)
        // =====================================================================
        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvPuntoFijo.Visible = false;

            // 1. El Panel Base (Fondo de ondas)
            pnlEspera = new Panel();
            pnlEspera.Location = dgvPuntoFijo.Location;
            pnlEspera.Size = dgvPuntoFijo.Size;
            pnlEspera.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlEspera.BackColor = Color.FromArgb(243, 244, 246);
            pnlEspera.Paint += PnlEspera_Paint;

            // 🚀 2. LA TARJETA BLINDADA
            Panel pnlTarjeta = new Panel();
            pnlTarjeta.Size = new Size(960, 480);
            pnlTarjeta.BackColor = Color.White;
            pnlTarjeta.BorderStyle = BorderStyle.FixedSingle;

            // Barra superior de acento
            Panel pnlHeaderBar = new Panel();
            pnlHeaderBar.Dock = DockStyle.Top;
            pnlHeaderBar.Height = 5;
            pnlHeaderBar.BackColor = Color.FromArgb(79, 70, 229);
            pnlTarjeta.Controls.Add(pnlHeaderBar);

            // ---------------------------------------------------
            // COLUMNA DERECHA (Oscura)
            // ---------------------------------------------------
            Panel pnlDerecha = new Panel();
            pnlDerecha.Width = 380;
            pnlDerecha.Height = pnlTarjeta.Height;
            pnlDerecha.Location = new Point(pnlTarjeta.Width - 380, 0);
            pnlDerecha.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlDerecha.BackColor = Color.FromArgb(17, 24, 39);

            Label lblMotor = new Label();
            lblMotor.Text = "⚙️ Base Matemática";
            lblMotor.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMotor.ForeColor = Color.White;
            lblMotor.AutoSize = true;
            lblMotor.Location = new Point(25, 40);

            Label lblFormula = new Label();
            lblFormula.Text = "xi+1 = g(xi)"; // 🛠️ Fórmula de Punto Fijo
            lblFormula.Font = new Font("Consolas", 14, FontStyle.Bold);
            lblFormula.ForeColor = Color.FromArgb(165, 180, 252);
            lblFormula.AutoSize = true;
            lblFormula.MaximumSize = new Size(340, 0);
            lblFormula.Location = new Point(25, 90);

            Panel pnlNotaOscura = new Panel();
            pnlNotaOscura.BackColor = Color.FromArgb(31, 41, 55);
            pnlNotaOscura.Size = new Size(330, 250);
            pnlNotaOscura.Location = new Point(25, 145);

            Panel bordeNotaOscura = new Panel();
            bordeNotaOscura.BackColor = Color.FromArgb(79, 70, 229);
            bordeNotaOscura.Dock = DockStyle.Left;
            bordeNotaOscura.Width = 4;
            pnlNotaOscura.Controls.Add(bordeNotaOscura);

            Label lblNotaOscura = new Label();
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nLa convergencia depende estrictamente de la derivada del despeje g(x).\n\nEl método solo convergerá si el valor absoluto de la derivada g'(x) evaluada en la aproximación es menor a 1. Si es mayor, diverge.";
            lblNotaOscura.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblNotaOscura.ForeColor = Color.FromArgb(209, 213, 219);
            lblNotaOscura.AutoSize = true;
            lblNotaOscura.MaximumSize = new Size(290, 0);
            lblNotaOscura.Location = new Point(15, 15);
            pnlNotaOscura.Controls.Add(lblNotaOscura);

            pnlDerecha.Controls.Add(lblMotor);
            pnlDerecha.Controls.Add(lblFormula);
            pnlDerecha.Controls.Add(pnlNotaOscura);

            // ---------------------------------------------------
            // COLUMNA IZQUIERDA (Blanca)
            // ---------------------------------------------------
            Label lblTitulo = new Label();
            lblTitulo.Text = "⚡ " + nombreMetodo;
            lblTitulo.Font = new Font("Segoe UI", 23, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(40, 30);

            Label lblDesc = new Label();
            lblDesc.Text = descripcion;
            lblDesc.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblDesc.ForeColor = Color.FromArgb(100, 116, 139);
            lblDesc.AutoSize = true;
            lblDesc.MaximumSize = new Size(500, 0);
            lblDesc.Location = new Point(45, 95);

            Panel pnlDivisor = new Panel();
            pnlDivisor.BackColor = Color.FromArgb(226, 232, 240);
            pnlDivisor.Size = new Size(480, 1);
            pnlDivisor.Location = new Point(45, 175);

            Label lblPasosTitulo = new Label();
            lblPasosTitulo.Text = "📌 Secuencia de Operación";
            lblPasosTitulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblPasosTitulo.ForeColor = Color.FromArgb(55, 65, 81);
            lblPasosTitulo.AutoSize = true;
            lblPasosTitulo.Location = new Point(45, 200);

            Label lblPasos = new Label();
            lblPasos.Text = "[ 1 ]  Ingresa la función original y un despeje g(x) válido.\n\n" +
                            "[ 2 ]  Define el valor inicial de aproximación (x0).\n\n" +
                            "[ 3 ]  Establece la tolerancia esperada y dale a 'Calcular'.";
            lblPasos.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblPasos.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasos.AutoSize = true;
            lblPasos.Location = new Point(45, 245);

            // ---------------------------------------------------
            // ENSAMBLAJE FINAL
            // ---------------------------------------------------
            pnlTarjeta.Controls.Add(lblTitulo);
            pnlTarjeta.Controls.Add(lblDesc);
            pnlTarjeta.Controls.Add(pnlDivisor);
            pnlTarjeta.Controls.Add(lblPasosTitulo);
            pnlTarjeta.Controls.Add(lblPasos);
            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            // Centrado dinámico
            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) => {
                pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
                pnlEspera.Invalidate();
            };
        }

        private void PnlEspera_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int ancho = pnlEspera.Width;
            int alto = pnlEspera.Height;
            int centroY = alto / 2;

            // 1. Dibujar cuadrícula sutil
            Pen penGrid = new Pen(Color.FromArgb(225, 230, 235), 1);
            for (int i = 0; i < ancho; i += 40)
                g.DrawLine(penGrid, i, 0, i, alto);
            for (int j = 0; j < alto; j += 40)
                g.DrawLine(penGrid, 0, j, ancho, j);

            // 2. Resaltar el Eje X y Eje Y central 
            Pen penEjes = new Pen(Color.FromArgb(200, 205, 215), 2);
            g.DrawLine(penEjes, 0, centroY, ancho, centroY); // Eje X
            g.DrawLine(penEjes, ancho / 2, 0, ancho / 2, alto); // Eje Y

            // 3. Dibujar Curva Principal (Azul)
            Pen penCurve1 = new Pen(Color.FromArgb(165, 180, 252), 3);
            PointF[] puntos1 = new PointF[ancho / 5];

            // 4. Dibujar Curva Secundaria (Gris clara)
            Pen penCurve2 = new Pen(Color.FromArgb(209, 213, 219), 2);
            penCurve2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            PointF[] puntos2 = new PointF[ancho / 5];

            for (int i = 0; i < puntos1.Length; i++)
            {
                float x = i * 5;

                // Onda principal
                float y1 = (float)(centroY + Math.Sin(x * 0.012) * 90 + Math.Cos(x * 0.004) * 30);
                puntos1[i] = new PointF(x, y1);

                // Onda secundaria desfasada
                float y2 = (float)(centroY + Math.Cos(x * 0.015) * 60 - Math.Sin(x * 0.008) * 40);
                puntos2[i] = new PointF(x, y2);
            }

            if (puntos1.Length > 1)
            {
                g.DrawCurve(penCurve2, puntos2);
                g.DrawCurve(penCurve1, puntos1);
            }

            penGrid.Dispose();
            penEjes.Dispose();
            penCurve1.Dispose();
            penCurve2.Dispose();
        }
    }
}
