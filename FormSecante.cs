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
        private Panel pnlEspera;
        public FormSecante()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            // 🚀 Inyectamos el panel de espera con su descripción
            ConfigurarEmptyState("Método de la Secante", "Método abierto que imita el comportamiento de Newton-Raphson, pero aproxima la derivada usando dos puntos iniciales sin requerir la ecuación diferencial analítica.");
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Validar vacíos (Aquí usamos x0 y x1)
            if (string.IsNullOrWhiteSpace(txtFuncionSecante.Text) || string.IsNullOrWhiteSpace(txtVI.Text) ||
                string.IsNullOrWhiteSpace(txtV2.Text) || string.IsNullOrWhiteSpace(txtTolSecante.Text))
            {
                MessageBox.Show("Llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // 🛡️ Validamos Función y en Punto Fijo validamos también g(x)
            if (!metodos.EsFuncionValida(txtFuncionSecante.Text)) return;

            // 🛡️ Validamos que el punto inicial y la tolerancia no tengan letras
            if (!metodos.SonNumerosValidos(txtVI.Text, "el Valor Inicial (x0)")) return;
            if (!metodos.SonNumerosValidos(txtV2.Text, "el Segundo Valor (x1)")) return;
            if (!metodos.SonNumerosValidos(txtTolSecante.Text, "la Tolerancia")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolSecante.Text)) return;

            try
            {
                string funcion = txtFuncionSecante.Text;

                // 2. Traductor universal
                double x0 = metodos.ConvertirADouble(txtVI.Text);
                double x1 = metodos.ConvertirADouble(txtV2.Text);
                double tol = metodos.ConvertirADouble(txtTolSecante.Text);

                // 🛡️ 3. REGLA MATEMÁTICA: SECANTE HORIZONTAL 🛡️
                double f0 = metodos.EvaluarFuncion(funcion, x0);
                double f1 = metodos.EvaluarFuncion(funcion, x1);

                // 🛡️ ¡NUEVO ESCUDO 5!: CONTINUIDAD DE LOS PUNTOS 🛡️
                // Bloquea el paso si x0 o x1 caen en una asíntota o división por cero
                if (!metodos.EsEvaluacionValida(f0, f1)) return;

                if (Math.Abs(f0 - f1) < 1e-12) // Si dan el mismo resultado
                {
                    MessageBox.Show(
                        "La función evaluada en x0 y x1 da el mismo resultado (f(x) = " + f0.ToString("G4") + ").\n\n" +
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
                // 🚀 EL CAMBIAZO: Mostramos la tabla con datos y ocultamos la bienvenida
                pnlEspera.Visible = false;
                dgvSecante.Visible = true;
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
                MessageBox.Show("Algo falló al intentar mandar la tabla:  " + ex.Message);
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
            // 🚀 REGRESAR AL ESTADO VACÍO
            dgvSecante.Visible = false;
            pnlEspera.Visible = true;
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            // Solo asegúrate de que el TextBox se llame igual en este form
            string funcion = txtFuncionSecante.Text;

            if (string.IsNullOrWhiteSpace(funcion))
            {
                MessageBox.Show("Escribe una función primero.");
                return;
            }

            // Llamamos a tu obra maestra
            FormGraficador visor = new FormGraficador(funcion);
            visor.ShowDialog();
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
            dgvSecante.Visible = false;

            // 1. El Panel Base (Fondo de ondas)
            pnlEspera = new Panel();
            pnlEspera.Location = dgvSecante.Location;
            pnlEspera.Size = dgvSecante.Size;
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
            lblFormula.Text = "f'(x) ≈ (f(xi) - f(xi-1)) / (xi - xi-1)"; // 🛠️ Fórmula de la Secante
            lblFormula.Font = new Font("Consolas", 11, FontStyle.Bold);
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
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nIdeal cuando calcular la derivada analítica de la función es muy complejo o costoso a nivel de CPU.\n\nSu velocidad de convergencia es superlineal (aproximadamente 1.618), siendo casi tan rápido como Newton-Raphson.";
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
            lblPasos.Text = "[ 1 ]  Ingresa la ecuación (ej. x^3 - 2*x - 5).\n\n" +
                            "[ 2 ]  Define los dos valores iniciales (x0 y x1).\n\n" +
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
