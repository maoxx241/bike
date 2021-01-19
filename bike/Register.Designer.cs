
namespace bike
{
    partial class Register
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.genderbutton = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.passwordbox1 = new System.Windows.Forms.TextBox();
            this.passwordbox2 = new System.Windows.Forms.TextBox();
            this.lastnamebox = new System.Windows.Forms.TextBox();
            this.firstnamebox = new System.Windows.Forms.TextBox();
            this.agebox = new System.Windows.Forms.TextBox();
            this.adminbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.usernamebox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirm Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Last name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "First Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Gender";
            // 
            // genderbutton
            // 
            this.genderbutton.AutoSize = true;
            this.genderbutton.Location = new System.Drawing.Point(140, 240);
            this.genderbutton.Name = "genderbutton";
            this.genderbutton.Size = new System.Drawing.Size(48, 17);
            this.genderbutton.TabIndex = 5;
            this.genderbutton.TabStop = true;
            this.genderbutton.Text = "Male";
            this.genderbutton.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(194, 240);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 17);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Female";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Age";
            // 
            // passwordbox1
            // 
            this.passwordbox1.Location = new System.Drawing.Point(140, 80);
            this.passwordbox1.Name = "passwordbox1";
            this.passwordbox1.Size = new System.Drawing.Size(100, 20);
            this.passwordbox1.TabIndex = 8;
            this.passwordbox1.UseSystemPasswordChar = true;
            // 
            // passwordbox2
            // 
            this.passwordbox2.Location = new System.Drawing.Point(140, 120);
            this.passwordbox2.Name = "passwordbox2";
            this.passwordbox2.Size = new System.Drawing.Size(100, 20);
            this.passwordbox2.TabIndex = 9;
            this.passwordbox2.UseSystemPasswordChar = true;
            // 
            // lastnamebox
            // 
            this.lastnamebox.Location = new System.Drawing.Point(140, 160);
            this.lastnamebox.Name = "lastnamebox";
            this.lastnamebox.Size = new System.Drawing.Size(100, 20);
            this.lastnamebox.TabIndex = 10;
            // 
            // firstnamebox
            // 
            this.firstnamebox.Location = new System.Drawing.Point(140, 200);
            this.firstnamebox.Name = "firstnamebox";
            this.firstnamebox.Size = new System.Drawing.Size(100, 20);
            this.firstnamebox.TabIndex = 11;
            // 
            // agebox
            // 
            this.agebox.Location = new System.Drawing.Point(140, 280);
            this.agebox.Name = "agebox";
            this.agebox.Size = new System.Drawing.Size(100, 20);
            this.agebox.TabIndex = 12;
            this.agebox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.agebox_KeyPress);
            // 
            // adminbox
            // 
            this.adminbox.Location = new System.Drawing.Point(140, 320);
            this.adminbox.Name = "adminbox";
            this.adminbox.Size = new System.Drawing.Size(100, 20);
            this.adminbox.TabIndex = 13;
            this.adminbox.UseSystemPasswordChar = true;
            this.adminbox.TextChanged += new System.EventHandler(this.adminbox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Admin code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Password";
            // 
            // usernamebox
            // 
            this.usernamebox.Location = new System.Drawing.Point(140, 40);
            this.usernamebox.Name = "usernamebox";
            this.usernamebox.Size = new System.Drawing.Size(100, 20);
            this.usernamebox.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Register";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 446);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.usernamebox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.adminbox);
            this.Controls.Add(this.agebox);
            this.Controls.Add(this.firstnamebox);
            this.Controls.Add(this.lastnamebox);
            this.Controls.Add(this.passwordbox2);
            this.Controls.Add(this.passwordbox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.genderbutton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Register";
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton genderbutton;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox passwordbox1;
        private System.Windows.Forms.TextBox passwordbox2;
        private System.Windows.Forms.TextBox lastnamebox;
        private System.Windows.Forms.TextBox firstnamebox;
        private System.Windows.Forms.TextBox agebox;
        private System.Windows.Forms.TextBox adminbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox usernamebox;
        private System.Windows.Forms.Button button1;
    }
}