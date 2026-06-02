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
        // 🚀 Variable global para nuestro panel de espera
        private Panel pnlEspera;
        public FormHornerNewton()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            // 🚀 Inyectamos el panel de espera con su descripción
            ConfigurarEmptyState("Método de Horner-Newton", "Algoritmo altamente optimizado para polinomios que utiliza la división sintética en cascada para evaluar el polinomio y su derivada de forma simultánea.");
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // 1. Verificamos que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtCoeficientes.Text) || string.IsNullOrWhiteSpace(txtR0.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Pon los coeficientes, separados por espacios, y llena todos los campos.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            // Validamos que los coeficientes sean solo números
            if (!metodos.SonNumerosValidos(txtCoeficientes.Text, "los Coeficientes")) return;

            // Validamos el punto inicial (r0)
            if (!metodos.SonNumerosValidos(txtR0.Text, "el Valor Inicial")) return;

            // Añade esta línea justo debajo de tus otros escudos:
            if (!metodos.EsToleranciaValida(txtTolerancia.Text)) return;

            try
            {

                // 2. Usamos el traductor universal (adiós a los Replace raros)
                double r0 = metodos.ConvertirADouble(txtR0.Text);
                double tol = metodos.ConvertirADouble(txtTolerancia.Text);

                // Partimos el texto en pedacitos cada vez que haya un espacio
                string[] partes = txtCoeficientes.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int n = partes.Length - 1; // Calculamos el grado del polinomio

                // 🛡️ REGLA MATEMÁTICA 1: Grado mínimo
                if (n < 1)
                {
                    MessageBox.Show("Necesitas al menos un polinomio de grado 1.");
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
                    MessageBox.Show("El primer coeficiente no puede ser cero.");
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
                        "La derivada del polinomio en el punto inicial r0 = " + r0 + " es cero.\n\n" +
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

                // 🚀 EL CAMBIAZO: Mostramos la tabla con datos y ocultamos la bienvenida
                pnlEspera.Visible = false;
                dgvHornerNewton.Visible = true;
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
                MessageBox.Show("Algo falló al intentar mandar la tabla: " + ex.Message);
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
            // 🚀 REGRESAR AL ESTADO VACÍO
            dgvHornerNewton.Visible = false;
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
            dgvHornerNewton.Visible = false;

            // 1. El Panel Base (Fondo de ondas)
            pnlEspera = new Panel();
            pnlEspera.Location = dgvHornerNewton.Location;
            pnlEspera.Size = dgvHornerNewton.Size;
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
            lblFormula.Text = "P(x) = (x - r) * Q(x) + R"; // 🛠️ Fórmula de Horner-Newton
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
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nReduce de forma brutal las operaciones aritméticas a nivel de procesador.\n\nEvalúa un polinomio de grado 'n' utilizando únicamente 'n' multiplicaciones y 'n' sumas.";
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
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
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
            lblPasos.Text = "[ 1 ]  Ingresa los coeficientes separados por espacios.\n\n" +
                            "[ 2 ]  Define el valor inicial (r0).\n\n" +
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
