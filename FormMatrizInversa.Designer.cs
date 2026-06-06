namespace Métodos_Numéricos
{
    partial class FormMatrizInversa
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
            btnCalcular = new Button();
            btnExportar = new Button();
            btnLimpiar = new Button();
            dgvMatrizInversa = new DataGridView();
            txtMatrizA = new TextBox();
            txtVectorB = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMatrizInversa).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(450, 63);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(550, 63);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(128, 29);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(684, 63);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvMatrizInversa
            // 
            dgvMatrizInversa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatrizInversa.Location = new Point(12, 168);
            dgvMatrizInversa.Name = "dgvMatrizInversa";
            dgvMatrizInversa.RowHeadersWidth = 51;
            dgvMatrizInversa.Size = new Size(776, 281);
            dgvMatrizInversa.TabIndex = 3;
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(103, 60);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(125, 84);
            txtMatrizA.TabIndex = 4;
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(304, 60);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(125, 84);
            txtVectorB.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 63);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 6;
            label1.Text = "Matriz A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(234, 67);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 7;
            label2.Text = "Vector B";
            // 
            // FormMatrizInversa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtVectorB);
            Controls.Add(txtMatrizA);
            Controls.Add(dgvMatrizInversa);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormMatrizInversa";
            Text = "FormMatrizInversa";
            ((System.ComponentModel.ISupportInitialize)dgvMatrizInversa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private DataGridView dgvMatrizInversa;
        private TextBox txtMatrizA;
        private TextBox txtVectorB;
        private Label label1;
        private Label label2;
    }
}