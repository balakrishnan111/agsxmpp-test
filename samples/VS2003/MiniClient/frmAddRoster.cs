using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using agsXMPP;

namespace MiniClient
{
	/// <summary>
	/// Summary description for femAddRoster.
	/// </summary>
	public class frmAddRoster : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtNickname;
		private System.Windows.Forms.TextBox txtJid;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdAdd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private XmppClientConnection _connection;
        
		public frmAddRoster(XmppClientConnection con)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_connection = con;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtNickname = new System.Windows.Forms.TextBox();
			this.txtJid = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtNickname
			// 
			this.txtNickname.Location = new System.Drawing.Point(80, 56);
			this.txtNickname.Name = "txtNickname";
			this.txtNickname.Size = new System.Drawing.Size(168, 20);
			this.txtNickname.TabIndex = 9;
			this.txtNickname.Text = "";
			// 
			// txtJid
			// 
			this.txtJid.Location = new System.Drawing.Point(80, 8);
			this.txtJid.Name = "txtJid";
			this.txtJid.Size = new System.Drawing.Size(168, 20);
			this.txtJid.TabIndex = 8;
			this.txtJid.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "Nickname:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "user@server.org";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Jabber ID:";
			// 
			// cmdAdd
			// 
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(96, 88);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(80, 24);
			this.cmdAdd.TabIndex = 12;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// frmAddRoster
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 118);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.txtNickname);
			this.Controls.Add(this.txtJid);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAddRoster";
			this.Text = "Add Contact";
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			Jid jid = new Jid(txtJid.Text);

			// Add the Rosteritem using the Rostermanager			
			if (txtNickname.Text.Length > 0)
				_connection.RosterManager.AddRosterItem(jid, txtNickname.Text);				
			else
				_connection.RosterManager.AddRosterItem(jid);

			// Ask for subscription now
			_connection.PresenceManager.Subcribe(jid);
						
			this.Close();
		}
	}
}
