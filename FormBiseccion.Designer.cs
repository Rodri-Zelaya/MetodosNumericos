namespace Métodos_Numéricos
{
    partial class FormBiseccion
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
            txtFuncionBiseccion = new TextBox();
            txtA = new TextBox();
            txtB = new TextBox();
            txtTolerancia = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dgvBiseccion = new DataGridView();
            btnCalcular = new Button();
            lblRaiz = new Label();
            btnExportar = new Button();
            btnGraficar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBiseccion).BeginInit();
            SuspendLayout();
            // 
            // txtFuncionBiseccion
            // 
            txtFuncionBiseccion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFuncionBiseccion.Location = new Point(118, 96);
            txtFuncionBiseccion.Name = "txtFuncionBiseccion";
            txtFuncionBiseccion.Size = new Size(474, 34);
            txtFuncionBiseccion.TabIndex = 0;
            // 
            // txtA
            // 
            txtA.Anchor = AnchorStyles.Top;
            txtA.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtA.Location = new Point(705, 56);
            txtA.Name = "txtA";
            txtA.Size = new Size(125, 34);
            txtA.TabIndex = 1;
            // 
            // txtB
            // 
            txtB.Anchor = AnchorStyles.Top;
            txtB.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtB.Location = new Point(705, 127);
            txtB.Name = "txtB";
            txtB.Size = new Size(125, 34);
            txtB.TabIndex = 2;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolerancia.Location = new Point(1003, 53);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(161, 34);
            txtTolerancia.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 97);
            label1.Name = "label1";
            label1.Size = new Size(109, 31);
            label1.TabIndex = 5;
            label1.Text = "Ecuación";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(608, 59);
            label2.Name = "label2";
            label2.Size = new Size(91, 31);
            label2.TabIndex = 6;
            label2.Text = "Valor A";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(608, 130);
            label3.Name = "label3";
            label3.Size = new Size(90, 31);
            label3.TabIndex = 7;
            label3.Text = "Valor B";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(874, 56);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 8;
            label4.Text = "Tolerancia";
            // 
            // dgvBiseccion
            // 
            dgvBiseccion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBiseccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBiseccion.BackgroundColor = Color.LightCoral;
            dgvBiseccion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBiseccion.Location = new Point(12, 203);
            dgvBiseccion.Name = "dgvBiseccion";
            dgvBiseccion.RowHeadersWidth = 51;
            dgvBiseccion.Size = new Size(1780, 467);
            dgvBiseccion.TabIndex = 10;
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.BackColor = SystemColors.ControlLightLight;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.ForeColor = Color.Black;
            btnCalcular.Location = new Point(1224, 30);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(175, 78);
            btnCalcular.TabIndex = 11;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = false;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(898, 130);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(71, 31);
            lblRaiz.TabIndex = 12;
            lblRaiz.Text = "Raiz: ";
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1405, 30);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(200, 78);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnGraficar
            // 
            btnGraficar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGraficar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGraficar.Location = new Point(1611, 30);
            btnGraficar.Name = "btnGraficar";
            btnGraficar.Size = new Size(171, 78);
            btnGraficar.TabIndex = 14;
            btnGraficar.Text = "Graficar";
            btnGraficar.UseVisualStyleBackColor = true;
            btnGraficar.Click += btnGraficar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(1611, 114);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(171, 76);
            btnLimpiar.TabIndex = 15;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormBiseccion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1804, 694);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGraficar);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
            Controls.Add(btnCalcular);
            Controls.Add(dgvBiseccion);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTolerancia);
            Controls.Add(txtB);
            Controls.Add(txtA);
            Controls.Add(txtFuncionBiseccion);
            Name = "FormBiseccion";
            Text = "FormBiseccion";
            ((System.ComponentModel.ISupportInitialize)dgvBiseccion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFuncionBiseccion;
        private TextBox txtA;
        private TextBox txtB;
        private TextBox txtTolerancia;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dgvBiseccion;
        private Button btnCalcular;
        private Label lblRaiz;
        private Button btnExportar;
        private Button btnGraficar;
        private Button btnLimpiar;
    }
}