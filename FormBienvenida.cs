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
            this.Resize += (s, e) => AcomodarAlCentro();
        }

        private void ConfigurarDashboard()
        {
            this.BackColor = Color.FromArgb(243, 244, 246);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            // Caja central para mantener todo centrado en pantallas grandes
            pnlCentro = new Panel { Size = new Size(1000, 600), BackColor = Color.Transparent };

            // 1. TARJETA PRINCIPAL
            Panel pnlWelcome = new Panel { Size = new Size(1000, 160), Location = new Point(0, 0), BackColor = Color.FromArgb(17, 24, 39) };
            pnlWelcome.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 5, BackColor = Color.FromArgb(79, 70, 229) });
            pnlWelcome.Controls.Add(new Label { Text = "Bienvenido a Math Engine", Font = new Font("Segoe UI", 32, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(40, 35) });
            pnlWelcome.Controls.Add(new Label { Text = "Motor de análisis numérico y aproximación algorítmica.\nSelecciona un módulo en el menú lateral para comenzar a operar.", Font = new Font("Segoe UI", 14), ForeColor = Color.FromArgb(156, 163, 175), AutoSize = true, Location = new Point(45, 95) });

            // 2. TARJETAS DE ESTADO 
            Panel pnlCard1 = CrearTarjetaInfo("🧠", "Motor de Cálculo", "Doble Precisión", new Point(20, 190));
            Panel pnlCard2 = CrearTarjetaInfo("⚡", "Estado del Sistema", "En Línea - Óptimo", new Point(345, 190));
            Panel pnlCard3 = CrearTarjetaInfo("📐", "Módulos Cargados", "Operativos", new Point(670, 190));

            // 3. RELOJ EN TIEMPO REAL
            lblHora = new Label { Font = new Font("Consolas", 80, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42), AutoSize = true, BackColor = Color.Transparent };
            lblFecha = new Label { Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, BackColor = Color.Transparent };

            timerReloj = new System.Windows.Forms.Timer { Interval = 1000 };
            timerReloj.Tick += (s, e) =>
            {
                lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
                lblFecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy").ToUpper();

                // 🚀 POSICIONAMIENTO DINÁMICO: La hora va fija arriba, la fecha siempre la persigue abajo
                lblHora.Location = new Point((pnlCentro.Width - lblHora.Width) / 2, 320);
                lblFecha.Location = new Point((pnlCentro.Width - lblFecha.Width) / 2, lblHora.Bottom + 5); // +5 px de separación segura
            };
            timerReloj.Start();

            pnlCentro.Controls.Add(pnlWelcome);
            pnlCentro.Controls.Add(pnlCard1);
            pnlCentro.Controls.Add(pnlCard2);
            pnlCentro.Controls.Add(pnlCard3);
            pnlCentro.Controls.Add(lblHora);
            pnlCentro.Controls.Add(lblFecha);

            this.Controls.Add(pnlCentro);
        }

        private void AcomodarAlCentro()
        {
            if (pnlCentro != null)
            {
                pnlCentro.Location = new Point((this.ClientSize.Width - pnlCentro.Width) / 2, (this.ClientSize.Height - pnlCentro.Height) / 2);
            }
            this.Invalidate();
        }

        private Panel CrearTarjetaInfo(string icono, string titulo, string subtitulo, Point ubicacion)
        {
            Panel card = new Panel { Size = new Size(310, 110), Location = ubicacion, BackColor = Color.White };

            // 🚀 EL SECRETO: Encerrar al emoji en una celda de 80x80 para que no ampute el texto
            Label lblIcono = new Label
            {
                Text = icono,
                Font = new Font("Segoe UI Emoji", 28),
                AutoSize = false,
                Size = new Size(80, 80),
                Location = new Point(10, 15),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            Label lblTitulo = new Label { Text = titulo, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(71, 85, 105), AutoSize = true, Location = new Point(95, 30), BackColor = Color.Transparent };
            Label lblSub = new Label { Text = subtitulo, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), AutoSize = true, Location = new Point(95, 55), BackColor = Color.Transparent };

            card.Controls.Add(lblIcono);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblSub);

            card.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 4, BackColor = Color.FromArgb(59, 130, 246) });
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