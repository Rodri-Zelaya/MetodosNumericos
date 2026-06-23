using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormExtrapolaciónRichardson : Form
    {
        private Panel pnlEspera;

        // Controles dinámicos
        private TextBox txtDh = new TextBox();
        private TextBox txtDhMedios = new TextBox();
        private TextBox txtOrden = new TextBox();
        private TextBox txtExacto = new TextBox();

        public FormExtrapolaciónRichardson()
        {
            InitializeComponent();
            InicializarCajasYEtiquetas();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Extrapolación de Richardson", "Método de refinamiento de resultados. Combina dos estimaciones numéricas de derivadas (con pasos h y h/2) para obtener una aproximación de orden superior mucho más exacta.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void InicializarCajasYEtiquetas()
        {
            this.Controls.Add(new Label { Text = "1. D(h):", Name = "lblDh", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtDh.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtDh.BorderStyle = BorderStyle.FixedSingle; txtDh.Size = new Size(130, 30);
            this.Controls.Add(txtDh);

            this.Controls.Add(new Label { Text = "2. D(h/2):", Name = "lblDhMedios", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtDhMedios.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtDhMedios.BorderStyle = BorderStyle.FixedSingle; txtDhMedios.Size = new Size(130, 30);
            this.Controls.Add(txtDhMedios);

            this.Controls.Add(new Label { Text = "3. Orden (n):", Name = "lblOrden", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtOrden.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtOrden.BorderStyle = BorderStyle.FixedSingle; txtOrden.Size = new Size(70, 30);
            txtOrden.Text = "2";
            this.Controls.Add(txtOrden);

            this.Controls.Add(new Label { Text = "4. Valor Exacto:", Name = "lblExacto", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            txtExacto.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtExacto.BorderStyle = BorderStyle.FixedSingle; txtExacto.Size = new Size(130, 30);
            this.Controls.Add(txtExacto);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();

            try
            {
                if (string.IsNullOrWhiteSpace(txtDh.Text) || string.IsNullOrWhiteSpace(txtDhMedios.Text) || string.IsNullOrWhiteSpace(txtOrden.Text))
                {
                    MessageBox.Show("Por favor, ingrese D(h), D(h/2) y el orden de aproximación 'n'.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double Dh = metodos.ConvertirADouble(txtDh.Text);
                double DhMedios = metodos.ConvertirADouble(txtDhMedios.Text);

                // 🚀 Forzamos a que el orden sea un número entero
                int n = (int)Math.Round(metodos.ConvertirADouble(txtOrden.Text));

                // 🚀 VALIDACIÓN BLINDADA: Evitar la División por Cero
                if (n <= 0)
                {
                    MessageBox.Show("El Orden (n) del error de truncamiento debe ser un número entero estrictamente mayor a cero.\n\nSi n = 0, la fórmula genera una división por cero.", "Violación Matemática", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Restauramos el valor por defecto para ayudar al usuario
                    txtOrden.Text = "2";
                    return;
                }

                double? exacto = null;
                if (!string.IsNullOrWhiteSpace(txtExacto.Text)) exacto = metodos.ConvertirADouble(txtExacto.Text);

                // 🚀 ARRANCAR MOTOR
                metodos.ExtrapolacionRichardson(Dh, DhMedios, n, exacto, dgvResultados);

                ConfigurarEstiloTuaniTablas(dgvResultados);

                pnlEspera.Visible = false;
                dgvResultados.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error analítico: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDh.Clear();
            txtDhMedios.Clear();
            txtOrden.Text = "2";
            txtExacto.Clear();
            dgvResultados.Rows.Clear();
            dgvResultados.Columns.Clear();
            dgvResultados.Visible = false;
            pnlEspera.Visible = true;
        }

        // =====================================================================
        // 🚀 ESTILO ANTI-APLASTAMIENTO Y ANTI-AZUL FEO DE WINDOWS
        // =====================================================================
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

            // 🚀 ESTOS DOS MATAN EL AZUL POR DEFECTO
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249); // Un gris/celeste súper sutil
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowTemplate.Height = 60;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            if (dgv.Columns.Count >= 3)
            {
                dgv.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[0].MinimumWidth = 220;
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                }
                else if (control is Label lbl) { lbl.ForeColor = Color.Black; }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvResultados.Visible = false;
            pnlEspera = new Panel { Location = new Point(40, 260), Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - 280), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(59, 130, 246) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Fórmula de Extrapolación", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "ER(h) = ( 2^n * D(h/2) - D(h) ) / ( 2^n - 1 )", Font = new Font("Consolas", 8, FontStyle.Bold), ForeColor = Color.FromArgb(147, 197, 253), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 160) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(59, 130, 246), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Nota Analítica:\n\n'n' representa el orden del error. Si tus datos provienen de una Diferencia Central, n=2.\n\nEl algoritmo combinará las derivadas para eliminar el error dominante.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Parámetros Requeridos", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Ingresa D(h) obtenida con un paso grande.\n\n[ 2 ]  Ingresa D(h/2) obtenida con la mitad del paso.\n\n[ 3 ]  (Opcional) Ingresa el valor exacto para calcular error.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
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
            pGrid.Dispose(); pEjes.Dispose();
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 80;

            // 🚀 SEPARAMOS MÁS LAS CAJAS PARA QUE NO SE COMAN LOS TEXTOS
            txtDh.Location = new Point(40, boxY);
            txtDhMedios.Location = new Point(200, boxY);
            txtOrden.Location = new Point(360, boxY);
            txtExacto.Location = new Point(460, boxY); // Movido más a la derecha

            MoverControl("lblDh", 40, boxY - 25);
            MoverControl("lblDhMedios", 200, boxY - 25);
            MoverControl("lblOrden", 360, boxY - 25);
            MoverControl("lblExacto", 460, boxY - 25);

            int btnX = this.ClientSize.Width - 170;
            if (btnLimpiar != null) { btnLimpiar.Location = new Point(btnX, boxY - 5); btnLimpiar.Size = new Size(130, 40); }
            if (btnCalcular != null) { btnCalcular.Location = new Point(btnX -= 145, boxY - 5); btnCalcular.Size = new Size(130, 40); }

            int tablaY = boxY + 70;
            dgvResultados.Location = new Point(40, tablaY);
            dgvResultados.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvResultados.Location;
                pnlEspera.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);
            }
        }

        private void MoverControl(string nombreOriginal, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl.Name == nombreOriginal) { ctrl.Location = new Point(x, y); break; }
        }
    }
}