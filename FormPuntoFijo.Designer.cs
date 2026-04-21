namespace Métodos_Numéricos
{
    partial class FormPuntoFijo
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
            label2 = new Label();
            label3 = new Label();
            txtFuncionPuntoFijo = new TextBox();
            txtX0PuntoFijo = new TextBox();
            txtTolPuntoFijo = new TextBox();
            btnCalcular = new Button();
            dgvPuntoFijo = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvPuntoFijo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(129, 14);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 0;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 60);
            label2.Name = "label2";
            label2.Size = new Size(26, 20);
            label2.TabIndex = 1;
            label2.Text = "X0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(344, 14);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 2;
            label3.Text = "Tolerancia";
            // 
            // txtFuncionPuntoFijo
            // 
            txtFuncionPuntoFijo.Location = new Point(195, 7);
            txtFuncionPuntoFijo.Name = "txtFuncionPuntoFijo";
            txtFuncionPuntoFijo.Size = new Size(125, 27);
            txtFuncionPuntoFijo.TabIndex = 5;
            // 
            // txtX0PuntoFijo
            // 
            txtX0PuntoFijo.Location = new Point(195, 53);
            txtX0PuntoFijo.Name = "txtX0PuntoFijo";
            txtX0PuntoFijo.Size = new Size(125, 27);
            txtX0PuntoFijo.TabIndex = 6;
            // 
            // txtTolPuntoFijo
            // 
            txtTolPuntoFijo.Location = new Point(427, 7);
            txtTolPuntoFijo.Name = "txtTolPuntoFijo";
            txtTolPuntoFijo.Size = new Size(125, 27);
            txtTolPuntoFijo.TabIndex = 7;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(593, 25);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(110, 51);
            btnCalcular.TabIndex = 9;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvPuntoFijo
            // 
            dgvPuntoFijo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntoFijo.Location = new Point(12, 104);
            dgvPuntoFijo.Name = "dgvPuntoFijo";
            dgvPuntoFijo.RowHeadersWidth = 51;
            dgvPuntoFijo.Size = new Size(776, 334);
            dgvPuntoFijo.TabIndex = 10;
            // 
            // FormPuntoFijo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvPuntoFijo);
            Controls.Add(btnCalcular);
            Controls.Add(txtTolPuntoFijo);
            Controls.Add(txtX0PuntoFijo);
            Controls.Add(txtFuncionPuntoFijo);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormPuntoFijo";
            Text = "FormPuntoFijo";
            ((System.ComponentModel.ISupportInitialize)dgvPuntoFijo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtFuncionPuntoFijo;
        private TextBox txtX0PuntoFijo;
        private TextBox txtTolPuntoFijo;
        private Button btnCalcular;
        private DataGridView dgvPuntoFijo;
    }
}