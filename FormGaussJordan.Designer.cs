namespace Métodos_Numéricos
{
    partial class FormGaussJordan
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
            dgvGaussJordan = new DataGridView();
            txtMatrizA = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtVectorB = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvGaussJordan).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(449, 77);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(549, 77);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(120, 29);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(675, 77);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(85, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvGaussJordan
            // 
            dgvGaussJordan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGaussJordan.Location = new Point(12, 172);
            dgvGaussJordan.Name = "dgvGaussJordan";
            dgvGaussJordan.RowHeadersWidth = 51;
            dgvGaussJordan.Size = new Size(776, 266);
            dgvGaussJordan.TabIndex = 3;
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(91, 62);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(125, 92);
            txtMatrizA.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 69);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 5;
            label1.Text = "Matriz A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(222, 69);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 6;
            label2.Text = "Vector B";
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(287, 62);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(125, 92);
            txtVectorB.TabIndex = 7;
            // 
            // FormGaussJordan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtVectorB);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMatrizA);
            Controls.Add(dgvGaussJordan);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormGaussJordan";
            Text = "FormGaussJordan";
            ((System.ComponentModel.ISupportInitialize)dgvGaussJordan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private DataGridView dgvGaussJordan;
        private TextBox txtMatrizA;
        private Label label1;
        private Label label2;
        private TextBox txtVectorB;
    }
}