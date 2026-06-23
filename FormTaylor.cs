using System;
using System.Drawing;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;

namespace Métodos_Numéricos
{
    public partial class FormTaylor : Form
    {
        private Panel pnlEspera;
        private Panel pnlControles;
        // 🚀 Adiós al ComboBox inútil
        private TextBox txtFuncion, txtA, txtX, txtGrado;
        private DataGridView dgvResultados;
        private Button btnCalcular, btnLimpiar, btnExportar;

        public FormTaylor()
        {
            InitializeComponent();
            License.iConfirmNonCommercialUse("Proyecto Universitario / Tuani");

            dgvResultados = new DataGridView();
            this.Controls.Add(dgvResultados);

            ConstruirPanelEntradas();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Aproximación de Taylor", "Genera polinomios de Taylor y Maclaurin calculando todas las derivadas de forma automática.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void ConstruirPanelEntradas()
        {
            pnlControles = new Panel { BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            Font fontLabels = new Font("Segoe UI", 11, FontStyle.Bold);
            Font fontCajas = new Font("Segoe UI", 11);

            // 🚀 FILA 1: Función
            // Empujamos la caja hasta X=150 para que la etiqueta respire
            pnlControles.Controls.Add(new Label { Text = "Función f(x):", Location = new Point(15, 32), AutoSize = true, Font = fontLabels });
            txtFuncion = new TextBox { Location = new Point(150, 30), Width = 320, Text = "e^x", Font = fontCajas };

            // 🚀 FILA 2: Centro y Evaluación
            pnlControles.Controls.Add(new Label { Text = "Centro (a):", Location = new Point(15, 92), AutoSize = true, Font = fontLabels });
            txtA = new TextBox { Location = new Point(150, 90), Width = 70, Text = "0", Font = fontCajas };

            // Separamos la segunda etiqueta mandándola hasta X=240 y su caja hasta X=370
            pnlControles.Controls.Add(new Label { Text = "Evaluar(x):", Location = new Point(240, 92), AutoSize = true, Font = fontLabels });
            txtX = new TextBox { Location = new Point(370, 90), Width = 100, Text = "0.5", Font = fontCajas };

            // 🚀 FILA 3: Grado
            pnlControles.Controls.Add(new Label { Text = "Grado (n):", Location = new Point(15, 152), AutoSize = true, Font = fontLabels });
            txtGrado = new TextBox { Location = new Point(150, 150), Width = 70, Text = "3", Font = fontCajas };

            // Botones
            btnCalcular = new Button { Text = "Calcular" };
            btnCalcular.Click += BtnCalcular_Click;

            btnExportar = new Button { Text = "Exportar" };
            btnExportar.Click += (s, e) => new MetodosNumericos().ExportarAExcel(dgvResultados);

            btnLimpiar = new Button { Text = "Limpiar" };
            btnLimpiar.Click += (s, e) => { dgvResultados.Visible = false; pnlEspera.Visible = true; };

            this.Controls.Add(btnCalcular);
            this.Controls.Add(btnExportar);
            this.Controls.Add(btnLimpiar);

            pnlControles.Controls.AddRange(new Control[] { txtFuncion, txtA, txtX, txtGrado });
            this.Controls.Add(pnlControles);
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            MetodosNumericos metodos = new MetodosNumericos();
            string formula = txtFuncion.Text.Trim();

            if (string.IsNullOrEmpty(formula))
            {
                MessageBox.Show("Ingresa una función matemática válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                double a = metodos.ConvertirADouble(txtA.Text);
                double x = metodos.ConvertirADouble(txtX.Text);
                int n = (int)metodos.ConvertirADouble(txtGrado.Text);

                Argument arg = new Argument("x");
                Expression expr = new Expression(formula, arg);
                if (!expr.checkSyntax()) throw new Exception("Error de sintaxis en la función: " + expr.getErrorMessage());

                metodos.EjecutarSerieTaylor(formula, a, x, n, dgvResultados);

                ConfigurarEstiloTuaniTablas(dgvResultados);
                pnlEspera.Visible = false;
                dgvResultados.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla analítica:\n" + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            dgv.DefaultCellStyle.Padding = new Padding(5);
            dgv.RowTemplate.Height = 45;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240);
            dgv.AllowUserToAddRows = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;

            int boxY = 40;
            int boxAlto = 210; // 🚀 Altura ajustada a las 3 filas espaciadas

            pnlControles.Location = new Point(40, boxY);
            pnlControles.Size = new Size(500, boxAlto);

            int btnX = this.ClientSize.Width - 170;
            if (btnLimpiar != null) { btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40); }
            if (btnExportar != null) { btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40); }
            if (btnCalcular != null) { btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40); }

            int tablaY = boxY + boxAlto + 25;
            if (dgvResultados != null)
            {
                dgvResultados.Location = new Point(40, tablaY);
                dgvResultados.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 20);
                if (pnlEspera != null) { pnlEspera.Location = dgvResultados.Location; pnlEspera.Size = dgvResultados.Size; }
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

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(14, 116, 144) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });

            string formulas =
                "▶ Serie de Taylor:\n" +
                " P_n(x) = Σ [ f⁽ⁱ⁾(a) / i! ] · (x - a)ⁱ\n\n" +
                "▶ Serie de Maclaurin:\n" +
                " Es un caso especial de Taylor\n" +
                " donde el centro evaluado es a = 0.\n\n" +
                "▶ Resto de Lagrange (Error):\n" +
                " R_n(x) = Valor Real - P_n(x)";

            pnlDerecha.Controls.Add(new Label { Text = formulas, Font = new Font("Consolas", 8, FontStyle.Bold), ForeColor = Color.FromArgb(167, 243, 208), AutoSize = true, Location = new Point(25, 90) });

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });

            pnlTarjeta.Controls.Add(new Label { Text = "📌 Modo de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            string pasos = "[ 1 ]  Escribe la función a aproximar (ej: e^x).\n[ 2 ]  Define el punto de evaluación (x).\n[ 3 ]  Define el centro (a). Usa 0 para Maclaurin.\n[ 4 ]  Define el grado (n) del polinomio.";
            pnlTarjeta.Controls.Add(new Label { Text = pasos, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });

            pnlTarjeta.Controls.Add(pnlDerecha);
            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) => { pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2); pnlEspera.Invalidate(); };
        }
    }
}