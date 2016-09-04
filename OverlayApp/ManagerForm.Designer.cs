namespace OverlayApp
{
    partial class ManagerForm
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
            this.screenSelectComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fadeInControl = new System.Windows.Forms.NumericUpDown();
            this.fadeOutControl = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.spotlightRadiusControl = new System.Windows.Forms.NumericUpDown();
            this.featheringRadiusControl = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.transparencyControl = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.colorPickerDialog = new System.Windows.Forms.ColorDialog();
            this.clearSpotsControl = new OverlayApp.HotkeyControl();
            this.toggleFollowControl = new OverlayApp.HotkeyControl();
            this.addSpotControl = new OverlayApp.HotkeyControl();
            this.autoHideDelayControl = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fadeInControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fadeOutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spotlightRadiusControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.featheringRadiusControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoHideDelayControl)).BeginInit();
            this.SuspendLayout();
            // 
            // screenSelectComboBox
            // 
            this.screenSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.screenSelectComboBox.FormattingEnabled = true;
            this.screenSelectComboBox.Location = new System.Drawing.Point(160, 12);
            this.screenSelectComboBox.Name = "screenSelectComboBox";
            this.screenSelectComboBox.Size = new System.Drawing.Size(113, 24);
            this.screenSelectComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Display";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Add spotlight";
            // 
            // refreshButton
            // 
            this.refreshButton.BackgroundImage = global::OverlayApp.Properties.Resources.icon_ios7_refresh_empty_128;
            this.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.refreshButton.Location = new System.Drawing.Point(288, 11);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(38, 39);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(343, 12);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(53, 65);
            this.applyButton.TabIndex = 5;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Toggle follow";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Clear";
            // 
            // fadeInControl
            // 
            this.fadeInControl.Location = new System.Drawing.Point(158, 165);
            this.fadeInControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fadeInControl.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.fadeInControl.Name = "fadeInControl";
            this.fadeInControl.Size = new System.Drawing.Size(169, 22);
            this.fadeInControl.TabIndex = 10;
            this.fadeInControl.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // fadeOutControl
            // 
            this.fadeOutControl.Location = new System.Drawing.Point(158, 205);
            this.fadeOutControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fadeOutControl.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.fadeOutControl.Name = "fadeOutControl";
            this.fadeOutControl.Size = new System.Drawing.Size(169, 22);
            this.fadeOutControl.TabIndex = 11;
            this.fadeOutControl.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Fade in time (ms)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Fade out time (ms)";
            // 
            // spotlightRadiusControl
            // 
            this.spotlightRadiusControl.Location = new System.Drawing.Point(158, 245);
            this.spotlightRadiusControl.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spotlightRadiusControl.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spotlightRadiusControl.Name = "spotlightRadiusControl";
            this.spotlightRadiusControl.Size = new System.Drawing.Size(169, 22);
            this.spotlightRadiusControl.TabIndex = 14;
            this.spotlightRadiusControl.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // featheringRadiusControl
            // 
            this.featheringRadiusControl.Location = new System.Drawing.Point(158, 291);
            this.featheringRadiusControl.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.featheringRadiusControl.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.featheringRadiusControl.Name = "featheringRadiusControl";
            this.featheringRadiusControl.Size = new System.Drawing.Size(169, 22);
            this.featheringRadiusControl.TabIndex = 15;
            this.featheringRadiusControl.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Spotlight radius";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "Feathering radius";
            // 
            // transparencyControl
            // 
            this.transparencyControl.Location = new System.Drawing.Point(158, 363);
            this.transparencyControl.Maximum = 90;
            this.transparencyControl.Minimum = 10;
            this.transparencyControl.Name = "transparencyControl";
            this.transparencyControl.Size = new System.Drawing.Size(166, 56);
            this.transparencyControl.TabIndex = 18;
            this.transparencyControl.TickFrequency = 10;
            this.transparencyControl.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.transparencyControl.Value = 50;
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 375);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Overlay transparency";
            // 
            // colorPickerDialog
            // 
            this.colorPickerDialog.SolidColorOnly = true;
            // 
            // clearSpotsControl
            // 
            this.clearSpotsControl.Hotkey = System.Windows.Forms.Keys.None;
            this.clearSpotsControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.clearSpotsControl.Location = new System.Drawing.Point(158, 129);
            this.clearSpotsControl.Name = "clearSpotsControl";
            this.clearSpotsControl.Size = new System.Drawing.Size(169, 22);
            this.clearSpotsControl.TabIndex = 7;
            this.clearSpotsControl.Text = "None";
            this.clearSpotsControl.TextChanged += new System.EventHandler(this.hotkeyControl1_TextChanged);
            // 
            // toggleFollowControl
            // 
            this.toggleFollowControl.Hotkey = System.Windows.Forms.Keys.None;
            this.toggleFollowControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.toggleFollowControl.Location = new System.Drawing.Point(158, 91);
            this.toggleFollowControl.Name = "toggleFollowControl";
            this.toggleFollowControl.Size = new System.Drawing.Size(169, 22);
            this.toggleFollowControl.TabIndex = 6;
            this.toggleFollowControl.Text = "None";
            this.toggleFollowControl.TextChanged += new System.EventHandler(this.hotkeyControl1_TextChanged);
            // 
            // addSpotControl
            // 
            this.addSpotControl.Hotkey = System.Windows.Forms.Keys.F12;
            this.addSpotControl.HotkeyModifiers = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.F12)));
            this.addSpotControl.Location = new System.Drawing.Point(158, 56);
            this.addSpotControl.Name = "addSpotControl";
            this.addSpotControl.Size = new System.Drawing.Size(169, 22);
            this.addSpotControl.TabIndex = 2;
            this.addSpotControl.Text = "None";
            this.addSpotControl.TextChanged += new System.EventHandler(this.hotkeyControl1_TextChanged);
            // 
            // autoHideDelayControl
            // 
            this.autoHideDelayControl.Location = new System.Drawing.Point(160, 329);
            this.autoHideDelayControl.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.autoHideDelayControl.Name = "autoHideDelayControl";
            this.autoHideDelayControl.Size = new System.Drawing.Size(167, 22);
            this.autoHideDelayControl.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 334);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "Autohude delay (0 off)";
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(525, 554);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.autoHideDelayControl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.transparencyControl);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.featheringRadiusControl);
            this.Controls.Add(this.spotlightRadiusControl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fadeOutControl);
            this.Controls.Add(this.fadeInControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clearSpotsControl);
            this.Controls.Add(this.toggleFollowControl);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addSpotControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screenSelectComboBox);
            this.Name = "ManagerForm";
            this.Text = "Spotlight Overlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManagerForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.fadeInControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fadeOutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spotlightRadiusControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.featheringRadiusControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoHideDelayControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox screenSelectComboBox;
        private System.Windows.Forms.Label label1;
        private HotkeyControl addSpotControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button applyButton;
        private HotkeyControl toggleFollowControl;
        private HotkeyControl clearSpotsControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown fadeInControl;
        private System.Windows.Forms.NumericUpDown fadeOutControl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown spotlightRadiusControl;
        private System.Windows.Forms.NumericUpDown featheringRadiusControl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar transparencyControl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColorDialog colorPickerDialog;
        private System.Windows.Forms.NumericUpDown autoHideDelayControl;
        private System.Windows.Forms.Label label10;
    }
}