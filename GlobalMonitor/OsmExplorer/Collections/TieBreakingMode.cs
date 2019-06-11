
namespace OsmExplorer.Collections
{
    /// <summary>
    /// Designates how a PriorityHeap breaks ties between values of equal priority.
    /// See remarks.
    /// </summary>
    public enum TieBreakingMode
    {
        /// <summary>
        /// Objects that have the same priority will be removed from the
        /// heap in a first-in, first-out order (queue) when PriorityHeap.Pop()
        /// is called, i.e. items pushed into the heap earlier for a given
        /// priority will be removed first.
        /// </summary>
        FIFO,
        /// <summary>
        /// Objects that have the same priority will be removed from the
        /// heap in a first-in, last-out order (stack) when PriorityHeap.Pop()
        /// is called, i.e. items pushed into the heap the lastest for a given
        /// priority will be removed first.
        /// </summary>
        LIFO
    }
}
