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
        // 🚀 Variable global para nuestro panel de espera
        private Panel pnlEspera;
        public FormNoLinealNewton()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            // 🚀 Inyectamos el panel de espera con su descripción
            ConfigurarEmptyState("Sistemas No Lineales", "Extensión multidimensional de Newton-Raphson diseñada para resolver ecuaciones complejas interconectadas simultáneamente empleando álgebra matricial.");
            // 🚀 INYECTAR EL ACOMODADOR AUTOMÁTICO AQUÍ
            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
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

                // 🚀 EL CAMBIAZO: Mostramos la tabla con datos y ocultamos la bienvenida
                pnlEspera.Visible = false;
                dgvNoLinealNewton.Visible = true;

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
            // 🚀 REGRESAR AL ESTADO VACÍO
            dgvNoLinealNewton.Visible = false;
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
            dgvNoLinealNewton.Visible = false;

            // 1. El Panel Base (Fondo de ondas)
            pnlEspera = new Panel();
            pnlEspera.Location = dgvNoLinealNewton.Location;
            pnlEspera.Size = dgvNoLinealNewton.Size;
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
            lblFormula.Text = "X_new = X_old - [J(X)]⁻¹ * F(X)"; // 🛠️ Fórmula Matricial
            lblFormula.Font = new Font("Consolas", 12, FontStyle.Bold);
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
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nDepende del cálculo y la inversión de la Matriz Jacobiana en cada paso iterativo.\n\nSi el determinante del Jacobiano llega a ser cero, el sistema colapsa matemáticamente (matriz singular).";
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
            lblPasos.Text = "[ 1 ]  Ingresa cada función del sistema en una línea nueva.\n\n" +
                            "[ 2 ]  Escribe los valores iniciales (uno por cada línea/variable).\n\n" +
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
        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return; // Evita errores si se minimiza

            int startX = 40;
            int boxY = 80;       // Altura con buen respiro desde el techo
            int espaciado = 30;  // Espaciado horizontal
            int boxAlto = 120;   // Altura diseñada especialmente para las cajas multilínea

            // 1. Alineación de Entradas de Texto (Izquierda a Derecha)
            MoverLabelPorTexto("Ecuaciones", startX, boxY - 30);
            txtFunciones.Location = new Point(startX, boxY);
            txtFunciones.Size = new Size(280, boxAlto); // Caja ancha y alta

            int nextX = startX + txtFunciones.Width + espaciado;
            MoverLabelPorTexto("iniciales", nextX, boxY - 30); // Busca "Valores iniciales"
            txtValoresIniciales.Location = new Point(nextX, boxY);
            txtValoresIniciales.Size = new Size(120, boxAlto); // Caja más delgada pero igual de alta

            nextX += txtValoresIniciales.Width + espaciado;
            MoverLabelPorTexto("Tolerancia", nextX, boxY - 30);
            txtTol.Location = new Point(nextX, boxY);
            txtTol.Size = new Size(100, 35); // Este se queda normal porque solo pide un número

            // 2. Alineación de Botones (Derecha a Izquierda, en fila)
            int btnAncho = 130;
            int btnAlto = 40;
            int separacionBtn = 15;

            int btnX = this.ClientSize.Width - 40 - btnAncho;

            // De derecha a izquierda: Limpiar -> Exportar -> Calcular
            btnLimpiar.Size = new Size(btnAncho, btnAlto);
            btnLimpiar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnExportar.Size = new Size(btnAncho, btnAlto);
            btnExportar.Location = new Point(btnX, boxY);

            btnX -= (btnAncho + separacionBtn);
            btnCalcular.Size = new Size(btnAncho, btnAlto);
            btnCalcular.Location = new Point(btnX, boxY);

            // OJO: Vi un botón de "Graficar" en tu captura de imagen, pero no estaba en el código que mandaste.
            // Si lo agregaste después en el diseñador, descomenta este pedacito y listo:
            /*
            btnX -= (btnAncho + separacionBtn);
            Control btnGraf = this.Controls.Find("btnGraficar", true).FirstOrDefault();
            if (btnGraf != null)
            {
                btnGraf.Size = new Size(btnAncho, btnAlto);
                btnGraf.Location = new Point(btnX, boxY);
            }
            */

            // 4. Empujar la Tabla y el Panel de Espera hacia abajo
            int tablaY = boxY + boxAlto + 40; // Aire debajo de las cajas multilíneas
            dgvNoLinealNewton.Location = new Point(40, tablaY);
            dgvNoLinealNewton.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvNoLinealNewton.Location;
                pnlEspera.Size = dgvNoLinealNewton.Size;
            }
        }

        // Función inteligente para rastrear y mover etiquetas por su texto
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
