namespace Métodos_Numéricos
{
    partial class FormMuller
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
            txtX1 = new TextBox();
            txtX0 = new TextBox();
            txtFuncionMuller = new TextBox();
            txtX2 = new TextBox();
            txtTolerancia = new TextBox();
            lblRaiz = new Label();
            dgvMuller = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnCalcular = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMuller).BeginInit();
            SuspendLayout();
            // 
            // txtX1
            // 
            txtX1.Location = new Point(252, 47);
            txtX1.Name = "txtX1";
            txtX1.Size = new Size(125, 27);
            txtX1.TabIndex = 0;
            // 
            // txtX0
            // 
            txtX0.Location = new Point(252, 12);
            txtX0.Name = "txtX0";
            txtX0.Size = new Size(125, 27);
            txtX0.TabIndex = 1;
            // 
            // txtFuncionMuller
            // 
            txtFuncionMuller.Location = new Point(68, 47);
            txtFuncionMuller.Name = "txtFuncionMuller";
            txtFuncionMuller.Size = new Size(125, 27);
            txtFuncionMuller.TabIndex = 2;
            // 
            // txtX2
            // 
            txtX2.Location = new Point(252, 80);
            txtX2.Name = "txtX2";
            txtX2.Size = new Size(125, 27);
            txtX2.TabIndex = 3;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Location = new Point(488, 27);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(125, 27);
            txtTolerancia.TabIndex = 4;
            // 
            // lblRaiz
            // 
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(425, 76);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(57, 28);
            lblRaiz.TabIndex = 5;
            lblRaiz.Text = "Raiz:";
            // 
            // dgvMuller
            // 
            dgvMuller.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMuller.Location = new Point(12, 134);
            dgvMuller.Name = "dgvMuller";
            dgvMuller.RowHeadersWidth = 51;
            dgvMuller.Size = new Size(776, 304);
            dgvMuller.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 50);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 7;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(220, 19);
            label2.Name = "label2";
            label2.Size = new Size(26, 20);
            label2.TabIndex = 8;
            label2.Text = "X0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(220, 54);
            label3.Name = "label3";
            label3.Size = new Size(26, 20);
            label3.TabIndex = 9;
            label3.Text = "X1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(220, 87);
            label4.Name = "label4";
            label4.Size = new Size(26, 20);
            label4.TabIndex = 10;
            label4.Text = "X2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(405, 34);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 11;
            label5.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(652, 39);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(115, 50);
            btnCalcular.TabIndex = 12;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // FormMuller
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCalcular);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvMuller);
            Controls.Add(lblRaiz);
            Controls.Add(txtTolerancia);
            Controls.Add(txtX2);
            Controls.Add(txtFuncionMuller);
            Controls.Add(txtX0);
            Controls.Add(txtX1);
            Name = "FormMuller";
            Text = "FormMuller";
            ((System.ComponentModel.ISupportInitialize)dgvMuller).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtX1;
        private TextBox txtX0;
        private TextBox txtFuncionMuller;
        private TextBox txtX2;
        private TextBox txtTolerancia;
        private Label lblRaiz;
        private DataGridView dgvMuller;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnCalcular;
    }
}