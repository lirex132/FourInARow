using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	public abstract class Algorithm
	{

		public abstract int Turn(Board1 board, Player myPlayer, Player otherPlayer);

	}
}
