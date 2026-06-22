using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormEuler : Form
    {
        private Panel pnlEspera;

        // Controles de entrada dinámicos
        private TextBox txtFuncion = new TextBox();
        private ComboBox cmbMetodo = new ComboBox(); // 🚀 EL NUEVO SELECTOR
        private TextBox txtX0 = new TextBox();
        private TextBox txtY0 = new TextBox();
        private TextBox txtXf = new TextBox();
        private TextBox txtN = new TextBox();

        public FormEuler()
        {
            InitializeComponent();
            InicializarControles();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Métodos de Euler (EDOs)", "Resuelve Ecuaciones Diferenciales Ordinarias usando aproximaciones de primer orden o su variante predictora-correctora (Heun) para mayor precisión.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void InicializarControles()
        {
            // Fila 1: Ecuación y Selector de Método
            this.Controls.Add(new Label { Text = "1. Función f(x, y):", Name = "lblFuncion", AutoSize = true });
            txtFuncion.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtFuncion.Size = new Size(200, 30);
            this.Controls.Add(txtFuncion);

            this.Controls.Add(new Label { Text = "2. Método a utilizar:", Name = "lblMetodo", AutoSize = true });
            cmbMetodo.Font = new Font("Segoe UI", 12, FontStyle.Bold); cmbMetodo.Size = new Size(220, 30);
            cmbMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodo.Items.Add("Euler Simple");
            cmbMetodo.Items.Add("Euler Mejorado (Heun)");
            cmbMetodo.SelectedIndex = 0; // Por defecto Euler Simple
            // Si cambian de método, regresamos a la pantalla de espera
            cmbMetodo.SelectedIndexChanged += (s, e) => { dgvResultados.Visible = false; pnlEspera.Visible = true; };
            this.Controls.Add(cmbMetodo);

            // Fila 2: Condiciones Iniciales y Parámetros
            this.Controls.Add(new Label { Text = "3. X inicial (x₀):", Name = "lblX0", AutoSize = true });
            txtX0.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtX0.Size = new Size(80, 30);
            this.Controls.Add(txtX0);

            this.Controls.Add(new Label { Text = "4. Y inicial (y₀):", Name = "lblY0", AutoSize = true });
            txtY0.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtY0.Size = new Size(80, 30);
            this.Controls.Add(txtY0);

            this.Controls.Add(new Label { Text = "5. X final (x_f):", Name = "lblXf", AutoSize = true });
            txtXf.Font = new Font("Segoe UI", 12, FontStyle.Bold); txtXf.Size = new Size(80, 30);
            this.Controls.Add(txtXf);

            this.Controls.Add(new Label { Text = "6. Pasos (n):", Name = "lblN", AutoSize = true });
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

                // Conexión al evaluador de funciones de 2 variables
                Func<double, double, double> funcionEDO = (x, y) => metodos.EvaluarXY(ecuacion, x, y);

                // 🚀 ENRUTADOR INTELIGENTE DE MÉTODOS
                if (cmbMetodo.SelectedIndex == 0)
                {
                    metodos.EjecutarEulerSimple(funcionEDO, x0, y0, xf, n, dgvResultados);
                }
                else
                {
                    metodos.EjecutarEulerMejorado(funcionEDO, x0, y0, xf, n, dgvResultados);
                }

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
            cmbMetodo.SelectedIndex = 0;
            dgvResultados.Rows.Clear(); dgvResultados.Columns.Clear();
            dgvResultados.Visible = false; pnlEspera.Visible = true;
        }

        // =====================================================================
        // 🎨 MAQUETACIÓN Y CAPA ESTÉTICA
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
            dgv.DefaultCellStyle.Padding = new Padding(5);

            dgv.RowTemplate.Height = 45;
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

            // Ajuste dinámico de columnas sin importar si son 5 (Simple) o 7 (Mejorado)
            if (dgv.Columns.Count > 0)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // La última columna rellena el espacio
            }
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int fila1Y = 40;
            int fila2Y = 110;

            // Fila 1: Ecuación y Combo
            MoverControl("lblFuncion", 40, fila1Y - 25);
            txtFuncion.Location = new Point(40, fila1Y);
            txtFuncion.Width = 250; // Hacemos la caja de la ecuación más larga

            MoverControl("lblMetodo", 340, fila1Y - 25); // Lo empujamos más a la derecha
            cmbMetodo.Location = new Point(340, fila1Y);

            // 🚀 EL ARREGLO ESTÁ AQUÍ: 160 píxeles de separación
            int espaciado = 160;

            txtX0.Location = new Point(40, fila2Y);
            MoverControl("lblX0", 40, fila2Y - 25);

            txtY0.Location = new Point(40 + espaciado, fila2Y);
            MoverControl("lblY0", 40 + espaciado, fila2Y - 25);

            txtXf.Location = new Point(40 + espaciado * 2, fila2Y);
            MoverControl("lblXf", 40 + espaciado * 2, fila2Y - 25);

            txtN.Location = new Point(40 + espaciado * 3, fila2Y);
            MoverControl("lblN", 40 + espaciado * 3, fila2Y - 25);

            // Botones
            int btnX = this.ClientSize.Width - 170;
            if (btnLimpiar != null) { btnLimpiar.Location = new Point(btnX, fila2Y - 5); btnLimpiar.Size = new Size(130, 40); }
            if (btnCalcular != null) { btnCalcular.Location = new Point(btnX -= 145, fila2Y - 5); btnCalcular.Size = new Size(130, 40); }

            // Tabla
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
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(245, 158, 11) }); // Ribete Ámbar para EDOs

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "Paso Espacial:\nh = (x_f - x_0) / n\n\nEuler Simple:\ny_i₊₁ = y_i + h · f(x_i, y_i)\n\nEuler Mejorado (Heun):\nUsa promedio de pendientes para\nreducir el error de truncamiento.", Font = new Font("Consolas", 8, FontStyle.Bold), ForeColor = Color.FromArgb(253, 230, 138), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 210) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(245, 158, 11), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Teoría del Método:\n\nEuler asume que la pendiente de la curva se mantiene constante en el intervalo 'h'.\n\nEl método de Heun (Mejorado) corrige esta suposición evaluando la derivada al inicio y al final del paso.", Font = new Font("Segoe UI", 8), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Modo de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Ingresa la ecuación diferencial f(x, y).\n[ 2 ]  Selecciona la variante de Euler a utilizar.\n[ 3 ]  Establece la condición inicial P(x₀, y₀).\n[ 4 ]  Define hasta dónde evaluar (x_f) y el paso (n).", Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
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