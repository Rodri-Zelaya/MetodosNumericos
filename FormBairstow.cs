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
            ConfigurarEmptyState("Método de Bairstow", "Este método avanzado extrae factores cuadráticos de un polinomio, \npermitiendo encontrar todas sus raíces paso a paso.");
            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
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

                // 👇 AGREGAR ESTO: Oculta el fondo bonito y muestra la tabla
                pnlEspera.Visible = false;
                dgvBairstow.Visible = true;
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
            // 👇 AGREGAR ESTO: Oculta la tabla vacía y regresa el fondo bonito
            dgvBairstow.Visible = false;
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

        private Panel pnlEspera;

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvBairstow.Visible = false;

            // 1. El Panel Base (Fondo de ondas)
            pnlEspera = new Panel();
            pnlEspera.Location = dgvBairstow.Location;
            pnlEspera.Size = dgvBairstow.Size;
            pnlEspera.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlEspera.BackColor = Color.FromArgb(243, 244, 246);
            pnlEspera.Paint += PnlEspera_Paint;

            // 🚀 2. LA TARJETA BLINDADA
            Panel pnlTarjeta = new Panel();
            pnlTarjeta.Size = new Size(960, 480); // 🛠️ Más grande para pantallas con escalado
            pnlTarjeta.BackColor = Color.White;
            pnlTarjeta.BorderStyle = BorderStyle.FixedSingle;

            // Barra superior de acento
            Panel pnlHeaderBar = new Panel();
            pnlHeaderBar.Dock = DockStyle.Top;
            pnlHeaderBar.Height = 5;
            pnlHeaderBar.BackColor = Color.FromArgb(79, 70, 229);
            pnlTarjeta.Controls.Add(pnlHeaderBar);

            // ---------------------------------------------------
            // COLUMNA DERECHA (Oscura) - Ahora es a prueba de estiramientos
            // ---------------------------------------------------
            Panel pnlDerecha = new Panel();
            pnlDerecha.Width = 380; // 🛠️ Más ancha para que no se corte la fórmula
            pnlDerecha.Height = pnlTarjeta.Height; // 🛠️ Toma la altura total
            pnlDerecha.Location = new Point(pnlTarjeta.Width - 380, 0);
            // 🛠️ LA MAGIA: Se pega arriba, abajo y a la derecha. Si la tarjeta crece, este también.
            pnlDerecha.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlDerecha.BackColor = Color.FromArgb(17, 24, 39);

            Label lblMotor = new Label();
            lblMotor.Text = "⚙️ Base Matemática";
            lblMotor.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMotor.ForeColor = Color.White;
            lblMotor.AutoSize = true;
            lblMotor.Location = new Point(25, 40);

            Label lblFormula = new Label();
            lblFormula.Text = "P(x) = (x² - rx - s) Q(x) + R(x)";
            lblFormula.Font = new Font("Consolas", 11, FontStyle.Bold);
            lblFormula.ForeColor = Color.FromArgb(165, 180, 252);
            lblFormula.AutoSize = true;
            lblFormula.MaximumSize = new Size(340, 0); // 🛠️ Tope de seguridad para la fórmula
            lblFormula.Location = new Point(25, 90);

            Panel pnlNotaOscura = new Panel();
            pnlNotaOscura.BackColor = Color.FromArgb(31, 41, 55);
            pnlNotaOscura.Size = new Size(330, 250); // 🛠️ Más grande para que el texto respire
            pnlNotaOscura.Location = new Point(25, 145);

            Panel bordeNotaOscura = new Panel();
            bordeNotaOscura.BackColor = Color.FromArgb(79, 70, 229);
            bordeNotaOscura.Dock = DockStyle.Left;
            bordeNotaOscura.Width = 4;
            pnlNotaOscura.Controls.Add(bordeNotaOscura);

            Label lblNotaOscura = new Label();
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nBairstow no requiere valores iniciales complejos para encontrar raíces imaginarias.\n\nSu algoritmo extrae iterativamente los factores cuadráticos hasta agotar el polinomio.";
            lblNotaOscura.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblNotaOscura.ForeColor = Color.FromArgb(209, 213, 219);
            lblNotaOscura.AutoSize = true;
            lblNotaOscura.MaximumSize = new Size(290, 0); // 🛠️ Límite exacto para que no se mutile
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
            lblTitulo.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(40, 30);

            Label lblDesc = new Label();
            lblDesc.Text = descripcion;
            lblDesc.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblDesc.ForeColor = Color.FromArgb(100, 116, 139);
            lblDesc.AutoSize = true;
            lblDesc.MaximumSize = new Size(500, 0); // Protege que no invada el lado oscuro
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
                            "[ 2 ]  Define el límite de tolerancia de error.\n\n" +
                            "[ 3 ]  Ejecuta el motor de cálculo para compilar la tabla.";
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
            pnlTarjeta.Controls.Add(pnlDerecha); // Añadimos el panel derecho

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

            // 2. Resaltar el Eje X y Eje Y central (Simulando el plano real)
            Pen penEjes = new Pen(Color.FromArgb(200, 205, 215), 2);
            g.DrawLine(penEjes, 0, centroY, ancho, centroY); // Eje X
            g.DrawLine(penEjes, ancho / 2, 0, ancho / 2, alto); // Eje Y

            // 3. Dibujar Curva Principal (Azul)
            Pen penCurve1 = new Pen(Color.FromArgb(165, 180, 252), 3);
            PointF[] puntos1 = new PointF[ancho / 5];

            // 4. Dibujar Curva Secundaria (Gris clara, para simular la secante o derivada)
            Pen penCurve2 = new Pen(Color.FromArgb(209, 213, 219), 2);
            penCurve2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; // Línea punteada
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
                g.DrawCurve(penCurve2, puntos2); // Dibujamos primero la punteada (fondo)
                g.DrawCurve(penCurve1, puntos1); // Dibujamos encima la principal
            }

            // Limpieza de recursos
            penGrid.Dispose();
            penEjes.Dispose();
            penCurve1.Dispose();
            penCurve2.Dispose();
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return; // Evita errores si se minimiza la ventana

            int startX = 40;
            int boxY = 75;       // 🛠️ Bajé todo un poco desde el techo para dar aire
            int espaciado = 50;  // 🛠️ Más espacio horizontal entre los bloques

            // 1. Alineación de Entradas de Texto (Izquierda)
            MoverLabelPorTexto("Coeficiente", startX, boxY - 30); // 🛠️ Separamos el título de la caja
            txtCoeficientes.Location = new Point(startX, boxY);
            txtCoeficientes.Size = new Size(350, 35);

            int nextX = startX + txtCoeficientes.Width + espaciado;
            MoverLabelPorTexto("Tolerancia", nextX, boxY - 30);
            txtTolerancia.Location = new Point(nextX, boxY);
            txtTolerancia.Size = new Size(120, 35);

            // 2. Alineación de Botones (Derecha)
            int btnAncho = 150;
            int btnAlto = 40;
            int separacionBtn = 20; // 🛠️ Botones con más distancia entre ellos

            int btnX = this.ClientSize.Width - 40 - btnAncho;

            btnLimpiar.Size = new Size(btnAncho, btnAlto);
            btnLimpiar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnExportar.Size = new Size(btnAncho, btnAlto);
            btnExportar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnCalcular.Size = new Size(btnAncho, btnAlto);
            btnCalcular.Location = new Point(btnX, boxY);

            // 3. Resultados (🚀 EL FIX: Dando respiro vertical y horizontal)
            int filaR0S0_Y = boxY + 70; // 🛠️ Los empujamos mucho más abajo de las cajas
            int filaRaiz_Y = filaR0S0_Y + 40; // 🛠️ Separamos la raíz de r0 y s0

            if (lblR0 != null)
            {
                lblR0.Location = new Point(startX, filaR0S0_Y);
            }

            if (lblS0 != null)
            {
                // 🛠️ Lo empujamos más a la derecha para que no se pegue con el texto de r0
                lblS0.Location = new Point(startX + 350, filaR0S0_Y);
            }

            if (lblRaiz != null)
            {
                lblRaiz.Location = new Point(startX, filaRaiz_Y);
            }

            // 4. Empujar la Tabla y el Panel de Espera hacia abajo
            int tablaY = filaRaiz_Y + 50; // 🛠️ Mucho más aire antes de que empiece la cuadrícula
            dgvBairstow.Location = new Point(40, tablaY);
            dgvBairstow.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvBairstow.Location;
                pnlEspera.Size = dgvBairstow.Size;
            }
        }
        // Función inteligente para mover etiquetas sin importar cómo se llamen (label1, label2, etc.)
        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl && lbl.Text.ToLower().Contains(palabraClave.ToLower()))
                {
                    lbl.Location = new Point(x, y);
                    break;
                }
            }
        }
    }
}
