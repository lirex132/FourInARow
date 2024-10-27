using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow.Model
{
	class Cell
	{
		public Disk disk { get; set; }

		public Cell()
		{
		}
		public Cell(Disk disk)
		{
			this.disk = disk;
		}
		public bool IsEmpty()
		{
			return disk == null;
		}
	}
}
