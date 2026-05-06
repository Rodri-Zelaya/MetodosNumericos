namespace Métodos_Numéricos
{
    partial class FormSecante
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
            txtFuncionSecante = new TextBox();
            txtVI = new TextBox();
            txtV2 = new TextBox();
            txtTolSecante = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnCalcular = new Button();
            dgvSecante = new DataGridView();
            lblRaiz = new Label();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSecante).BeginInit();
            SuspendLayout();
            // 
            // txtFuncionSecante
            // 
            txtFuncionSecante.Location = new Point(117, 39);
            txtFuncionSecante.Name = "txtFuncionSecante";
            txtFuncionSecante.Size = new Size(215, 27);
            txtFuncionSecante.TabIndex = 0;
            // 
            // txtVI
            // 
            txtVI.Location = new Point(483, 15);
            txtVI.Name = "txtVI";
            txtVI.Size = new Size(125, 27);
            txtVI.TabIndex = 1;
            // 
            // txtV2
            // 
            txtV2.Location = new Point(483, 54);
            txtV2.Name = "txtV2";
            txtV2.Size = new Size(125, 27);
            txtV2.TabIndex = 2;
            // 
            // txtTolSecante
            // 
            txtTolSecante.Location = new Point(784, 11);
            txtTolSecante.Name = "txtTolSecante";
            txtTolSecante.Size = new Size(125, 27);
            txtTolSecante.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 46);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 5;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(382, 18);
            label2.Name = "label2";
            label2.Size = new Size(86, 20);
            label2.TabIndex = 6;
            label2.Text = "Valor Inicial";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(371, 61);
            label3.Name = "label3";
            label3.Size = new Size(106, 20);
            label3.TabIndex = 7;
            label3.Text = "Segundo Valor";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(701, 18);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 8;
            label4.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(1026, 28);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(125, 53);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvSecante
            // 
            dgvSecante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSecante.Location = new Point(15, 97);
            dgvSecante.Name = "dgvSecante";
            dgvSecante.RowHeadersWidth = 51;
            dgvSecante.Size = new Size(1313, 363);
            dgvSecante.TabIndex = 11;
            // 
            // lblRaiz
            // 
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(711, 54);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(57, 28);
            lblRaiz.TabIndex = 12;
            lblRaiz.Text = "Raiz:";
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(1157, 28);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(124, 53);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormSecante
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1340, 472);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
            Controls.Add(dgvSecante);
            Controls.Add(btnCalcular);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTolSecante);
            Controls.Add(txtV2);
            Controls.Add(txtVI);
            Controls.Add(txtFuncionSecante);
            Name = "FormSecante";
            Text = "FormSecante";
            ((System.ComponentModel.ISupportInitialize)dgvSecante).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFuncionSecante;
        private TextBox txtVI;
        private TextBox txtV2;
        private TextBox txtTolSecante;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnCalcular;
        private DataGridView dgvSecante;
        private Label lblRaiz;
        private Button btnExportar;
    }
}