using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FourInARow.View
{
	class DiskView : Control
	{
		int targetY;
		int startY;
		int delta;
		bool isAnimate;

		public DiskView() : base()
		{
			//SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw |ControlStyles.AllPaintingInWmPaint |ControlStyles.SupportsTransparentBackColor, true);
			//BackColor = Color.Transparent;
		}

		
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			float d = 0.05f;
			Rectangle r1 = ClientRectangle;
			Rectangle r = new Rectangle((int)((r1.Width * d)), (int)(r1.Height * d), (int)(r1.Width * (1 - 2 * d)), (int)(r1.Height * (1 - 2 * d)));
			//Brush transaprentBrush = new SolidBrush(Color.Transparent);
			//pevent.Graphics.FillRectangle(Brushes.Transparent, r);
			//transaprentBrush.Dispose();
			Brush brush = new SolidBrush(BackColor);
			pevent.Graphics.FillEllipse(brush, r);

			brush.Dispose();
		}

		public async void AnimateStart(int startY, int delta)
		{
			
			this.startY = startY;
			this.delta = delta;
			isAnimate = true;
			targetY = Location.Y;
			Location = new Point(Location.X,startY);
			await Task.Delay(100);
			while (true)
			{
				await Task.Delay(10);
				Location = new Point(Location.X, Location.Y+delta);
				//Invalidate();
				if (Location.Y > targetY)
				{
					Location = new Point(Location.X, targetY);
					break;
						
				}
			}
		}

		
	}
}
