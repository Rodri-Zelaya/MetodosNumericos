using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormNewtonDiferenciaDividida : Form
    {
        private Panel pnlEspera;

        public FormNewtonDiferenciaDividida ()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Interpolación de Newton", "Polinomio por Diferencias Divididas. El método traza una curva polinómica exacta que pasa por cada uno de los puntos ingresados.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();

            try
            {
                // 1. Recorrer la tabla de puntos cargados
                foreach (DataGridViewRow fila in dgvPuntos.Rows)
                {
                    if (fila.IsNewRow) continue;

                    if (fila.Cells[0].Value != null && fila.Cells[1].Value != null)
                    {
                        string valX = fila.Cells[0].Value.ToString().Trim();
                        string valY = fila.Cells[1].Value.ToString().Trim();

                        if (!string.IsNullOrEmpty(valX) && !string.IsNullOrEmpty(valY))
                        {
                            listX.Add(metodos.ConvertirADouble(valX));
                            listY.Add(metodos.ConvertirADouble(valY));
                        }
                    }
                }

                if (listX.Count < 2)
                {
                    MessageBox.Show("Se requieren al menos 2 puntos en la tabla para realizar la interpolación.", "Datos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtXEvaluar.Text))
                {
                    MessageBox.Show("Por favor, ingresa el valor de X que deseas evaluar en el polinomio interpolante.", "Falta Valor de Evaluación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double xEval = metodos.ConvertirADouble(txtXEvaluar.Text);

                // 🚀 ARRANCAR MOTOR MATEMÁTICO CON LAS NUEVAS TABLAS COHERENTES
                metodos.NewtonDiferenciasDivididas(listX.ToArray(), listY.ToArray(), xEval, dgvMatrizDiferencias, dgvResultadosInterpolacion);

                // 🚀 ESTILOS: TABLA DE LA MATRIZ DE DIFERENCIAS (ESCALERA)
                dgvMatrizDiferencias.EnableHeadersVisualStyles = false;
                dgvMatrizDiferencias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 65, 81);
                dgvMatrizDiferencias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvMatrizDiferencias.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvMatrizDiferencias.RowHeadersVisible = false;
                dgvMatrizDiferencias.DefaultCellStyle.BackColor = Color.White;
                dgvMatrizDiferencias.DefaultCellStyle.ForeColor = Color.Black;
                dgvMatrizDiferencias.DefaultCellStyle.Font = new Font("Consolas", 10, FontStyle.Regular);
                dgvMatrizDiferencias.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);

                // 🚀 ESTILOS: TABLA DE RESULTADOS FINALES
                dgvResultadosInterpolacion.EnableHeadersVisualStyles = false;
                dgvResultadosInterpolacion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvResultadosInterpolacion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvResultadosInterpolacion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvResultadosInterpolacion.RowHeadersVisible = false;
                dgvResultadosInterpolacion.DefaultCellStyle.BackColor = Color.White;
                dgvResultadosInterpolacion.DefaultCellStyle.ForeColor = Color.Black;
                dgvResultadosInterpolacion.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvResultadosInterpolacion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 238, 245);

                pnlEspera.Visible = false;
                dgvMatrizDiferencias.Visible = true;
                dgvResultadosInterpolacion.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla en el cálculo analítico: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                new MetodosNumericos().ExportarAExcel(dgvResultadosInterpolacion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar resultados: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvPuntos.Rows.Clear();
            dgvMatrizDiferencias.Rows.Clear();
            dgvMatrizDiferencias.Columns.Clear();
            dgvResultadosInterpolacion.Rows.Clear();
            txtXEvaluar.Clear();

            dgvMatrizDiferencias.Visible = false;
            dgvResultadosInterpolacion.Visible = false;
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
                else if (control is Label lbl)
                {
                    lbl.ForeColor = Color.Black;
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (control is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.BorderStyle = BorderStyle.FixedSingle;
                    dgv.GridColor = Color.FromArgb(220, 225, 230);
                }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvMatrizDiferencias.Visible = false;
            dgvResultadosInterpolacion.Visible = false;
            pnlEspera = new Panel { Location = new Point(40, 260), Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - 280), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(79, 70, 229) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "f[x0, x1] = (y1 - y0) / (x1 - x0)\n\nP_n(x) = b0 + b1(x-x0) + b2(x-x0)(x-x1)...", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(165, 180, 252), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 160) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(79, 70, 229), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Nota del Método:\n\nEl sistema armará de forma dinámica las diferencias divididas en forma de escalera.\n\nLa primera fila (resaltada en color amarillo) almacena directamente los coeficientes b del polinomio interpolador de Newton.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Digita los puntos experimentales en la tabla izquierda.\n\n[ 2 ]  Escribe el valor X que deseas interpolar analíticamente.\n\n[ 3 ]  Presiona 'Calcular' para desplegar la matriz en escalera.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
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

        // =====================================================================
        // 🚀 ALINEACIÓN DE CONTROLES DINÁMICOS CON NUEVOS NOMBRES
        // =====================================================================
        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 80;
            int boxAlto = 160;

            MoverLabelPorTexto("introdu", 40, boxY - 30);
            dgvPuntos.Location = new Point(40, boxY);
            dgvPuntos.Size = new Size(320, boxAlto);

            if (dgvPuntos.Columns.Count >= 2)
            {
                dgvPuntos.RowHeadersWidth = 25;
                dgvPuntos.Columns[0].Width = 135;
                dgvPuntos.Columns[1].Width = 135;
            }

            MoverLabelPorTexto("predecir", 380, boxY - 30);
            txtXEvaluar.Location = new Point(380, boxY);
            txtXEvaluar.Size = new Size(120, 30);

            lblTipoPolinomio.Location = new Point(380, boxY + 45);
            lblTipoPolinomio.AutoSize = true;
            lblTipoPolinomio.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTipoPolinomio.ForeColor = Color.FromArgb(79, 70, 229);

            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40);
            btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40);

            // ==========================================
            // ALINEACIÓN DE LAS DOS TABLAS DE RESULTADOS
            // ==========================================
            int tablaSumatoriasY = boxY + boxAlto + 20;
            int altoResultados = 180;
            int tablaFinalY = this.ClientSize.Height - altoResultados - 20;

            int altoSumatorias = tablaFinalY - tablaSumatoriasY - 15;

            dgvMatrizDiferencias.Location = new Point(40, tablaSumatoriasY);
            dgvMatrizDiferencias.Size = new Size(this.ClientSize.Width - 80, altoSumatorias);

            dgvResultadosInterpolacion.Location = new Point(40, tablaFinalY);
            dgvResultadosInterpolacion.Size = new Size(this.ClientSize.Width - 80, altoResultados);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvMatrizDiferencias.Location;
                pnlEspera.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaSumatoriasY - 20);
            }
        }

        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl is Label lbl && (lbl.Text.ToLower().Contains(palabraClave.ToLower()) || lbl.Name.ToLower().Contains(palabraClave.ToLower())))
                {
                    lbl.Location = new Point(x, y);
                    break;
                }
        }
    }
}