namespace Métodos_Numéricos
{
    partial class FormNoLinealNewton
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
            txtF = new TextBox();
            txtY0 = new TextBox();
            txtX0 = new TextBox();
            txtG = new TextBox();
            txtTol = new TextBox();
            btnCalcular = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblRespuesta = new Label();
            dgvNoLinealNewton = new DataGridView();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNoLinealNewton).BeginInit();
            SuspendLayout();
            // 
            // txtF
            // 
            txtF.Location = new Point(206, 30);
            txtF.Name = "txtF";
            txtF.Size = new Size(206, 27);
            txtF.TabIndex = 0;
            // 
            // txtY0
            // 
            txtY0.Location = new Point(535, 85);
            txtY0.Name = "txtY0";
            txtY0.Size = new Size(125, 27);
            txtY0.TabIndex = 1;
            // 
            // txtX0
            // 
            txtX0.Location = new Point(535, 30);
            txtX0.Name = "txtX0";
            txtX0.Size = new Size(125, 27);
            txtX0.TabIndex = 2;
            // 
            // txtG
            // 
            txtG.Location = new Point(206, 85);
            txtG.Name = "txtG";
            txtG.Size = new Size(206, 27);
            txtG.TabIndex = 3;
            // 
            // txtTol
            // 
            txtTol.Location = new Point(824, 30);
            txtTol.Name = "txtTol";
            txtTol.Size = new Size(125, 27);
            txtTol.TabIndex = 4;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(1041, 30);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(129, 60);
            btnCalcular.TabIndex = 5;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(741, 33);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 6;
            label1.Text = "Tolerancia";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(505, 33);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 7;
            label2.Text = "x0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(505, 92);
            label3.Name = "label3";
            label3.Size = new Size(24, 20);
            label3.TabIndex = 8;
            label3.Text = "y0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(107, 33);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 9;
            label4.Text = "Ecuación 1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(107, 92);
            label5.Name = "label5";
            label5.Size = new Size(80, 20);
            label5.TabIndex = 10;
            label5.Text = "Ecuación 2";
            // 
            // lblRespuesta
            // 
            lblRespuesta.AutoSize = true;
            lblRespuesta.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRespuesta.Location = new Point(727, 92);
            lblRespuesta.Name = "lblRespuesta";
            lblRespuesta.Size = new Size(111, 25);
            lblRespuesta.TabIndex = 11;
            lblRespuesta.Text = "RESPUESTA";
            // 
            // dgvNoLinealNewton
            // 
            dgvNoLinealNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNoLinealNewton.Location = new Point(12, 139);
            dgvNoLinealNewton.Name = "dgvNoLinealNewton";
            dgvNoLinealNewton.RowHeadersWidth = 51;
            dgvNoLinealNewton.Size = new Size(1324, 308);
            dgvNoLinealNewton.TabIndex = 12;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(1176, 30);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(139, 60);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormNoLinealNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 474);
            Controls.Add(btnExportar);
            Controls.Add(dgvNoLinealNewton);
            Controls.Add(lblRespuesta);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCalcular);
            Controls.Add(txtTol);
            Controls.Add(txtG);
            Controls.Add(txtX0);
            Controls.Add(txtY0);
            Controls.Add(txtF);
            Name = "FormNoLinealNewton";
            Text = "FormNoLinealNewton";
            ((System.ComponentModel.ISupportInitialize)dgvNoLinealNewton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtF;
        private TextBox txtY0;
        private TextBox txtX0;
        private TextBox txtG;
        private TextBox txtTol;
        private Button btnCalcular;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblRespuesta;
        private DataGridView dgvNoLinealNewton;
        private Button btnExportar;
    }
}