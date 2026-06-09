using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormDiferenciacionNumerica : Form
    {
        private Panel pnlEspera;

        public FormDiferenciacionNumerica()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Diferenciación Numérica", "Método por Diferencias Finitas Tabuladas. Calcula la primera derivada (tasa de cambio) y la segunda derivada (aceleración) de un conjunto de datos discretos.");

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

                if (listX.Count < 3)
                {
                    MessageBox.Show("Se requieren al menos 3 puntos en la tabla para apreciar la diferencia central.", "Datos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🚀 ARRANCAR MOTOR DE DIFERENCIACIÓN
                metodos.DiferenciacionNumericaTabular(listX.ToArray(), listY.ToArray(), dgvDerivadas);

                // 🚀 ESTILOS PARA LA TABLA DE DERIVADAS
                dgvDerivadas.EnableHeadersVisualStyles = false;
                dgvDerivadas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvDerivadas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvDerivadas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDerivadas.RowHeadersVisible = false;
                dgvDerivadas.DefaultCellStyle.BackColor = Color.White;
                dgvDerivadas.DefaultCellStyle.ForeColor = Color.Black;
                dgvDerivadas.DefaultCellStyle.Font = new Font("Consolas", 10, FontStyle.Regular);
                dgvDerivadas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);

                pnlEspera.Visible = false;
                dgvDerivadas.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la rutina analítica: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try { new MetodosNumericos().ExportarAExcel(dgvDerivadas); }
            catch (Exception ex) { MessageBox.Show("Error al exportar los datos: " + ex.Message); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvPuntos.Rows.Clear();
            dgvDerivadas.Rows.Clear();
            dgvDerivadas.Columns.Clear();

            dgvDerivadas.Visible = false;
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
                else if (control is DataGridView dgv) { dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.FixedSingle; dgv.GridColor = Color.FromArgb(220, 225, 230); }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvDerivadas.Visible = false;
            pnlEspera = new Panel { Location = new Point(40, 260), Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - 280), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(16, 185, 129) }); // Verde esmeralda para derivadas

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });

            // 🚀 ETIQUETA ACTUALIZADA CON LAS DOS DERIVADAS
            pnlDerecha.Controls.Add(new Label { Text = "1ra Derivada (Velocidad):\nf'(x) = (y_{i+1} - y_{i-1}) / 2h\n\n2da Derivada (Aceleración):\nf''(x) = (y_{i+1} - 2y_i + y_{i-1}) / h²", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(167, 243, 208), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 180) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(16, 185, 129), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Análisis Numérico:\n\nEl método de Diferencia Central es el más preciso ya que su error de truncamiento es proporcional a h².\n\nLa columna Central estará resaltada indicando la aproximación óptima recomendada.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Ingresa los puntos discretos (x, y) en orden ascendente.\n\n[ 2 ]  Haz clic en 'Calcular'.\n\n[ 3 ]  Analiza la tabla comparativa de tasas de cambio.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
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

            // Dibujar una línea de datos discretos en lugar de curvas continuas
            Pen pPuntos = new Pen(Color.FromArgb(16, 185, 129), 3);
            Pen pTangente = new Pen(Color.FromArgb(245, 158, 11), 2) { DashStyle = DashStyle.Dash };
            g.DrawEllipse(pPuntos, w / 2 - 50, cY - 20, 8, 8);
            g.DrawEllipse(pPuntos, w / 2, cY - 40, 8, 8);
            g.DrawEllipse(pPuntos, w / 2 + 50, cY - 80, 8, 8);
            g.DrawLine(pTangente, w / 2 - 40, cY - 10, w / 2 + 40, cY - 70); // Simulación de tangente

            pGrid.Dispose(); pEjes.Dispose(); pPuntos.Dispose(); pTangente.Dispose();
        }

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

            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40);
            btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40);

            // ==========================================
            // ALINEACIÓN DE LA TABLA ÚNICA DE RESULTADOS
            // ==========================================
            int tablaY = boxY + boxAlto + 20;

            dgvDerivadas.Location = new Point(40, tablaY);
            dgvDerivadas.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvDerivadas.Location;
                pnlEspera.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);
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