using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormRegresionPolinomial : Form
    {
        private Panel pnlEspera;

        public FormRegresionPolinomial()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Ajuste de Curvas", "Mínimos Cuadrados. Introduce tus puntos en la tabla. El sistema generará automáticamente la Tabla de Sumatorias completa y la ecuación del modelo.");

            numGrado.ValueChanged += (s, e) => ActualizarTextoTipo();
            ActualizarTextoTipo();

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
                foreach (DataGridViewRow fila in dgvDatos.Rows)
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
                    MessageBox.Show("Por favor, ingresa al menos 2 puntos (X, Y) para calcular.", "Datos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int grado = (int)numGrado.Value;

                if (listX.Count <= grado)
                {
                    MessageBox.Show($"Para un polinomio de Grado {grado} necesitas al menos {grado + 1} puntos.", "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🚀 SE MANDAN LAS DOS TABLAS (Sumatorias y Resultados) AL MOTOR
                metodos.RegresionPolinomial(listX.ToArray(), listY.ToArray(), grado, dgvSumatorias, dgvRegresion);

                // 🚀 ESTILOS PARA LA NUEVA TABLA DE SUMATORIAS
                dgvSumatorias.EnableHeadersVisualStyles = false;
                dgvSumatorias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 65, 81);
                dgvSumatorias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvSumatorias.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvSumatorias.RowHeadersVisible = false;
                dgvSumatorias.DefaultCellStyle.BackColor = Color.White;
                dgvSumatorias.DefaultCellStyle.ForeColor = Color.Black;
                dgvSumatorias.DefaultCellStyle.Font = new Font("Consolas", 10, FontStyle.Regular); // Letra de código para los números
                dgvSumatorias.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);

                // 🚀 ESTILOS PARA LA TABLA DE RESULTADOS
                dgvRegresion.EnableHeadersVisualStyles = false;
                dgvRegresion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvRegresion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvRegresion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvRegresion.RowHeadersVisible = false;
                dgvRegresion.DefaultCellStyle.BackColor = Color.White;
                dgvRegresion.DefaultCellStyle.ForeColor = Color.Black;
                dgvRegresion.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                dgvRegresion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 238, 245);

                pnlEspera.Visible = false;
                dgvSumatorias.Visible = true;
                dgvRegresion.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla en el cálculo: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // Exportar resultados (Opcional: Podés meter un MessageBox que pregunte cuál tabla exportar)
                new MetodosNumericos().ExportarAExcel(dgvRegresion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un inconveniente al exportar: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            dgvSumatorias.Rows.Clear();
            dgvSumatorias.Columns.Clear();
            dgvRegresion.Rows.Clear();
            numGrado.Value = 1;

            dgvSumatorias.Visible = false;
            dgvRegresion.Visible = false;
            pnlEspera.Visible = true;
        }

        private void ActualizarTextoTipo()
        {
            int grado = (int)numGrado.Value;
            switch (grado)
            {
                case 1:
                    lblTipoRegresion.Text = "➔ Línea Recta (Regresión Simple)";
                    lblTipoRegresion.ForeColor = Color.FromArgb(79, 70, 229);
                    break;
                case 2:
                    lblTipoRegresion.Text = "➔ Parábola (Regresión Cuadrática)";
                    lblTipoRegresion.ForeColor = Color.FromArgb(13, 148, 136);
                    break;
                case 3:
                    lblTipoRegresion.Text = "➔ Curva Cúbica (Regresión Cúbica)";
                    lblTipoRegresion.ForeColor = Color.FromArgb(219, 39, 119);
                    break;
                default:
                    lblTipoRegresion.Text = $"➔ Polinomio de Grado {grado}";
                    lblTipoRegresion.ForeColor = Color.FromArgb(55, 65, 81);
                    break;
            }
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
                else if (control is NumericUpDown num)
                {
                    num.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    num.BorderStyle = BorderStyle.FixedSingle;
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
            dgvSumatorias.Visible = false;
            dgvRegresion.Visible = false;
            pnlEspera = new Panel { Location = new Point(40, 260), Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - 280), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(79, 70, 229) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "S_r = Σ(yi - a0 - a1xi...)^2\n\nGrado 1: y = a0 + a1x (Simple)\nGrado 2: y = a0 + a1x + a2x^2", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(165, 180, 252), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 160) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(79, 70, 229), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Nota de Interfaz:\n\nEl sistema generará automáticamente la matriz de sumatorias requerida.\n\nLa última fila de la tabla de sumatorias estará resaltada en amarillo conteniendo los totales listos para el sistema de ecuaciones normales.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Digita tus puntos en la tabla de entrada.\n\n[ 2 ]  Selecciona el Grado del polinomio arriba.\n\n[ 3 ]  Dale a 'Calcular' para generar tablas y resultados.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
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
        // 🚀 MOTOR DE ALINEACIÓN DE CONTROLES DINÁMICOS (ACTUALIZADO)
        // =====================================================================
        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 80;
            int boxAlto = 160;

            MoverLabelPorTexto("introdu", 40, boxY - 30);
            dgvDatos.Location = new Point(40, boxY);
            dgvDatos.Size = new Size(320, boxAlto);

            if (dgvDatos.Columns.Count >= 2)
            {
                dgvDatos.RowHeadersWidth = 25;
                dgvDatos.Columns[0].Width = 135;
                dgvDatos.Columns[1].Width = 135;
            }

            MoverLabelPorTexto("grado", 380, boxY - 30);
            numGrado.Location = new Point(380, boxY);
            numGrado.Size = new Size(80, 40);

            lblTipoRegresion.Location = new Point(380, boxY + 45);
            lblTipoRegresion.AutoSize = true;
            lblTipoRegresion.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40);
            btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40);

            // ==========================================
            // ALINEAR LAS DOS TABLAS (UX CORREGIDO AL 100%)
            // ==========================================
            int tablaSumatoriasY = boxY + boxAlto + 20;

            // Le damos 200px para que quepan todas las filas sin hacer scroll, 
            // incluso si es Grado 3 (que tira más coeficientes).
            int altoResultados = 200;
            int tablaFinalY = this.ClientSize.Height - altoResultados - 20;

            // La tabla de sumatorias devora el resto del espacio en medio
            int altoSumatorias = tablaFinalY - tablaSumatoriasY - 15;

            dgvSumatorias.Location = new Point(40, tablaSumatoriasY);
            dgvSumatorias.Size = new Size(this.ClientSize.Width - 80, altoSumatorias);

            dgvRegresion.Location = new Point(40, tablaFinalY);
            dgvRegresion.Size = new Size(this.ClientSize.Width - 80, altoResultados);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvSumatorias.Location;
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