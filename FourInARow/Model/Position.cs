using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FourInARow.Model
{
	public class Position
	{
		public  Rectangle rect {  get; set; }
		private int x { get; set; }
		private int y { get; set; }
		private int row { get; set; }
		private int col { get; set; }
		private int status { get; set; } //-1= empty, 0= yellow/player1, 1= red/player2

		public Position(Rectangle rect, int x, int y, int row, int col)
		{

			this.rect = rect;
			this.x = x;
			this.y = y;
			this.row = row;
			this.col = col;
		}
	}
}
