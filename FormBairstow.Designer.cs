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
            txtCoeficientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCoeficientes.Location = new Point(164, 56);
            txtCoeficientes.Name = "txtCoeficientes";
            txtCoeficientes.Size = new Size(255, 34);
            txtCoeficientes.TabIndex = 0;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolerancia.Location = new Point(715, 22);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(216, 34);
            txtTolerancia.TabIndex = 3;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(466, 81);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(1177, 28);
            lblRaiz.TabIndex = 4;
            lblRaiz.Text = "Raiz:";
            // 
            // dgvBairstow
            // 
            dgvBairstow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBairstow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBairstow.BackgroundColor = SystemColors.ActiveCaption;
            dgvBairstow.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBairstow.Location = new Point(12, 128);
            dgvBairstow.Name = "dgvBairstow";
            dgvBairstow.RowHeadersWidth = 51;
            dgvBairstow.Size = new Size(1309, 319);
            dgvBairstow.TabIndex = 5;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 56);
            label1.Name = "label1";
            label1.Size = new Size(146, 25);
            label1.TabIndex = 6;
            label1.Text = "Coeficientes";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(586, 22);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 9;
            label4.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(937, 12);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(188, 59);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1131, 12);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(190, 59);
            btnExportar.TabIndex = 11;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // lblR0
            // 
            lblR0.Anchor = AnchorStyles.Top;
            lblR0.AutoSize = true;
            lblR0.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblR0.Location = new Point(130, 18);
            lblR0.Name = "lblR0";
            lblR0.Size = new Size(345, 31);
            lblR0.TabIndex = 12;
            lblR0.Text = "R0 calculado automaticamente";
            // 
            // lblS0
            // 
            lblS0.Anchor = AnchorStyles.Top;
            lblS0.AutoSize = true;
            lblS0.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblS0.Location = new Point(130, 81);
            lblS0.Name = "lblS0";
            lblS0.Size = new Size(343, 31);
            lblS0.TabIndex = 13;
            lblS0.Text = "S0 calculado automaticamente";
            // 
            // FormBairstow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 493);
            Controls.Add(btnExportar);
            Controls.Add(label1);
            Controls.Add(btnCalcular);
            Controls.Add(lblS0);
            Controls.Add(lblRaiz);
            Controls.Add(label4);
            Controls.Add(txtTolerancia);
            Controls.Add(dgvBairstow);
            Controls.Add(txtCoeficientes);
            Controls.Add(lblR0);
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