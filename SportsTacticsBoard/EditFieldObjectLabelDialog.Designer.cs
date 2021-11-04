namespace SportsTacticsBoard
{
  partial class EditFieldObjectLabelDialog
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
      if (disposing && (components != null)) {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFieldObjectLabelDialog));
      this.fieldObjectLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.customLabelLabel = new System.Windows.Forms.Label();
      this.customLabelTextBox = new System.Windows.Forms.TextBox();
      this.fieldObjectNameTextBox = new System.Windows.Forms.TextBox();
      this.revertToDefaultButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // fieldObjectLabel
      // 
      resources.ApplyResources(this.fieldObjectLabel, "fieldObjectLabel");
      this.fieldObjectLabel.Name = "fieldObjectLabel";
      // 
      // okButton
      // 
      resources.ApplyResources(this.okButton, "okButton");
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      resources.ApplyResources(this.cancelButton, "cancelButton");
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      // 
      // customLabelLabel
      // 
      resources.ApplyResources(this.customLabelLabel, "customLabelLabel");
      this.customLabelLabel.Name = "customLabelLabel";
      // 
      // customLabelTextBox
      // 
      resources.ApplyResources(this.customLabelTextBox, "customLabelTextBox");
      this.customLabelTextBox.Name = "customLabelTextBox";
      // 
      // fieldObjectNameTextBox
      // 
      resources.ApplyResources(this.fieldObjectNameTextBox, "fieldObjectNameTextBox");
      this.fieldObjectNameTextBox.Name = "fieldObjectNameTextBox";
      this.fieldObjectNameTextBox.ReadOnly = true;
      this.fieldObjectNameTextBox.TabStop = false;
      // 
      // revertToDefaultButton
      // 
      resources.ApplyResources(this.revertToDefaultButton, "revertToDefaultButton");
      this.revertToDefaultButton.DialogResult = System.Windows.Forms.DialogResult.No;
      this.revertToDefaultButton.Name = "revertToDefaultButton";
      this.revertToDefaultButton.UseVisualStyleBackColor = true;
      // 
      // EditFieldObjectLabelDialog
      // 
      this.AcceptButton = this.okButton;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.Controls.Add(this.revertToDefaultButton);
      this.Controls.Add(this.fieldObjectNameTextBox);
      this.Controls.Add(this.customLabelTextBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.customLabelLabel);
      this.Controls.Add(this.fieldObjectLabel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditFieldObjectLabelDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label customLabelLabel;
    private System.Windows.Forms.TextBox customLabelTextBox;
    private System.Windows.Forms.TextBox fieldObjectNameTextBox;
    private System.Windows.Forms.Button revertToDefaultButton;
    private System.Windows.Forms.Label fieldObjectLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
  }
}