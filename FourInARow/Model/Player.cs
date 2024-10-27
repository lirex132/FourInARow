using FourInARow.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	public class Player
	{

		public bool isFirst { get; set; }
		public String name { get; set; }
		public int chosenCol{ get;set;}

		

		public Player()
		{
			chosenCol = -1;
		}

		public virtual int Turn(Board1 board, Player otherPlayer)
		{
			return board.IsFull(chosenCol) ? -1: chosenCol;
		}
	}
}
