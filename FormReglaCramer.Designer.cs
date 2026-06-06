namespace Métodos_Numéricos
{
    partial class FormReglaCramer
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
            txtMatrizA = new TextBox();
            txtVectorB = new TextBox();
            la = new Label();
            label2 = new Label();
            dgvReglaCramer = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReglaCramer).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(472, 65);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(572, 65);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(116, 29);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(694, 65);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(94, 67);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(125, 99);
            txtMatrizA.TabIndex = 3;
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(305, 65);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(125, 101);
            txtVectorB.TabIndex = 4;
            // 
            // la
            // 
            la.AutoSize = true;
            la.Location = new Point(23, 74);
            la.Name = "la";
            la.Size = new Size(65, 20);
            la.TabIndex = 5;
            la.Text = "Matriz A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(235, 74);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 6;
            label2.Text = "Vector B";
            // 
            // dgvReglaCramer
            // 
            dgvReglaCramer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReglaCramer.Location = new Point(12, 172);
            dgvReglaCramer.Name = "dgvReglaCramer";
            dgvReglaCramer.RowHeadersWidth = 51;
            dgvReglaCramer.Size = new Size(776, 266);
            dgvReglaCramer.TabIndex = 7;
            // 
            // FormReglaCramer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvReglaCramer);
            Controls.Add(label2);
            Controls.Add(la);
            Controls.Add(txtVectorB);
            Controls.Add(txtMatrizA);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormReglaCramer";
            Text = "FormReglaCramer";
            ((System.ComponentModel.ISupportInitialize)dgvReglaCramer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private TextBox txtMatrizA;
        private TextBox txtVectorB;
        private Label la;
        private Label label2;
        private DataGridView dgvReglaCramer;
    }
}