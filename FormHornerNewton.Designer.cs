namespace Métodos_Numéricos
{
    partial class FormHornerNewton
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
            txtR0 = new TextBox();
            txtTolerancia = new TextBox();
            label1 = new Label();
            lblRaiz = new Label();
            label3 = new Label();
            label4 = new Label();
            btnCalcular = new Button();
            dgvHornerNewton = new DataGridView();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHornerNewton).BeginInit();
            SuspendLayout();
            // 
            // txtCoeficientes
            // 
            txtCoeficientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCoeficientes.Location = new Point(188, 45);
            txtCoeficientes.Name = "txtCoeficientes";
            txtCoeficientes.Size = new Size(362, 34);
            txtCoeficientes.TabIndex = 0;
            // 
            // txtR0
            // 
            txtR0.Anchor = AnchorStyles.Top;
            txtR0.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtR0.Location = new Point(556, 8);
            txtR0.Name = "txtR0";
            txtR0.Size = new Size(169, 34);
            txtR0.TabIndex = 1;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolerancia.Location = new Point(556, 70);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(169, 34);
            txtTolerancia.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(37, 49);
            label1.Name = "label1";
            label1.Size = new Size(145, 31);
            label1.TabIndex = 3;
            label1.Text = "Coeficientes";
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(782, 70);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(66, 31);
            lblRaiz.TabIndex = 4;
            lblRaiz.Text = "RAIZ";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(427, 70);
            label3.Name = "label3";
            label3.Size = new Size(123, 31);
            label3.TabIndex = 5;
            label3.Text = "Tolerancia";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(477, 11);
            label4.Name = "label4";
            label4.Size = new Size(36, 31);
            label4.TabIndex = 6;
            label4.Text = "r0";
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(974, 8);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(159, 56);
            btnCalcular.TabIndex = 7;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvHornerNewton
            // 
            dgvHornerNewton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHornerNewton.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHornerNewton.BackgroundColor = SystemColors.ActiveCaption;
            dgvHornerNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHornerNewton.Location = new Point(12, 123);
            dgvHornerNewton.Name = "dgvHornerNewton";
            dgvHornerNewton.RowHeadersWidth = 51;
            dgvHornerNewton.Size = new Size(1323, 323);
            dgvHornerNewton.TabIndex = 8;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1139, 8);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(203, 56);
            btnExportar.TabIndex = 9;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormHornerNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1347, 475);
            Controls.Add(btnExportar);
            Controls.Add(dgvHornerNewton);
            Controls.Add(btnCalcular);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lblRaiz);
            Controls.Add(label1);
            Controls.Add(txtTolerancia);
            Controls.Add(txtR0);
            Controls.Add(txtCoeficientes);
            Name = "FormHornerNewton";
            Text = "FormHornerNewton";
            ((System.ComponentModel.ISupportInitialize)dgvHornerNewton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCoeficientes;
        private TextBox txtR0;
        private TextBox txtTolerancia;
        private Label label1;
        private Label lblRaiz;
        private Label label3;
        private Label label4;
        private Button btnCalcular;
        private DataGridView dgvHornerNewton;
        private Button btnExportar;
    }
}