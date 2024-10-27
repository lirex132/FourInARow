using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace FourInARow.Model
{
	public class Board1 
	{
		//UInt64 BitBoard { get; set; }
		public const int ROW_AMOUNT = 6;
		public const int COL_AMOUNT = 7;
		public const int WIN_LENGTH = 4;
		public int colAmount { get; private set; }
		public int rowAmount { get; private set; }
		//public Node nodeHead { get; set; }
		Cell[,] boardMat { get; set; }
		public int recentCellRow { get; private set; }
		public int recentCellCol { get; private set; }
		int winLength;
		public int diskCount { get; private set; }

		//Class constructor, calls Init()
		public Board1()
		{
			Init();
		}

		//Class initialization method, creates and sets all relevant properties of the class
		public void Init()
		{
			/*
			Node[,] posMat = new Node[ROW_AMOUNT, COL_AMOUNT];
			Node tempNode;
			Position tempPos;
			Rectangle tempRect;
			int count = 0;
			int x = 50, y = 50, w = 30, h = 30 ;
			*/
			recentCellCol = -1;
			recentCellRow = -1;
			diskCount = 0;
			winLength = WIN_LENGTH;

			boardMat = new Cell[ROW_AMOUNT, COL_AMOUNT];
			for (int i = 0; i < boardMat.GetLength(0); i++)
			{
				for (int j = 0; j < boardMat.GetLength(1); j++)
				{
					boardMat[i, j] = new Cell();
				}
			}
			/*
			for (int i = 0; i < ROW_AMOUNT; i++, x+=30)
			{
				for (int j = 0; j < COL_AMOUNT; j++, y+=30)
				{
					tempRect = new Rectangle(x, y, w, h);
					tempPos = new Position(tempRect, x, y, i, j);
					tempNode = new Node(tempPos);

					posMat[i, j] = tempNode;
					if (j > 0)
					{						
						tempNode.left = posMat[i, j - 1];
						posMat[i, j - 1].right = tempNode;
						count++;
					}

					if (i != 0)
					{
						tempNode.up = posMat[i - 1, j];
						posMat[i - 1, j].down = tempNode;
						
					}

					if (i > 0  )
					{
	
						if (j < COL_AMOUNT - 1)
						{
							tempNode.upperRight = posMat[i - 1, j + 1];
							posMat[i - 1, j + 1].lowerLeft = tempNode;
						}
						if (j > 0  )
						{
							tempNode.upperLeft = posMat[i - 1, j - 1];
							posMat[i - 1, j - 1].lowerRight = tempNode;
						}
					}
	
				}
			}
			*/
			colAmount = boardMat.GetLength(1);
			rowAmount = boardMat.GetLength(0);
			//nodeHead = posMat[0, 0];
		}

		/*
		public void Draw(Graphics gr)
		{
			int count = 0;
			Pen p = new Pen(Color.Red, 3);
			for (Node i = nodeHead; i.right != null; i = i.right)
			{
				count++;
			}
			for (Node rowN = nodeHead; rowN.down != null; rowN = rowN.down)
			{
				for (Node colN = nodeHead; colN.right != null; colN = colN.right)
				{
					count++;
					gr.DrawRectangle(p, colN.val.rect);
				}
			}
		}
		*/

		public bool IsFull()
		{
			return diskCount >= rowAmount * colAmount;
		}
		public bool IsFull(int col)
		{
			if (col < 0 || col > colAmount)
			{
				return true;
			}
			return !boardMat[0, col].IsEmpty();
		}

		public bool IsEmpty(int col)
		{
			return boardMat[rowAmount - 1, col].disk ==null;
		}
		public bool CheckForWin()
		{
			int count = 1;
			Player player = boardMat[recentCellRow, recentCellCol].disk.player;
			//vertical
			for (int i = recentCellRow - 1; i >= 0 && !(boardMat[i, recentCellCol].IsEmpty() || boardMat[i, recentCellCol].disk.player != player); i--)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}

			for (int i = recentCellRow + 1; i < rowAmount && !(boardMat[i, recentCellCol].IsEmpty() || boardMat[i, recentCellCol].disk.player != player); i++)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}

			//horizontal
			count = 1;
			for (int i = recentCellCol - 1; i >= 0 && !(boardMat[recentCellRow, i].IsEmpty() || boardMat[recentCellRow, i].disk.player != player); i--)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}
			for (int i = recentCellCol + 1; i < colAmount && !(boardMat[recentCellRow, i].IsEmpty() || boardMat[recentCellRow, i].disk.player != player); i++)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}

			//upper left to lower right diagonal
			count = 1;
			for (int i = recentCellRow - 1, j = recentCellCol - 1; i >= 0 && j >= 0 && !(boardMat[i, j].IsEmpty() || boardMat[i, j].disk.player != player); i--, j--)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}
			for (int i = recentCellRow + 1, j = recentCellCol + 1; i < rowAmount && j < colAmount && !(boardMat[i, j].IsEmpty() || boardMat[i, j].disk.player != player); i++, j++)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}

			//lower left to upper right diagonal
			count = 1;
			for (int i = recentCellRow + 1, j = recentCellCol - 1; i < rowAmount && j >= 0 && !(boardMat[i, j].IsEmpty() || boardMat[i, j].disk.player != player); i++, j--)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}
			for (int i = recentCellRow - 1, j = recentCellCol + 1; i >= 0 && j < colAmount && !(boardMat[i, j].IsEmpty() || boardMat[i, j].disk.player != player); i--, j++)
			{
				count++;
				if (count >= winLength)
				{
					return true;
				}
			}

			return false;
		}

		//Drops a disk into a column
		//Parameters: p - player who is making the move
		//			  col - column, into which to drop the disk
		public void DropDisk(Player p, int col)
		{
			Disk disk = new Disk(p);
			int row = GetTopEmptyRow(col);
			recentCellCol = col;
			recentCellRow = row;
			boardMat[recentCellRow, recentCellCol].disk = disk;

			diskCount++;

		}

		public void DropDisk(Player p, int col, out int previousRecentRow, out int previousRecentCol)
		{
			Disk disk = new Disk(p);
			int row = GetTopEmptyRow(col);
			previousRecentRow = recentCellRow;
			previousRecentCol = recentCellCol;
			recentCellCol = col;
			recentCellRow = row;
			boardMat[recentCellRow, recentCellCol].disk = disk;

			diskCount++;

		}

		public void UndoMove(int previousRecentRow, int previousRecentCol )
		{
			boardMat[recentCellRow, recentCellCol] = null;
			recentCellRow = previousRecentRow;
			recentCellCol = previousRecentCol;
		}
		public Disk GetDisk(int row, int col)
		{
			return boardMat[row, col].disk;
		}
		public int GetTopEmptyRow(int col)
		{
			for (int i = rowAmount - 1; i >= 0; i--)
			{
				Cell cell = boardMat[i, col];
				if (cell.IsEmpty())
				{
					return i;
				}
			}
			return -1;
		}

		public Board1 Clone()
		{
			Board1 clonedBoard = new Board1();
			clonedBoard.recentCellCol = this.recentCellCol;
			clonedBoard.recentCellRow = this.recentCellRow;
			clonedBoard.diskCount = this.diskCount;
			for (int i = 0; i < boardMat.GetLength(0); i++)
			{
				for (int j = 0; j < boardMat.GetLength(1); j++)
				{
					clonedBoard.boardMat[i, j] = new Cell(this.boardMat[i,j].disk);
				}
			}
			return clonedBoard;
		}

		public int FindMaxChainLength(Player player,int col,int deltaRow,out int openEnds)
		{
			int chainLength = 0;
			int currentChainLength;
			int currentOpenEnds;

			openEnds = 0;
			if (IsFull(col))
			{
				openEnds = 0;
				return 0;
			}
			int row = GetTopEmptyRow(col)-deltaRow;
			if (row < 0)
			{
				openEnds = 0;
				return 0;
			}
			//Horizontal
			int openEnd1, openEnd2;
			currentChainLength = FindTail(player, row, col, 0, -1,out openEnd1)+1+FindTail(player, row, col, 0 ,1, out openEnd2);
			currentOpenEnds = openEnd1 + openEnd2;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}

			
			if (chainLength >= WIN_LENGTH)
			{
				
				return chainLength;
			}

			//Vertical
			currentChainLength = FindTail(player, row, col, 1, 0, out openEnd1) + 1 ;
			currentOpenEnds = openEnd1  ;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			//top left to bottom right diagonal
			currentChainLength = FindTail(player, row, col, -1, -1, out openEnd1) + 1 + FindTail(player, row, col, 1, 1, out openEnd2);
			currentOpenEnds = openEnd1 + openEnd2;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			//bottom left to top right diagonal
			currentChainLength = FindTail(player, row, col, 1, -1, out openEnd1) + 1 + FindTail(player, row, col, -1, 1, out openEnd2);
			currentOpenEnds = openEnd1 + openEnd2;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			return chainLength;
		}

		public int FindExistingMaxChainLength(Player player, int col,  out int openEnds)
		{
			int chainLength = 0;
			int currentChainLength;
			int currentOpenEnds;

			openEnds = 0;
			if (IsFull(col))
			{
				openEnds = 0;
				return 0;
			}
			int row = GetTopEmptyRow(col) +1;
			if (row > rowAmount)
			{
				row = rowAmount - 1;
			}
			//Horizontal
			int openEnd1, openEnd2;
			currentChainLength = FindTail(player, row, col, 0, -1, out openEnd1) + 1 + FindTail(player, row, col, 0, 1, out openEnd2);
			currentOpenEnds = openEnd1 + openEnd2;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			//Vertical
			currentChainLength = FindTail(player, row, col, 1, 0, out openEnd1) + 1;
			currentOpenEnds = openEnd1;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			//top left to bottom right diagonal
			currentChainLength = FindTail(player, row, col, -1, -1, out openEnd1) + 1 + FindTail(player, row, col, 1, 1, out openEnd2);
			currentOpenEnds = openEnd1 + openEnd2;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			//bottom left to top right diagonal
			currentChainLength = FindTail(player, row, col, 1, -1, out openEnd1) + 1 + FindTail(player, row, col, -1, 1, out openEnd2);
			currentOpenEnds = openEnd1 + openEnd2;
			if (chainLength < currentChainLength || chainLength == currentChainLength && openEnds < currentOpenEnds)
			{
				chainLength = currentChainLength;
				openEnds = currentOpenEnds;
			}


			if (chainLength >= WIN_LENGTH)
			{

				return chainLength;
			}

			return chainLength;
		}
		int FindTail(Player player, int startRow, int startCol, int deltaRow, int deltaCol, out int openEnd)
		{
			int count = 0;
			int i = 1;
			int row, col;

			openEnd = 0;
			while (true)
			{
				row = startRow + i * deltaRow;
				col = startCol + i * deltaCol;
				if (row < 0 || row >= rowAmount || col < 0 || col >= colAmount)
				{
					break;
				}
				var cell = boardMat[row, col];
				if (cell.disk == null)
				{
					openEnd = 1;
					break;
				}
				if (cell.disk.player != player)
				{
					break;
				}
				i++;
				count++;
			}
			return count;
		}
		//bool static IsColumnFull(int col)
		//{
		//	return IsSpaceFull(0, col);

		//}

		//bool IsSpaceFull(int row, int col)
		//{
		//	UInt64 topRowSpot = BitBoard << ( col);
		//	UInt16 mask = 1;
		//	if ((topRowSpot & mask) == 1)
		//	{
		//		return true;
		//	}
		//	else
		//		return false;

		//}

	}
}
