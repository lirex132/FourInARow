using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	class RandomAlgorithm : Algorithm
	{
		public override int Turn(Board1 board, Player myPlayer, Player otherPlayer)
		{
			if (board.IsFull())
			{
				return -1;
			}
			Random rand = new Random();
			while (true)
			{
				int col = rand.Next(0, board.colAmount);
				if (!board.IsFull(col))
				{
					return col;
				}
			}
		}
	}
}
