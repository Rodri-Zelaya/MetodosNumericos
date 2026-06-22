using System;
using System.Drawing;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormRungeKutta : Form
    {
        private Panel pnlEspera;

        // Controles de entrada dinámicos
        private TextBox txtFuncion = new TextBox();
        private TextBox txtX0 = new TextBox();
        private TextBox txtY0 = new TextBox();
        private TextBox txtXf = new TextBox();
        private TextBox txtN = new TextBox();

        public FormRungeKutta()
        {
            InitializeComponent();
            InicializarControles();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Método de Runge-Kutta (RK4)", "Algoritmo de cuarto orden para la resolución numérica de Ecuaciones Diferenciales Ordinarias (EDO). Ofrece una precisión extremadamente alta al evaluar y promediar cuatro pendientes distintas en cada paso temporal.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void InicializarControles()
        {
            // Fila 1: Ecuación
            this.Controls.Add(new Label { Text = "1. Función f(x, y):", Name = "lblFuncion", AutoSize = true });
            txtFuncion.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtFuncion.Size = new Size(300, 30);
            this.Controls.Add(txtFuncion);

            // Fila 2: Condiciones Iniciales y Parámetros
            this.Controls.Add(new Label { Text = "2. X inicial (x₀):", Name = "lblX0", AutoSize = true });
            txtX0.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtX0.Size = new Size(80, 30);
            this.Controls.Add(txtX0);

            this.Controls.Add(new Label { Text = "3. Y inicial (y₀):", Name = "lblY0", AutoSize = true });
            txtY0.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtY0.Size = new Size(80, 30);
            this.Controls.Add(txtY0);

            this.Controls.Add(new Label { Text = "4. X final (x_f):", Name = "lblXf", AutoSize = true });
            txtXf.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtXf.Size = new Size(80, 30);
            this.Controls.Add(txtXf);

            this.Controls.Add(new Label { Text = "5. Pasos (n):", Name = "lblN", AutoSize = true });
            txtN.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtN.Size = new Size(80, 30);
            this.Controls.Add(txtN);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();

            try
            {
                if (string.IsNullOrWhiteSpace(txtFuncion.Text) || string.IsNullOrWhiteSpace(txtX0.Text) ||
                    string.IsNullOrWhiteSpace(txtY0.Text) || string.IsNullOrWhiteSpace(txtXf.Text) || string.IsNullOrWhiteSpace(txtN.Text))
                {
                    MessageBox.Show("Por favor, ingrese todos los parámetros iniciales y la función.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ecuacion = txtFuncion.Text;
                double x0 = metodos.ConvertirADouble(txtX0.Text);
                double y0 = metodos.ConvertirADouble(txtY0.Text);
                double xf = metodos.ConvertirADouble(txtXf.Text);
                int n = int.Parse(txtN.Text);

                Func<double, double, double> funcionEDO = (x, y) => metodos.EvaluarXY(ecuacion, x, y);

                // 🚀 CONEXIÓN DIRECTA A TU MOTOR RK4
                metodos.EjecutarRungeKutta4(funcionEDO, x0, y0, xf, n, dgvResultados);

                ConfigurarEstiloTuaniTablas(dgvResultados);

                pnlEspera.Visible = false;
                dgvResultados.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla analítica en el procesamiento: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFuncion.Clear(); txtX0.Clear(); txtY0.Clear(); txtXf.Clear(); txtN.Clear();
            dgvResultados.Rows.Clear(); dgvResultados.Columns.Clear();
            dgvResultados.Visible = false; pnlEspera.Visible = true;
        }

        private void ConfigurarEstiloTuaniTablas(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); // Letra un pelo más pequeña para las 8 columnas
            dgv.ColumnHeadersHeight = 35;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgv.RowHeadersVisible = false;
            dgv.DefaultCellStyle.Font = new Font("Consolas", 10, FontStyle.Regular);
            dgv.DefaultCellStyle.Padding = new Padding(3);

            dgv.RowTemplate.Height = 40;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            if (dgv.Columns.Count > 0)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int fila1Y = 40;
            int fila2Y = 110;

            // Fila 1: Ecuación
            MoverControl("lblFuncion", 40, fila1Y - 25);
            txtFuncion.Location = new Point(40, fila1Y);

            // Fila 2: Variables (Con el espaciado de 160px corregido)
            int espaciado = 160;

            txtX0.Location = new Point(40, fila2Y); MoverControl("lblX0", 40, fila2Y - 25);
            txtY0.Location = new Point(40 + espaciado, fila2Y); MoverControl("lblY0", 40 + espaciado, fila2Y - 25);
            txtXf.Location = new Point(40 + espaciado * 2, fila2Y); MoverControl("lblXf", 40 + espaciado * 2, fila2Y - 25);
            txtN.Location = new Point(40 + espaciado * 3, fila2Y); MoverControl("lblN", 40 + espaciado * 3, fila2Y - 25);

            // Botones
            int btnX = this.ClientSize.Width - 170;
            if (btnLimpiar != null) { btnLimpiar.Location = new Point(btnX, fila2Y - 5); btnLimpiar.Size = new Size(130, 40); }
            if (btnCalcular != null) { btnCalcular.Location = new Point(btnX -= 145, fila2Y - 5); btnCalcular.Size = new Size(130, 40); }

            int tablaY = fila2Y + 60;
            dgvResultados.Location = new Point(40, tablaY);
            dgvResultados.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);

            if (pnlEspera != null)
            {
                pnlEspera.Location = dgvResultados.Location;
                pnlEspera.Size = dgvResultados.Size;
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
                else if (control is Label lbl) { lbl.ForeColor = Color.Black; lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold); }
                else if (control is TextBox txt) { txt.BorderStyle = BorderStyle.FixedSingle; }

                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvResultados.Visible = false;
            pnlEspera = new Panel { BackColor = Color.FromArgb(243, 244, 246) };

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(245, 158, 11) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "Cuatro Pendientes:\nk₁ = f(x_i, y_i)\nk₂ = f(x_i + h/2, y_i + k₁·h/2)\nk₃ = f(x_i + h/2, y_i + k₂·h/2)\nk₄ = f(x_i + h, y_i + k₃·h)\n\nFórmula de Combinación:\ny_i₊₁ = y_i + (h/6)·(k₁ + 2k₂ + 2k₃ + k₄)", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(253, 230, 138), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 170), Location = new Point(25, 280) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(245, 158, 11), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Alta Precisión:\n\nRK4 es un método de cuarto orden, lo que significa que su error de truncamiento global es del orden de O(h⁴).\n\nEs el estándar de la ingeniería por lograr un equilibrio perfecto entre costo de cómputo y exactitud analítica.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Modo de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Ingresa la ecuación diferencial f(x, y).\n[ 2 ]  Establece la condición inicial P(x₀, y₀).\n[ 3 ]  Define hasta dónde evaluar (x_f).\n[ 4 ]  Asigna el número total de pasos (n).", Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) => { pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2); pnlEspera.Invalidate(); };
        }

        private void MoverControl(string nombre, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl.Name == nombre) { ctrl.Location = new Point(x, y); break; }
        }
    }
}