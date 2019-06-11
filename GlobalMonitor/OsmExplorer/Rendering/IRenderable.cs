namespace OsmExplorer.Rendering
{
    /// <summary>
    /// Interface for objects that can be rendered within a RenderCollection.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Gets or sets a System.Drawing.Pen used for rendering.
        /// </summary>
        System.Drawing.Pen RenderPen { get; set; }
        /// <summary>
        /// Renders a given IRenderable object.
        /// </summary>
        /// <param name="graphics">A System.Drawing.Graphics object.</param>
        /// <param name="collection">An associated RenderCollection.</param>
        void Render(System.Drawing.Graphics graphics, RenderCollection collection);
    }
}
