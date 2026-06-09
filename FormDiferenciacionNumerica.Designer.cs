namespace Métodos_Numéricos
{
    partial class FormDiferenciacionNumerica
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
            dgvPuntos = new DataGridView();
            ColX = new DataGridViewTextBoxColumn();
            ColY = new DataGridViewTextBoxColumn();
            dgvDerivadas = new DataGridView();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPuntos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDerivadas).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(447, 58);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(547, 58);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(134, 29);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(687, 58);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvPuntos
            // 
            dgvPuntos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntos.Columns.AddRange(new DataGridViewColumn[] { ColX, ColY });
            dgvPuntos.Location = new Point(12, 58);
            dgvPuntos.Name = "dgvPuntos";
            dgvPuntos.RowHeadersWidth = 51;
            dgvPuntos.Size = new Size(305, 104);
            dgvPuntos.TabIndex = 3;
            // 
            // ColX
            // 
            ColX.HeaderText = "Valor X";
            ColX.MinimumWidth = 6;
            ColX.Name = "ColX";
            ColX.Width = 125;
            // 
            // ColY
            // 
            ColY.HeaderText = "Valor Y";
            ColY.MinimumWidth = 6;
            ColY.Name = "ColY";
            ColY.Width = 125;
            // 
            // dgvDerivadas
            // 
            dgvDerivadas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDerivadas.Location = new Point(12, 202);
            dgvDerivadas.Name = "dgvDerivadas";
            dgvDerivadas.RowHeadersWidth = 51;
            dgvDerivadas.Size = new Size(769, 221);
            dgvDerivadas.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(151, 20);
            label1.TabIndex = 5;
            label1.Text = "Introduzca los puntos";
            // 
            // FormDiferenciacionNumerica
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(dgvDerivadas);
            Controls.Add(dgvPuntos);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormDiferenciacionNumerica";
            Text = "FormDiferenciacionNumerica";
            ((System.ComponentModel.ISupportInitialize)dgvPuntos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDerivadas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private DataGridView dgvPuntos;
        private DataGridViewTextBoxColumn ColX;
        private DataGridViewTextBoxColumn ColY;
        private DataGridView dgvDerivadas;
        private Label label1;
    }
}