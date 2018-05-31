using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OsmExplorer.Collections
{
    /// <summary>
    /// Maintains a set of items sorted by priority with O(log n) insertion and removal.
    /// </summary>
    /// <typeparam name="P">IComparable value type</typeparam>
    /// <typeparam name="T">Item type to sort by priority</typeparam>
    [DebuggerStepThrough]
    [DebuggerDisplayAttribute("Count = {Count}")]
    public class PriorityHeap<P, T> where P : IComparable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private IPriorityHeap<P, T> m_list;

        /// <summary>
        /// Creates a new PriorityHeap with the default comparer 
        /// (items sorted smallest to largest) and the default TieBreakingMode (FIFO).
        /// </summary>
        public PriorityHeap() 
        {
            m_list = new PriorityQueue<P, T>();
        }
        /// <summary>
        /// Creates a new PriorityHeap with the default comparer 
        /// (items sorted smallest to largest) and the specified TieBreakingMode.
        /// </summary>
        /// <param name="tMode"></param>
        public PriorityHeap(TieBreakingMode tMode) 
        {
            switch (tMode)
            {
                case TieBreakingMode.FIFO:
                    m_list = new PriorityQueue<P, T>();
                    return;
                case TieBreakingMode.LIFO:
                    m_list = new PriorityStack<P, T>();
                    return;
            }
        }
        /// <summary>
        /// Creates a new PriorityHeap with the specified IComparer and
        /// the default TieBreakingMode (FIFO).
        /// </summary>
        /// <param name="comparer">A comparer for the priority values</param>
        public PriorityHeap(IComparer<P> comparer) 
        {
            m_list = new PriorityQueue<P, T>(comparer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        /// <param name="tMode"></param>
        public PriorityHeap(IComparer<P> comparer, TieBreakingMode tMode)
        {
            switch (tMode) 
            {
                case TieBreakingMode.FIFO:
                    m_list = new PriorityQueue<P, T>(comparer);
                    return;
                case TieBreakingMode.LIFO:
                    m_list = new PriorityStack<P, T>(comparer);
                    return;
            }
        }

        /// <summary>
        /// Adds an item with a specified priority to the PriorityHeap
        /// </summary>
        /// <param name="priority">Priority value</param>
        /// <param name="value">Item to be added</param>
        /// <remarks>This is an O(log n) operation where n is the number of items in the PriorityStack</remarks>
        public void Push(P priority, T value)
        {
            m_list.Push(priority, value);
        }
        /// <summary>
        /// Removes and returns the item at the top of the PriorityHeap
        /// </summary>
        /// <returns>Item with highest priority</returns>
        /// <remarks>This is an O(log n) operation where n is the number of items in the PriorityStack</remarks>
        public T Pop()
        {
            try
            {
                return m_list.Pop();
            }
            catch (InvalidOperationException ex) 
            {
                throw new InvalidOperationException("PriorityHeap empty.", ex);
            }
        }
        /// <summary>
        /// Returns the item at the top of the PriorityHeap without removing it.
        /// </summary>
        /// <returns>The item with the highest priority</returns>
        /// <remarks>This is an O(1) operation</remarks>
        public T Peek()
        {
            try
            {
                return m_list.Peek();
            }
            catch (InvalidOperationException ex) 
            {
                throw new InvalidOperationException("PriorityHeap empty.", ex);
            }
        }
        /// <summary>
        /// Returns whether the PriorityHeap is empty or not. This is an O(1) operation.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return m_list.IsEmpty;
            }
        }
        /// <summary>
        /// Number of items in the PriorityHeap.
        /// </summary>
        public int Count 
        {
            get 
            {
                return m_list.Count;
            }
        }

        private interface IPriorityHeap<p, t>
        {
            void Push(p priority, t value);
            t Pop();
            t Peek();
            bool IsEmpty { get; }
            int Count { get; }
        }
        private class PriorityQueue<p, t> : IPriorityHeap<p, t> 
        {
            private int m_Count;
            private SortedDictionary<p, Queue<t>> m_list;

            public PriorityQueue() 
            {
                m_list = new SortedDictionary<p, Queue<t>>();
                m_Count = 0;
            }
            public PriorityQueue(IComparer<p> comparer) 
            {
                m_list = new SortedDictionary<p, Queue<t>>(comparer);
                m_Count = 0;
            }

            public void Push(p priority, t value)
            {
                Queue<t> q;
                if (!m_list.TryGetValue(priority, out q))
                {
                    q = new Queue<t>();
                    m_list.Add(priority, q);
                }
                q.Enqueue(value);
                m_Count++;
            }
            public t Pop()
            {
                try
                {
                    var kvp = m_list.First();
                    var value = kvp.Value.Dequeue();
                    m_Count--;
                    if (kvp.Value.Count == 0)
                        m_list.Remove(kvp.Key);
                    return value;
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            public t Peek()
            {
                try
                {
                    return m_list.First().Value.Peek();
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            public bool IsEmpty
            {
                get 
                {
                    return !m_list.Any();
                }
            }
            public int Count
            {
                get 
                {
                    return m_Count;
                }
            }
        }
        private class PriorityStack<p, t> : IPriorityHeap<p, t>
        {
            private int m_Count;
            private SortedDictionary<p, Stack<t>> m_list;

            public PriorityStack() 
            {
                m_Count = 0;
                m_list = new SortedDictionary<p, Stack<t>>();
            }
            public PriorityStack(IComparer<p> comparer) 
            {
                m_Count = 0;
                m_list = new SortedDictionary<p, Stack<t>>(comparer);
            }

            public void Push(p priority, t value)
            {
                Stack<t> q;
                if (!m_list.TryGetValue(priority, out q))
                {
                    q = new Stack<t>();
                    m_list.Add(priority, q);
                }
                q.Push(value);
                m_Count++;
            }
            public t Pop()
            {
                try
                {
                    var kvp = m_list.First();
                    var value = kvp.Value.Pop();
                    m_Count--;
                    if (kvp.Value.Count == 0)
                        m_list.Remove(kvp.Key);
                    return value;
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            public t Peek()
            {
                try
                {
                    return m_list.First().Value.Peek();
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            public bool IsEmpty
            {
                get
                {
                    return !m_list.Any();
                }
            }
            public int Count
            {
                get
                {
                    return m_Count;
                }
            }
        }
    }
}
