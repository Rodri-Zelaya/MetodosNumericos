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
            txtTol = new TextBox();
            btnCalcular = new Button();
            label1 = new Label();
            lblRespuesta = new Label();
            dgvNoLinealNewton = new DataGridView();
            btnExportar = new Button();
            txtFunciones = new TextBox();
            label2 = new Label();
            txtValoresIniciales = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvNoLinealNewton).BeginInit();
            SuspendLayout();
            // 
            // txtTol
            // 
            txtTol.Location = new Point(879, 27);
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
            label1.Location = new Point(796, 30);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 6;
            label1.Text = "Tolerancia";
            // 
            // lblRespuesta
            // 
            lblRespuesta.AutoSize = true;
            lblRespuesta.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRespuesta.Location = new Point(796, 111);
            lblRespuesta.Name = "lblRespuesta";
            lblRespuesta.Size = new Size(111, 25);
            lblRespuesta.TabIndex = 11;
            lblRespuesta.Text = "RESPUESTA";
            // 
            // dgvNoLinealNewton
            // 
            dgvNoLinealNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNoLinealNewton.Location = new Point(9, 151);
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
            // txtFunciones
            // 
            txtFunciones.Location = new Point(89, 12);
            txtFunciones.Multiline = true;
            txtFunciones.Name = "txtFunciones";
            txtFunciones.Size = new Size(287, 133);
            txtFunciones.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 70);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 15;
            label2.Text = "Funciones";
            // 
            // txtValoresIniciales
            // 
            txtValoresIniciales.Location = new Point(502, 12);
            txtValoresIniciales.Multiline = true;
            txtValoresIniciales.Name = "txtValoresIniciales";
            txtValoresIniciales.Size = new Size(287, 133);
            txtValoresIniciales.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(382, 70);
            label3.Name = "label3";
            label3.Size = new Size(114, 20);
            label3.TabIndex = 17;
            label3.Text = "Valores iniciales";
            // 
            // FormNoLinealNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 474);
            Controls.Add(label3);
            Controls.Add(txtValoresIniciales);
            Controls.Add(label2);
            Controls.Add(txtFunciones);
            Controls.Add(btnExportar);
            Controls.Add(dgvNoLinealNewton);
            Controls.Add(lblRespuesta);
            Controls.Add(label1);
            Controls.Add(btnCalcular);
            Controls.Add(txtTol);
            Name = "FormNoLinealNewton";
            Text = "FormNoLinealNewton";
            ((System.ComponentModel.ISupportInitialize)dgvNoLinealNewton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtTol;
        private Button btnCalcular;
        private Label label1;
        private Label lblRespuesta;
        private DataGridView dgvNoLinealNewton;
        private Button btnExportar;
        private TextBox txtFunciones;
        private Label label2;
        private TextBox txtValoresIniciales;
        private Label label3;
    }
}