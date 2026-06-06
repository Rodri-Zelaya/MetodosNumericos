namespace Métodos_Numéricos
{
    partial class FormEliminaciónGaussiana
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
            btnExportarExcel = new Button();
            btnLimpiar = new Button();
            dgvEliminacionGaussiana = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            txtMatrizA = new TextBox();
            txtVectorB = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvEliminacionGaussiana).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(452, 45);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.Location = new Point(552, 45);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(127, 29);
            btnExportarExcel.TabIndex = 1;
            btnExportarExcel.Text = "Exportar Excel";
            btnExportarExcel.UseVisualStyleBackColor = true;
            btnExportarExcel.Click += btnExportarExcel_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(685, 45);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvEliminacionGaussiana
            // 
            dgvEliminacionGaussiana.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEliminacionGaussiana.Location = new Point(12, 158);
            dgvEliminacionGaussiana.Name = "dgvEliminacionGaussiana";
            dgvEliminacionGaussiana.RowHeadersWidth = 51;
            dgvEliminacionGaussiana.Size = new Size(776, 280);
            dgvEliminacionGaussiana.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 45);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 4;
            label1.Text = "Matriz A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(214, 49);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 5;
            label2.Text = "Vector B";
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(83, 42);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(125, 86);
            txtMatrizA.TabIndex = 6;
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(284, 42);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(125, 86);
            txtVectorB.TabIndex = 7;
            // 
            // FormEliminaciónGaussiana
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtVectorB);
            Controls.Add(txtMatrizA);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvEliminacionGaussiana);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportarExcel);
            Controls.Add(btnCalcular);
            Name = "FormEliminaciónGaussiana";
            Text = "EliminaciónGaussiana";
            ((System.ComponentModel.ISupportInitialize)dgvEliminacionGaussiana).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportarExcel;
        private Button btnLimpiar;
        private DataGridView dgvEliminacionGaussiana;
        private Label label1;
        private Label label2;
        private TextBox txtMatrizA;
        private TextBox txtVectorB;
    }
}