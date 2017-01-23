using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace NetronLight
{
	/// <summary>
	/// A 'light' version of the Netron graph control
	/// without all the advanced diagramming stuff
	/// see however http://netron.sf.net for more info.
	/// This control shows the simplicity with which you can still achieve good results,
	/// it's a toy-model to explore and can eventually help you if you want to go for a 
	/// bigger adventure in the full Netron control.
	/// Question and comments are welcome via the forum of The Netron Project or mail me
	/// [Illumineo@users.sourceforge.net]
	/// 
	/// Thank you for downloading the code and your feedback!
	/// 
	/// </summary>
	public class GraphControl : ScrollableControl
	{
		#region Events and delegates
		/// <summary>
		/// the info coming with the show-props event
		/// </summary>
		public delegate void ShowProps(object ent);

		/// <summary>
		/// notifies the host to show the properties usually in the property grid
		/// </summary>
		public event ShowProps OnShowProps;

		#endregion

		#region Fields


		/// <summary>
		/// the collection of shapes on the canvas
		/// </summary>
		protected ShapeCollection shapes;

		/// <summary>
		/// the collection of connections on the canvas
		/// </summary>
		protected ConnectionCollection connections;
		/// <summary>
		/// the entity hovered by the mouse
		/// </summary>
		protected Entity hoveredEntity;
		/// <summary>
		/// the unique entity currently selected
		/// </summary>
		protected Entity selectedEntity;
		/// <summary>
		/// whether we are tracking, i.e. moving something around
		/// </summary>
		protected bool tracking = false;
		/// <summary>
		/// just a reference point for the OnMouseDown event
		/// </summary>
		protected Point refp;
		/// <summary>
		/// the context menu of the control
		/// </summary>
		protected ContextMenu menu;
		/// <summary>
		/// A simple, general purpose random generator
		/// </summary>
		protected Random rnd;
		/// <summary>
		/// simple proxy for the propsgrid of the control
		/// </summary>
		protected Proxy proxy;

		/// <summary>
		/// drawing a grid on the canvas?
		/// </summary>
		protected bool showGrid = true;

		/// <summary>
		/// just the default gridsize used in the paint-background method
		/// </summary>
		protected Size gridSize = new Size(10,10);

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the shape collection
		/// </summary>
		public ShapeCollection Shapes
		{
			get{return shapes;}
			set{shapes = value;}
		}

		/// <summary>
		/// Gets or sets whether the grid is drawn on the canvas
		/// </summary>
		public bool ShowGrid
		{
			get{return showGrid;}
			set{showGrid = value; Invalidate();}
		}

		#endregion

		#region Constructor
		/// <summary>
		/// Default ctor
		/// </summary>
		public GraphControl()
		{
			//double-buffering
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			//init the collections
			shapes = new ShapeCollection();
			connections = new ConnectionCollection();

			//menu
			menu = new ContextMenu();
			BuildMenu();
			this.ContextMenu = menu;

			//init the randomizer
			rnd = new Random();

			//init the proxy
			proxy = new Proxy(this);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Builds the context menu
		/// </summary>
		private void BuildMenu()
		{
			MenuItem mnuDelete = new MenuItem("Удалить",new EventHandler(OnDelete));
			menu.MenuItems.Add(mnuDelete);
			
			MenuItem mnuProps = new MenuItem("Свойства", new EventHandler(OnProps));
			menu.MenuItems.Add(mnuProps);

			MenuItem mnuDash = new MenuItem("-");
			menu.MenuItems.Add(mnuDash);

			MenuItem mnuNewConnection = new MenuItem("Добавить связь", new EventHandler(OnNewConnection));
			menu.MenuItems.Add(mnuNewConnection);

			MenuItem mnuShapes = new MenuItem("Элементы");
			menu.MenuItems.Add(mnuShapes);

			MenuItem mnuRecShape = new MenuItem("Прямоугольник", new EventHandler(OnRecShape));
			mnuShapes.MenuItems.Add(mnuRecShape);
			
			MenuItem mnuOvalShape = new MenuItem("Овал", new EventHandler(OnOvalShape));
			mnuShapes.MenuItems.Add(mnuOvalShape);

			MenuItem mnuTLShape = new MenuItem("Текст", new EventHandler(OnTextLabelShape));
			mnuShapes.MenuItems.Add(mnuTLShape);


			
		}

        //static bool onPaintBackgroundFlag = false;  
		protected override void OnPaintBackground(PaintEventArgs e)
		{
            //if (onPaintBackgroundFlag) return;
            //onPaintBackgroundFlag = true;

            base.OnPaintBackground(e);
			Graphics g = e.Graphics;

			if(showGrid)
				ControlPaint.DrawGrid(g,this.ClientRectangle,gridSize,this.BackColor);

            //onPaintBackgroundFlag = false;
		}

		
		#region Menu handlers
		/// <summary>
		/// Deletes the currently selected object from the canvas
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDelete(object sender, EventArgs e)
		{
			if(selectedEntity!=null)
			{
				if(typeof(ShapeBase).IsInstanceOfType(selectedEntity))
				{
					this.shapes.Remove(selectedEntity as ShapeBase);
					this.Invalidate();
				}
				else
				if(typeof(Connection).IsInstanceOfType(selectedEntity))
				{
					this.connections.Remove(selectedEntity as Connection);
					this.Invalidate();
				}

			}
		}


		/// <summary>
		/// Asks the host to show the props
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnProps(object sender, EventArgs e)
		{
			object thing;
			if(this.selectedEntity==null) 
				thing = this.proxy;
			else
				thing =selectedEntity;
			if(this.OnShowProps!=null)
				OnShowProps(thing);

		}

		/// <summary>
		/// Adds a rectangular shape
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnRecShape(object sender, EventArgs e)
		{
			AddShape(ShapeTypes.Rectangular,refp);
		}

		private void OnOvalShape(object sender, EventArgs e)
		{
			AddShape(ShapeTypes.Oval,refp);
		}
		private void OnTextLabelShape(object sender, EventArgs e)
		{
		AddShape(ShapeTypes.TextLabel,refp);
		}
		private void OnNewConnection(object sender, EventArgs e)
		{
			AddConnection(refp);
		}
		#endregion

		/// <summary>
		/// Paints the control
		/// </summary>
		/// <remarks>
		/// If you switch the painting order of connections and shapes the connection line
		/// will be underneath/above the shape
		/// </remarks>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			//use the best quality, with a performance penalty
			g.SmoothingMode= System.Drawing.Drawing2D.SmoothingMode.AntiAlias;		
			//similarly for the connections
			for(int k=0; k<connections.Count; k++)
			{
				connections[k].Paint(g);
				connections[k].From.Paint(g);
				connections[k].To.Paint(g);
			}
			//loop over the shapes and draw
			for(int k=0; k<shapes.Count; k++)
			{
				shapes[k].Paint(g);
			}
			
			
		}


		/// <summary>
		/// Adds a shape to the canvas or diagram
		/// </summary>
		/// <param name="shape"></param>
		public ShapeBase AddShape(ShapeBase shape)
		{
			shapes.Add(shape);
			shape.Site = this;
			this.Invalidate();
			return shape;
		}
		/// <summary>
		/// Adds a predefined shape
		/// </summary>
		/// <param name="type"></param>
		public ShapeBase AddShape(ShapeTypes type, Point location)
		{
			ShapeBase shape = null;
			switch(type)
			{
				case ShapeTypes.Rectangular:
					shape = new SimpleRectangle(this);
					break;
				case ShapeTypes.Oval:
					shape = new OvalShape(this);
					break;
				case ShapeTypes.TextLabel:
					shape = new TextLabel(this);
					shape.Location = location;
					shape.ShapeColor = Color.Transparent;
					shape.Text = "A text label (change the text in the property grid)";
					shape.Width = 350;
					shape.Height = 30;
					shapes.Add(shape);
					return shape;


			}
			if(shape==null) return null;
			shape.ShapeColor = Color.FromArgb(rnd.Next(0,255),rnd.Next(0,255),rnd.Next(0,255));
			shape.Location = location;
			shapes.Add(shape);
			return shape;
		}


		#region AddConnection overloads
		/// <summary>
		/// Adds a connection to the diagram
		/// </summary>
		/// <param name="con"></param>
		public Connection AddConnection(Connection con)
		{
			connections.Add(con);
			con.Site = this;
			con.From.Site = this;
			con.To.Site = this;
			this.Invalidate();
			return con;
		}
		public Connection AddConnection(Point startPoint)
		{
			//let's take a random point and assume this control is not infinitesimal (bigger than 20x20).
			Point rndPoint = new Point(rnd.Next(20,this.Width-20),rnd.Next(20,this.Height-20));
			Connection con = new Connection(startPoint, rndPoint);			
			AddConnection(con);
			//use the end-point and simulate a dragging operation, see the OnMouseDown handler
			selectedEntity= con.To;				
			tracking = true;
			refp=rndPoint;
			this.Invalidate();
			return con;

		}

		public Connection AddConnection(Connector from, Connector to)
		{
			Connection con = AddConnection(from.Point,to.Point);
			from.AttachConnector(con.From);
			to.AttachConnector(con.To);
			

			return con;

		}

		public Connection AddConnection(Point from, Point to)
		{
			Connection con = new Connection(from, to);
			this.AddConnection(con);
			return con;
		}
		#endregion

		#region Mouse event handlers

		/// <summary>
		/// Handles the mouse-down event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			Point p = new Point(e.X,e.Y);

			#region LMB & RMB
			
				//test for connectors
				for(int k=0; k<connections.Count; k++)
				{
					if(connections[k].From.Hit(p))
					{					
						if(selectedEntity!=null) selectedEntity.IsSelected=false;
						selectedEntity= connections[k].From;					
						tracking = true;
						refp=p;
						return;
					}

					if(connections[k].To.Hit(p))
					{					
						if(selectedEntity!=null) selectedEntity.IsSelected=false;
						selectedEntity= connections[k].To;				
						tracking = true;
						refp=p;
						return;
					}
				}

				//test for connections
				for(int k=0; k<this.connections.Count;k++)
				{
					if(connections[k].Hit(p))
					{
						if(selectedEntity!=null) selectedEntity.IsSelected=false;
						selectedEntity = this.connections[k];
						selectedEntity.IsSelected = true;
						if(OnShowProps!=null)
							OnShowProps(this.connections[k]);
						if(e.Button==MouseButtons.Right)
						{
							if(OnShowProps!=null)
								OnShowProps(this);
						}
						return;
					}
				}
				//test for shapes
				for(int k=0; k<shapes.Count; k++)
				{
					if(shapes[k].Hit(p))
					{
						//shapes[k].ShapeColor = Color.WhiteSmoke;
						tracking = true;
						if(selectedEntity!=null) selectedEntity.IsSelected=false;
						selectedEntity = shapes[k];
						selectedEntity.IsSelected = true;
						refp=p;
						if(OnShowProps!=null)
							OnShowProps(this.shapes[k]);
						if(e.Button==MouseButtons.Right)
						{
							if(OnShowProps!=null)
								OnShowProps(this);
						}
						return;
					}
				}
				if(selectedEntity!=null) selectedEntity.IsSelected=false;
				selectedEntity = null;
				Invalidate();
				refp = p; //useful for all kind of things
				//nothing was selected but we'll show the props of the control in this case
				if(OnShowProps!=null)
					OnShowProps(this.proxy);
				

			
			#endregion
			

		}

		/// <summary>
		/// Handles the mouse-up event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			
			//test if we connected a connection
			if(tracking)
			{
				Point p =new Point(e.X,e.Y);
				if(typeof(Connector).IsInstanceOfType(selectedEntity))
				{
					Connector con;
					for(int k=0; k<shapes.Count; k++)
					{					
						if((con=shapes[k].HitConnector(p))!=null)
						{
							con.AttachConnector(selectedEntity as Connector);					
							tracking = false;
							return;
						}
					}
					(selectedEntity as Connector).Release();
					
				}
				tracking = false;
			}
			
		}


		/// <summary>
		/// Handles the mouse-move event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			Point p = new Point(e.X,e.Y);

			if(tracking)
			{				
				selectedEntity.Move(new Point(p.X-refp.X,p.Y-refp.Y));
				refp=p;
				Invalidate();
				if(typeof(Connector).IsInstanceOfType(selectedEntity))
				{
					HoverNone();
					//test the connecting points of hovered shapes
					for(int k=0; k<shapes.Count; k++)
					{
						//if(shapes[k].Hit(p))
					{
						shapes[k].HitConnector(p);
						//shapes[k].Invalidate();
						//return;
					}
					}
				}
			}


			//hovering stuff
			for(int k=0; k<shapes.Count; k++)
			{
				if(shapes[k].Hit(p))
				{
					if(hoveredEntity!=null) hoveredEntity.hovered = false;
					shapes[k].hovered = true;
					hoveredEntity = shapes[k];
					hoveredEntity.Invalidate();
					return;
				}
			}

			for(int k=0; k<connections.Count; k++)
			{
				if(connections[k].Hit(p))
				{
					if(hoveredEntity!=null) hoveredEntity.hovered = false;
					connections[k].hovered = true;
					hoveredEntity = connections[k];
					hoveredEntity.Invalidate();
					return;
				}
			}

			for(int k=0; k<connections.Count; k++)
			{
				if(connections[k].From.Hit(p))
				{
					connections[k].From.hovered = true;
					hoveredEntity = connections[k].From;
					hoveredEntity.Invalidate();
					return;
				}

				if(connections[k].To.Hit(p))
				{
					connections[k].To.hovered = true;
					hoveredEntity = connections[k].To;
					hoveredEntity.Invalidate();
					return;
				}
			}
			HoverNone();

			


			
		}


		#endregion

		/// <summary>
		/// Resets the hovering status of the control, i.e. the hoverEntity is set to null.
		/// </summary>
		private void HoverNone()
		{
			if(hoveredEntity!=null) 
			{
				hoveredEntity.hovered = false;
				hoveredEntity.Invalidate();
			}
			hoveredEntity = null;
		}

		#endregion
	
	}

	/// <summary>
	/// Simple proxy class for the control to display only specific properties.
	/// Not as sophisticated as the property-bag of the full Netron-control
	/// but does the job in this simple context.
	/// </summary>
	public class Proxy
	{
		#region Fields
		private GraphControl site;
		#endregion

		#region Constructor
		public Proxy(GraphControl site)
		{this.site = site;}
		#endregion

		#region Methods
		[Browsable(false)]
		public GraphControl Site
		{
			get{return site;}
			set{site = value;}
		}
		[Browsable(true), Description("The backcolor of the canvas"), Category("Layout")]
		public Color BackColor
		{
			get{return this.site.BackColor;}
			set{this.site.BackColor = value;}
		}

		[Browsable(true), Description("Gets or sets whether the grid is shown"), Category("Layout")]
		public bool ShowGrid
		{
			get{return this.site.ShowGrid;}
			set{this.site.ShowGrid = value;}
		}
		#endregion
	}
}
