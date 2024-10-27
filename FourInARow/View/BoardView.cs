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
	class BoardView
	{
		Board1 board;
		float width;
		float height;
		float cellWidth;
		float cellHeight;
		Panel p;
		public Panel panel { get { return p; } set
			{
				p = value;
				InitView();
			}
		}

		private void InitView()
		{
			width = p.Width;
			height = p.Height;
			cellWidth = width / board.colAmount;
			cellHeight = height / board.rowAmount;
		}

		private async void AddDisk(int row, int col, PlayerView p1, PlayerView p2)
		{
			DiskView disk = new DiskView();
			this.p.Controls.Add(disk);
			disk.BackColor = board.GetDisk(row, col).player == p1.player ? p1.color : p2.color;
			disk.Width = (int)cellWidth;
			disk.Height = (int)cellHeight;
			disk.Location = new Point((int)(cellWidth * col), (int)(cellHeight * (row)));
			//disk.AnimateStart(0, (int)(cellHeight/10));
		}
		public void AddCurrentDisk(PlayerView p1, PlayerView p2)
		{

			AddDisk(board.recentCellRow, board.recentCellCol, p1, p2);
		}

		public BoardView()
		{

		}

		public int ClickedCol(int x)
		{
			//return (int)((x-p.Location.X -p.Parent.Location.X) / cellWidth);
			return (int)(x / cellWidth);
		}

		//public void Draw(Graphics gr, Board1 board)
		//{
		//	Node nodeHead = board.nodeHead;
		//	int count = 0;
		//	Pen p = new Pen(Color.Red, 3);

		//	for (Node rowN = nodeHead; rowN.down != null; rowN = rowN.down) // change rowN.down != null to rowN != null
		//	{
		//		for (Node colN = nodeHead; colN.right != null; colN = colN.right)// change colN.right != null to colN != null
		//		{
		//			count++;
		//			gr.DrawRectangle(p, colN.val.rect);
		//		}
		//	}
		//}

		public void SetModel(Board1 board)
		{
			this.board = board;
		}

		public void ShowBoard()
		{

		}
	}
}
