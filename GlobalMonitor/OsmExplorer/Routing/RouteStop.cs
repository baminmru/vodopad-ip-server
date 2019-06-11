using OsmExplorer.Rendering;
using OsmExplorer.Spatial;
using System.Drawing;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Class representing a stop along a route.
    /// </summary>
    public class RouteStop : IRenderable
    {
        /// <summary>
        /// Creates a new RouteStop with the specified location.
        /// </summary>
        /// <param name="location">LatLon location.</param>
        public RouteStop(LatLon location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets or sets the location of the RouteStop.
        /// </summary>
        public LatLon Location { get; set; }
        /// <summary>
        /// Gets or sets the System.Drawing.Pen used to render this RouteStop.
        /// </summary>
        public System.Drawing.Pen RenderPen { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }
        public int ID { get; set; }
        public System.Drawing.Image Image { get; set; }
        public System.Drawing.Image HiliteImage { get; set; } 

        /// <summary>
        /// Renders the RouteStop using the specified System.Drawing.Graphics and RenderCollection.
        /// </summary>
        /// <param name="graphics">A System.Drawing.Graphics object.</param>
        /// <param name="collection">An associated RenderCollection.</param>
        public virtual void Render(System.Drawing.Graphics graphics, RenderCollection collection)
        {
            if (this.RenderPen == null)
                this.RenderPen = new System.Drawing.Pen(System.Drawing.Color.Black, 2F);

            PointF point = collection.LatLonToPoint(this.Location);
           
            if (Image != null)
            {
                graphics.DrawImage(Image, point.X-Image.Size.Width/2,point.Y-Image.Size.Height/2 );
            }
            else
            {
                graphics.DrawEllipse(this.RenderPen, point.X - 4, point.Y - 4, 9, 9);
            }
        }
       
    }
}
