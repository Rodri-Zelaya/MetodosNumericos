namespace Métodos_Numéricos
{
    partial class FormReglaFalsa
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
            txtFuncionReglaFalsa = new TextBox();
            txtA = new TextBox();
            txtB = new TextBox();
            txtTolerancia = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dgvReglaFalsa = new DataGridView();
            btnCalcular = new Button();
            lblRaiz = new Label();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReglaFalsa).BeginInit();
            SuspendLayout();
            // 
            // txtFuncionReglaFalsa
            // 
            txtFuncionReglaFalsa.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFuncionReglaFalsa.Location = new Point(124, 40);
            txtFuncionReglaFalsa.Name = "txtFuncionReglaFalsa";
            txtFuncionReglaFalsa.Size = new Size(424, 34);
            txtFuncionReglaFalsa.TabIndex = 0;
            // 
            // txtA
            // 
            txtA.Anchor = AnchorStyles.Top;
            txtA.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtA.Location = new Point(533, 5);
            txtA.Name = "txtA";
            txtA.Size = new Size(125, 34);
            txtA.TabIndex = 1;
            // 
            // txtB
            // 
            txtB.Anchor = AnchorStyles.Top;
            txtB.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtB.Location = new Point(533, 65);
            txtB.Name = "txtB";
            txtB.Size = new Size(125, 34);
            txtB.TabIndex = 2;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolerancia.Location = new Point(833, 6);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(197, 34);
            txtTolerancia.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 43);
            label1.Name = "label1";
            label1.Size = new Size(100, 31);
            label1.TabIndex = 5;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(425, 8);
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
            label3.Location = new Point(426, 68);
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
            label4.Location = new Point(704, 9);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 8;
            label4.Text = "Tolerancia";
            // 
            // dgvReglaFalsa
            // 
            dgvReglaFalsa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReglaFalsa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReglaFalsa.BackgroundColor = SystemColors.ActiveCaption;
            dgvReglaFalsa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReglaFalsa.Location = new Point(16, 123);
            dgvReglaFalsa.Name = "dgvReglaFalsa";
            dgvReglaFalsa.RowHeadersWidth = 51;
            dgvReglaFalsa.Size = new Size(1318, 315);
            dgvReglaFalsa.TabIndex = 10;
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(969, 14);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(168, 60);
            btnCalcular.TabIndex = 11;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(715, 68);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(65, 31);
            lblRaiz.TabIndex = 12;
            lblRaiz.Text = "Raiz:";
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1143, 14);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(191, 60);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormReglaFalsa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1346, 468);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
            Controls.Add(btnCalcular);
            Controls.Add(dgvReglaFalsa);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTolerancia);
            Controls.Add(txtB);
            Controls.Add(txtA);
            Controls.Add(txtFuncionReglaFalsa);
            Name = "FormReglaFalsa";
            Text = "FormReglaFalsa";
            Load += FormReglaFalsa_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReglaFalsa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFuncionReglaFalsa;
        private TextBox txtA;
        private TextBox txtB;
        private TextBox txtTolerancia;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dgvReglaFalsa;
        private Button btnCalcular;
        private Label lblRaiz;
        private Button btnExportar;
    }
}