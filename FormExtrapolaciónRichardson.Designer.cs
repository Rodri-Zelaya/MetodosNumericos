namespace Métodos_Numéricos
{
    partial class FormExtrapolaciónRichardson
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
            dgvResultados = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnCalcular = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(12, 225);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.RowHeadersWidth = 51;
            dgvResultados.Size = new Size(776, 213);
            dgvResultados.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 46);
            label1.Name = "label1";
            label1.Size = new Size(41, 20);
            label1.TabIndex = 1;
            label1.Text = "D(h):";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 85);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 2;
            label2.Text = "D(h/2):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 127);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 3;
            label3.Text = "Orden(n):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(38, 168);
            label4.Name = "label4";
            label4.Size = new Size(94, 20);
            label4.TabIndex = 4;
            label4.Text = "Valor Exacto:";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(445, 64);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 5;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(664, 64);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormExtrapolaciónRichardson
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCalcular);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvResultados);
            Name = "FormExtrapolaciónRichardson";
            Text = "FormExtrapolaciónRichardson";
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvResultados;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnCalcular;
        private Button btnLimpiar;
    }
}