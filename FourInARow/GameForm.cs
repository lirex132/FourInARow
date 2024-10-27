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
	public partial class GameForm : Form
	{
		public Form mainMenu;
		bool closedFromButton;
		BoardView boardView = new BoardView();
		PlayerView player1View = new PlayerView();
		PlayerView player2View = new PlayerView();
		public GameForm()
		{
			InitializeComponent();
			boardView.SetModel(Program.game.board);
			boardView.panel = panel3;
			player1View.SetModel(Program.game.player1);
			player2View.SetModel(Program.game.player2);
			player1View.label = label1;
			player2View.label = label2;
			player1View.ShowPlayer();
			player2View.ShowPlayer();
			boardView.ShowBoard();

			ShowCurrentPlayer();
			DelayedDrawTurn();
			
		}

		private void ShowCurrentPlayer()
		{
			if (Program.game.isGameOver)
			{
				return;
			}
			Player currentPlayer = Program.game.GetCurrentPlayer();
			label3.Text = currentPlayer.name + "'s Turn";
			if (player1View.player == currentPlayer)
			{
				label3.BackColor = player1View.color;
			}
			else
			{
				label3.BackColor = player2View.color;
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void mainMenu_btn_Click(object sender, EventArgs e)
		{
			closedFromButton = true;
			Close();
			mainMenu.Show();
		}

		private void GameForm_Deactivate(object sender, EventArgs e)
		{
			if (!closedFromButton)
			{
				mainMenu.Close();
			}
		}

		private void panel3_Click(object sender, EventArgs e)
		{
			if (Program.game.waitingPlayerMove)
			{
				Program.game.GetCurrentPlayer().chosenCol = boardView.ClickedCol(this.boardView.panel.PointToClient(MousePosition).X);
				if (Program.game.GetCurrentPlayer().chosenCol < 0)
				{
					return;
				}
				if(Program.game.PlayerTurn())
				{
					DrawTurn();
					ShowCurrentPlayer();
					DelayedDrawTurn();
				}
			}
		}

		void DrawTurn()
		{
			boardView.AddCurrentDisk(player1View, player2View);
			if (Program.game.isGameOver)
			{
				ShowGameOver();

			}
		}

		async void DelayedDrawTurn()
		{
			if (!Program.game.waitingPlayerMove && !Program.game.isGameOver)
			{
				await Task.Delay(100);
				int n = 0;
				while (!Program.game.PlayerTurn())
				{
					//n++;
					//if (n > 100)
					//{
					//	throw new Exception("Ai algorithm exceeded 100 attempts");
					//}
				}
				DrawTurn();
				ShowCurrentPlayer();
			}
		}
	


		private void ShowGameOver()
		{
			Player winner = Program.game.winner;
			if (winner ==null)
			{
				label3.Text = "Tie Game";
				label3.BackColor = Color.White;
			}
			else if (winner ==player1View.player)
			{
				label3.Text = winner.name+ " Has Won";
				label3.BackColor = player1View.color;
			}
			else 
			{
				label3.Text = winner.name + " Has Won";
				label3.BackColor = player2View.color;
			}
			label3.Show();
		}
	}
}
