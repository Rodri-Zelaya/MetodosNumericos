using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormExtrapolaciónRichardson : Form
    {
        private Panel pnlEspera;

        // Controles de entrada manuales
        private TextBox txtDh = new TextBox();
        private TextBox txtDhMedios = new TextBox();
        private TextBox txtOrden = new TextBox();
        private TextBox txtExacto = new TextBox();

        public FormExtrapolaciónRichardson()
        {
            InitializeComponent();
            InicializarCajasTexto();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Extrapolación de Richardson", "Método de refinamiento de resultados. Combina dos estimaciones numéricas de derivadas (con pasos h y h/2) para obtener una aproximación de orden superior mucho más exacta.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void InicializarCajasTexto()
        {
            // Propiedades base para las cajas
            TextBox[] cajas = { txtDh, txtDhMedios, txtOrden, txtExacto };
            foreach (var txt in cajas)
            {
                txt.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.Size = new Size(180, 30);
                this.Controls.Add(txt);
            }
            txtOrden.Text = "2"; // Por defecto, diferencia central es de orden 2
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
                double n = metodos.ConvertirADouble(txtOrden.Text);

                // 🚀 ARRANCAR MOTOR DE RICHARDSON
                metodos.ExtrapolacionRichardson(Dh, DhMedios, n, txtExacto.Text, dgvResultados);

                // 🚀 ESTILOS PARA LA TABLA DE RESULTADOS
                dgvResultados.EnableHeadersVisualStyles = false;
                dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvResultados.RowHeadersVisible = false;
                dgvResultados.DefaultCellStyle.BackColor = Color.White;
                dgvResultados.DefaultCellStyle.ForeColor = Color.Black;
                dgvResultados.DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Regular);
                dgvResultados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);

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
            dgvResultados.Visible = false;
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
                }
                else if (control is Label lbl) { lbl.ForeColor = Color.Black; lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold); }
                else if (control is DataGridView dgv) { dgv.BackgroundColor = Color.White; dgv.BorderStyle = BorderStyle.FixedSingle; dgv.GridColor = Color.FromArgb(220, 225, 230); }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvResultados.Visible = false;
            pnlEspera = new Panel { Location = new Point(40, 260), Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - 280), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(59, 130, 246) }); // Azul brillante

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Fórmula de Extrapolación", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "ER(h) = ( 2^n * D(h/2) - D(h) ) / ( 2^n - 1 )", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(147, 197, 253), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 160) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(59, 130, 246), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Nota Analítica:\n\n'n' representa el orden del error. Si tus datos provienen de una Diferencia Central, n=2.\n\nEl algoritmo combinará las derivadas para eliminar el error dominante, saltando la precisión a un orden O(h^4).", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
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

            // Fijar posiciones de las cajas de texto
            txtDh.Location = new Point(40, boxY);
            txtDhMedios.Location = new Point(240, boxY);
            txtOrden.Location = new Point(440, boxY);
            txtExacto.Location = new Point(640, boxY);

            // Alinear las etiquetas exactamente arriba de sus cajas
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl)
                {
                    if (lbl.Text.ToLower().Contains("d(h):")) lbl.Location = new Point(40, boxY - 25);
                    else if (lbl.Text.ToLower().Contains("d(h/2)")) lbl.Location = new Point(240, boxY - 25);
                    else if (lbl.Text.ToLower().Contains("orden")) lbl.Location = new Point(440, boxY - 25);
                    else if (lbl.Text.ToLower().Contains("exacto")) lbl.Location = new Point(640, boxY - 25);
                }
            }

            // Alinear botones
            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY - 5); btnLimpiar.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY - 5); btnCalcular.Size = new Size(130, 40);

            // Alinear tabla y panel
            int tablaY = boxY + 70;
            dgvResultados.Location = new Point(40, tablaY);
            dgvResultados.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvResultados.Location;
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