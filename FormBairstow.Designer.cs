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
            txtR0 = new TextBox();
            txtS0 = new TextBox();
            txtTolerancia = new TextBox();
            lblRaiz = new Label();
            dgvBairstow = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnCalcular = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBairstow).BeginInit();
            SuspendLayout();
            // 
            // txtCoeficientes
            // 
            txtCoeficientes.Location = new Point(109, 48);
            txtCoeficientes.Name = "txtCoeficientes";
            txtCoeficientes.Size = new Size(125, 27);
            txtCoeficientes.TabIndex = 0;
            // 
            // txtR0
            // 
            txtR0.Location = new Point(279, 12);
            txtR0.Name = "txtR0";
            txtR0.Size = new Size(125, 27);
            txtR0.TabIndex = 1;
            // 
            // txtS0
            // 
            txtS0.Location = new Point(279, 79);
            txtS0.Name = "txtS0";
            txtS0.Size = new Size(125, 27);
            txtS0.TabIndex = 2;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Location = new Point(503, 12);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(125, 27);
            txtTolerancia.TabIndex = 3;
            // 
            // lblRaiz
            // 
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(420, 78);
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
            dgvBairstow.Size = new Size(776, 319);
            dgvBairstow.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 55);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 6;
            label1.Text = "Coeficientes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(251, 19);
            label2.Name = "label2";
            label2.Size = new Size(22, 20);
            label2.TabIndex = 7;
            label2.Text = "r0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(250, 86);
            label3.Name = "label3";
            label3.Size = new Size(23, 20);
            label3.TabIndex = 8;
            label3.Text = "s0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(420, 19);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 9;
            label4.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(651, 1);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(123, 57);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // FormBairstow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCalcular);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvBairstow);
            Controls.Add(lblRaiz);
            Controls.Add(txtTolerancia);
            Controls.Add(txtS0);
            Controls.Add(txtR0);
            Controls.Add(txtCoeficientes);
            Name = "FormBairstow";
            Text = "FormBairstow";
            ((System.ComponentModel.ISupportInitialize)dgvBairstow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCoeficientes;
        private TextBox txtR0;
        private TextBox txtS0;
        private TextBox txtTolerancia;
        private Label lblRaiz;
        private DataGridView dgvBairstow;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnCalcular;
    }
}