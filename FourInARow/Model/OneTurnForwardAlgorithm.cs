using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	class OneTurnForwardAlgorithm : Algorithm
	{
		public override int Turn(Board1 board, Player myPlayer, Player otherPlayer)
		{
			if (board.diskCount == 0)
			{
				return board.colAmount / 2;
			}
			if (board.diskCount == 1)
			{
				if (board.IsEmpty(board.colAmount / 2))
				{
					return board.colAmount / 2;
				}
				else return (board.colAmount / 2) + 1;

			}
			List<AlgorithmData> myList = GetAlgorithmData(board, myPlayer);
			if (myList.Count == 0)
			{
				return -1;
			}
			myList.Sort((a, b) =>
			{
				int result = b.maxChainLength - a.maxChainLength;
				if (result == 0)
				{
					result = b.openEnds - a.openEnds;
				}
				return result;
			});
			if (myList[0].maxChainLength >= Board1.WIN_LENGTH)
			{
				return myList[0].col;
			}
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

			//Debug.WriteLine(otherList.Count+", "+ sb);
			if (otherList.Count > 0 && otherList[0].maxChainLength >= Board1.WIN_LENGTH)
			{
				return GetRandomBestResult(otherList,null);
			}
			
			
			return GetRandomBestResult(myList,GetColsToAvoid(board,otherPlayer));
		}

		int GetRandomBestResult(List<AlgorithmData> myList, HashSet<int> colsToAvoid)
		{
			int chainLength = myList[0].maxChainLength;
			int openEnds = myList[0].openEnds;
			List<int> colList = new List<int>();
			for (int i = 0; i < myList.Count; i++)
			{
				if (myList[i].maxChainLength >= chainLength && myList[i].openEnds == openEnds&& (colsToAvoid==null||!colsToAvoid.Contains(myList[i].col)))
				{
					colList.Add(myList[i].col);
				}
				else
					break;
			}
			if (colList.Count == 0)
			{
				for (int i = 0; i < myList.Count; i++)
				{
					if ((colsToAvoid == null || !colsToAvoid.Contains(myList[i].col)))
					{
						colList.Add(myList[i].col);
						break;
					}
				}
			}
			if (colList.Count == 0)
			{
				colList.Add(myList[0].col);
			}
			if (colList.Count == 1)
			{
				return colList[0];
			}

			Random rand = new Random();
			return colList[rand.Next(0, colList.Count)];
		}

		HashSet<int> GetColsToAvoid(Board1 board, Player player)
		{
			HashSet<int> set = new HashSet<int>();

			List<AlgorithmData> dataList = new List<AlgorithmData>();
			int openEnds;
			for (int i = 0; i < board.colAmount; i++)
			{
				int length = board.FindMaxChainLength(player, i,1, out openEnds);
				if (length >=Board1.WIN_LENGTH)
				{
					set.Add(i);
				}

			}
			return set;
		}

		List<AlgorithmData> GetAlgorithmData(Board1 board, Player player)
		{
			List<AlgorithmData> dataList = new List<AlgorithmData>();
			int openEnds;
			for (int i = 0; i < board.colAmount; i++)
			{
				int length = board.FindMaxChainLength(player, i,0, out openEnds);
				if (length == 0)
				{
					continue;
				}
				AlgorithmData data = new AlgorithmData(i, length, openEnds, player);
				dataList.Add(data);
			}
			return dataList;
		}
	}


}
