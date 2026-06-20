using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormEliminaciónGaussiana : Form
    {
        private Panel pnlEspera;

        public FormEliminaciónGaussiana()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Eliminación Gaussiana", "Método directo exacto. Transforma la matriz de coeficientes en una matriz triangular superior mediante eliminación hacia adelante. Posteriormente, aplica sustitución hacia atrás para encontrar el valor de cada incógnita.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatrizA.Text) || string.IsNullOrWhiteSpace(txtVectorB.Text))
            {
                MessageBox.Show("Por favor, llena la Matriz A y el Vector B para continuar.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            try
            {
                // 1. Parsear Matriz A
                string[] lineasA = txtMatrizA.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int n = lineasA.Length;
                double[,] A = new double[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] elementos = lineasA[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (elementos.Length != n)
                    {
                        MessageBox.Show($"La Matriz A debe ser cuadrada ({n}x{n}). Fila {i + 1} tiene {elementos.Length} elementos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int j = 0; j < n; j++) A[i, j] = metodos.ConvertirADouble(elementos[j]);
                }

                // 2. Parsear Vector B
                string[] lineasB = txtVectorB.Text.Split(new string[] { "\r\n", "\n", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (lineasB.Length != n)
                {
                    MessageBox.Show($"El vector b debe tener exactamente {n} términos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                double[] b = new double[n];
                for (int i = 0; i < n; i++) b[i] = metodos.ConvertirADouble(lineasB[i]);

                // 🚀 ARRANCAR MOTOR DE ELIMINACIÓN GAUSSIANA
                metodos.EliminacionGaussianaPasoAPaso(A, b, dgvEliminacionGaussiana);

                // 🚀 ESTILOS DEL DATAGRIDVIEW
                dgvEliminacionGaussiana.EnableHeadersVisualStyles = false;
                dgvEliminacionGaussiana.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvEliminacionGaussiana.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvEliminacionGaussiana.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvEliminacionGaussiana.RowHeadersVisible = false;
                dgvEliminacionGaussiana.DefaultCellStyle.BackColor = Color.White;
                dgvEliminacionGaussiana.DefaultCellStyle.ForeColor = Color.Black;
                dgvEliminacionGaussiana.DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Regular);
                dgvEliminacionGaussiana.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 238, 245);
                dgvEliminacionGaussiana.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
                dgvEliminacionGaussiana.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvEliminacionGaussiana.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                pnlEspera.Visible = false;
                dgvEliminacionGaussiana.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla en el proceso: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try { new MetodosNumericos().ExportarAExcel(dgvEliminacionGaussiana); }
            catch (Exception ex) { MessageBox.Show("Error al exportar: " + ex.Message); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            new MetodosNumericos().LimpiarPantalla(dgvEliminacionGaussiana, new TextBox[] { txtMatrizA, txtVectorB }, new Label[] { });
            dgvEliminacionGaussiana.Visible = false;
            pnlEspera.Visible = true;
        }

        private void AplicarEstiloTuani(Control.ControlCollection controles)
        {
            this.BackColor = Color.FromArgb(243, 244, 246);
            foreach (Control control in controles)
            {
                if (control is Button btn)
                {
                    btn.UseVisualStyleBackColor = false;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(17, 24, 39);
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 65, 81);
                }
                else if (control is Label lbl) { lbl.ForeColor = Color.Black; lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold); }
                else if (control is TextBox txt) { txt.Font = new Font("Segoe UI", 11, FontStyle.Regular); txt.BorderStyle = BorderStyle.FixedSingle; }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvEliminacionGaussiana.Visible = false;
            pnlEspera = new Panel { Location = dgvEliminacionGaussiana.Location, Size = dgvEliminacionGaussiana.Size, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(79, 70, 229) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "1. Triangulación Superior:\n   Dejar ceros bajo la diagonal\n\n2. Sustitución Hacia Atrás:\n   x_i = (b_i - Σ(a_ij * x_j)) / a_ii", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(165, 180, 252), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 190) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(79, 70, 229), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Nota Técnica:\n\nA diferencia de Gauss-Jordan, este método no opera sobre las filas superiores al pivote.\n\nEs algorítmicamente más eficiente (requiere menos operaciones en el CPU) para sistemas matriciales inmensos.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Ingresa la Matriz A (Coeficientes).\n\n[ 2 ]  Ingresa el Vector b (Términos independientes).\n\n[ 3 ]  Dale a 'Calcular' para triangular el sistema.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) => { pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2); pnlEspera.Invalidate(); };
        }

        private void PnlEspera_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = pnlEspera.Width, h = pnlEspera.Height, cY = h / 2;
            Pen pGrid = new Pen(Color.FromArgb(225, 230, 235), 1), pEjes = new Pen(Color.FromArgb(200, 205, 215), 2);
            for (int i = 0; i < w; i += 40) g.DrawLine(pGrid, i, 0, i, h);
            for (int j = 0; j < h; j += 40) g.DrawLine(pGrid, 0, j, w, j);
            g.DrawLine(pEjes, 0, cY, w, cY); g.DrawLine(pEjes, w / 2, 0, w / 2, h);
            Pen p1 = new Pen(Color.FromArgb(165, 180, 252), 3), p2 = new Pen(Color.FromArgb(209, 213, 219), 2) { DashStyle = DashStyle.Dash };
            PointF[] pt1 = new PointF[w / 5], pt2 = new PointF[w / 5];
            for (int i = 0; i < pt1.Length; i++) { float x = i * 5; pt1[i] = new PointF(x, (float)(cY + Math.Sin(x * 0.012) * 90 + Math.Cos(x * 0.004) * 30)); pt2[i] = new PointF(x, (float)(cY + Math.Cos(x * 0.015) * 60 - Math.Sin(x * 0.008) * 40)); }
            if (pt1.Length > 1) { g.DrawCurve(p2, pt2); g.DrawCurve(p1, pt1); }
            pGrid.Dispose(); pEjes.Dispose(); p1.Dispose(); p2.Dispose();
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 80;
            MoverLabelPorTexto("matri", 40, boxY - 30);
            txtMatrizA.Location = new Point(40, boxY);
            txtMatrizA.Size = new Size(250, 150);

            MoverLabelPorTexto("vector", 320, boxY - 30);
            txtVectorB.Location = new Point(320, boxY);
            txtVectorB.Size = new Size(100, 150);

            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40);
            btnExportarExcel.Location = new Point(btnX -= 145, boxY); btnExportarExcel.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40);

            int tablaY = boxY + 190;
            dgvEliminacionGaussiana.Location = new Point(40, tablaY);
            dgvEliminacionGaussiana.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);

            if (pnlEspera != null) { pnlEspera.Location = dgvEliminacionGaussiana.Location; pnlEspera.Size = dgvEliminacionGaussiana.Size; }
        }

        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl is Label lbl && (lbl.Text.ToLower().Contains(palabraClave.ToLower()) || lbl.Name.ToLower().Contains(palabraClave.ToLower())))
                { lbl.Location = new Point(x, y); break; }
        }
    }
}