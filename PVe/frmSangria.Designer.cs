namespace PVe
{
    partial class frmSangria
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
            this.txtValorSangria = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gpSangria = new System.Windows.Forms.GroupBox();
            this.txtMotivoSangria = new System.Windows.Forms.TextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.lblQtde = new System.Windows.Forms.Label();
            this.cbxSangria = new System.Windows.Forms.CheckBox();
            this.gpSangria.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtValorSangria
            // 
            this.txtValorSangria.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorSangria.Location = new System.Drawing.Point(12, 29);
            this.txtValorSangria.Name = "txtValorSangria";
            this.txtValorSangria.Size = new System.Drawing.Size(277, 24);
            this.txtValorSangria.TabIndex = 0;
            this.txtValorSangria.Text = "0,00";
            this.txtValorSangria.TextChanged += new System.EventHandler(this.txtValorSangria_TextChanged);
            this.txtValorSangria.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtValorSangria_KeyUp);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(208, 203);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Valor Sangria";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // gpSangria
            // 
            this.gpSangria.Controls.Add(this.txtMotivoSangria);
            this.gpSangria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpSangria.Location = new System.Drawing.Point(12, 78);
            this.gpSangria.Name = "gpSangria";
            this.gpSangria.Size = new System.Drawing.Size(277, 119);
            this.gpSangria.TabIndex = 66;
            this.gpSangria.TabStop = false;
            this.gpSangria.Text = "Motivo Sangria";
            this.gpSangria.Enter += new System.EventHandler(this.gpSangria_Enter);
            // 
            // txtMotivoSangria
            // 
            this.txtMotivoSangria.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivoSangria.Location = new System.Drawing.Point(7, 26);
            this.txtMotivoSangria.Multiline = true;
            this.txtMotivoSangria.Name = "txtMotivoSangria";
            this.txtMotivoSangria.Size = new System.Drawing.Size(264, 87);
            this.txtMotivoSangria.TabIndex = 0;
            this.txtMotivoSangria.TextChanged += new System.EventHandler(this.txtMotivoSangria_TextChanged);
            this.txtMotivoSangria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMotivoSangria_KeyPress);
            this.txtMotivoSangria.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMotivoSangria_KeyUp);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(127, 203);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 3;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // lblQtde
            // 
            this.lblQtde.AutoSize = true;
            this.lblQtde.Location = new System.Drawing.Point(16, 213);
            this.lblQtde.Name = "lblQtde";
            this.lblQtde.Size = new System.Drawing.Size(19, 13);
            this.lblQtde.TabIndex = 69;
            this.lblQtde.Text = "00";
            this.lblQtde.Click += new System.EventHandler(this.lblQtde_Click);
            // 
            // cbxSangria
            // 
            this.cbxSangria.AutoSize = true;
            this.cbxSangria.Checked = true;
            this.cbxSangria.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSangria.Location = new System.Drawing.Point(222, 59);
            this.cbxSangria.Name = "cbxSangria";
            this.cbxSangria.Size = new System.Drawing.Size(62, 17);
            this.cbxSangria.TabIndex = 1;
            this.cbxSangria.Text = "Sangria";
            this.cbxSangria.UseVisualStyleBackColor = true;
            this.cbxSangria.CheckedChanged += new System.EventHandler(this.cbxSangria_CheckedChanged);
            // 
            // frmSangria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 233);
            this.Controls.Add(this.cbxSangria);
            this.Controls.Add(this.lblQtde);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.gpSangria);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtValorSangria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSangria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sangria do Caixa";
            this.Load += new System.EventHandler(this.frmSangria_Load);
            this.gpSangria.ResumeLayout(false);
            this.gpSangria.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtValorSangria;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpSangria;
        private System.Windows.Forms.TextBox txtMotivoSangria;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lblQtde;
        private System.Windows.Forms.CheckBox cbxSangria;
    }
}