namespace PVe
{
    partial class frmCpf_Cnpj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCpf_Cnpj));
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtCnpj = new System.Windows.Forms.MaskedTextBox();
            this.btnCpfCliente = new System.Windows.Forms.Button();
            this.rbCpf = new System.Windows.Forms.RadioButton();
            this.RBCnpj = new System.Windows.Forms.RadioButton();
            this.btnSalvaCpf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.Location = new System.Drawing.Point(21, 33);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(44, 20);
            this.lblDescricao.TabIndex = 1;
            this.lblDescricao.Text = "CPF:";
            // 
            // txtCnpj
            // 
            this.txtCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCnpj.Location = new System.Drawing.Point(20, 56);
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(182, 27);
            this.txtCnpj.TabIndex = 3;
            this.txtCnpj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCpf_KeyPress);
            this.txtCnpj.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCpf_KeyUp);
            // 
            // btnCpfCliente
            // 
            this.btnCpfCliente.Location = new System.Drawing.Point(18, 89);
            this.btnCpfCliente.Name = "btnCpfCliente";
            this.btnCpfCliente.Size = new System.Drawing.Size(75, 23);
            this.btnCpfCliente.TabIndex = 5;
            this.btnCpfCliente.Text = "&Cadastrar";
            this.btnCpfCliente.UseVisualStyleBackColor = true;
            this.btnCpfCliente.Click += new System.EventHandler(this.btnCpfCliente_Click);
            // 
            // rbCpf
            // 
            this.rbCpf.AutoSize = true;
            this.rbCpf.Checked = true;
            this.rbCpf.Location = new System.Drawing.Point(25, 12);
            this.rbCpf.Name = "rbCpf";
            this.rbCpf.Size = new System.Drawing.Size(45, 17);
            this.rbCpf.TabIndex = 6;
            this.rbCpf.TabStop = true;
            this.rbCpf.Text = "CPF";
            this.rbCpf.UseVisualStyleBackColor = true;
            this.rbCpf.CheckedChanged += new System.EventHandler(this.rbCpf_CheckedChanged);
            // 
            // RBCnpj
            // 
            this.RBCnpj.AutoSize = true;
            this.RBCnpj.Location = new System.Drawing.Point(150, 12);
            this.RBCnpj.Name = "RBCnpj";
            this.RBCnpj.Size = new System.Drawing.Size(52, 17);
            this.RBCnpj.TabIndex = 7;
            this.RBCnpj.Text = "CNPJ";
            this.RBCnpj.UseVisualStyleBackColor = true;
            this.RBCnpj.CheckedChanged += new System.EventHandler(this.RBCnpj_CheckedChanged);
            // 
            // btnSalvaCpf
            // 
            this.btnSalvaCpf.Location = new System.Drawing.Point(127, 89);
            this.btnSalvaCpf.Name = "btnSalvaCpf";
            this.btnSalvaCpf.Size = new System.Drawing.Size(75, 23);
            this.btnSalvaCpf.TabIndex = 4;
            this.btnSalvaCpf.Text = "Cancelar";
            this.btnSalvaCpf.UseVisualStyleBackColor = true;
            this.btnSalvaCpf.Click += new System.EventHandler(this.btnSalvaCpf_Click);
            // 
            // frmCpf_Cnpj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 120);
            this.Controls.Add(this.RBCnpj);
            this.Controls.Add(this.rbCpf);
            this.Controls.Add(this.btnCpfCliente);
            this.Controls.Add(this.btnSalvaCpf);
            this.Controls.Add(this.txtCnpj);
            this.Controls.Add(this.lblDescricao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCpf_Cnpj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmCpf_Cnpj_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.MaskedTextBox txtCnpj;
        private System.Windows.Forms.Button btnCpfCliente;
        private System.Windows.Forms.RadioButton rbCpf;
        private System.Windows.Forms.RadioButton RBCnpj;
        private System.Windows.Forms.Button btnSalvaCpf;
    }
}