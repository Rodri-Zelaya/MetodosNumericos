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

        // Variables globales para el módulo predictivo
        private double[] coeficientesModelados = null;
        private TextBox txtXEstimar;
        private Label lblYEstimado;
        private Button btnEstimar;

        public FormRegresionPolinomial()
        {
            InitializeComponent();
            InyectarModuloPredictivo();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Ajuste de Curvas", "Mínimos Cuadrados. Introduce tus puntos en la tabla. El sistema generará automáticamente la Tabla de Sumatorias completa, el modelo matemático a 8 decimales y sus métricas integradas.");

            numGrado.ValueChanged += (s, e) => ActualizarTextoTipo();
            ActualizarTextoTipo();

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void InyectarModuloPredictivo()
        {
            Label lblTituloEstimar = new Label { Text = "🔮 Estimar (X):", AutoSize = true, Font = new Font("Segoe UI",11, FontStyle.Bold), Name = "lblEstimar" };
            txtXEstimar = new TextBox { Size = new Size(100, 28), Enabled = false, Font = new Font("Segoe UI", 11) };
            btnEstimar = new Button { Text = "Predecir Y", Size = new Size(100, 32), Enabled = false, BackColor = Color.FromArgb(13, 148, 136), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnEstimar.FlatAppearance.BorderSize = 0;
            lblYEstimado = new Label { Text = "Y predicho = ---", AutoSize = true, Font = new Font("Consolas", 12, FontStyle.Bold), ForeColor = Color.FromArgb(220, 38, 38) };

            btnEstimar.Click += BtnEstimar_Click;

            this.Controls.Add(lblTituloEstimar);
            this.Controls.Add(txtXEstimar);
            this.Controls.Add(btnEstimar);
            this.Controls.Add(lblYEstimado);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();
            // 🚀 Lista auxiliar para contar las 'X' únicas
            HashSet<double> puntosXUnicos = new HashSet<double>();

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
                            double xParseado = metodos.ConvertirADouble(valX);
                            listX.Add(xParseado);
                            listY.Add(metodos.ConvertirADouble(valY));

                            // 🚀 Agregamos al conjunto de valores únicos
                            puntosXUnicos.Add(xParseado);
                        }
                    }
                }

                if (listX.Count < 2)
                {
                    MessageBox.Show("Por favor, ingresa al menos 2 puntos (X, Y) para calcular.", "Datos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int grado = (int)numGrado.Value;

                // 🚀 VALIDACIÓN BLINDADA: Verificar puntos X distintos
                if (puntosXUnicos.Count <= grado)
                {
                    MessageBox.Show($"Para calcular un polinomio de Grado {grado}, necesitas al menos {grado + 1} puntos con valores de X que sean DIFERENTES entre sí.\n\nActualmente tienes datos redundantes que provocarían una matriz singular (sin solución única).", "Falta Dispersión de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ecuacion;
                double r2, r;

                // 🚀 INVOCAR MOTOR POTENCIADO
                metodos.RegresionPolinomialCompleta(listX.ToArray(), listY.ToArray(), grado, dgvSumatorias, dgvRegresion, out coeficientesModelados, out ecuacion, out r2, out r);

                // =========================================================
                // 🚀 INYECTAR ECUACIÓN Y MÉTRICAS DIRECTO AL DATAGRIDVIEW
                // =========================================================
                int idxEsp = dgvRegresion.Rows.Add("", "");
                dgvRegresion.Rows[idxEsp].Height = 10;
                dgvRegresion.Rows[idxEsp].DefaultCellStyle.BackColor = Color.White;

                int idxEq = dgvRegresion.Rows.Add("📈 Modelo Matemático (Y)", ecuacion);
                dgvRegresion.Rows[idxEq].DefaultCellStyle.BackColor = Color.FromArgb(16, 185, 129); // Verde esmeralda
                dgvRegresion.Rows[idxEq].DefaultCellStyle.ForeColor = Color.White;
                dgvRegresion.Rows[idxEq].DefaultCellStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
                dgvRegresion.Rows[idxEq].Height = 40;

                int idxR2 = dgvRegresion.Rows.Add("📊 Coef. Determinación (R²)", r2.ToString("F8"));
                dgvRegresion.Rows[idxR2].DefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55); // Gris muy oscuro
                dgvRegresion.Rows[idxR2].DefaultCellStyle.ForeColor = Color.White;
                dgvRegresion.Rows[idxR2].DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Bold);

                int idxR = dgvRegresion.Rows.Add("📉 Coef. Correlación (r)", r.ToString("F8"));
                dgvRegresion.Rows[idxR].DefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55);
                dgvRegresion.Rows[idxR].DefaultCellStyle.ForeColor = Color.White;
                dgvRegresion.Rows[idxR].DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Bold);

                // Habilitar predictor
                txtXEstimar.Enabled = true;
                btnEstimar.Enabled = true;

                // Aplicar estilos "Tuani" a ambas tablas
                ConfigurarEstiloTuaniTablas(dgvSumatorias);
                ConfigurarEstiloTuaniTablas(dgvRegresion);

                pnlEspera.Visible = false;
                dgvSumatorias.Visible = true;
                dgvRegresion.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla en el cálculo: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEstimar_Click(object sender, EventArgs e)
        {
            if (coeficientesModelados == null || string.IsNullOrWhiteSpace(txtXEstimar.Text)) return;

            try
            {
                MetodosNumericos metodos = new MetodosNumericos();
                double xVal = metodos.ConvertirADouble(txtXEstimar.Text);
                double yResultado = 0;

                for (int i = 0; i < coeficientesModelados.Length; i++)
                    yResultado += coeficientesModelados[i] * Math.Pow(xVal, i);

                lblYEstimado.Text = $"Y predicho = {yResultado.ToString("F8")}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al evaluar el modelo: " + ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (coeficientesModelados == null)
                {
                    MessageBox.Show("Primero debes calcular el modelo antes de exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                new MetodosNumericos().ExportarRegresionAExcelCompleto(dgvSumatorias, dgvRegresion);
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
            txtXEstimar.Text = "";

            coeficientesModelados = null;
            txtXEstimar.Enabled = false;
            btnEstimar.Enabled = false;
            lblYEstimado.Text = "Y predicho = ---";

            dgvSumatorias.Visible = false;
            dgvRegresion.Visible = false;
            pnlEspera.Visible = true;
        }

        private void ActualizarTextoTipo()
        {
            int grado = (int)numGrado.Value;
            switch (grado)
            {
                case 1: lblTipoRegresion.Text = "➔ Línea Recta (Regresión Simple)"; lblTipoRegresion.ForeColor = Color.FromArgb(79, 70, 229); break;
                case 2: lblTipoRegresion.Text = "➔ Parábola (Regresión Cuadrática)"; lblTipoRegresion.ForeColor = Color.FromArgb(13, 148, 136); break;
                case 3: lblTipoRegresion.Text = "➔ Curva Cúbica (Regresión Cúbica)"; lblTipoRegresion.ForeColor = Color.FromArgb(219, 39, 119); break;
                default: lblTipoRegresion.Text = $"➔ Polinomio de Grado {grado}"; lblTipoRegresion.ForeColor = Color.FromArgb(55, 65, 81); break;
            }
        }

        // =================================================================
        // 🎨 DISEÑO TUANI MINIMALISTA PARA TABLAS
        // =================================================================
        private void ConfigurarEstiloTuaniTablas(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgv.RowHeadersVisible = false;
            dgv.DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Regular);
            dgv.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            dgv.RowTemplate.Height = 32;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);

            // 🚀 AQUÍ ESTÁ EL CAMBIO (Cambiar Fill por AllCells)
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
        }

        private void AplicarEstiloTuani(Control.ControlCollection controles)
        {
            this.BackColor = Color.FromArgb(243, 244, 246);
            foreach (Control control in controles)
            {
                if (control is Button btn && btn != btnEstimar)
                {
                    btn.UseVisualStyleBackColor = false; btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(17, 24, 39); btn.ForeColor = Color.White; btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand; btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 65, 81);
                }
                else if (control is Label lbl && lbl != lblYEstimado) { lbl.ForeColor = Color.Black; lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold); }
                else if (control is NumericUpDown num) { num.Font = new Font("Segoe UI", 12, FontStyle.Bold); num.BorderStyle = BorderStyle.FixedSingle; }
                else if (control is DataGridView dgv && dgv == dgvDatos) // Solo a la tabla de entrada inicial
                {
                    dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.FixedSingle; dgv.GridColor = Color.FromArgb(220, 225, 230);
                }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvSumatorias.Visible = false;
            dgvRegresion.Visible = false;
            pnlEspera = new Panel { BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(13, 148, 136) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "Sistema de Ecuaciones:\n[ ∑X^i ] · [ b ] = [ ∑X^i·Y ]\n\nr  = Coef. de Correlación\nR² = Coef. de Determinación", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(165, 180, 252), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 220), Location = new Point(25, 190) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(13, 148, 136), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Predictor Integrado:\n\nUna vez generado el modelo con sus 8 decimales de precisión, puedes usar el panel de estimación para predecir cualquier valor de Y basado en la tendencia de la curva calculada.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Digita tus puntos en la tabla de entrada.\n[ 2 ]  Selecciona el Grado del polinomio arriba.\n[ 3 ]  Calcula el modelo y utiliza el predictor.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            AcomodarPanelEspera(pnlTarjeta);
            pnlEspera.Resize += (s, e) => { AcomodarPanelEspera(pnlTarjeta); };
        }

        private void AcomodarPanelEspera(Panel pnlTarjeta)
        {
            if (pnlEspera == null || pnlTarjeta == null) return;
            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Invalidate();
        }

        private void PnlEspera_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = pnlEspera.Width, h = pnlEspera.Height, cY = h / 2;
            Pen pGrid = new Pen(Color.FromArgb(225, 230, 235), 1), pEjes = new Pen(Color.FromArgb(200, 205, 215), 2);
            for (int i = 0; i < w; i += 40) g.DrawLine(pGrid, i, 0, i, h);
            for (int j = 0; j < h; j += 40) g.DrawLine(pGrid, 0, j, w, j);
            g.DrawLine(pEjes, 0, cY, w, cY); g.DrawLine(pEjes, w / 2, 0, w / 2, h);
            Pen p1 = new Pen(Color.FromArgb(13, 148, 136), 3);
            PointF[] pt1 = new PointF[w / 5];
            for (int i = 0; i < pt1.Length; i++) { float x = i * 5; pt1[i] = new PointF(x, (float)(cY + Math.Sin(x * 0.012) * 90 + Math.Cos(x * 0.004) * 30)); }
            if (pt1.Length > 1) { g.DrawCurve(p1, pt1); }
            pGrid.Dispose(); pEjes.Dispose(); p1.Dispose();
        }

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

            // Módulo Predictivo dinámico (Con más espacio para que no choque)
            int predY = boxY + boxAlto + 15;
            MoverLabelPorTexto("Estimar", 40, predY + 4);
            txtXEstimar.Location = new Point(190, predY);      // Lo empujamos de 160 a 190
            btnEstimar.Location = new Point(300, predY - 2);   // Lo empujamos de 270 a 300
            lblYEstimado.Location = new Point(420, predY + 3); // Lo empujamos de 390 a 420

            // ==========================================
            // ALINEACIÓN DE TABLAS (SIN LABELS ABAJO)
            // ==========================================
            int tablaSumatoriasY = predY + 45;
            int altoResultados = 210; // Creció porque ahora alberga la ecuación y las métricas
            int tablaFinalY = this.ClientSize.Height - altoResultados - 20;

            int altoSumatorias = tablaFinalY - tablaSumatoriasY - 15;

            dgvSumatorias.Location = new Point(40, tablaSumatoriasY);
            dgvSumatorias.Size = new Size(this.ClientSize.Width - 80, Math.Max(100, altoSumatorias));

            dgvRegresion.Location = new Point(40, tablaFinalY);
            dgvRegresion.Size = new Size(this.ClientSize.Width - 80, altoResultados);

            if (pnlEspera != null)
            {
                pnlEspera.Location = new Point(40, tablaSumatoriasY);
                pnlEspera.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaSumatoriasY - 20);
            }
        }

        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl && (lbl.Text.ToLower().Contains(palabraClave.ToLower()) || lbl.Name.ToLower().Contains(palabraClave.ToLower())))
                {
                    lbl.Location = new Point(x, y);
                    break;
                }
            }
        }
    }
}