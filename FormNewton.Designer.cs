namespace Métodos_Numéricos
{
    partial class FormNewton
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
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtFuncionNewton = new TextBox();
            txtVI = new TextBox();
            txtTolNewton = new TextBox();
            btnCalcular = new Button();
            dgvNewton = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvNewton).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 21);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 0;
            label1.Text = "Función";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(234, 46);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 2;
            label3.Text = "Valor Inicial";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(464, 18);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 3;
            label4.Text = "Tolerancia";
            // 
            // txtFuncionNewton
            // 
            txtFuncionNewton.Location = new Point(103, 18);
            txtFuncionNewton.Name = "txtFuncionNewton";
            txtFuncionNewton.Size = new Size(125, 27);
            txtFuncionNewton.TabIndex = 5;
            // 
            // txtVI
            // 
            txtVI.Location = new Point(326, 43);
            txtVI.Name = "txtVI";
            txtVI.Size = new Size(125, 27);
            txtVI.TabIndex = 7;
            // 
            // txtTolNewton
            // 
            txtTolNewton.Location = new Point(547, 18);
            txtTolNewton.Name = "txtTolNewton";
            txtTolNewton.Size = new Size(125, 27);
            txtTolNewton.TabIndex = 8;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(678, 32);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(110, 49);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvNewton
            // 
            dgvNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNewton.Location = new Point(12, 105);
            dgvNewton.Name = "dgvNewton";
            dgvNewton.RowHeadersWidth = 51;
            dgvNewton.Size = new Size(776, 333);
            dgvNewton.TabIndex = 11;
            // 
            // FormNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvNewton);
            Controls.Add(btnCalcular);
            Controls.Add(txtTolNewton);
            Controls.Add(txtVI);
            Controls.Add(txtFuncionNewton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "FormNewton";
            Text = "FormNewton";
            ((System.ComponentModel.ISupportInitialize)dgvNewton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox txtFuncionNewton;
        private TextBox txtVI;
        private TextBox txtTolNewton;
        private Button btnCalcular;
        private DataGridView dgvNewton;
    }
}