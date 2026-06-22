using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;

            EstilarBotones();
        }

        private void EstilarBotones()
        {
            // --- BOTÓN ENTRAR (Estilo Dashboard Premium) ---
            if (btnEntrar != null)
            {
                btnEntrar.FlatStyle = FlatStyle.Flat;
                btnEntrar.FlatAppearance.BorderSize = 0;
                btnEntrar.BackColor = Color.FromArgb(79, 70, 229); // Morado vibrante
                btnEntrar.ForeColor = Color.White;
                btnEntrar.Font = new Font("Segoe UI", 26, FontStyle.Bold);
                btnEntrar.Size = new Size(350, 85); // Más grande y proporcionado
                btnEntrar.Cursor = Cursors.Hand;

                // Efecto Hover
                btnEntrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(99, 90, 255);
            }

            // --- BOTÓN SALIR (La 'X' roja) ---
            if (btnSalir != null)
            {
                btnSalir.Text = "✕";
                btnSalir.FlatStyle = FlatStyle.Flat;
                btnSalir.FlatAppearance.BorderSize = 0;
                btnSalir.BackColor = Color.Transparent; // Transparente para no romper el fondo
                btnSalir.ForeColor = Color.FromArgb(156, 163, 175);
                btnSalir.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                btnSalir.Size = new Size(50, 50);
                btnSalir.Cursor = Cursors.Hand;

                btnSalir.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38); // Rojo brillante al pasar el mouse
                btnSalir.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 27, 27);

                btnSalir.Click -= btnSalir_Click;
                btnSalir.Click += btnSalir_Click;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (btnEntrar != null)
            {
                // Botón exactamente en el centro, un poco más abajo de los textos gigantes
                btnEntrar.Location = new Point((this.Width - btnEntrar.Width) / 2, (this.Height / 2) + 60);
            }
            if (btnSalir != null)
            {
                btnSalir.Location = new Point(this.Width - btnSalir.Width - 20, 20);
            }
            this.Invalidate(); // Repinta el fondo al cambiar de tamaño
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.Width <= 0 || this.Height <= 0) return;

            // 🚀 1. DEGRADADO DARK MODE (EL FONDO TUANI)
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                Color.FromArgb(17, 24, 39), // Azul oscuro
                Color.FromArgb(8, 12, 20),  // Casi negro
                45F))
            {
                g.FillRectangle(brush, rect);
            }

            // 🚀 2. LÍNEAS Y NODOS TECNOLÓGICOS (Vuelven las constelaciones)
            using (Pen pen = new Pen(Color.FromArgb(90, 255, 255, 255), 2f)) // Blanco con opacidad sutil
            {
                g.DrawLine(pen, 0, this.Height / 3, this.Width / 3, this.Height / 2);
                g.DrawLine(pen, this.Width / 3, this.Height / 2, this.Width, 100);
                g.DrawLine(pen, this.Width / 2, this.Height, this.Width - 200, this.Height / 3);

                // Nodos morados
                SolidBrush nodeBrush = new SolidBrush(Color.FromArgb(129, 140, 248));
                g.FillEllipse(nodeBrush, (this.Width / 3) - 6, (this.Height / 2) - 6, 12, 12);
                g.FillEllipse(nodeBrush, (this.Width - 200) - 6, (this.Height / 3) - 6, 12, 12);
                nodeBrush.Dispose();
            }

            // 🚀 3. SÍMBOLOS MATEMÁTICOS GIGANTES (Como marcas de agua flotantes)
            using (Font mathFont = new Font("Cambria", 90, FontStyle.Italic))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(60, 255, 255, 255))) // Opacidad perfecta para no estorbar
            {
                g.DrawString("∑", mathFont, textBrush, this.Width - 250, 150);
                g.DrawString("π", mathFont, textBrush, 150, this.Height - 300);
                g.DrawString("∫", mathFont, textBrush, 200, 100);
                g.DrawString("x²", mathFont, textBrush, this.Width - 400, this.Height - 250);
            }

            // 🚀 4. TEXTOS DE ALTO IMPACTO (Gigantes y bien centrados)
            using (Font titleFont = new Font("Segoe UI", 85, FontStyle.Bold)) // Aumentado a 85 para que sea masivo
            using (Font subFont = new Font("Segoe UI", 22, FontStyle.Regular))
            using (SolidBrush whiteTextBrush = new SolidBrush(Color.White))
            using (SolidBrush accentTextBrush = new SolidBrush(Color.FromArgb(165, 180, 252))) // Morado pastel
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
            {
                StringFormat centrado = new StringFormat();
                centrado.Alignment = StringAlignment.Center;
                centrado.LineAlignment = StringAlignment.Center;

                float centroY = this.Height / 2;

                // TÍTULO (MATH ENGINE gigante con sombra)
                RectangleF rectTitulo = new RectangleF(0, centroY - 220, this.Width, 150);
                RectangleF rectTituloSombra = new RectangleF(6, centroY - 214, this.Width, 150);

                g.DrawString("MATH ENGINE", titleFont, shadowBrush, rectTituloSombra, centrado);
                g.DrawString("MATH ENGINE", titleFont, whiteTextBrush, rectTitulo, centrado);

                // SUBTÍTULO
                RectangleF rectSub = new RectangleF(0, centroY - 60, this.Width, 60);
                g.DrawString("MÉTODOS NUMÉRICOS", subFont, accentTextBrush, rectSub, centrado);
            }

            // 🚀 5. FOOTER DE PRESENTACIÓN UNIVERSITARIA (UNI)
            using (Font devFont = new Font("Segoe UI", 20, FontStyle.Bold))    // 🚀 Aumentado de 15 a 20
            using (Font uniFont = new Font("Segoe UI", 15, FontStyle.Regular)) // 🚀 Aumentado de 12 a 15
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (SolidBrush grayBrush = new SolidBrush(Color.FromArgb(156, 163, 175)))
            {
                StringFormat centrado = new StringFormat();
                centrado.Alignment = StringAlignment.Center;
                centrado.LineAlignment = StringAlignment.Center;

                // 🚀 Cajas más altas (45px) y ubicaciones (Y) recalculadas para evitar que se aplasten
                RectangleF rectDev = new RectangleF(0, this.Height - 120, this.Width, 45);
                g.DrawString("Desarrollado por: Rodrigo Zelaya, Hillary Ordoñez e Ismaurily Pichardo", devFont, whiteBrush, rectDev, centrado);

                RectangleF rectUni = new RectangleF(0, this.Height - 70, this.Width, 45);
                g.DrawString("Universidad Nacional de Ingeniería (UNI) - Managua, Nicaragua", uniFont, grayBrush, rectUni, centrado);
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDash principal = new FormDash();
            principal.ShowDialog();
            this.Close();
        }
    }
}