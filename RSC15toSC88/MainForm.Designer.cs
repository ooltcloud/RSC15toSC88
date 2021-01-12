namespace RSC15toSC88
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StartButton = new System.Windows.Forms.Button();
            this.MidiPortAComboBox = new System.Windows.Forms.ComboBox();
            this.ReloadPortlistButton = new System.Windows.Forms.Button();
            this.MidiPortBComboBox = new System.Windows.Forms.ComboBox();
            this.ComPortComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MidiPortALampDisplay = new System.Windows.Forms.Button();
            this.MidiPortBLampDisplay = new System.Windows.Forms.Button();
            this.ComPortLampDisplay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(32, 168);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(278, 34);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // MidiPortAComboBox
            // 
            this.MidiPortAComboBox.FormattingEnabled = true;
            this.MidiPortAComboBox.Location = new System.Drawing.Point(85, 30);
            this.MidiPortAComboBox.Name = "MidiPortAComboBox";
            this.MidiPortAComboBox.Size = new System.Drawing.Size(121, 20);
            this.MidiPortAComboBox.TabIndex = 1;
            // 
            // ReloadPortlistButton
            // 
            this.ReloadPortlistButton.Location = new System.Drawing.Point(256, 30);
            this.ReloadPortlistButton.Name = "ReloadPortlistButton";
            this.ReloadPortlistButton.Size = new System.Drawing.Size(77, 46);
            this.ReloadPortlistButton.TabIndex = 2;
            this.ReloadPortlistButton.Text = "Reload PortList";
            this.ReloadPortlistButton.UseVisualStyleBackColor = true;
            this.ReloadPortlistButton.Click += new System.EventHandler(this.ReloadPortlistButton_Click);
            // 
            // MidiPortBComboBox
            // 
            this.MidiPortBComboBox.FormattingEnabled = true;
            this.MidiPortBComboBox.Location = new System.Drawing.Point(85, 56);
            this.MidiPortBComboBox.Name = "MidiPortBComboBox";
            this.MidiPortBComboBox.Size = new System.Drawing.Size(121, 20);
            this.MidiPortBComboBox.TabIndex = 3;
            // 
            // ComPortComboBox
            // 
            this.ComPortComboBox.FormattingEnabled = true;
            this.ComPortComboBox.Location = new System.Drawing.Point(85, 124);
            this.ComPortComboBox.Name = "ComPortComboBox";
            this.ComPortComboBox.Size = new System.Drawing.Size(121, 20);
            this.ComPortComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "MIDI In";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port B";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Serial Out";
            // 
            // MidiPortALampDisplay
            // 
            this.MidiPortALampDisplay.BackColor = System.Drawing.Color.Gray;
            this.MidiPortALampDisplay.Enabled = false;
            this.MidiPortALampDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MidiPortALampDisplay.Location = new System.Drawing.Point(212, 30);
            this.MidiPortALampDisplay.Name = "MidiPortALampDisplay";
            this.MidiPortALampDisplay.Size = new System.Drawing.Size(20, 20);
            this.MidiPortALampDisplay.TabIndex = 6;
            this.MidiPortALampDisplay.UseVisualStyleBackColor = false;
            // 
            // MidiPortBLampDisplay
            // 
            this.MidiPortBLampDisplay.BackColor = System.Drawing.Color.Gray;
            this.MidiPortBLampDisplay.Enabled = false;
            this.MidiPortBLampDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MidiPortBLampDisplay.Location = new System.Drawing.Point(212, 56);
            this.MidiPortBLampDisplay.Name = "MidiPortBLampDisplay";
            this.MidiPortBLampDisplay.Size = new System.Drawing.Size(20, 20);
            this.MidiPortBLampDisplay.TabIndex = 6;
            this.MidiPortBLampDisplay.UseVisualStyleBackColor = false;
            // 
            // ComPortLampDisplay
            // 
            this.ComPortLampDisplay.BackColor = System.Drawing.Color.Gray;
            this.ComPortLampDisplay.Enabled = false;
            this.ComPortLampDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComPortLampDisplay.Location = new System.Drawing.Point(212, 124);
            this.ComPortLampDisplay.Name = "ComPortLampDisplay";
            this.ComPortLampDisplay.Size = new System.Drawing.Size(20, 20);
            this.ComPortLampDisplay.TabIndex = 6;
            this.ComPortLampDisplay.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 216);
            this.Controls.Add(this.ComPortLampDisplay);
            this.Controls.Add(this.MidiPortBLampDisplay);
            this.Controls.Add(this.MidiPortALampDisplay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComPortComboBox);
            this.Controls.Add(this.MidiPortBComboBox);
            this.Controls.Add(this.ReloadPortlistButton);
            this.Controls.Add(this.MidiPortAComboBox);
            this.Controls.Add(this.StartButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "RSC15→SC88 Bridge (version 2101)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ComboBox MidiPortAComboBox;
        private System.Windows.Forms.Button ReloadPortlistButton;
        private System.Windows.Forms.ComboBox MidiPortBComboBox;
        private System.Windows.Forms.ComboBox ComPortComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button MidiPortALampDisplay;
        private System.Windows.Forms.Button MidiPortBLampDisplay;
        private System.Windows.Forms.Button ComPortLampDisplay;
    }
}

