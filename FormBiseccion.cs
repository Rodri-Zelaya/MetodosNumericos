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
        // 🚀 Variable global para nuestro panel de espera
        private Panel pnlEspera;
        public FormBiseccion()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            // 🚀 Inyectamos el panel de espera con su descripción
            ConfigurarEmptyState("Método de Bisección", "Método cerrado que divide repetidamente a la mitad un intervalo que contiene una raíz hasta reducir el error al mínimo tolerado.");
            // 🚀 INYECTAR EL ACOMODADOR AUTOMÁTICO AQUÍ
            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
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

                // 🛡️ ¡NUEVO ESCUDO!: CONTINUIDAD Y DIVISIÓN POR CERO 🛡️
                // Lo ponemos aquí para que atrape el NaN antes de que Bolzano se confunda
                if (!metodos.EsEvaluacionValida(fa, fb)) return;

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

                // 🚀 EL CAMBIAZO: Mostramos la tabla con datos y ocultamos la bienvenida
                pnlEspera.Visible = false;
                dgvBiseccion.Visible = true;
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
            // 🚀 REGRESAR AL ESTADO VACÍO
            dgvBiseccion.Visible = false;
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
                    // 🛡️ EL FIX: Apagamos el estilo nativo para que el botón no quede en blanco
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
            dgvBiseccion.Visible = false;

            // 1. El Panel Base (Fondo de ondas)
            pnlEspera = new Panel();
            pnlEspera.Location = dgvBiseccion.Location;
            pnlEspera.Size = dgvBiseccion.Size;
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
            lblFormula.Text = "xr = (a + b) / 2"; // 🛠️ Fórmula de Bisección
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
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nDepende estrictamente del Teorema de Bolzano.\n\nSiempre converge si la función es continua en el intervalo y cambia de signo, aunque su velocidad es lineal (suele ser más lenta que los métodos abiertos).";
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
            lblTitulo.Font = new Font("Segoe UI", 26, FontStyle.Bold);
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
            lblPasos.Text = "[ 1 ]  Ingresa la ecuación (ej. x^2 - 4).\n\n" +
                            "[ 2 ]  Define los límites 'a' y 'b' (deben rodear la raíz).\n\n" +
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

        // =====================================================================
        // 🚀 MOTOR DE ALINEACIÓN AUTOMÁTICA DE INTERFAZ (BISECCIÓN)
        // =====================================================================
        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return; // Evita errores si se minimiza la ventana

            int startX = 40;
            int boxY = 75;       // Altura con buen respiro desde el techo
            int espaciado = 40;  // Espaciado horizontal entre cajas

            // 1. Alineación de Entradas de Texto (Izquierda a Derecha)
            MoverLabelPorTexto("Ecuación", startX, boxY - 30);
            txtFuncionBiseccion.Location = new Point(startX, boxY);
            txtFuncionBiseccion.Size = new Size(280, 35); // Caja larga para la función

            int nextX = startX + txtFuncionBiseccion.Width + espaciado;
            MoverLabelPorTexto("Valor A", nextX, boxY - 30);
            txtA.Location = new Point(nextX, boxY);
            txtA.Size = new Size(80, 35); // Cajita pequeña para el número

            nextX += txtA.Width + espaciado;
            MoverLabelPorTexto("Valor B", nextX, boxY - 30);
            txtB.Location = new Point(nextX, boxY);
            txtB.Size = new Size(80, 35); // Cajita pequeña para el número

            nextX += txtB.Width + espaciado;
            MoverLabelPorTexto("Tolerancia", nextX, boxY - 30);
            txtTolerancia.Location = new Point(nextX, boxY);
            txtTolerancia.Size = new Size(120, 35);

            // 2. Alineación de Botones (Derecha a Izquierda, en una sola fila nítida)
            int btnAncho = 130; // Un pelín más angostos porque son 4 botones
            int btnAlto = 40;
            int separacionBtn = 15;

            int btnX = this.ClientSize.Width - 40 - btnAncho;

            // De derecha a izquierda: Limpiar -> Graficar -> Exportar -> Calcular
            btnLimpiar.Size = new Size(btnAncho, btnAlto);
            btnLimpiar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnGraficar.Size = new Size(btnAncho, btnAlto);
            btnGraficar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnExportar.Size = new Size(btnAncho, btnAlto);
            btnExportar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnCalcular.Size = new Size(btnAncho, btnAlto);
            btnCalcular.Location = new Point(btnX, boxY);

            // 3. Resultado (Raíz)
            int filaRaiz_Y = boxY + 70; // Fila inferior espaciosa

            // Movemos tanto el label dinámico como cualquier etiqueta estática "Raiz:" que tengas
            MoverLabelPorTexto("Raiz", startX, filaRaiz_Y);
            if (lblRaiz != null)
            {
                lblRaiz.Location = new Point(startX, filaRaiz_Y);
            }

            // 4. Empujar la Tabla y el Panel de Espera hacia abajo
            int tablaY = filaRaiz_Y + 40; // Aire debajo del resultado
            dgvBiseccion.Location = new Point(40, tablaY);
            dgvBiseccion.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvBiseccion.Location;
                pnlEspera.Size = dgvBiseccion.Size;
            }
        }

        // Función inteligente para rastrear y mover etiquetas por su texto
        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
            {
                // Ignoramos el lblRaiz dinámico porque lo movemos manualmente arriba
                if (ctrl is Label lbl && lbl.Text.ToLower().Contains(palabraClave.ToLower()) && ctrl.Name != "lblRaiz")
                {
                    lbl.Location = new Point(x, y);
                    break;
                }
            }
        }
    }
}

//Probando que el push funcione bien y suba al git hub los cambios  
//Probando el repositorio secundario New Implementations 