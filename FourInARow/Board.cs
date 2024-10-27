using FourInARow.Model;
using FourInARow.View;
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
	public partial class Board : Form
	{
		Board1 b;
		BoardView bView;
		public Board()
		{

			InitializeComponent();
			b = new Board1();
			bView = new BoardView();
			//b.Init();
		}

		private void Board_Load(object sender, EventArgs e)
		{
			
		}

		private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
			//bView.Draw(e.Graphics, b);
		}
	}
}
