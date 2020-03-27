namespace PVe
{
    partial class frmTipoPagamento
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
            this.gdvTipoPagamento = new System.Windows.Forms.DataGridView();
            this.TIPO_PAGTO_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gdvTipoPagamento)).BeginInit();
            this.SuspendLayout();
            // 
            // gdvTipoPagamento
            // 
            this.gdvTipoPagamento.AllowUserToAddRows = false;
            this.gdvTipoPagamento.AllowUserToDeleteRows = false;
            this.gdvTipoPagamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvTipoPagamento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TIPO_PAGTO_ID,
            this.TipoPagamento});
            this.gdvTipoPagamento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvTipoPagamento.Location = new System.Drawing.Point(0, 0);
            this.gdvTipoPagamento.Name = "gdvTipoPagamento";
            this.gdvTipoPagamento.ReadOnly = true;
            this.gdvTipoPagamento.Size = new System.Drawing.Size(344, 283);
            this.gdvTipoPagamento.TabIndex = 0;
            this.gdvTipoPagamento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gdvTipoPagamento_KeyUp);
            // 
            // TIPO_PAGTO_ID
            // 
            this.TIPO_PAGTO_ID.DataPropertyName = "TIPO_PAGTO_ID";
            this.TIPO_PAGTO_ID.HeaderText = "Código";
            this.TIPO_PAGTO_ID.Name = "TIPO_PAGTO_ID";
            this.TIPO_PAGTO_ID.ReadOnly = true;
            // 
            // TipoPagamento
            // 
            this.TipoPagamento.DataPropertyName = "TIPO_PAGTO";
            this.TipoPagamento.HeaderText = "Tipo de Pagamento";
            this.TipoPagamento.Name = "TipoPagamento";
            this.TipoPagamento.ReadOnly = true;
            this.TipoPagamento.Width = 200;
            // 
            // frmTipoPagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 283);
            this.Controls.Add(this.gdvTipoPagamento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmTipoPagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela de Tipo Pagamento";
            this.Load += new System.EventHandler(this.frmTipoPagamento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdvTipoPagamento)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gdvTipoPagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO_PAGTO_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPagamento;
    }
}