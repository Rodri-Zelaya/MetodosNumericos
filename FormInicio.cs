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
            // --- BOTÓN ENTRAR (Contraste oscuro para resaltar en el fondo pastel) ---
            if (btnEntrar != null)
            {
                btnEntrar.FlatStyle = FlatStyle.Flat;
                btnEntrar.FlatAppearance.BorderSize = 0;
                // Un azul marino/pizarra oscuro y elegante
                btnEntrar.BackColor = Color.FromArgb(30, 40, 60);
                btnEntrar.ForeColor = Color.White;
                btnEntrar.Font = new Font("Segoe UI", 32, FontStyle.Bold);
                btnEntrar.Size = new Size(400, 100);
                btnEntrar.Cursor = Cursors.Hand;
            }

            // --- BOTÓN SALIR (La 'X' roja) ---
            if (btnSalir != null)
            {
                btnSalir.Text = "X";
                btnSalir.FlatStyle = FlatStyle.Flat;
                btnSalir.FlatAppearance.BorderSize = 0;
                // Rojo un poco más suave para que no desentone con lo pastel
                btnSalir.BackColor = Color.FromArgb(239, 68, 68);
                btnSalir.ForeColor = Color.White;
                btnSalir.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                btnSalir.Size = new Size(50, 50);
                btnSalir.Cursor = Cursors.Hand;

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
                // Botón exactamente en el centro
                btnEntrar.Location = new Point((this.Width - btnEntrar.Width) / 2, (this.Height / 2));
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

            // 1. DEGRADADO PASTEL (De Celestito a Verde Tiernito)
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                Color.FromArgb(199, 233, 255), // Celestito pastel
                Color.FromArgb(220, 245, 220), // Verde tiernito pastel
                45F))
            {
                g.FillRectangle(brush, rect);
            }

            // 2. Líneas y Nodos (Color oscuro semi-transparente para contrastar el fondo claro)
            using (Pen pen = new Pen(Color.FromArgb(40, 30, 41, 59), 2f))
            {
                g.DrawLine(pen, 0, this.Height / 3, this.Width / 3, this.Height / 2);
                g.DrawLine(pen, this.Width / 3, this.Height / 2, this.Width, 100);
                g.DrawLine(pen, this.Width / 2, this.Height, this.Width - 200, this.Height / 3);

                g.FillEllipse(Brushes.MediumAquamarine, (this.Width / 3) - 5, (this.Height / 2) - 5, 10, 10);
                g.FillEllipse(Brushes.MediumAquamarine, (this.Width - 200) - 5, (this.Height / 3) - 5, 10, 10);
            }

            // 3. Símbolos Matemáticos (Oscuros transparentes)
            using (Font mathFont = new Font("Cambria", 70, FontStyle.Italic))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(25, 30, 41, 59)))
            {
                g.DrawString("∑", mathFont, textBrush, this.Width - 250, 150);
                g.DrawString("π", mathFont, textBrush, 150, this.Height - 300);
                g.DrawString("∫", mathFont, textBrush, 200, 100);
                g.DrawString("x²", mathFont, textBrush, this.Width - 400, this.Height - 250);
            }

            // 4. TEXTOS (Dinámicamente centrados arriba del botón)
            using (Font titleFont = new Font("Segoe UI", 60, FontStyle.Bold))
            using (Font subFont = new Font("Segoe UI", 30, FontStyle.Regular)) // Un pelín más pequeño para balance
            using (Font devFont = new Font("Segoe UI", 20, FontStyle.Italic))
            // Pizarra Oscuro para el texto principal
            using (SolidBrush darkTextBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            // Blanco para el efecto de luz/grabado debajo de las letras
            using (SolidBrush whiteHighlightBrush = new SolidBrush(Color.FromArgb(180, 255, 255, 255)))
            {
                StringFormat centrado = new StringFormat();
                centrado.Alignment = StringAlignment.Center;
                centrado.LineAlignment = StringAlignment.Center;

                // 📐 MATEMÁTICA PARA CENTRAR: Tomamos la altura de la mitad de la pantalla
                float centroY = this.Height / 2;

                // Cajas virtuales agrupadas justo arriba del botón "ENTRAR"
                // El título empieza 250 pixeles arriba del centro
                RectangleF rectTitulo = new RectangleF(0, centroY - 260, this.Width, 150);
                RectangleF rectTituloLuz = new RectangleF(2, centroY - 258, this.Width, 150);

                // El subtítulo va 120 pixeles arriba del centro
                RectangleF rectSub = new RectangleF(0, centroY - 120, this.Width, 60);
                RectangleF rectSubLuz = new RectangleF(2, centroY - 118, this.Width, 60);

                // Desarrolladores se queda abajo
                RectangleF rectDev = new RectangleF(0, this.Height - 70, this.Width, 50);

                // Imprimir Título (Primero la luz blanca, luego el texto oscuro encima)
                g.DrawString("MÉTODOS NUMÉRICOS", titleFont, whiteHighlightBrush, rectTituloLuz, centrado);
                g.DrawString("MÉTODOS NUMÉRICOS", titleFont, darkTextBrush, rectTitulo, centrado);

                // Imprimir Subtítulo
                g.DrawString("SISTEMA PARA EL CÁLCULO DE RAÍCES LINEALES Y NO LINEALES", subFont, whiteHighlightBrush, rectSubLuz, centrado);
                g.DrawString("SISTEMA PARA EL CÁLCULO DE RAÍCES LINEALES Y NO LINEALES", subFont, darkTextBrush, rectSub, centrado);

                // Imprimir Desarrolladores
                g.DrawString("Desarrollado por: Rodrigo Zelaya, Hillary Ordoñez e Ismaurily Pichardo", devFont, darkTextBrush, rectDev, centrado);
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