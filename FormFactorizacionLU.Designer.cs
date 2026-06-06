namespace Métodos_Numéricos
{
    partial class FormFactorizacionLU
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
            dgvFactorizaciónLU = new DataGridView();
            txtMatrizA = new TextBox();
            txtVectorB = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFactorizaciónLU).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(463, 51);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(563, 51);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(113, 29);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(682, 52);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar ";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvFactorizaciónLU
            // 
            dgvFactorizaciónLU.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFactorizaciónLU.Location = new Point(12, 176);
            dgvFactorizaciónLU.Name = "dgvFactorizaciónLU";
            dgvFactorizaciónLU.RowHeadersWidth = 51;
            dgvFactorizaciónLU.Size = new Size(776, 262);
            dgvFactorizaciónLU.TabIndex = 3;
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(94, 54);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(125, 85);
            txtMatrizA.TabIndex = 4;
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(294, 52);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(125, 87);
            txtVectorB.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 61);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 6;
            label1.Text = "Matriz A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(225, 56);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 7;
            label2.Text = "Vector B";
            // 
            // FormFactorizacionLU
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtVectorB);
            Controls.Add(txtMatrizA);
            Controls.Add(dgvFactorizaciónLU);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormFactorizacionLU";
            Text = "FormFactorizacionLU";
            ((System.ComponentModel.ISupportInitialize)dgvFactorizaciónLU).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private DataGridView dgvFactorizaciónLU;
        private TextBox txtMatrizA;
        private TextBox txtVectorB;
        private Label label1;
        private Label label2;
    }
}