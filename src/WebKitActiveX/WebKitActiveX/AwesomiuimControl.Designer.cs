namespace WebKitActiveX
{
    partial class AwesomiumControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AwesomiumWebControl = new Awesomium.Windows.Forms.WebControl(this.components);
            this.SuspendLayout();
            // 
            // AwesomiumWebControl
            // 
            this.AwesomiumWebControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AwesomiumWebControl.BackColor = System.Drawing.SystemColors.Control;
            this.AwesomiumWebControl.Location = new System.Drawing.Point(0, 0);
            this.AwesomiumWebControl.Margin = new System.Windows.Forms.Padding(0);
            this.AwesomiumWebControl.MinimumSize = new System.Drawing.Size(1, 1);
            this.AwesomiumWebControl.Size = new System.Drawing.Size(200, 200);
            this.AwesomiumWebControl.Source = new System.Uri("about:blank", System.UriKind.Absolute);
            this.AwesomiumWebControl.TabIndex = 0;
            this.AwesomiumWebControl.ViewType = Awesomium.Core.WebViewType.Offscreen;
            // 
            // AwesomiumControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.AwesomiumWebControl);
            this.Name = "AwesomiumControl";
            this.Size = new System.Drawing.Size(200, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private Awesomium.Windows.Forms.WebControl AwesomiumWebControl;
    }
}
