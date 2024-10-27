using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	class AlgorithmData
	{
		public int col { get; private set; }
		public int maxChainLength { get; private set; }
		public int openEnds { get; private set; }
		public int score { get; set; }
		public Player player { get; private set; }

		public AlgorithmData(int col, int maxChainLength, int openEnds, Player player)
		{
			this.col = col;
			this.maxChainLength = maxChainLength;
			this.openEnds = openEnds;
			this.player = player;
		}


		public override string ToString()
		{
			return "C:"+col+" L:"+ maxChainLength+" E:" + openEnds;
		}
	}
}
