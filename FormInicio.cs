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
                // El morado vibrante que usamos en las tarjetas del sistema
                btnEntrar.BackColor = Color.FromArgb(79, 70, 229);
                btnEntrar.ForeColor = Color.White;
                btnEntrar.Font = new Font("Segoe UI", 28, FontStyle.Bold);
                btnEntrar.Size = new Size(350, 90);
                btnEntrar.Cursor = Cursors.Hand;

                // Efecto Hover sencillo
                btnEntrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(99, 90, 255);
            }

            // --- BOTÓN SALIR (La 'X' roja) ---
            if (btnSalir != null)
            {
                btnSalir.Text = "X";
                btnSalir.FlatStyle = FlatStyle.Flat;
                btnSalir.FlatAppearance.BorderSize = 0;
                // Rojo elegante
                btnSalir.BackColor = Color.FromArgb(220, 38, 38);
                btnSalir.ForeColor = Color.White;
                btnSalir.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                btnSalir.Size = new Size(50, 50);
                btnSalir.Cursor = Cursors.Hand;

                btnSalir.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68);

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
                // Botón exactamente en el centro, un poco más abajo de los textos
                btnEntrar.Location = new Point((this.Width - btnEntrar.Width) / 2, (this.Height / 2) + 20);
            }
            if (btnSalir != null)
            {
                btnSalir.Location = new Point(this.Width - btnSalir.Width - 30, 30);
            }
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.Width <= 0 || this.Height <= 0) return;

            // 🚀 1. DEGRADADO DARK MODE (Integrado con el menú del sistema)
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                Color.FromArgb(17, 24, 39), // El Azul oscuro de tu menú lateral
                Color.FromArgb(8, 12, 20),  // Un tono casi negro para dar profundidad
                45F))
            {
                g.FillRectangle(brush, rect);
            }

            // 🚀 2. LÍNEAS Y NODOS TECNOLÓGICOS (Constelación más visible)
            // 🛠️ FIX: Subimos la opacidad de 30 a 120 para que las líneas blancas destaquen más
            using (Pen pen = new Pen(Color.FromArgb(120, 255, 255, 255), 2f))
            {
                g.DrawLine(pen, 0, this.Height / 3, this.Width / 3, this.Height / 2);
                g.DrawLine(pen, this.Width / 3, this.Height / 2, this.Width, 100);
                g.DrawLine(pen, this.Width / 2, this.Height, this.Width - 200, this.Height / 3);

                // Nodos color morado a juego con el botón
                SolidBrush nodeBrush = new SolidBrush(Color.FromArgb(129, 140, 248));
                g.FillEllipse(nodeBrush, (this.Width / 3) - 5, (this.Height / 2) - 5, 10, 10);
                g.FillEllipse(nodeBrush, (this.Width - 200) - 5, (this.Height / 3) - 5, 10, 10);
                nodeBrush.Dispose();
            }

            // 🚀 3. SÍMBOLOS MATEMÁTICOS (Más interactivos y presentes)
            using (Font mathFont = new Font("Cambria", 70, FontStyle.Italic))
            // 🛠️ FIX: Subimos la opacidad de 15 a 90 para que no parezcan fantasmas
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(90, 255, 255, 255)))
            {
                g.DrawString("∑", mathFont, textBrush, this.Width - 250, 150);
                g.DrawString("π", mathFont, textBrush, 150, this.Height - 300);
                g.DrawString("∫", mathFont, textBrush, 200, 100);
                g.DrawString("x²", mathFont, textBrush, this.Width - 400, this.Height - 250);
            }

            // 🚀 4. TEXTOS DE ALTO IMPACTO (Centrados)
            using (Font titleFont = new Font("Segoe UI", 60, FontStyle.Bold))
            using (Font subFont = new Font("Segoe UI", 24, FontStyle.Regular))
            using (Font devFont = new Font("Segoe UI", 16, FontStyle.Italic))
            using (SolidBrush whiteTextBrush = new SolidBrush(Color.White))
            using (SolidBrush accentTextBrush = new SolidBrush(Color.FromArgb(165, 180, 252))) // Morado pastel claro
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(150, 0, 0, 0))) // Sombra negra
            // 🛠️ FIX: Ahora usamos blanco puro (Color.White) en lugar del gris apagado
            using (SolidBrush footerBrush = new SolidBrush(Color.White))
            {
                StringFormat centrado = new StringFormat();
                centrado.Alignment = StringAlignment.Center;
                centrado.LineAlignment = StringAlignment.Center;

                float centroY = this.Height / 2;

                // Coordenadas
                RectangleF rectTitulo = new RectangleF(0, centroY - 250, this.Width, 150);
                RectangleF rectTituloSombra = new RectangleF(4, centroY - 246, this.Width, 150);

                RectangleF rectSub = new RectangleF(0, centroY - 100, this.Width, 60);

                RectangleF rectDev = new RectangleF(0, this.Height - 60, this.Width, 50);

                // TÍTULO (Primero dibujamos la sombra para dar efecto 3D, luego el texto blanco)
                g.DrawString("MÉTODOS NUMÉRICOS", titleFont, shadowBrush, rectTituloSombra, centrado);
                g.DrawString("MÉTODOS NUMÉRICOS", titleFont, whiteTextBrush, rectTitulo, centrado);

                // SUBTÍTULO (Color de acento para contrastar)
                g.DrawString("SISTEMA PARA EL CÁLCULO DE RAÍCES LINEALES Y NO LINEALES", subFont, accentTextBrush, rectSub, centrado);

                // DESARROLLADORES (Footer resaltado en blanco)
                g.DrawString("Desarrollado por: Rodrigo Zelaya, Hillary Ordoñez e Ismaurily Pichardo", devFont, footerBrush, rectDev, centrado);
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