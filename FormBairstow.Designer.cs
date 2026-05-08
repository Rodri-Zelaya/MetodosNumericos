namespace Métodos_Numéricos
{
    partial class FormBairstow
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
            txtCoeficientes = new TextBox();
            txtTolerancia = new TextBox();
            lblRaiz = new Label();
            dgvBairstow = new DataGridView();
            label1 = new Label();
            label4 = new Label();
            btnCalcular = new Button();
            btnExportar = new Button();
            lblR0 = new Label();
            lblS0 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBairstow).BeginInit();
            SuspendLayout();
            // 
            // txtCoeficientes
            // 
            txtCoeficientes.Location = new Point(108, 48);
            txtCoeficientes.Name = "txtCoeficientes";
            txtCoeficientes.Size = new Size(215, 27);
            txtCoeficientes.TabIndex = 0;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Location = new Point(666, 11);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(125, 27);
            txtTolerancia.TabIndex = 3;
            // 
            // lblRaiz
            // 
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(594, 63);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(57, 28);
            lblRaiz.TabIndex = 4;
            lblRaiz.Text = "Raiz:";
            // 
            // dgvBairstow
            // 
            dgvBairstow.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBairstow.Location = new Point(12, 128);
            dgvBairstow.Name = "dgvBairstow";
            dgvBairstow.RowHeadersWidth = 51;
            dgvBairstow.Size = new Size(1309, 319);
            dgvBairstow.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 48);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 6;
            label1.Text = "Coeficientes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(583, 18);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 9;
            label4.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(929, 29);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(123, 58);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(1097, 29);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(126, 58);
            btnExportar.TabIndex = 11;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // lblR0
            // 
            lblR0.AutoSize = true;
            lblR0.Location = new Point(343, 14);
            lblR0.Name = "lblR0";
            lblR0.Size = new Size(215, 20);
            lblR0.TabIndex = 12;
            lblR0.Text = "R0 calculado automaticamente";
            // 
            // lblS0
            // 
            lblS0.AutoSize = true;
            lblS0.Location = new Point(343, 70);
            lblS0.Name = "lblS0";
            lblS0.Size = new Size(214, 20);
            lblS0.TabIndex = 13;
            lblS0.Text = "S0 calculado automaticamente";
            // 
            // FormBairstow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 493);
            Controls.Add(lblS0);
            Controls.Add(lblR0);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(dgvBairstow);
            Controls.Add(lblRaiz);
            Controls.Add(txtTolerancia);
            Controls.Add(txtCoeficientes);
            Name = "FormBairstow";
            Text = "FormBairstow";
            ((System.ComponentModel.ISupportInitialize)dgvBairstow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCoeficientes;
        private TextBox txtTolerancia;
        private Label lblRaiz;
        private DataGridView dgvBairstow;
        private Label label1;
        private Label label4;
        private Button btnCalcular;
        private Button btnExportar;
        private Label lblR0;
        private Label lblS0;
    }
}