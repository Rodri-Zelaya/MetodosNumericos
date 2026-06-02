namespace Métodos_Numéricos
{
    partial class FormDash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDash));
            PanelMenuLateral = new Panel();
            panelSubMenuNoLineales = new Panel();
            btnNewtonNoLineal = new Button();
            btnMétodosNoLineales = new Button();
            panelSubMenuPolinomios = new Panel();
            button8 = new Button();
            button7 = new Button();
            btnMuller = new Button();
            btnPolinomios = new Button();
            panelSubMenuAbiertos = new Panel();
            button3 = new Button();
            button1 = new Button();
            button2 = new Button();
            btnMétodoAbiertos = new Button();
            panelSubMenuCerrados = new Panel();
            button5 = new Button();
            button4 = new Button();
            btnMétodosCerrados = new Button();
            PanelLogo = new Panel();
            pictureBox1 = new PictureBox();
            panelContenedorPrincipal = new Panel();
            PanelMenuLateral.SuspendLayout();
            panelSubMenuNoLineales.SuspendLayout();
            panelSubMenuPolinomios.SuspendLayout();
            panelSubMenuAbiertos.SuspendLayout();
            panelSubMenuCerrados.SuspendLayout();
            PanelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // PanelMenuLateral
            // 
            PanelMenuLateral.Controls.Add(panelSubMenuNoLineales);
            PanelMenuLateral.Controls.Add(btnMétodosNoLineales);
            PanelMenuLateral.Controls.Add(panelSubMenuPolinomios);
            PanelMenuLateral.Controls.Add(btnPolinomios);
            PanelMenuLateral.Controls.Add(panelSubMenuAbiertos);
            PanelMenuLateral.Controls.Add(btnMétodoAbiertos);
            PanelMenuLateral.Controls.Add(panelSubMenuCerrados);
            PanelMenuLateral.Controls.Add(btnMétodosCerrados);
            PanelMenuLateral.Controls.Add(PanelLogo);
            PanelMenuLateral.Dock = DockStyle.Left;
            PanelMenuLateral.Location = new Point(0, 0);
            PanelMenuLateral.Name = "PanelMenuLateral";
            PanelMenuLateral.Size = new Size(253, 753);
            PanelMenuLateral.TabIndex = 0;
            // 
            // panelSubMenuNoLineales
            // 
            panelSubMenuNoLineales.Controls.Add(btnNewtonNoLineal);
            panelSubMenuNoLineales.Dock = DockStyle.Top;
            panelSubMenuNoLineales.Location = new Point(0, 540);
            panelSubMenuNoLineales.Name = "panelSubMenuNoLineales";
            panelSubMenuNoLineales.Size = new Size(253, 31);
            panelSubMenuNoLineales.TabIndex = 0;
            // 
            // btnNewtonNoLineal
            // 
            btnNewtonNoLineal.Dock = DockStyle.Top;
            btnNewtonNoLineal.Location = new Point(0, 0);
            btnNewtonNoLineal.Name = "btnNewtonNoLineal";
            btnNewtonNoLineal.Size = new Size(253, 31);
            btnNewtonNoLineal.TabIndex = 0;
            btnNewtonNoLineal.Text = "Newton Raphson No Lineal";
            btnNewtonNoLineal.UseVisualStyleBackColor = true;
            btnNewtonNoLineal.Click += btnNewtonNoLineal_Click;
            // 
            // btnMétodosNoLineales
            // 
            btnMétodosNoLineales.Dock = DockStyle.Top;
            btnMétodosNoLineales.Location = new Point(0, 493);
            btnMétodosNoLineales.Name = "btnMétodosNoLineales";
            btnMétodosNoLineales.Size = new Size(253, 47);
            btnMétodosNoLineales.TabIndex = 0;
            btnMétodosNoLineales.Text = "Métodos No Lineales";
            btnMétodosNoLineales.UseVisualStyleBackColor = true;
            btnMétodosNoLineales.Click += btnMétodosNoLineales_Click;
            // 
            // panelSubMenuPolinomios
            // 
            panelSubMenuPolinomios.Controls.Add(button8);
            panelSubMenuPolinomios.Controls.Add(button7);
            panelSubMenuPolinomios.Controls.Add(btnMuller);
            panelSubMenuPolinomios.Dock = DockStyle.Top;
            panelSubMenuPolinomios.Location = new Point(0, 403);
            panelSubMenuPolinomios.Name = "panelSubMenuPolinomios";
            panelSubMenuPolinomios.Size = new Size(253, 90);
            panelSubMenuPolinomios.TabIndex = 0;
            // 
            // button8
            // 
            button8.Dock = DockStyle.Top;
            button8.Location = new Point(0, 58);
            button8.Name = "button8";
            button8.Size = new Size(253, 29);
            button8.TabIndex = 0;
            button8.Text = "Bairstow";
            button8.UseVisualStyleBackColor = true;
            button8.Click += btnBairstow_Click;
            // 
            // button7
            // 
            button7.Dock = DockStyle.Top;
            button7.Location = new Point(0, 29);
            button7.Name = "button7";
            button7.Size = new Size(253, 29);
            button7.TabIndex = 0;
            button7.Text = "Horner Newton";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btnHornerNewton_Click;
            // 
            // btnMuller
            // 
            btnMuller.Dock = DockStyle.Top;
            btnMuller.Location = new Point(0, 0);
            btnMuller.Name = "btnMuller";
            btnMuller.Size = new Size(253, 29);
            btnMuller.TabIndex = 0;
            btnMuller.Text = "Muller";
            btnMuller.UseVisualStyleBackColor = true;
            btnMuller.Click += btnMuller_Click;
            // 
            // btnPolinomios
            // 
            btnPolinomios.Dock = DockStyle.Top;
            btnPolinomios.Location = new Point(0, 355);
            btnPolinomios.Name = "btnPolinomios";
            btnPolinomios.Size = new Size(253, 48);
            btnPolinomios.TabIndex = 0;
            btnPolinomios.Text = "Polinomios";
            btnPolinomios.UseVisualStyleBackColor = true;
            btnPolinomios.Click += btnPolinomios_Click;
            // 
            // panelSubMenuAbiertos
            // 
            panelSubMenuAbiertos.Controls.Add(button3);
            panelSubMenuAbiertos.Controls.Add(button1);
            panelSubMenuAbiertos.Controls.Add(button2);
            panelSubMenuAbiertos.Dock = DockStyle.Top;
            panelSubMenuAbiertos.Location = new Point(0, 268);
            panelSubMenuAbiertos.Name = "panelSubMenuAbiertos";
            panelSubMenuAbiertos.Size = new Size(253, 87);
            panelSubMenuAbiertos.TabIndex = 0;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Top;
            button3.Location = new Point(0, 58);
            button3.Name = "button3";
            button3.Size = new Size(253, 29);
            button3.TabIndex = 2;
            button3.Text = "Punto Fijo";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnPuntoFijo_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Top;
            button1.Location = new Point(0, 29);
            button1.Name = "button1";
            button1.Size = new Size(253, 29);
            button1.TabIndex = 0;
            button1.Text = "Secante";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSecante_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Top;
            button2.Location = new Point(0, 0);
            button2.Name = "button2";
            button2.Size = new Size(253, 29);
            button2.TabIndex = 1;
            button2.Text = "Newton Raphson";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnNewtonRaphson_Click;
            // 
            // btnMétodoAbiertos
            // 
            btnMétodoAbiertos.Dock = DockStyle.Top;
            btnMétodoAbiertos.Location = new Point(0, 225);
            btnMétodoAbiertos.Name = "btnMétodoAbiertos";
            btnMétodoAbiertos.Size = new Size(253, 43);
            btnMétodoAbiertos.TabIndex = 0;
            btnMétodoAbiertos.Text = "Métodos Abiertos";
            btnMétodoAbiertos.UseVisualStyleBackColor = true;
            btnMétodoAbiertos.Click += btnMétodoAbiertos_Click;
            // 
            // panelSubMenuCerrados
            // 
            panelSubMenuCerrados.Controls.Add(button5);
            panelSubMenuCerrados.Controls.Add(button4);
            panelSubMenuCerrados.Dock = DockStyle.Top;
            panelSubMenuCerrados.Location = new Point(0, 165);
            panelSubMenuCerrados.Name = "panelSubMenuCerrados";
            panelSubMenuCerrados.Size = new Size(253, 60);
            panelSubMenuCerrados.TabIndex = 0;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Top;
            button5.Location = new Point(0, 29);
            button5.Name = "button5";
            button5.Size = new Size(253, 29);
            button5.TabIndex = 1;
            button5.Text = "Regla Falsa";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnReglaFalsa_Click;
            // 
            // button4
            // 
            button4.Dock = DockStyle.Top;
            button4.Location = new Point(0, 0);
            button4.Name = "button4";
            button4.Size = new Size(253, 29);
            button4.TabIndex = 0;
            button4.Text = "Bisección";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnBiseccion_Click;
            // 
            // btnMétodosCerrados
            // 
            btnMétodosCerrados.Dock = DockStyle.Top;
            btnMétodosCerrados.Location = new Point(0, 122);
            btnMétodosCerrados.Name = "btnMétodosCerrados";
            btnMétodosCerrados.Size = new Size(253, 43);
            btnMétodosCerrados.TabIndex = 0;
            btnMétodosCerrados.Text = "Métodos Cerrados";
            btnMétodosCerrados.UseVisualStyleBackColor = true;
            btnMétodosCerrados.Click += btnMétodosCerrados_Click;
            // 
            // PanelLogo
            // 
            PanelLogo.Controls.Add(pictureBox1);
            PanelLogo.Dock = DockStyle.Top;
            PanelLogo.Location = new Point(0, 0);
            PanelLogo.Name = "PanelLogo";
            PanelLogo.Size = new Size(253, 122);
            PanelLogo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(253, 122);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelContenedorPrincipal
            // 
            panelContenedorPrincipal.Dock = DockStyle.Fill;
            panelContenedorPrincipal.Location = new Point(253, 0);
            panelContenedorPrincipal.Name = "panelContenedorPrincipal";
            panelContenedorPrincipal.Size = new Size(547, 753);
            panelContenedorPrincipal.TabIndex = 1;
            // 
            // FormDash
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 753);
            Controls.Add(panelContenedorPrincipal);
            Controls.Add(PanelMenuLateral);
            Name = "FormDash";
            Text = "FormDash";
            WindowState = FormWindowState.Maximized;
            PanelMenuLateral.ResumeLayout(false);
            panelSubMenuNoLineales.ResumeLayout(false);
            panelSubMenuPolinomios.ResumeLayout(false);
            panelSubMenuAbiertos.ResumeLayout(false);
            panelSubMenuCerrados.ResumeLayout(false);
            PanelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelMenuLateral;
        private Panel PanelLogo;
        private Panel panelContenedorPrincipal;
        private Button btnMétodosCerrados;
        private Panel panelSubMenuAbiertos;
        private Button btnMétodoAbiertos;
        private Panel panelSubMenuCerrados;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button1;
        private Button button2;
        private Panel panelSubMenuPolinomios;
        private Button button8;
        private Button button7;
        private Button btnMuller;
        private Button btnPolinomios;
        private Panel panelSubMenuNoLineales;
        private Button btnNewtonNoLineal;
        private Button btnMétodosNoLineales;
        private PictureBox pictureBox1;
    }
}