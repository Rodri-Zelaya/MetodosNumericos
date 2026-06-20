using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormFactorizacionLU : Form
    {
        private Panel pnlEspera;

        public FormFactorizacionLU()
        {
            InitializeComponent();
            AplicarEstiloTuani(this.Controls);
            ConfigurarEmptyState("Factorización LU", "Descompone la matriz original A en el producto de una matriz triangular inferior (L) y una triangular superior (U). Extremadamente eficiente para resolver múltiples vectores b con la misma matriz de coeficientes.");

            this.Resize += (s, e) => AcomodarControles();
            AcomodarControles();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatrizA.Text) || string.IsNullOrWhiteSpace(txtVectorB.Text))
            {
                MessageBox.Show("Por favor, llena la Matriz A y el Vector B para continuar.");
                return;
            }

            MetodosNumericos metodos = new MetodosNumericos();

            try
            {
                string[] lineasA = txtMatrizA.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int n = lineasA.Length;
                double[,] A = new double[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] elementos = lineasA[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (elementos.Length != n)
                    {
                        MessageBox.Show($"La Matriz A debe ser cuadrada ({n}x{n}).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int j = 0; j < n; j++) A[i, j] = metodos.ConvertirADouble(elementos[j]);
                }

                string[] lineasB = txtVectorB.Text.Split(new string[] { "\r\n", "\n", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (lineasB.Length != n)
                {
                    MessageBox.Show($"El vector b debe tener exactamente {n} términos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                double[] b = new double[n];
                for (int i = 0; i < n; i++) b[i] = metodos.ConvertirADouble(lineasB[i]);

                // 🚀 INVOCAR MOTOR LU (DOOLITTLE)
                metodos.FactorizacionLUPasoAPaso(A, b, dgvFactorizaciónLU);

                // 🚀 ESTILOS DEL DATAGRIDVIEW
                dgvFactorizaciónLU.EnableHeadersVisualStyles = false;
                dgvFactorizaciónLU.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
                dgvFactorizaciónLU.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvFactorizaciónLU.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvFactorizaciónLU.RowHeadersVisible = false;
                dgvFactorizaciónLU.DefaultCellStyle.BackColor = Color.White;
                dgvFactorizaciónLU.DefaultCellStyle.ForeColor = Color.Black;
                dgvFactorizaciónLU.DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Regular);
                dgvFactorizaciónLU.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 238, 245);
                dgvFactorizaciónLU.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
                dgvFactorizaciónLU.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvFactorizaciónLU.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                pnlEspera.Visible = false;
                dgvFactorizaciónLU.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falla en el proceso: " + ex.Message, "Error Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try { new MetodosNumericos().ExportarAExcel(dgvFactorizaciónLU); }
            catch (Exception ex) { MessageBox.Show("Error al exportar: " + ex.Message); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            new MetodosNumericos().LimpiarPantalla(dgvFactorizaciónLU, new TextBox[] { txtMatrizA, txtVectorB }, new Label[] { });
            dgvFactorizaciónLU.Visible = false;
            pnlEspera.Visible = true;
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
                else if (control is TextBox txt) { txt.Font = new Font("Segoe UI", 11, FontStyle.Regular); txt.BorderStyle = BorderStyle.FixedSingle; }
                if (control.HasChildren) AplicarEstiloTuani(control.Controls);
            }
        }

        private void ConfigurarEmptyState(string nombreMetodo, string descripcion)
        {
            dgvFactorizaciónLU.Visible = false;
            pnlEspera = new Panel { Location = dgvFactorizaciónLU.Location, Size = dgvFactorizaciónLU.Size, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.FromArgb(243, 244, 246) };

            Panel pnlTarjeta = new Panel { Size = new Size(960, 480), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            pnlTarjeta.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(79, 70, 229) });

            Panel pnlDerecha = new Panel { Width = 380, Height = 480, Location = new Point(580, 0), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, BackColor = Color.FromArgb(17, 24, 39) };
            pnlDerecha.Controls.Add(new Label { Text = "⚙️ Base Matemática", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(25, 40) });
            pnlDerecha.Controls.Add(new Label { Text = "1. Descomponer: A = L · U\n\n2. Adelante: L · y = b\n\n3. Atrás: U · x = y", Font = new Font("Consolas", 10, FontStyle.Bold), ForeColor = Color.FromArgb(165, 180, 252), AutoSize = true, Location = new Point(25, 90) });

            Panel pnlNota = new Panel { BackColor = Color.FromArgb(31, 41, 55), Size = new Size(330, 250), Location = new Point(25, 190) };
            pnlNota.Controls.Add(new Panel { BackColor = Color.FromArgb(79, 70, 229), Dock = DockStyle.Left, Width = 4 });
            pnlNota.Controls.Add(new Label { Text = "💡 Nota Técnica:\n\nEsta implementación usa el algoritmo de Doolittle (1s en la diagonal de L).\n\nEs el método preferido en software industrial para sistemas lineales porque evita tener que re-calcular la matriz entera si solo cambian los términos independientes.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(209, 213, 219), AutoSize = true, MaximumSize = new Size(290, 0), Location = new Point(15, 15) });
            pnlDerecha.Controls.Add(pnlNota);

            pnlTarjeta.Controls.Add(new Label { Text = "⚡ " + nombreMetodo, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(40, 30) });
            pnlTarjeta.Controls.Add(new Label { Text = descripcion, Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, MaximumSize = new Size(500, 0), Location = new Point(45, 95) });
            pnlTarjeta.Controls.Add(new Panel { BackColor = Color.FromArgb(226, 232, 240), Size = new Size(480, 1), Location = new Point(45, 175) });
            pnlTarjeta.Controls.Add(new Label { Text = "📌 Secuencia de Operación", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(45, 200) });
            pnlTarjeta.Controls.Add(new Label { Text = "[ 1 ]  Ingresa la Matriz A y el Vector b.\n[ 2 ]  Dale a 'Calcular' para desglosar L y U.", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(45, 245) });
            pnlTarjeta.Controls.Add(pnlDerecha);

            pnlEspera.Controls.Add(pnlTarjeta);
            this.Controls.Add(pnlEspera);
            pnlEspera.BringToFront();

            pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2);
            pnlEspera.Resize += (s, e) => { pnlTarjeta.Location = new Point((pnlEspera.Width - pnlTarjeta.Width) / 2, (pnlEspera.Height - pnlTarjeta.Height) / 2); };
        }

        private void AcomodarControles()
        {
            if (this.ClientSize.Width == 0) return;
            int boxY = 80;
            MoverLabelPorTexto("matri", 40, boxY - 30); txtMatrizA.Location = new Point(40, boxY); txtMatrizA.Size = new Size(250, 150);
            MoverLabelPorTexto("vector", 320, boxY - 30); txtVectorB.Location = new Point(320, boxY); txtVectorB.Size = new Size(100, 150);
            int btnX = this.ClientSize.Width - 170;
            btnLimpiar.Location = new Point(btnX, boxY); btnLimpiar.Size = new Size(130, 40);
            btnExportar.Location = new Point(btnX -= 145, boxY); btnExportar.Size = new Size(130, 40);
            btnCalcular.Location = new Point(btnX -= 145, boxY); btnCalcular.Size = new Size(130, 40);
            int tablaY = boxY + 190;
            dgvFactorizaciónLU.Location = new Point(40, tablaY); dgvFactorizaciónLU.Size = new Size(this.ClientSize.Width - 80, this.ClientSize.Height - tablaY - 40);
            if (pnlEspera != null) { pnlEspera.Location = dgvFactorizaciónLU.Location; pnlEspera.Size = dgvFactorizaciónLU.Size; }
        }

        private void MoverLabelPorTexto(string palabraClave, int x, int y)
        {
            foreach (Control ctrl in this.Controls)
                if (ctrl is Label lbl && (lbl.Text.ToLower().Contains(palabraClave.ToLower()) || lbl.Name.ToLower().Contains(palabraClave.ToLower())))
                { lbl.Location = new Point(x, y); break; }
        }
    }
}