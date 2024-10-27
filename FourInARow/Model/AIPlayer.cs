using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourInARow.enums;

namespace FourInARow.Model
{
	class AIPlayer : Player
	{
		Algorithm alg { get; set; }

		public AIPlayer() 
		{
			alg =  new MinMaxAlgorithm();
		//	alg = new MinMaxAlgorithm();//  new OneTurnForwardAlgorithm();
		}

		public override int Turn(Board1 board,  Player otherPlayer)
		{
			return alg.Turn(board, this, otherPlayer);
		}
	}
}
