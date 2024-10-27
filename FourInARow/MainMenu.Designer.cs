namespace FourInARow
{
	partial class MainMenu
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
			this.pvp = new System.Windows.Forms.Button();
			this.pve = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pvp
			// 
			this.pvp.Location = new System.Drawing.Point(233, 32);
			this.pvp.Name = "pvp";
			this.pvp.Size = new System.Drawing.Size(183, 54);
			this.pvp.TabIndex = 0;
			this.pvp.Text = "Player vs Player";
			this.pvp.UseVisualStyleBackColor = true;
			this.pvp.Click += new System.EventHandler(this.pvp_Click);
			// 
			// pve
			// 
			this.pve.Location = new System.Drawing.Point(233, 111);
			this.pve.Name = "pve";
			this.pve.Size = new System.Drawing.Size(183, 53);
			this.pve.TabIndex = 1;
			this.pve.Text = "Player vs AI";
			this.pve.UseVisualStyleBackColor = true;
			this.pve.Click += new System.EventHandler(this.pve_Click);
			// 
			// MainMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(655, 385);
			this.Controls.Add(this.pve);
			this.Controls.Add(this.pvp);
			this.Name = "MainMenu";
			this.Text = "MainMenu";
			this.Load += new System.EventHandler(this.MainMenu_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button pvp;
		private System.Windows.Forms.Button pve;
	}
}