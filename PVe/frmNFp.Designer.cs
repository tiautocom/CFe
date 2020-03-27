namespace PVe
{
    partial class frmNFp
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
            this.mtbDtIni = new System.Windows.Forms.MaskedTextBox();
            this.mtbDtFim = new System.Windows.Forms.MaskedTextBox();
            this.lblIni = new System.Windows.Forms.Label();
            this.lblFim = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mtbDtIni
            // 
            this.mtbDtIni.Location = new System.Drawing.Point(26, 39);
            this.mtbDtIni.Mask = "00/00/0000";
            this.mtbDtIni.Name = "mtbDtIni";
            this.mtbDtIni.Size = new System.Drawing.Size(109, 29);
            this.mtbDtIni.TabIndex = 2;
            this.mtbDtIni.ValidatingType = typeof(System.DateTime);
            // 
            // mtbDtFim
            // 
            this.mtbDtFim.Location = new System.Drawing.Point(155, 39);
            this.mtbDtFim.Mask = "00/00/0000";
            this.mtbDtFim.Name = "mtbDtFim";
            this.mtbDtFim.Size = new System.Drawing.Size(109, 29);
            this.mtbDtFim.TabIndex = 3;
            this.mtbDtFim.ValidatingType = typeof(System.DateTime);
            // 
            // lblIni
            // 
            this.lblIni.AutoSize = true;
            this.lblIni.Location = new System.Drawing.Point(24, 9);
            this.lblIni.Name = "lblIni";
            this.lblIni.Size = new System.Drawing.Size(54, 24);
            this.lblIni.TabIndex = 4;
            this.lblIni.Text = "Inicio";
            // 
            // lblFim
            // 
            this.lblFim.AutoSize = true;
            this.lblFim.Location = new System.Drawing.Point(151, 9);
            this.lblFim.Name = "lblFim";
            this.lblFim.Size = new System.Drawing.Size(42, 24);
            this.lblFim.TabIndex = 5;
            this.lblFim.Text = "Fim";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "GERAR";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmNFp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 141);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblFim);
            this.Controls.Add(this.lblIni);
            this.Controls.Add(this.mtbDtFim);
            this.Controls.Add(this.mtbDtIni);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmNFp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNFp";
            this.Load += new System.EventHandler(this.frmNFp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtbDtIni;
        private System.Windows.Forms.MaskedTextBox mtbDtFim;
        private System.Windows.Forms.Label lblIni;
        private System.Windows.Forms.Label lblFim;
        private System.Windows.Forms.Button button1;

    }
}