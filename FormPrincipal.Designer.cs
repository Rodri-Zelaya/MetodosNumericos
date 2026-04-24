namespace Métodos_Numéricos
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnMuller = new Button();
            btnPuntoFijo = new Button();
            btnSecante = new Button();
            btnNewton = new Button();
            btnReglaFalsa = new Button();
            btnBiseccion = new Button();
            panelContenedor = new Panel();
            btnBairstow = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(btnBairstow);
            panel1.Controls.Add(btnMuller);
            panel1.Controls.Add(btnPuntoFijo);
            panel1.Controls.Add(btnSecante);
            panel1.Controls.Add(btnNewton);
            panel1.Controls.Add(btnReglaFalsa);
            panel1.Controls.Add(btnBiseccion);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 34);
            panel1.TabIndex = 0;
            // 
            // btnMuller
            // 
            btnMuller.Location = new Point(544, -1);
            btnMuller.Name = "btnMuller";
            btnMuller.Size = new Size(94, 35);
            btnMuller.TabIndex = 2;
            btnMuller.Text = "Muller";
            btnMuller.UseVisualStyleBackColor = true;
            btnMuller.Click += btnMuller_Click;
            // 
            // btnPuntoFijo
            // 
            btnPuntoFijo.Location = new Point(444, 0);
            btnPuntoFijo.Name = "btnPuntoFijo";
            btnPuntoFijo.Size = new Size(94, 34);
            btnPuntoFijo.TabIndex = 4;
            btnPuntoFijo.Text = "Punto Fijo";
            btnPuntoFijo.UseVisualStyleBackColor = true;
            btnPuntoFijo.Click += btnPuntoFijo_Click;
            // 
            // btnSecante
            // 
            btnSecante.Location = new Point(344, 0);
            btnSecante.Name = "btnSecante";
            btnSecante.Size = new Size(94, 34);
            btnSecante.TabIndex = 3;
            btnSecante.Text = "Secante ";
            btnSecante.UseVisualStyleBackColor = true;
            btnSecante.Click += btnSecante_Click;
            // 
            // btnNewton
            // 
            btnNewton.Location = new Point(200, 0);
            btnNewton.Name = "btnNewton";
            btnNewton.Size = new Size(138, 34);
            btnNewton.TabIndex = 2;
            btnNewton.Text = "Newton Raphson";
            btnNewton.UseVisualStyleBackColor = true;
            btnNewton.Click += btnNewton_Click;
            // 
            // btnReglaFalsa
            // 
            btnReglaFalsa.Location = new Point(100, 0);
            btnReglaFalsa.Name = "btnReglaFalsa";
            btnReglaFalsa.Size = new Size(94, 34);
            btnReglaFalsa.TabIndex = 1;
            btnReglaFalsa.Text = "Regla Falsa";
            btnReglaFalsa.UseVisualStyleBackColor = true;
            btnReglaFalsa.Click += btnReglaFalsa_Click;
            // 
            // btnBiseccion
            // 
            btnBiseccion.Location = new Point(0, 0);
            btnBiseccion.Name = "btnBiseccion";
            btnBiseccion.Size = new Size(94, 34);
            btnBiseccion.TabIndex = 0;
            btnBiseccion.Text = "Bisección";
            btnBiseccion.UseVisualStyleBackColor = true;
            btnBiseccion.Click += btnBiseccion_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.BackColor = SystemColors.ActiveCaption;
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(0, 34);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(800, 416);
            panelContenedor.TabIndex = 1;
            // 
            // btnBairstow
            // 
            btnBairstow.Location = new Point(644, -1);
            btnBairstow.Name = "btnBairstow";
            btnBairstow.Size = new Size(94, 35);
            btnBairstow.TabIndex = 5;
            btnBairstow.Text = "Bairstow";
            btnBairstow.UseVisualStyleBackColor = true;
            btnBairstow.Click += btnBairstow_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelContenedor);
            Controls.Add(panel1);
            Name = "FormPrincipal";
            Text = "FormPrincipal";
            Load += FormPrincipal_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnPuntoFijo;
        private Button btnSecante;
        private Button btnNewton;
        private Button btnReglaFalsa;
        private Button btnBiseccion;
        private Panel panelContenedor;
        private Button btnMuller;
        private Button btnBairstow;
    }
}