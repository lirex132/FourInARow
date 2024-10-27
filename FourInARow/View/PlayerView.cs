
using FourInARow.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow.View
{
	class PlayerView
	{
		public Color color { get; private set; }
		public Player player { get; private set; }
		public Label label { get; set; }
		public void SetModel(Player player)
		{
			this.player = player;
			if (player.isFirst)
			{
				color = Color.Yellow;
			}
			else
			{
				color = Color.Red;
			}
		}

		public void ShowPlayer()
		{
			label.Text = player.name;
			label.BackColor = color;
		}
	}
}
