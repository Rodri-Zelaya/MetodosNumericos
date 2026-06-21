using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser; // 🚀 Importando mXparser

namespace Métodos_Numéricos
{
    public partial class FormTrapecio : Form
    {
        private Panel pnlEspera;
        private Panel pnlControles;

        // Controles de entrada
        private ComboBox cmbTipoIntegral;
        private TextBox txtFuncion;
        private TextBox txtA, txtB, txtN;
        private TextBox txtC, txtD, txtNy;
        private Label lblCy, lblDy, lblNy;

        public FormTrapecio()
        {
            InitializeComponent();

            // 🚀 Confirmar uso no comercial (Requerido por mXparser v5+)
            License.iConfirmNonCommercialUse("Proyecto Universitario / Tuani");

            ConstruirPanelEntradas();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Motor de Integración", "Evalúa funciones matemáticas (1D y 2D) desde texto utilizando la Regla del Trapecio compuesta con mXparser.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void ConstruirPanelEntradas()
        {
            pnlControles = new Panel { BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            // 🚀 ComboBox más ancho (Width: 420)
            cmbTipoIntegral = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10), Width = 420, Location = new Point(10, 15) };
            cmbTipoIntegral.Items.Add("1D: Regla del Trapecio");
            cmbTipoIntegral.Items.Add("2D: Regla del Trapecio");
            cmbTipoIntegral.Items.Add("1D: Regla de Simpson 1/3");
            cmbTipoIntegral.Items.Add("2D: Regla de Simpson 1/3");
            cmbTipoIntegral.Items.Add("1D: Regla de Simpson 3/8");
            cmbTipoIntegral.Items.Add("2D: Regla de Simpson 3/8");
            cmbTipoIntegral.Items.Add("1D: Integración de Romberg");
            cmbTipoIntegral.Items.Add("2D: Integración de Romberg");
            cmbTipoIntegral.SelectedIndex = 0;
            cmbTipoIntegral.SelectedIndexChanged += CmbTipoIntegral_SelectedIndexChanged;

            // 🚀 Caja de función mucho más ancha (Width: 350)
            pnlControles.Controls.Add(new Label { Text = "Función f:", Location = new Point(10, 55), AutoSize = true });
            txtFuncion = new TextBox { Location = new Point(80, 53), Width = 350, Text = "x^2" };

            // Entradas X (Separadas para que respiren mejor)
            pnlControles.Controls.Add(new Label { Text = "Lim A (x):", Location = new Point(10, 95), AutoSize = true });
            txtA = new TextBox { Location = new Point(80, 93), Width = 60, Text = "0" };

            pnlControles.Controls.Add(new Label { Text = "Lim B (x):", Location = new Point(160, 95), AutoSize = true });
            txtB = new TextBox { Location = new Point(230, 93), Width = 60, Text = "5" };

            pnlControles.Controls.Add(new Label { Text = "Int n (x):", Location = new Point(10, 130), AutoSize = true });
            txtN = new TextBox { Location = new Point(80, 128), Width = 60, Text = "10" };

            // Entradas Y (Solo 2D)
            lblCy = new Label { Text = "Lim C (y):", Location = new Point(10, 170), AutoSize = true, Visible = false };
            txtC = new TextBox { Location = new Point(80, 168), Width = 60, Text = "0", Visible = false };

            lblDy = new Label { Text = "Lim D (y):", Location = new Point(160, 170), AutoSize = true, Visible = false };
            txtD = new TextBox { Location = new Point(230, 168), Width = 60, Text = "3", Visible = false };

            lblNy = new Label { Text = "Int n (y):", Location = new Point(10, 205), AutoSize = true, Visible = false };
            txtNy = new TextBox { Location = new Point(80, 203), Width = 60, Text = "4", Visible = false };

            pnlControles.Controls.AddRange(new Control[] { cmbTipoIntegral, txtFuncion, txtA, txtB, txtN, lblCy, txtC, lblDy, txtD, lblNy, txtNy });
            this.Controls.Add(pnlControles);
        }
        private void CmbTipoIntegral_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Las opciones 2D están en las posiciones 1, 3, 5 y 7 del ComboBox
            bool esDoble = (cmbTipoIntegral.SelectedIndex == 1 || cmbTipoIntegral.SelectedIndex == 3 || cmbTipoIntegral.SelectedIndex == 5 || cmbTipoIntegral.SelectedIndex == 7);
            lblCy.Visible = txtC.Visible = lblDy.Visible = txtD.Visible = lblNy.Visible = txtNy.Visible = esDoble;

            // En Romberg usamos la "n" como Niveles. Le ponemos 4 por defecto para no saturar.
            bool esRomberg = (cmbTipoIntegral.SelectedIndex == 6 || cmbTipoIntegral.SelectedIndex == 7);
            if (esRomberg) { txtN.Text = "5"; txtNy.Text = "5"; }

            txtFuncion.Text = esDoble ? "x^2 * y + 3 * (x * y^2) + sin(x * y)" : "x^2";
            AcomodarControles();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();
            string formula = txtFuncion.Text.Trim();

            if (string.IsNullOrEmpty(formula))
            {
                MessageBox.Show("Oe bro, tienes que escribir una función matemática.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 🚀 Detectamos si seleccionó una opción 1D (índices pares) o 2D (índices impares)
                bool esModo1D = (cmbTipoIntegral.SelectedIndex == 0 || cmbTipoIntegral.SelectedIndex == 2 ||
                                 cmbTipoIntegral.SelectedIndex == 4 || cmbTipoIntegral.SelectedIndex == 6);

                if (esModo1D) // MODO 1D
                {
                    double a = metodos.ConvertirADouble(txtA.Text);
                    double b = metodos.ConvertirADouble(txtB.Text);
                    int n = (int)metodos.ConvertirADouble(txtN.Text);

                    // mXparser OPTIMIZADO PARA 1D
                    Argument xArg = new Argument("x");
                    Expression expr = new Expression(formula, xArg);

                    if (!expr.checkSyntax())
                        throw new Exception("Error de sintaxis: " + expr.getErrorMessage());

                    Func<double, double> funcion1D = (x) =>
                    {
                        xArg.setArgumentValue(x);
                        return expr.calculate();
                    };

                    // Ruteo a los motores 1D
                    if (cmbTipoIntegral.SelectedIndex == 0)
                        metodos.EjecutarTrapecio(funcion1D, a, b, n, dgvResultados);
                    else if (cmbTipoIntegral.SelectedIndex == 2)
                        metodos.EjecutarSimpson13(funcion1D, a, b, n, dgvResultados);
                    else if (cmbTipoIntegral.SelectedIndex == 4)
                        metodos.EjecutarSimpson38(funcion1D, a, b, n, dgvResultados);
                    else if (cmbTipoIntegral.SelectedIndex == 6)
                        metodos.EjecutarRomberg(funcion1D, a, b, n, dgvResultados);
                }
                else // MODO 2D
                {
                    double ax = metodos.ConvertirADouble(txtA.Text);
                    double bx = metodos.ConvertirADouble(txtB.Text);
                    int nx = (int)metodos.ConvertirADouble(txtN.Text);

                    double cy = metodos.ConvertirADouble(txtC.Text);
                    double dy = metodos.ConvertirADouble(txtD.Text);
                    int ny = (int)metodos.ConvertirADouble(txtNy.Text);

                    // mXparser OPTIMIZADO PARA 2D
                    Argument xArg = new Argument("x");
                    Argument yArg = new Argument("y");
                    Expression expr = new Expression(formula, xArg, yArg);

                    if (!expr.checkSyntax())
                        throw new Exception("Error de sintaxis: " + expr.getErrorMessage());

                    Func<double, double, double> funcion2D = (x, y) =>
                    {
                        xArg.setArgumentValue(x);
                        yArg.setArgumentValue(y);
                        return expr.calculate();
                    };

                    // Ruteo a los motores 2D
                    if (cmbTipoIntegral.SelectedIndex == 1)
                        metodos.EjecutarTrapecio(funcion2D, ax, bx, nx, cy, dy, ny, dgvResultados);
                    else if (cmbTipoIntegral.SelectedIndex == 3)
                        metodos.EjecutarSimpson13(funcion2D, ax, bx, nx, cy, dy, ny, dgvResultados);
                    else if (cmbTipoIntegral.SelectedIndex == 5)
                        metodos.EjecutarSimpson38(funcion2D, ax, bx, nx, cy, dy, ny, dgvResultados);
                    else if (cmbTipoIntegral.SelectedIndex == 7)
                        metodos.EjecutarRomberg(funcion2D, ax, bx, nx, cy, dy, ny, dgvResultados);
                }

                ConfigurarEstiloTuaniTablas(dgvResultados);
                pnlEspera.Visible = false;
                dgvResultados.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla analítica en la integración:\n" + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvResultados.Rows.Clear();
            dgvResultados.Columns.Clear();
            dgvResultados.Visible = false;
            pnlEspera.Visible = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try { new MetodosNumericos().ExportarAExcel(dgvResultados); }
            catch (Exception ex) { MessageBox.Show("Error al exportar: " + ex.Message); }
        }

        // =====================================================================
        // 🎨 MAQUETACIÓN Y CAPA ESTÉTICA TUANI (SIN CAMBIOS)
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
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            if (dgv.Columns.Count >= 3)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 80;

            // Detectar si es 1D o 2D para ajustar la altura de la caja
            bool esModo1D = (cmbTipoIntegral.SelectedIndex == 0 || cmbTipoIntegral.SelectedIndex == 2 ||
                             cmbTipoIntegral.SelectedIndex == 4 || cmbTipoIntegral.SelectedIndex == 6);

            int boxAlto = esModo1D ? 170 : 250;

            // 🚀 Aumentamos el Width del Panel blanco a 450
            pnlControles.Location = new Point(40, boxY);
            pnlControles.Size = new Size(450, boxAlto);

            // Los botones se acomodan a la derecha de la pantalla
            int btnX = this.ClientSize.Width - 170;
            if (btnLimpiar != null) { btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40); }
            if (btnExportar != null) { btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40); }
            if (btnCalcular != null) { btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40); }

            int tablaY = boxY + boxAlto + 25;
            if (dgvResultados != null)
            {
                dgvResultados.Location = new Point(40, tablaY);
                dgvResultados.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);

                if (pnlEspera != null)
                {
                    pnlEspera.Location = dgvResultados.Location;
                    pnlEspera.Size = dgvResultados.Size;
                }
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
                else if (control is Label lbl && control.Parent != pnlControles) { lbl.ForeColor = Color.Black; lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold); }

                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            if (dgvResultados != null) dgvResultados.Visible = false;
            pnlEspera = new Panel { BackColor = Color.FromArgb(243, 244, 246) };
            pnlEspera.Paint += PnlEspera_Paint;

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(14, 116, 144) });

            // Panel oscuro de la derecha
            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });

            // Fórmulas actualizadas para todos los métodos
            string formulasMatematicas =
                "▶ Regla del Trapecio:\n" +
                " 1D: I ≈ (h/2) · Σ W_i f(x_i)\n" +
                " 2D: I ≈ (hx·hy/4) · ΣΣ W_ij f(x_i, y_j)\n\n" +
                "▶ Regla de Simpson 1/3:\n" +
                " 1D: I ≈ (h/3) · Σ W_i f(x_i)\n" +
                " 2D: I ≈ (hx·hy/9) · ΣΣ W_ij f(x_i, y_j)\n\n" +
                "▶ Regla de Simpson 3/8:\n" +
                " 1D: I ≈ (3h/8) · Σ W_i f(x_i)\n" +
                " 2D: I ≈ (9·hx·hy/64) · ΣΣ W_ij f(x_i, y_j)\n\n" +
                "▶ Integración de Romberg:\n" +
               " R_{j,k} = [4^k R_{j+1,k-1} - R_{j,k-1}] / [4^k - 1]";

            pnlDerecha.Controls.Add(new Label { Text = formulasMatematicas, Font = new Font("Consolas", 9, FontStyle.Bold), ForeColor = Color.FromArgb(167, 243, 208), AutoSize = true, Location = new Point(25, 90) });

            // Título y descripción principal
            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });

            string textoDescripcion = "Evalúa funciones matemáticas (1D y 2D) desde texto utilizando las Reglas del Trapecio y Simpson, integradas con el motor mXparser de alta precisión.";
            pnlTarjeta.Controls.Add(new Label { Text = textoDescripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });

            // Separador visual
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });

            // Instrucciones
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Modo de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });

            string pasos =
                "[ 1 ]  Selecciona el método y dimensionalidad.\n" +
                "[ 2 ]  Escribe la función (ej: x^2 o sin(x*y)).\n" +
                "[ 3 ]  Ingresa los límites y el número de intervalos (n).\n" +
                "[ 4 ]  Presiona 'Calcular' para resolver.";
            pnlTarjeta.Controls.Add(new Label { Text = pasos, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });

            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            // Centrado dinámico
            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) => { pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2); pnlEspera.Invalidate(); };
        }

        private void PnlEspera_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = pnlEspera.Width, h = pnlEspera.Height, cY = h / 2;
            using (Pen pGrid = new Pen(Color.FromArgb(225, 230, 235), 1))
            using (Pen pEjes = new Pen(Color.FromArgb(200, 205, 215), 2))
            {
                for (int i = 0; i < w; i += 40) g.DrawLine(pGrid, i, 0, i, h);
                for (int j = 0; j < h; j += 40) g.DrawLine(pGrid, 0, j, w, j);
                g.DrawLine(pEjes, 0, cY, w, cY);
            }

            using (Pen pCurve = new Pen(Color.FromArgb(14, 116, 144), 3))
            using (HatchBrush brushTrapecio = new HatchBrush(HatchStyle.LightVertical, Color.FromArgb(204, 251, 241), Color.Transparent))
            using (Pen pTrapecio = new Pen(Color.FromArgb(45, 212, 191), 2))
            {
                PointF[] areaTrapecio = { new PointF(w / 2 - 80, cY), new PointF(w / 2 - 80, cY - 40), new PointF(w / 2 + 60, cY - 90), new PointF(w / 2 + 60, cY) };
                g.FillPolygon(brushTrapecio, areaTrapecio);
                g.DrawPolygon(pTrapecio, areaTrapecio);
            }
        }
    }
}