using FourInARow.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FourInARow.Model
{
	public class Disk
	{
		public Player player { get; private set; }

		public Disk(Player player)
		{
			this.player = player;
		}

	}
}
