using FourInARow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow
{
	public class Game
	{
		public enum GameType
		{
			PlayerVersusPlayer,
			PlayerVersusAi
		}

		public Player player1 { get; private set; }
		public Player player2{ get;private set; }
		public Player winner{ get;private set; }
		public Board1 board { get; private set; }
		public bool isPlayer1Move { get;private set; }
		public bool isGameOver { get;private set; }
		public bool waitingPlayerMove { get; private set; }
		GameType gameType = GameType.PlayerVersusPlayer;

		void Init()
		{
			

		}

		public void GameStart(GameType gt)
		{
			board = new Board1();
			gameType = gt;
			winner = null;
			player1 = new Player();
			isGameOver = false;
			waitingPlayerMove = false;
			player1.name = "HumanP1";
			if (gameType == GameType.PlayerVersusPlayer)
			{
				player2 = new Player();
				player2.name = "HumanP2";
			}
			else
			{
				player2 = new AIPlayer();
				player2.name = "AiP2";
			}

			Random rand = new Random();
			player1.isFirst = rand.Next(2)==1;
			player2.isFirst = !player1.isFirst;
			isPlayer1Move = player1.isFirst;
			SetWaitingPlayerMove();
			
		}

		void SetWaitingPlayerMove()
		{
			waitingPlayerMove = isPlayer1Move && !(player1 is AIPlayer) || !isPlayer1Move && !(player2 is AIPlayer);
		}

		public bool PlayerTurn()
		{
			//if (waitingPlayerMove)
			//{
			//	return false;
			//}

			Player player = null;
			int col = -1;
			if (isPlayer1Move)
			{
				col = player1.Turn(board, player2);
				player = player1;
			}
			else
			{
				col = player2.Turn(board, player1);
				player = player2;
			}
			if (col < 0)
			{
				return false;
			}
			board.DropDisk(player, col);
			if(board.CheckForWin())
			{
				winner = player;
				isGameOver = true;
			}
			if (!isGameOver)
			{
				isGameOver = board.IsFull();
			}
			PlayerTurnOver();
			if (!isGameOver)
			{
				SetWaitingPlayerMove();
			}
			return true;
		}

		public Player GetCurrentPlayer()
		{
			return isPlayer1Move ? player1 : player2;
		}

		//public bool TryPlayerMove(int col)
		//{
			
		//	GetCurrentPlayer().chosenCol = col;
		//	//waitingPlayerMove = false;
		//	if (!PlayerTurn())
		//	{
		//		//waitingPlayerMove = true;
		//		return false;
		//	}
		//	return true;
			
	
		//}

		void PlayerTurnOver()
		{
			isPlayer1Move = !isPlayer1Move;
		}
		void DropDisk(int col)
		{

		}

	}

	
}
