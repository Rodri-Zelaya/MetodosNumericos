using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormGaussSeidel : Form
    {
        private Panel pnlEspera;
        public FormGaussSeidel()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Método de Gauss-Seidel", "Método iterativo para resolver sistemas lineales que acelera la convergencia al utilizar los valores recién calculados de las variables en la misma iteración.");
            // 🚀 INYECTAR EL ACOMODADOR AUTOMÁTICO AQUÍ
            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatrizA.Text) || string.IsNullOrWhiteSpace(txtVectorB.Text) ||
                string.IsNullOrWhiteSpace(txtValoresIniciales.Text) || string.IsNullOrWhiteSpace(txtTolerancia.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos de matrices, valores iniciales y tolerancia.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            if (!metodos.EsToleranciaValida(txtTolerancia.Text)) return;

            // 🚀 Límite de seguridad
            int maxIter = 100;

            try
            {
                // 1. Parsear Matriz A
                string[] lineasA = txtMatrizA.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int n = lineasA.Length;
                double[,] A = new double[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] elementos = lineasA[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (elementos.Length != n)
                    {
                        MessageBox.Show($"La Matriz A debe ser cuadrada ({n}x{n}). La fila {i + 1} tiene {elementos.Length} elementos.", "Error de Dimensión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int j = 0; j < n; j++) A[i, j] = metodos.ConvertirADouble(elementos[j]);
                }

                // 🚀 INICIO DE AUDITORÍA MATRICIAL BLINDADA (Igual que en Jacobi) 🚀
                bool esDominante = true;
                for (int i = 0; i < n; i++)
                {
                    // 🚨 Validar división por cero en la diagonal
                    if (A[i, i] == 0)
                    {
                        MessageBox.Show($"Violación Matemática: El elemento de la diagonal en la fila {i + 1} es CERO.\n\nEl método de Gauss-Seidel divide por este valor. Modifique el sistema (ej. intercambiando filas) antes de continuar.", "División por Cero", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Bloqueo total
                    }

                    // 🚨 Validar Criterio EDD
                    double sumaFila = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j) sumaFila += Math.Abs(A[i, j]);
                    }

                    if (Math.Abs(A[i, i]) <= sumaFila)
                    {
                        esDominante = false;
                    }
                }

                // Advertir si no es dominante, pero permitir intentar (Gauss-Seidel a veces converge aunque no sea EDD)
                if (!esDominante)
                {
                    DialogResult advertencia = MessageBox.Show("La matriz ingresada NO es Estrictamente Dominante por Diagonal (EDD).\n\nAunque Gauss-Seidel tiene mejor convergencia que Jacobi, el método iterativo podría diverger (no encontrar solución). ¿Deseas intentar calcularlo de todos modos?", "Riesgo de Divergencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (advertencia == DialogResult.No) return;
                }
                // 🚀 FIN DE AUDITORÍA MATRICIAL 🚀

                // 2. Parsear Vector B
                string[] lineasB = txtVectorB.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (lineasB.Length != n)
                {
                    MessageBox.Show($"El vector b debe tener exactamente {n} términos.", "Error de Dimensión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                double[] b = new double[n];
                for (int i = 0; i < n; i++) b[i] = metodos.ConvertirADouble(lineasB[i]);

                // 3. Parsear Valores Iniciales (X0)
                string[] lineasX0 = txtValoresIniciales.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (lineasX0.Length != n)
                {
                    MessageBox.Show($"Debes proporcionar {n} valores iniciales (uno por cada variable del sistema).", "Error de Dimensión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                double[] X0 = new double[n];
                for (int i = 0; i < n; i++) X0[i] = metodos.ConvertirADouble(lineasX0[i]);

                // 4. Tolerancia
                double tol = metodos.ConvertirADouble(txtTolerancia.Text);

                // 🚀 ARRANCAR MOTOR DE GAUSS-SEIDEL
                metodos.GaussSeidel(A, b, X0, tol, maxIter, dgvGaussSeidel);

                // Configuración visual...
                dgvGaussSeidel.EnableHeadersVisualStyles = false;
                dgvGaussSeidel.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvGaussSeidel.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvGaussSeidel.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                dgvGaussSeidel.RowHeadersVisible = false;
                dgvGaussSeidel.DefaultCellStyle.BackColor = Color.White;
                dgvGaussSeidel.DefaultCellStyle.ForeColor = Color.Black;
                dgvGaussSeidel.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvGaussSeidel.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 238, 245);
                dgvGaussSeidel.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
                dgvGaussSeidel.DefaultCellStyle.SelectionForeColor = Color.White;

                pnlEspera.Visible = false;
                dgvGaussSeidel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla en el proceso: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                new MetodosNumericos().ExportarAExcel(dgvGaussSeidel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un inconveniente al exportar: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // 🛠️ REGLA AJUSTADA: Removida la caja del array de limpieza
            new MetodosNumericos().LimpiarPantalla(
                dgvGaussSeidel,
                new TextBox[] { txtMatrizA, txtVectorB, txtValoresIniciales, txtTolerancia },
                new Label[] { }
            );

            dgvGaussSeidel.Visible = false;
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
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.FromArgb(243, 244, 246);
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                }

                if (control.HasChildren)
                {
                    AplicarEstiloTuani(control.Controls);
                }
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvGaussSeidel.Visible = false;

            pnlEspera = new Panel();
            pnlEspera.Location = dgvGaussSeidel.Location;
            pnlEspera.Size = dgvGaussSeidel.Size;
            pnlEspera.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlEspera.BackColor = Color.FromArgb(243, 244, 246);
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel();
            pnlTarjeta.Size = new Size(960, 480);
            pnlTarjeta.BackColor = Color.White;
            pnlTarjeta.BorderStyle = BorderStyle.FixedSingle;

            Panel pnlHeaderBar = new Panel();
            pnlHeaderBar.Dock = DockStyle.Top;
            pnlHeaderBar.Height = 5;
            pnlHeaderBar.BackColor = Color.FromArgb(79, 70, 229);
            pnlTarjeta.Controls.Add(pnlHeaderBar);

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
            lblFormula.Text = "xi = (bi - Σ aij·xj) / aii";
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
            lblNotaOscura.Text = "💡 Nota Técnica:\n\nLa convergencia está 100% garantizada si la matriz original es 'Estrictamente Diagonal Dominante'.\n\nSi el sistema diverge (los números crecen al infinito), intenta intercambiar las filas para que los números mayores queden en la diagonal.";
            lblNotaOscura.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblNotaOscura.ForeColor = Color.FromArgb(209, 213, 219);
            lblNotaOscura.AutoSize = true;
            lblNotaOscura.MaximumSize = new Size(290, 0);
            lblNotaOscura.Location = new Point(15, 15);
            pnlNotaOscura.Controls.Add(lblNotaOscura);

            pnlDerecha.Controls.Add(lblMotor);
            pnlDerecha.Controls.Add(lblFormula);
            pnlDerecha.Controls.Add(pnlNotaOscura);

            Label lblTitulo = new Label();
            lblTitulo.Text = "⚡ " + nombreMetodo;
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.AutoSize = true;
            lblTitulo.MaximumSize = new Size(490, 0);
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
            lblPasos.Text = "[ 1 ]  Ingresa la Matriz A y el Vector b.\n\n" +
                            "[ 2 ]  Ingresa los Valores Iniciales (X0) para arrancar (ej. puros ceros).\n\n" +
                            "[ 3 ]  Define el límite de tolerancia esperada y dale a 'Calcular'.";
            lblPasos.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblPasos.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasos.AutoSize = true;
            lblPasos.Location = new Point(45, 245);

            pnlTarjeta.Controls.Add(lblTitulo);
            pnlTarjeta.Controls.Add(lblDesc);
            pnlTarjeta.Controls.Add(pnlDivisor);
            pnlTarjeta.Controls.Add(lblPasosTitulo);
            pnlTarjeta.Controls.Add(lblPasos);
            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) =>
            {
                pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
                pnlEspera.Invalidate();
            };
        }

        private void PnlEspera_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int ancho = pnlEspera.Width;
            int alto = pnlEspera.Height;
            int centroY = alto / 2;

            Pen penGrid = new Pen(Color.FromArgb(225, 230, 235), 1);
            for (int i = 0; i < ancho; i += 40) g.DrawLine(penGrid, i, 0, i, alto);
            for (int j = 0; j < alto; j += 40) g.DrawLine(penGrid, 0, j, ancho, j);

            Pen penEjes = new Pen(Color.FromArgb(200, 205, 215), 2);
            g.DrawLine(penEjes, 0, centroY, ancho, centroY);
            g.DrawLine(penEjes, ancho / 2, 0, ancho / 2, alto);

            Pen penCurve1 = new Pen(Color.FromArgb(165, 180, 252), 3);
            PointF[] puntos1 = new PointF[ancho / 5];
            Pen penCurve2 = new Pen(Color.FromArgb(209, 213, 219), 2);
            penCurve2.DashStyle = DashStyle.Dash;
            PointF[] puntos2 = new PointF[ancho / 5];

            for (int i = 0; i < puntos1.Length; i++)
            {
                float x = i * 5;
                float y1 = (float)(centroY + Math.Sin(x * 0.012) * 90 + Math.Cos(x * 0.004) * 30);
                puntos1[i] = new PointF(x, y1);
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

        // 🚀 MOTOR DE ALINEACIÓN AUTOMÁTICA DE INTERFAZ (GAUSS-SEIDEL)
        // =====================================================================
        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return; // Evita errores si se minimiza

            int startX = 40;
            int boxY = 80;       // Altura con buen respiro desde el techo
            int espaciado = 30;  // Espaciado horizontal
            int boxAlto = 150;   // 🛠️ Altura especial para las cajas multilínea

            // 1. Alineación de Entradas de Texto (Izquierda a Derecha)
            MoverLabelPorTexto("Matri", startX, boxY - 30); // Atrapa Matriz o Matrix
            txtMatrizA.Location = new Point(startX, boxY);
            txtMatrizA.Size = new Size(220, boxAlto); // Caja ancha para la matriz

            int nextX = startX + txtMatrizA.Width + espaciado;
            MoverLabelPorTexto("Vector", nextX, boxY - 30);
            txtVectorB.Location = new Point(nextX, boxY);
            txtVectorB.Size = new Size(80, boxAlto); // Caja delgada y alta para el vector

            nextX += txtVectorB.Width + espaciado;
            MoverLabelPorTexto("Inicial", nextX, boxY - 30);
            txtValoresIniciales.Location = new Point(nextX, boxY);
            txtValoresIniciales.Size = new Size(80, boxAlto); // Caja delgada y alta para X0

            // 2. Alineación de Botones (Derecha a Izquierda, en una fila nítida)
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

            // 3. Empujar la Tabla y el Panel de Espera hacia abajo
            int tablaY = boxY + boxAlto + 40; // 🛠️ Damos aire debajo de las cajas altas
            dgvGaussSeidel.Location = new Point(40, tablaY);
            dgvGaussSeidel.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvGaussSeidel.Location;
                pnlEspera.Size = dgvGaussSeidel.Size;
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
