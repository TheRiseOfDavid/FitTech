namespace laplace_demo_int_array
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.explain_text = new System.Windows.Forms.Label();
            this.before_text = new System.Windows.Forms.Label();
            this.after_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // explain_text
            // 
            this.explain_text.AutoSize = true;
            this.explain_text.Font = new System.Drawing.Font("新細明體", 16F);
            this.explain_text.Location = new System.Drawing.Point(75, 26);
            this.explain_text.Name = "explain_text";
            this.explain_text.Size = new System.Drawing.Size(60, 22);
            this.explain_text.TabIndex = 0;
            this.explain_text.Text = "label1";
            // 
            // before_text
            // 
            this.before_text.AutoSize = true;
            this.before_text.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.before_text.Location = new System.Drawing.Point(75, 99);
            this.before_text.Name = "before_text";
            this.before_text.Size = new System.Drawing.Size(63, 19);
            this.before_text.TabIndex = 1;
            this.before_text.Text = "label2";
            // 
            // after_text
            // 
            this.after_text.AutoSize = true;
            this.after_text.Font = new System.Drawing.Font("Consolas", 12F);
            this.after_text.Location = new System.Drawing.Point(499, 99);
            this.after_text.Name = "after_text";
            this.after_text.Size = new System.Drawing.Size(63, 19);
            this.after_text.TabIndex = 2;
            this.after_text.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.after_text);
            this.Controls.Add(this.before_text);
            this.Controls.Add(this.explain_text);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label explain_text;
        private System.Windows.Forms.Label before_text;
        private System.Windows.Forms.Label after_text;
    }
}

