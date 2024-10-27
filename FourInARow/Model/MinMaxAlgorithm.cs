using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FourInARow.Model
{
	class MinMaxAlgorithm : Algorithm
	{
		const int DEPTH_MAX = 4;
		const int MAX_SCORE = 100000;
		const int CHAIN_LENGTH_MODIFIER = 100;
		const int OPEN_ENDS_MODIFIER = 150;
		Algorithm algOneTurnForwardAlgorithm;

		public override int Turn(Board1 board, Player myPlayer, Player otherPlayer)
		{
			algOneTurnForwardAlgorithm = new OneTurnForwardAlgorithm();
			int bestColumn = -1;
			
			int bestScore = int.MinValue;
			if (board.diskCount == 0)
			{
				return board.colAmount / 2;
			}
			for (int i = 0; i < Board1.COL_AMOUNT; i++)
			{
				if (!board.IsFull(i))
				{
					Board1 virtBoard = board.Clone();
					virtBoard.DropDisk(myPlayer, i);
					//int score = MiniMax(virtBoard, DEPTH_MAX - 1, false, myPlayer, otherPlayer);
					int score = AlphaBeta(virtBoard, DEPTH_MAX - 1, false,int.MinValue, int.MaxValue, myPlayer, otherPlayer);
					
					if (score > bestScore)
					{
						bestColumn = i;
						bestScore = score;
						
					}
					Debug.WriteLine("score: " + bestScore);
					Debug.WriteLine("col: " + bestColumn);
				}
			}
			Debug.WriteLine("best score: "+ bestScore);
			Debug.WriteLine("best col: "+bestColumn);
			return bestColumn;
		}

		int GetRandomBestResult(List< AlgorithmData> myList)
		{
			int chainLength = myList[0].maxChainLength;
			int openEnds = myList[0].openEnds;
			List<int> colList = new List<int>();
			for (int i = 0; i < myList.Count; i++)
			{
				if (myList[i].maxChainLength == chainLength && myList[i].openEnds == openEnds)
				{
					colList.Add(myList[i].col);
				}
				else
					break;
			}
			if (colList.Count == 1)
			{
				return colList[0];
			}

			Random rand = new Random();
			return colList[rand.Next(0, colList.Count)];
		}
		
		public int HeuristicFunction(Board1 board, Player myPlayer, Player otherPlayer)
		{
			
			List<AlgorithmData> myList = GetAlgorithmData(board, myPlayer);


			//if (myList.Count == 0)
			//{
			//	return -1;
			//}
			
			myList.Sort((a, b) =>
			{
				int result = 0;
				if (a.openEnds > 0 && b.openEnds > 0)
				{
					result = b.maxChainLength - a.maxChainLength;
					if (result == 0)
					{
						result = b.openEnds - a.openEnds;
					}
				}
				else if (a.openEnds > 0)
				{
					result = -1;
				}
				else if (b.openEnds > 0)
				{
					result = 1;
				}
				else
				{
					result = b.maxChainLength - a.maxChainLength;
				}
				return result;
			});

			if (myList.Count!=0  &&myList[0].maxChainLength >= Board1.WIN_LENGTH)
			{
				return MAX_SCORE;
			}
			List<AlgorithmData> otherList = GetAlgorithmData(board, otherPlayer);
			otherList.Sort((a, b) =>
			{
				int result = b.maxChainLength - a.maxChainLength;
				if (result == 0)
				{
					result = b.openEnds - a.openEnds;
				}
				return result;
			});
			//StringBuilder sb = new System.Text.StringBuilder();
			//for (int i = 0; i < otherList.Count; i++)
			//{
			//	sb.Append(otherList[i]).Append(", ");

			//}

			//Debug.WriteLine(otherList.Count + ", " + sb);
			foreach (AlgorithmData ad in otherList)
			{

				if (otherList.Count > 0 && ad.maxChainLength >= Board1.WIN_LENGTH)
				{
					return -MAX_SCORE;
				}
			}
			
			int chainLengthScore = 0;
			int openEndsScore = 0;
			foreach (AlgorithmData ad in myList)
			{
				chainLengthScore += ad.maxChainLength * CHAIN_LENGTH_MODIFIER;
				openEndsScore += ad.openEnds * OPEN_ENDS_MODIFIER;
			}
			Debug.WriteLine(chainLengthScore + openEndsScore);
			return chainLengthScore + openEndsScore;

		}
		
		List<AlgorithmData> GetAlgorithmData(Board1 board, Player player)
		{
			List<AlgorithmData> dataList = new List<AlgorithmData>();
			int openEnds;
			for (int i = 0; i < board.colAmount; i++)
			{
				int topDiskRow = board.GetTopEmptyRow(i) + 1;
				if (topDiskRow >= board.rowAmount)
				{
					topDiskRow = board.rowAmount - 1;
				}
				if (topDiskRow <0)
				{
					topDiskRow = 0;
				}
				if (board.GetDisk(topDiskRow, i)!=null&& board.GetDisk(topDiskRow, i).player == player)
				{

					int length = board.FindExistingMaxChainLength(player, i, out openEnds);
					if (length == 0)
					{
						continue;
					}
					AlgorithmData data = new AlgorithmData(i, length, openEnds, player);
					dataList.Add(data);
				}
			}
			return dataList;
		}

		List<AlgorithmData> GetAlgorithmData2Down(Board1 board, Player player)
		{
			List<AlgorithmData> dataList = new List<AlgorithmData>();
			int openEnds;
			for (int i = 0; i < board.colAmount; i++)
			{
				int topDiskRow = board.GetTopEmptyRow(i) + 2;
				if (topDiskRow >= board.rowAmount)
				{
					topDiskRow = board.rowAmount - 1;
				}
				if (board.GetDisk(topDiskRow, i) != null && board.GetDisk(topDiskRow, i).player == player)
				{

					int length = board.FindExistingMaxChainLength(player, i, out openEnds);
					if (length == 0)
					{
						continue;
					}
					AlgorithmData data = new AlgorithmData(i, length, openEnds, player);
					dataList.Add(data);
				}
			}
			return dataList;
		}

		public int MiniMax(Board1 board, int depth, bool maximizingPlayer, Player myPlayer, Player otherPlayer)
		{

			if (depth == 0 || board.CheckForWin())
			{

				return HeuristicFunction(board, myPlayer, otherPlayer);


			}
			int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;
			for (int i = 0; i < Board1.COL_AMOUNT; i++)
			{
				if (board.IsFull(i) == false)
				{
					Board1 virtBoard = board.Clone();
					if (maximizingPlayer)
					{
						virtBoard.DropDisk(myPlayer, i);
						bestScore = Math.Max(bestScore, MiniMax(virtBoard, depth - 1, !maximizingPlayer, myPlayer, otherPlayer));

					}
					else
					{
						virtBoard.DropDisk(otherPlayer, i);
						bestScore = Math.Min(bestScore, MiniMax(virtBoard, depth - 1, !maximizingPlayer, myPlayer, otherPlayer));

					}
				}
			}

			return bestScore;
		}

		public int AlphaBeta(Board1 board, int depth, bool maximizingPlayer,int alpha, int beta, Player myPlayer, Player otherPlayer)
		{

			if (board.CheckForWin()||depth == 0  )
			{

				return HeuristicFunction(board, myPlayer, otherPlayer)*(depth+1);


			}
			int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;
			for (int i = 0; i < Board1.COL_AMOUNT; i++)
			{
				if (board.IsFull(i) == false)
				{
					Board1 virtBoard = board.Clone();
					if (maximizingPlayer)
					{
						virtBoard.DropDisk(myPlayer, i);
						bestScore = Math.Max(bestScore, AlphaBeta(virtBoard, depth - 1, !maximizingPlayer,alpha,beta, myPlayer, otherPlayer));
						alpha = Math.Max(alpha, bestScore);
						if (alpha >= beta)
						{
							break;
						}
					}
					else
					{
						virtBoard.DropDisk(otherPlayer, i);
						bestScore = Math.Min(bestScore, AlphaBeta(virtBoard, depth - 1, !maximizingPlayer, alpha, beta, myPlayer, otherPlayer));
						beta = Math.Min(beta, bestScore);
						if (beta <= alpha)
						{
							break;
						}
					}
				}
			}

			return bestScore;
		}

	}
}
