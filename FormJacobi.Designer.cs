namespace Métodos_Numéricos
{
    partial class FormJacobi
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtMatrizA = new TextBox();
            txtVectorB = new TextBox();
            txtValoresIniciales = new TextBox();
            txtTolerancia = new TextBox();
            dgvJacobi = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvJacobi).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Location = new Point(583, 52);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(147, 46);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Location = new Point(736, 52);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(172, 46);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Location = new Point(914, 52);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(153, 46);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(799, 202);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 4;
            label2.Text = "Vector B";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 186);
            label3.Name = "label3";
            label3.Size = new Size(114, 20);
            label3.TabIndex = 5;
            label3.Text = "Valores Iniciales";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Location = new Point(477, 52);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 6;
            label4.Text = "Tolerancia";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(72, 78);
            label5.Name = "label5";
            label5.Size = new Size(65, 20);
            label5.TabIndex = 7;
            label5.Text = "Matriz A";
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(143, 28);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(175, 100);
            txtMatrizA.TabIndex = 8;
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(869, 147);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(175, 100);
            txtVectorB.TabIndex = 9;
            // 
            // txtValoresIniciales
            // 
            txtValoresIniciales.Location = new Point(143, 147);
            txtValoresIniciales.Multiline = true;
            txtValoresIniciales.Name = "txtValoresIniciales";
            txtValoresIniciales.Size = new Size(175, 92);
            txtValoresIniciales.TabIndex = 10;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Location = new Point(458, 78);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(119, 27);
            txtTolerancia.TabIndex = 11;
            // 
            // dgvJacobi
            // 
            dgvJacobi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvJacobi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvJacobi.Location = new Point(12, 262);
            dgvJacobi.Name = "dgvJacobi";
            dgvJacobi.RowHeadersWidth = 51;
            dgvJacobi.Size = new Size(1055, 188);
            dgvJacobi.TabIndex = 12;
            // 
            // FormJacobi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1079, 462);
            Controls.Add(dgvJacobi);
            Controls.Add(txtTolerancia);
            Controls.Add(txtValoresIniciales);
            Controls.Add(txtVectorB);
            Controls.Add(txtMatrizA);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormJacobi";
            Text = "FormJacobi";
            ((System.ComponentModel.ISupportInitialize)dgvJacobi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtMatrizA;
        private TextBox txtVectorB;
        private TextBox txtValoresIniciales;
        private TextBox txtTolerancia;
        private DataGridView dgvJacobi;
    }
}