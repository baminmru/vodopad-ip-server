namespace OsmExplorer.Data 
{
    /// <summary>
    /// Delegate method that reports import progress of the OsmXmlImporter class.
    /// </summary>
    /// <param name="Progress">Progress percentage of an .osm XML file import.</param>
    /// <param name="UserState">Status of the import.</param>
    public delegate void XmlImportProgress(int Progress, OsmXmlImporter.ImportStatus UserState);
}