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
            txtCoeficientes.Location = new Point(175, 49);
            txtCoeficientes.Name = "txtCoeficientes";
            txtCoeficientes.Size = new Size(202, 27);
            txtCoeficientes.TabIndex = 0;
            // 
            // txtR0
            // 
            txtR0.Location = new Point(515, 16);
            txtR0.Name = "txtR0";
            txtR0.Size = new Size(125, 27);
            txtR0.TabIndex = 1;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Location = new Point(515, 63);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(125, 27);
            txtTolerancia.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 56);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 3;
            label1.Text = "Coeficientes";
            // 
            // lblRaiz
            // 
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(731, 45);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(57, 28);
            lblRaiz.TabIndex = 4;
            lblRaiz.Text = "RAIZ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(417, 70);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 5;
            label3.Text = "Tolerancia";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(454, 19);
            label4.Name = "label4";
            label4.Size = new Size(22, 20);
            label4.TabIndex = 6;
            label4.Text = "r0";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(1039, 28);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(121, 53);
            btnCalcular.TabIndex = 7;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvHornerNewton
            // 
            dgvHornerNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHornerNewton.Location = new Point(12, 123);
            dgvHornerNewton.Name = "dgvHornerNewton";
            dgvHornerNewton.RowHeadersWidth = 51;
            dgvHornerNewton.Size = new Size(1323, 323);
            dgvHornerNewton.TabIndex = 8;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(1166, 28);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(139, 53);
            btnExportar.TabIndex = 9;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormHornerNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
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