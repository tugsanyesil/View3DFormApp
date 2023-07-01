namespace View3DFormApp
{
    partial class View3DForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.View3DPictureBox = new System.Windows.Forms.PictureBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.View3DPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // View3DPictureBox
            // 
            this.View3DPictureBox.Location = new System.Drawing.Point(12, 12);
            this.View3DPictureBox.Name = "View3DPictureBox";
            this.View3DPictureBox.Size = new System.Drawing.Size(300, 300);
            this.View3DPictureBox.TabIndex = 0;
            this.View3DPictureBox.TabStop = false;
            this.View3DPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.View3DPictureBox_MouseDown);
            this.View3DPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.View3DPictureBox_MouseUp);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(318, 12);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(25, 13);
            this.InfoLabel.TabIndex = 1;
            this.InfoLabel.Text = "Info";
            // 
            // View3DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 322);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.View3DPictureBox);
            this.Name = "View3DForm";
            this.Text = "View3DFormApp";
            this.Load += new System.EventHandler(this.View3DForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.View3DPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox View3DPictureBox;
        private System.Windows.Forms.Label InfoLabel;
    }
}

