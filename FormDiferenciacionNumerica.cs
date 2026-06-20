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

        // Nuevos controles para el Modo Focalizado
        private TextBox txtX0;
        private TextBox txtH;
        private TextBox txtExacto;
        private DataGridView dgvResultados;

        public FormDiferenciacionNumerica()
        {
            InitializeComponent();
            InyectarControlesFocalizados();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Diferenciación Focalizada", "Evalúa derivadas en un punto X₀ específico. Ingresa los puntos en la tabla (o calcula los 3 valores f(X₀-h), f(X₀), f(X₀+h) si tienes una función) y el sistema hará la sustitución analítica paso a paso.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void InyectarControlesFocalizados()
        {
            // Parámetros
            this.Controls.Add(new Label { Text = "1. Punto a Evaluar (X₀):", Name = "lblX0", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtX0 = new TextBox { Size = new Size(100, 30), Font = new Font("Segoe UI", 12, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(txtX0);

            this.Controls.Add(new Label { Text = "2. Tamaño de Paso (h):", Name = "lblH", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtH = new TextBox { Size = new Size(100, 30), Font = new Font("Segoe UI", 12, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(txtH);

            this.Controls.Add(new Label { Text = "3. Valor Exacto f'(X₀) [Para el Error]:", Name = "lblExacto", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtExacto = new TextBox { Size = new Size(120, 30), Font = new Font("Segoe UI", 12, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(txtExacto);

            // Nueva Tabla de Resultados
            dgvResultados = new DataGridView();
            this.Controls.Add(dgvResultados);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();

            try
            {
                // Extraer Puntos
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

                if (string.IsNullOrWhiteSpace(txtX0.Text) || string.IsNullOrWhiteSpace(txtH.Text))
                {
                    MessageBox.Show("Por favor, ingresa el valor de X₀ y el paso h.", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double x0 = metodos.ConvertirADouble(txtX0.Text);
                double h = metodos.ConvertirADouble(txtH.Text);
                double? vExacto = null;
                if (!string.IsNullOrWhiteSpace(txtExacto.Text)) vExacto = metodos.ConvertirADouble(txtExacto.Text);

                double xBack = Math.Round(x0 - h, 6);
                double xForw = Math.Round(x0 + h, 6);

                // Buscar los 3 puntos clave en la tabla ingresada
                bool hasY0 = EncontrarEnTabla(x0, listX, listY, out double y0);
                bool hasYBack = EncontrarEnTabla(xBack, listX, listY, out double yBack);
                bool hasYForw = EncontrarEnTabla(xForw, listX, listY, out double yForw);

                if (!hasY0)
                {
                    MessageBox.Show($"El punto central X = {x0} no se encontró en tu tabla.", "Falta Punto Central", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Configurar Tabla de Resultados
                dgvResultados.Columns.Clear();
                dgvResultados.Rows.Clear();
                dgvResultados.Columns.Add("Metodo", "Método Diferencial");
                dgvResultados.Columns.Add("Sustitucion", "Sustitución en la Fórmula");
                dgvResultados.Columns.Add("Resultado", "Aproximación");
                dgvResultados.Columns.Add("Error", "Error Absoluto |E|");

                // 1. HACIA ADELANTE
                if (hasYForw)
                {
                    double res = (yForw - y0) / h;
                    string sust = $"[f({xForw}) - f({x0})] / {h}   ➔   [{yForw} - {y0}] / {h}";
                    dgvResultados.Rows.Add("Hacia Adelante", sust, res.ToString("F6"), CalcularError(res, vExacto));
                }
                else dgvResultados.Rows.Add("Hacia Adelante", $"Falta f({xForw}) en la tabla", "---", "---");

                // 2. HACIA ATRÁS
                if (hasYBack)
                {
                    double res = (y0 - yBack) / h;
                    string sust = $"[f({x0}) - f({xBack})] / {h}   ➔   [{y0} - {yBack}] / {h}";
                    dgvResultados.Rows.Add("Hacia Atrás", sust, res.ToString("F6"), CalcularError(res, vExacto));
                }
                else dgvResultados.Rows.Add("Hacia Atrás", $"Falta f({xBack}) en la tabla", "---", "---");

                // 3. CENTRAL (1ra Derivada)
                if (hasYForw && hasYBack)
                {
                    double res = (yForw - yBack) / (2 * h);
                    string sust = $"[f({xForw}) - f({xBack})] / 2({h})   ➔   [{yForw} - {yBack}] / {2 * h}";
                    int idxCentral = dgvResultados.Rows.Add("Central (Mejor Aproximación)", sust, res.ToString("F6"), CalcularError(res, vExacto));

                    dgvResultados.Rows[idxCentral].DefaultCellStyle.BackColor = Color.FromArgb(16, 185, 129); // Verde Esmeralda
                    dgvResultados.Rows[idxCentral].DefaultCellStyle.ForeColor = Color.White;
                    dgvResultados.Rows[idxCentral].DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Bold);
                }
                else dgvResultados.Rows.Add("Central (1ra Derivada)", $"Faltan puntos perimetrales", "---", "---");

                // 4. CENTRAL (2da Derivada / Aceleración)
                if (hasYForw && hasYBack)
                {
                    double res2 = (yForw - 2 * y0 + yBack) / (h * h);
                    string sust2 = $"[f({xForw}) - 2f({x0}) + f({xBack})] / {h}²   ➔   [{yForw} - 2({y0}) + {yBack}] / {h * h}";
                    int idxSegunda = dgvResultados.Rows.Add("Segunda Derivada f''(X₀)", sust2, res2.ToString("F6"), "--- (Requiere Exacto f'')");
                    dgvResultados.Rows[idxSegunda].DefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55);
                    dgvResultados.Rows[idxSegunda].DefaultCellStyle.ForeColor = Color.White;
                }

                ConfigurarEstiloTuaniTablas(dgvResultados);

                pnlEspera.Visible = false;
                dgvResultados.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la rutina analítica: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EncontrarEnTabla(double target, List<double> X, List<double> Y, out double yVal)
        {
            yVal = 0;
            for (int i = 0; i < X.Count; i++)
            {
                if (Math.Abs(X[i] - target) < 1e-5) // Tolerancia para evitar fallos de decimales de C#
                {
                    yVal = Y[i];
                    return true;
                }
            }
            return false;
        }

        private string CalcularError(double aproximado, double? exacto)
        {
            if (!exacto.HasValue) return "---";
            double errorAbsoluto = Math.Abs(exacto.Value - aproximado);
            return errorAbsoluto.ToString("F6");
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try { new MetodosNumericos().ExportarAExcel(dgvResultados); }
            catch (Exception ex) { MessageBox.Show("Error al exportar: " + ex.Message); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvPuntos.Rows.Clear();
            dgvResultados.Rows.Clear();
            dgvResultados.Columns.Clear();
            txtX0.Clear();
            txtH.Clear();
            txtExacto.Clear();

            dgvResultados.Visible = false;
            pnlEspera.Visible = true;
        }

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
            dgv.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);

            // 🚀 EL SECRETO PARA QUE NO SE APLASTE EL TEXTO
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // La fila crece sola hacia abajo

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            // 🚀 REPARTIR EL ESPACIO DE LAS COLUMNAS INTELIGENTEMENTE
            if (dgv.Columns.Count >= 4)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Apagamos el general
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Método

                // La columna de Sustitución se roba todo el espacio del centro para respirar
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Resultado
                dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Error
            }
        }

        private void AplicarEstiloTuani(Control.ControlCollection controles)
        {
            this.BackColor = Color.FromArgb(243, 244, 246);
            foreach (Control control in controles)
            {
                if (control is Button btn)
                {
                    btn.UseVisualStyleBackColor = false; btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(17, 24, 39); btn.ForeColor = Color.White; btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand; btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 65, 81);
                }
                else if (control is Label lbl && lbl.Name != "lblX0" && lbl.Name != "lblH" && lbl.Name != "lblExacto") { lbl.ForeColor = Color.Black; lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold); }
                else if (control is DataGridView dgv && dgv == dgvPuntos)
                {
                    dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.FixedSingle; dgv.GridColor = Color.FromArgb(220, 225, 230);
                }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvResultados.Visible = false;
            pnlEspera = new Panel { BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(16, 185, 129) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "f'(x) Central:\n[f(X_0+h) - f(X_0-h)] / 2h\n\nError Absoluto:\nE = |Exacto - Aproximado|", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(167, 243, 208), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 180) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(16, 185, 129), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Análisis de Funciones:\n\nSi el problema te da una función como sen(x), solo necesitas calcular manualmente los 3 valores f(X_0-h), f(X_0) y f(X_0+h) e ingresarlos en la tabla.\n\nEl sistema armará la sustitución exacta paso a paso.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Llena X₀, h y el Valor Exacto (opcional).\n[ 2 ]  Ingresa los 3 valores clave en la tabla.\n[ 3 ]  Presiona 'Calcular' para ver la sustitución.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
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
            Pen pPuntos = new Pen(Color.FromArgb(16, 185, 129), 3);
            Pen pTangente = new Pen(Color.FromArgb(245, 158, 11), 2) { DashStyle = DashStyle.Dash };
            g.DrawEllipse(pPuntos, w / 2 - 50, cY - 20, 8, 8); g.DrawEllipse(pPuntos, w / 2, cY - 40, 8, 8); g.DrawEllipse(pPuntos, w / 2 + 50, cY - 80, 8, 8);
            g.DrawLine(pTangente, w / 2 - 40, cY - 10, w / 2 + 40, cY - 70);
            pGrid.Dispose(); pEjes.Dispose(); pPuntos.Dispose(); pTangente.Dispose();
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 80;

            MoverLabelPorTexto("introdu", 40, boxY - 30);
            dgvPuntos.Location = new Point(40, boxY);
            dgvPuntos.Size = new Size(250, 160);

            if (dgvPuntos.Columns.Count >= 2)
            {
                dgvPuntos.RowHeadersWidth = 25;
                dgvPuntos.Columns[0].Width = 100;
                dgvPuntos.Columns[1].Width = 100;
            }

            // Acomodar los nuevos parámetros a la derecha de la tabla
            int panelParametrosX = 330;
            int alineacionCajasX = panelParametrosX + 330;

            MoverControl("lblX0", panelParametrosX, boxY);
            txtX0.Location = new Point(alineacionCajasX, boxY - 3);

            MoverControl("lblH", panelParametrosX, boxY + 50);
            txtH.Location = new Point(alineacionCajasX, boxY + 47);

            MoverControl("lblExacto", panelParametrosX, boxY + 100);
            txtExacto.Location = new Point(alineacionCajasX, boxY + 97);

            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40);
            btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40);

            // ==========================================
            // ALINEACIÓN DE LA TABLA DE RESULTADOS ESPECÍFICOS
            // ==========================================
            int tablaY = boxY + 180;
            dgvResultados.Location = new Point(40, tablaY);
            dgvResultados.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvResultados.Location;
                pnlEspera.Size = dgvResultados.Size;
            }
        }

        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl is Label lbl && (lbl.Text.ToLower().Contains(palabraClave.ToLower()) || lbl.Name.ToLower().Contains(palabraClave.ToLower())))
                { lbl.Location = new Point(x, y); break; }
        }

        private void MoverControl(string nombreOriginal, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl.Name == nombreOriginal) { ctrl.Location = new Point(x, y); break; }
        }
    }
}