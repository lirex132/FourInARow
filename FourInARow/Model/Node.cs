using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	public class Node
	{
		public Position val { get; set; }
		public Node up { get; set; }
		public Node down { get; set; }
		public Node right { get; set; }
		public Node left { get; set; }
		public Node upperRight { get; set; }
		public Node upperLeft { get; set; }
		public Node lowerRight { get; set; }
		public Node lowerLeft { get; set; }

		public Node(Position val)
		{
			this.val = val;
		}

		public Node()
		{
			
		}
	}
}
