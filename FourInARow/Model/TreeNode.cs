using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	class TreeNode
	{
		public List<TreeNode> children { get; private set; }
		public int score { get; set; }
		public Board1 boardState { get;  set; }
		public Player player{ get; set; }

		public TreeNode()
		{
		}

		public TreeNode(int score, Board1 boardState)
		{

		}

	}
}
