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
            lblRaiz = new Label();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPuntoFijo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(336, 15);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 0;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(353, 64);
            label2.Name = "label2";
            label2.Size = new Size(26, 20);
            label2.TabIndex = 1;
            label2.Text = "X0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(651, 15);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 2;
            label3.Text = "Tolerancia";
            // 
            // txtFuncionPuntoFijo
            // 
            txtFuncionPuntoFijo.Location = new Point(402, 12);
            txtFuncionPuntoFijo.Name = "txtFuncionPuntoFijo";
            txtFuncionPuntoFijo.Size = new Size(188, 27);
            txtFuncionPuntoFijo.TabIndex = 5;
            // 
            // txtX0PuntoFijo
            // 
            txtX0PuntoFijo.Location = new Point(402, 57);
            txtX0PuntoFijo.Name = "txtX0PuntoFijo";
            txtX0PuntoFijo.Size = new Size(125, 27);
            txtX0PuntoFijo.TabIndex = 6;
            // 
            // txtTolPuntoFijo
            // 
            txtTolPuntoFijo.Location = new Point(744, 12);
            txtTolPuntoFijo.Name = "txtTolPuntoFijo";
            txtTolPuntoFijo.Size = new Size(125, 27);
            txtTolPuntoFijo.TabIndex = 7;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(962, 7);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(115, 51);
            btnCalcular.TabIndex = 9;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvPuntoFijo
            // 
            dgvPuntoFijo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntoFijo.Location = new Point(301, 121);
            dgvPuntoFijo.Name = "dgvPuntoFijo";
            dgvPuntoFijo.RowHeadersWidth = 51;
            dgvPuntoFijo.Size = new Size(776, 334);
            dgvPuntoFijo.TabIndex = 10;
            // 
            // lblRaiz
            // 
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(661, 64);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(57, 28);
            lblRaiz.TabIndex = 11;
            lblRaiz.Text = "Raiz:";
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(962, 64);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(115, 51);
            btnExportar.TabIndex = 12;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormPuntoFijo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1342, 467);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
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
        private Label lblRaiz;
        private Button btnExportar;
    }
}