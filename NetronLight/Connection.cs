using System;
using System.Drawing;
namespace NetronLight
{
	/// <summary>
	/// Represents the connection between two connectors
	/// </summary>
	public class Connection : Entity
	{

		#region Fields
		protected Connector from;
		protected Connector to;

		#endregion

		#region Properties
		public Connector From
		{
			get{return from;}
			set{from = value;}
		}

		public Connector To
		{
			get{return to;}
			set{to = value;}
		}

		#endregion

		#region Constructor
		/// <summary>
		/// Default ctor
		/// </summary>
		public Connection()
		{
			
		}
		/// <summary>
		/// Constructs a connection between the two given points
		/// </summary>
		/// <param name="from">the starting point of the connection</param>
		/// <param name="to">the end-point of the connection</param>
		public Connection(Point from, Point to)
		{
			this.from = new Connector(from);
			this.from.Name = "From";
			this.to = new Connector(to);
			this.To.Name = "To";
		}
		#endregion

		#region Methods

		/// <summary>
		/// Paints the connection on the canvas
		/// </summary>
		/// <param name="g"></param>
		public override void Paint(System.Drawing.Graphics g)
		{
			if(hovered || isSelected)
				g.DrawLine(new Pen(Color.Red,2F),From.Point,To.Point);
			else
			g.DrawLine(Pens.Black,From.Point,To.Point);
		}
		/// <summary>
		/// Invalidates the connection
		/// </summary>
		public override void Invalidate()
		{
			Rectangle f = new Rectangle(from.Point,new Size(10,10));
			Rectangle t = new Rectangle(to.Point,new Size(10,10));
			site.Invalidate(Rectangle.Union(f,t));
		}

		/// <summary>
		/// Tests if the mouse hits this connection
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public override bool Hit(Point p)
		{
			Point p1,p2, s;
			RectangleF r1, r2;
			float o,u;
			p1 = from.Point; p2 = to.Point;
	
			// p1 must be the leftmost point.
			if (p1.X > p2.X) { s = p2; p2 = p1; p1 = s; }

			r1 = new RectangleF(p1.X, p1.Y, 0, 0);
			r2 = new RectangleF(p2.X, p2.Y, 0, 0);
			r1.Inflate(3, 3);
			r2.Inflate(3, 3);
			//this is like a topological neighborhood
			//the connection is shifted left and right
			//and the point under consideration has to be in between.						
			if (RectangleF.Union(r1, r2).Contains(p))
			{
				if (p1.Y < p2.Y) //SWNE
				{
					o = r1.Left + (((r2.Left - r1.Left) * (p.Y - r1.Bottom)) / (r2.Bottom - r1.Bottom));
					u = r1.Right + (((r2.Right - r1.Right) * (p.Y - r1.Top)) / (r2.Top - r1.Top));
					return ((p.X > o) && (p.X < u));
				}
				else //NWSE
				{
					o = r1.Left + (((r2.Left - r1.Left) * (p.Y - r1.Top)) / (r2.Top - r1.Top));
					u = r1.Right + (((r2.Right - r1.Right) * (p.Y - r1.Bottom)) / (r2.Bottom - r1.Bottom));
					return ((p.X > o) && (p.X < u));
				}
			}
			return false;
		}

		/// <summary>
		/// Moves the connection with the given shift
		/// </summary>
		/// <param name="p"></param>
		public override void Move(Point p)
		{

		}


		#endregion

	}
}
