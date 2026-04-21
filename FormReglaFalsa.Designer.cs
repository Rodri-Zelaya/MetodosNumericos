namespace Métodos_Numéricos
{
    partial class FormReglaFalsa
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
            txtFuncionReglaFalsa = new TextBox();
            txtA = new TextBox();
            txtB = new TextBox();
            txtTolerancia = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dgvReglaFalsa = new DataGridView();
            btnCalcular = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReglaFalsa).BeginInit();
            SuspendLayout();
            // 
            // txtFuncionReglaFalsa
            // 
            txtFuncionReglaFalsa.Location = new Point(72, 45);
            txtFuncionReglaFalsa.Name = "txtFuncionReglaFalsa";
            txtFuncionReglaFalsa.Size = new Size(125, 27);
            txtFuncionReglaFalsa.TabIndex = 0;
            // 
            // txtA
            // 
            txtA.Location = new Point(267, 18);
            txtA.Name = "txtA";
            txtA.Size = new Size(125, 27);
            txtA.TabIndex = 1;
            // 
            // txtB
            // 
            txtB.Location = new Point(267, 70);
            txtB.Name = "txtB";
            txtB.Size = new Size(125, 27);
            txtB.TabIndex = 2;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Location = new Point(482, 18);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(125, 27);
            txtTolerancia.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 52);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 5;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(202, 25);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 6;
            label2.Text = "Valor A";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(202, 77);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 7;
            label3.Text = "Valor B";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(399, 21);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 8;
            label4.Text = "Tolerancia";
            // 
            // dgvReglaFalsa
            // 
            dgvReglaFalsa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReglaFalsa.Location = new Point(16, 123);
            dgvReglaFalsa.Name = "dgvReglaFalsa";
            dgvReglaFalsa.RowHeadersWidth = 51;
            dgvReglaFalsa.Size = new Size(772, 315);
            dgvReglaFalsa.TabIndex = 10;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(636, 31);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(122, 54);
            btnCalcular.TabIndex = 11;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // FormReglaFalsa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCalcular);
            Controls.Add(dgvReglaFalsa);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTolerancia);
            Controls.Add(txtB);
            Controls.Add(txtA);
            Controls.Add(txtFuncionReglaFalsa);
            Name = "FormReglaFalsa";
            Text = "FormReglaFalsa";
            Load += FormReglaFalsa_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReglaFalsa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFuncionReglaFalsa;
        private TextBox txtA;
        private TextBox txtB;
        private TextBox txtTolerancia;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dgvReglaFalsa;
        private Button btnCalcular;
    }
}