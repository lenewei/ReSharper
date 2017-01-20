namespace MyReSharperPlugins
{
   partial class DependencyResult
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
         this.RtbAssembly = new System.Windows.Forms.RichTextBox();
         this.RtbDependent = new System.Windows.Forms.RichTextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // RtbAssembly
         // 
         this.RtbAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
         this.RtbAssembly.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.RtbAssembly.Location = new System.Drawing.Point(12, 32);
         this.RtbAssembly.Name = "RtbAssembly";
         this.RtbAssembly.Size = new System.Drawing.Size(566, 582);
         this.RtbAssembly.TabIndex = 2;
         this.RtbAssembly.Text = "";
         // 
         // RtbDependent
         // 
         this.RtbDependent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.RtbDependent.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.RtbDependent.Location = new System.Drawing.Point(584, 32);
         this.RtbDependent.Name = "RtbDependent";
         this.RtbDependent.Size = new System.Drawing.Size(624, 582);
         this.RtbDependent.TabIndex = 4;
         this.RtbDependent.Text = "";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(13, 14);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(143, 12);
         this.label1.TabIndex = 5;
         this.label1.Text = "Refereneces per project";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(584, 14);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(149, 12);
         this.label2.TabIndex = 6;
         this.label2.Text = "Dependents per reference";
         // 
         // DependencyResult
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoScroll = true;
         this.AutoSize = true;
         this.ClientSize = new System.Drawing.Size(1220, 655);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.RtbDependent);
         this.Controls.Add(this.RtbAssembly);
         this.Name = "DependencyResult";
         this.Text = "DependencyResult";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.RichTextBox RtbAssembly;
      private System.Windows.Forms.RichTextBox RtbDependent;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
   }
}