namespace Métodos_Numéricos
{
    partial class FormTrapecio
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
            dgvResultados = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(460, 58);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(571, 58);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(94, 29);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(683, 58);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(398, 270);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.RowHeadersWidth = 51;
            dgvResultados.Size = new Size(300, 188);
            dgvResultados.TabIndex = 4;
            // 
            // FormTrapecio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvResultados);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Name = "FormTrapecio";
            Text = "FormTrapecio";
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private DataGridView dgvResultados;
    }
}