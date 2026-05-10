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
            btnNewtonNL = new Button();
            btnHornerNewton = new Button();
            btnSecante = new Button();
            btnBairstow = new Button();
            btnMuller = new Button();
            btnPuntoFijo = new Button();
            btnNewton = new Button();
            btnReglaFalsa = new Button();
            btnBiseccion = new Button();
            panelContenedor = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(btnNewtonNL);
            panel1.Controls.Add(btnHornerNewton);
            panel1.Controls.Add(btnSecante);
            panel1.Controls.Add(btnBairstow);
            panel1.Controls.Add(btnMuller);
            panel1.Controls.Add(btnPuntoFijo);
            panel1.Controls.Add(btnNewton);
            panel1.Controls.Add(btnReglaFalsa);
            panel1.Controls.Add(btnBiseccion);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1352, 64);
            panel1.TabIndex = 0;
            // 
            // btnNewtonNL
            // 
            btnNewtonNL.Cursor = Cursors.Hand;
            btnNewtonNL.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNewtonNL.Location = new Point(1603, -1);
            btnNewtonNL.Name = "btnNewtonNL";
            btnNewtonNL.Size = new Size(319, 66);
            btnNewtonNL.TabIndex = 7;
            btnNewtonNL.Text = "Newton Raphson No Lineal";
            btnNewtonNL.UseVisualStyleBackColor = true;
            btnNewtonNL.Click += btnNewtonNL_Click;
            // 
            // btnHornerNewton
            // 
            btnHornerNewton.Cursor = Cursors.Hand;
            btnHornerNewton.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHornerNewton.Location = new Point(1396, 0);
            btnHornerNewton.Name = "btnHornerNewton";
            btnHornerNewton.Size = new Size(191, 66);
            btnHornerNewton.TabIndex = 6;
            btnHornerNewton.Text = "Horner Newton";
            btnHornerNewton.UseVisualStyleBackColor = true;
            btnHornerNewton.Click += btnHornerNewton_Click;
            // 
            // btnSecante
            // 
            btnSecante.Cursor = Cursors.Hand;
            btnSecante.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSecante.Location = new Point(625, 0);
            btnSecante.Name = "btnSecante";
            btnSecante.Size = new Size(157, 66);
            btnSecante.TabIndex = 3;
            btnSecante.Text = "Secante ";
            btnSecante.UseVisualStyleBackColor = true;
            btnSecante.Click += btnSecante_Click;
            // 
            // btnBairstow
            // 
            btnBairstow.Cursor = Cursors.Hand;
            btnBairstow.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBairstow.Location = new Point(1199, -1);
            btnBairstow.Name = "btnBairstow";
            btnBairstow.Size = new Size(181, 66);
            btnBairstow.TabIndex = 5;
            btnBairstow.Text = "Bairstow";
            btnBairstow.UseVisualStyleBackColor = true;
            btnBairstow.Click += btnBairstow_Click;
            // 
            // btnMuller
            // 
            btnMuller.Cursor = Cursors.Hand;
            btnMuller.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMuller.Location = new Point(996, -1);
            btnMuller.Name = "btnMuller";
            btnMuller.Size = new Size(179, 66);
            btnMuller.TabIndex = 2;
            btnMuller.Text = "Muller";
            btnMuller.UseVisualStyleBackColor = true;
            btnMuller.Click += btnMuller_Click;
            // 
            // btnPuntoFijo
            // 
            btnPuntoFijo.Cursor = Cursors.Hand;
            btnPuntoFijo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPuntoFijo.Location = new Point(808, -1);
            btnPuntoFijo.Name = "btnPuntoFijo";
            btnPuntoFijo.Size = new Size(158, 66);
            btnPuntoFijo.TabIndex = 4;
            btnPuntoFijo.Text = "Punto Fijo";
            btnPuntoFijo.UseVisualStyleBackColor = true;
            btnPuntoFijo.Click += btnPuntoFijo_Click;
            // 
            // btnNewton
            // 
            btnNewton.Cursor = Cursors.Hand;
            btnNewton.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNewton.Location = new Point(383, -1);
            btnNewton.Name = "btnNewton";
            btnNewton.Size = new Size(213, 65);
            btnNewton.TabIndex = 2;
            btnNewton.Text = "Newton Raphson";
            btnNewton.UseVisualStyleBackColor = true;
            btnNewton.Click += btnNewton_Click;
            // 
            // btnReglaFalsa
            // 
            btnReglaFalsa.Cursor = Cursors.Hand;
            btnReglaFalsa.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReglaFalsa.Location = new Point(194, 0);
            btnReglaFalsa.Name = "btnReglaFalsa";
            btnReglaFalsa.Size = new Size(164, 65);
            btnReglaFalsa.TabIndex = 1;
            btnReglaFalsa.Text = "Regla Falsa";
            btnReglaFalsa.UseVisualStyleBackColor = true;
            btnReglaFalsa.Click += btnReglaFalsa_Click;
            // 
            // btnBiseccion
            // 
            btnBiseccion.BackColor = SystemColors.ControlLightLight;
            btnBiseccion.Cursor = Cursors.Hand;
            btnBiseccion.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBiseccion.Location = new Point(0, -2);
            btnBiseccion.Name = "btnBiseccion";
            btnBiseccion.Size = new Size(169, 66);
            btnBiseccion.TabIndex = 0;
            btnBiseccion.Text = "Bisección";
            btnBiseccion.UseVisualStyleBackColor = false;
            btnBiseccion.Click += btnBiseccion_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContenedor.BackColor = SystemColors.ActiveCaption;
            panelContenedor.Location = new Point(0, 64);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1352, 445);
            panelContenedor.TabIndex = 1;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1352, 509);
            Controls.Add(panelContenedor);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormPrincipal";
            Text = "FormPrincipal";
            WindowState = FormWindowState.Maximized;
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
        private Button btnHornerNewton;
        private Button btnNewtonNL;
    }
}