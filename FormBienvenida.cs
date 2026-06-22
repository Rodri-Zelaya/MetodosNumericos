using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormBienvenida : Form
    {
        private System.Windows.Forms.Timer timerReloj;
        private Label lblHora;
        private Label lblFecha;
        private Panel pnlCentro;

        public FormBienvenida()
        {
            InitializeComponent();
            ConfigurarDashboard();

            this.Paint += FondoMatematico_Paint;
            this.Resize += (s, e) => AcomodarControles();
        }

        private void ConfigurarDashboard()
        {
            this.BackColor = Color.FromArgb(243, 244, 246);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            // 🚀 ARREGLO 1: Hacemos el panel central un poco más alto para acomodar los nuevos tamaños
            pnlCentro = new Panel { Size = new Size(960, 470), BackColor = Color.Transparent };

            // =========================================================
            // 1. BANNER PRINCIPAL (Más alto para que el texto respire)
            // =========================================================
            // 🚀 ARREGLO 2: Aumentamos la altura de 130 a 150
            Panel pnlWelcome = new Panel { Size = new Size(960, 150), Location = new Point(0, 0), BackColor = Color.FromArgb(17, 24, 39) };
            pnlWelcome.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(79, 70, 229) });
            pnlWelcome.Controls.Add(new Label { Text = "Bienvenido a Math Engine", Font = new Font("Segoe UI", 28, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(30, 25) });
            // 🚀 ARREGLO 3: Bajamos un poquito este texto a Y=80 para separarlo del título
            pnlWelcome.Controls.Add(new Label { Text = "Plataforma integral para la resolución de problemas mediante Métodos Numéricos.\nSelecciona una categoría en el menú lateral para iniciar los cálculos.", Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(156, 163, 175), AutoSize = true, Location = new Point(35, 80) });

            // =========================================================
            // 2. CUADRÍCULA DE 4 PILARES
            // =========================================================

            // Fila 1 (Bajamos las tarjetas a Y=170 para dar espacio al nuevo banner)
            Panel pnlCard1 = CrearTarjetaPilar("🎯", "Búsqueda de Raíces", "Incluye: Métodos Cerrados, Abiertos y Polinomios.\nEncuentra intersecciones y soluciones a f(x)=0.", new Point(0, 170), Color.FromArgb(16, 185, 129));
            Panel pnlCard2 = CrearTarjetaPilar("⚙️", "Sistemas de Ecuaciones", "Incluye: Métodos Lineales, No Lineales e Iterativos.\nResolución de matrices y sistemas complejos multivariable.", new Point(490, 170), Color.FromArgb(59, 130, 246));

            // Fila 2 (Bajamos estas a Y=320)
            Panel pnlCard3 = CrearTarjetaPilar("📈", "Análisis de Datos", "Incluye: Ajuste de Curvas e Interpolación Numérica.\nModelado matemático a partir de puntos de datos discretos.", new Point(0, 320), Color.FromArgb(245, 158, 11));
            Panel pnlCard4 = CrearTarjetaPilar("∫", "Cálculo Avanzado", "Incluye: Diferenciación, Integración y EDOs.\nAproximación de áreas, derivadas y problemas de valor inicial.", new Point(490, 320), Color.FromArgb(139, 92, 246));

            // =========================================================
            // 3. RELOJ EN LA ESQUINA SUPERIOR DERECHA
            // =========================================================
            lblHora = new Label { Font = new Font("Consolas", 28, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), AutoSize = true, BackColor = Color.Transparent };
            lblFecha = new Label { Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, BackColor = Color.Transparent };

            timerReloj = new System.Windows.Forms.Timer { Interval = 1000 };
            timerReloj.Tick += (s, e) =>
            {
                lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
                lblFecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy").ToUpper();
                AcomodarControles();
            };
            timerReloj.Start();

            // Ensamblaje
            pnlCentro.Controls.Add(pnlWelcome);
            pnlCentro.Controls.Add(pnlCard1);
            pnlCentro.Controls.Add(pnlCard2);
            pnlCentro.Controls.Add(pnlCard3);
            pnlCentro.Controls.Add(pnlCard4);

            this.Controls.Add(pnlCentro);
            this.Controls.Add(lblHora);
            this.Controls.Add(lblFecha);
        }

        private void AcomodarControles()
        {
            if (pnlCentro != null)
            {
                pnlCentro.Location = new Point((this.ClientSize.Width - pnlCentro.Width) / 2, (this.ClientSize.Height - pnlCentro.Height) / 2 + 20);
            }

            if (lblHora.Text != null)
            {
                lblHora.Location = new Point(this.ClientSize.Width - lblHora.Width - 25, 15);
                lblFecha.Location = new Point(this.ClientSize.Width - lblFecha.Width - 25, lblHora.Bottom - 5);
            }
            this.Invalidate();
        }

        // ==========================================
        // MOTOR DE TARJETAS GIGANTES
        // ==========================================
        private Panel CrearTarjetaPilar(string icono, string titulo, string descripcion, Point ubicacion, Color colorAcento)
        {
            // 🚀 ARREGLO 4: Hacemos las tarjetas más altas (130px) para que el texto encaje sí o sí
            Panel card = new Panel { Size = new Size(470, 130), Location = ubicacion, BackColor = Color.White, Cursor = Cursors.Hand };
            card.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 4, BackColor = colorAcento });

            Label lblIcono = new Label { Text = icono, Font = new Font("Segoe UI", 36), AutoSize = false, Size = new Size(80, 80), Location = new Point(15, 20), TextAlign = ContentAlignment.MiddleCenter };
            Label lblTitulo = new Label { Text = titulo, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(105, 25) };

            // 🚀 ARREGLO 5: Le damos más altura a la caja de texto (60px) para que el segundo renglón no se ampute
            Label lblDesc = new Label
            {
                Text = descripcion,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = false,
                Size = new Size(350, 60),
                Location = new Point(105, 55)
            };

            card.Controls.Add(lblIcono);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblDesc);

            EventHandler hoverEnter = (s, e) => { card.BackColor = Color.FromArgb(248, 250, 252); card.Top -= 3; };
            EventHandler hoverLeave = (s, e) => { card.BackColor = Color.White; card.Top += 3; };

            card.MouseEnter += hoverEnter; card.MouseLeave += hoverLeave;
            foreach (Control c in card.Controls) { c.MouseEnter += hoverEnter; c.MouseLeave += hoverLeave; }

            return card;
        }

        // ==========================================
        // DIBUJAR FONDO MATEMÁTICO (Ondas)
        // ==========================================
        private void FondoMatematico_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = this.Width;
            int h = this.Height;

            Pen penGrid = new Pen(Color.FromArgb(230, 235, 240), 1);
            for (int i = 0; i < w; i += 50) g.DrawLine(penGrid, i, 0, i, h);
            for (int j = 0; j < h; j += 50) g.DrawLine(penGrid, 0, j, w, j);

            Pen penCurva1 = new Pen(Color.FromArgb(199, 210, 254), 3);
            Pen penCurva2 = new Pen(Color.FromArgb(165, 180, 252), 2);

            PointF[] puntos1 = new PointF[w];
            PointF[] puntos2 = new PointF[w];

            int offsetH = h - 150;

            for (int x = 0; x < w; x++)
            {
                double angle = x * 0.01;
                float y1 = offsetH + (float)(Math.Sin(angle) * 60 + Math.Cos(angle * 0.5) * 30);
                float y2 = offsetH + (float)(Math.Cos(angle * 1.2) * 40 - Math.Sin(angle * 0.8) * 20) + 30;

                puntos1[x] = new PointF(x, y1);
                puntos2[x] = new PointF(x, y2);
            }

            if (puntos1.Length > 1)
            {
                g.DrawCurve(penCurva1, puntos1);
                g.DrawCurve(penCurva2, puntos2);
            }

            penGrid.Dispose();
            penCurva1.Dispose();
            penCurva2.Dispose();
        }
    }
}