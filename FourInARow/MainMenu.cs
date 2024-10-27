using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow
{
	public partial class MainMenu : Form
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void pvp_Click(object sender, EventArgs e)
		{
			Program.game.GameStart(Game.GameType.PlayerVersusPlayer);
			var f = new GameForm();
			f.mainMenu = this;
			f.Show();
			Hide();
		}

		private void pve_Click(object sender, EventArgs e)
		{
			Program.game.GameStart(Game.GameType.PlayerVersusAi);
			var f = new GameForm();
			f.mainMenu = this;
			f.Show();
			Hide();
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{

		}
	}
}
