namespace eddo.csa.exceldna.Git
{
    partial class GitPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbBranches = new ComboBox();
            lblBranches = new Label();
            btnLoadPendingChanges = new Button();
            SuspendLayout();
            // 
            // cmbBranches
            // 
            cmbBranches.Anchor =      AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            cmbBranches.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBranches.Enabled = false;
            cmbBranches.FormattingEnabled = true;
            cmbBranches.Location = new Point( 6, 40 );
            cmbBranches.Margin = new Padding( 5 );
            cmbBranches.Name = "cmbBranches";
            cmbBranches.Size = new Size( 246, 33 );
            cmbBranches.TabIndex = 1;
            // 
            // lblBranches
            // 
            lblBranches.AutoSize = true;
            lblBranches.Enabled = false;
            lblBranches.Location = new Point( 5, 5 );
            lblBranches.Margin = new Padding( 5 );
            lblBranches.Name = "lblBranches";
            lblBranches.Size = new Size( 180, 25 );
            lblBranches.TabIndex = 0;
            lblBranches.Text = "Configured Branches:";
            // 
            // btnLoadPendingChanges
            // 
            btnLoadPendingChanges.Anchor =     AnchorStyles.Top  |  AnchorStyles.Right ;
            btnLoadPendingChanges.Location = new Point( 257, 40 );
            btnLoadPendingChanges.Margin = new Padding( 0, 5, 5, 5 );
            btnLoadPendingChanges.Name = "btnLoadPendingChanges";
            btnLoadPendingChanges.Size = new Size( 81, 33 );
            btnLoadPendingChanges.TabIndex = 2;
            btnLoadPendingChanges.Text = "Load";
            btnLoadPendingChanges.UseVisualStyleBackColor = true;
            btnLoadPendingChanges.Click +=  btnLoadPendingChanges_Click ;
            // 
            // GitPanel
            // 
            AutoScaleDimensions = new SizeF( 10F, 25F );
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add( btnLoadPendingChanges );
            Controls.Add( lblBranches );
            Controls.Add( cmbBranches );
            Name = "GitPanel";
            Size = new Size( 343, 715 );
            ResumeLayout( false );
            PerformLayout();
        }

        #endregion

        private ComboBox cmbBranches;
        private Label lblBranches;
        private Button btnLoadPendingChanges;
    }
}
