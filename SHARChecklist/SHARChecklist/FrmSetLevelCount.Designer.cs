
namespace SHARChecklist
{
    partial class FrmSetLevelCount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetLevelCount));
            this.LblInfo = new System.Windows.Forms.Label();
            this.NUDLevel = new System.Windows.Forms.NumericUpDown();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // LblInfo
            // 
            this.LblInfo.Location = new System.Drawing.Point(13, 13);
            this.LblInfo.Name = "LblInfo";
            this.LblInfo.Size = new System.Drawing.Size(182, 29);
            this.LblInfo.TabIndex = 0;
            this.LblInfo.Text = "Set the total level count. To be used for mods that don\'t have 7 levels.";
            // 
            // NUDLevel
            // 
            this.NUDLevel.Location = new System.Drawing.Point(201, 17);
            this.NUDLevel.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.NUDLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDLevel.Name = "NUDLevel";
            this.NUDLevel.Size = new System.Drawing.Size(45, 20);
            this.NUDLevel.TabIndex = 1;
            this.NUDLevel.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(90, 45);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnOK
            // 
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(171, 45);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 3;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // FrmSetLevelCount
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(258, 79);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.NUDLevel);
            this.Controls.Add(this.LblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetLevelCount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Level Count";
            this.Load += new System.EventHandler(this.FrmSetLevelCount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUDLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblInfo;
        private System.Windows.Forms.NumericUpDown NUDLevel;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
    }
}