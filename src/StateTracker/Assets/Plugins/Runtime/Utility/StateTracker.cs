using System;
using System.Collections.Generic;

namespace AmarilisLib
{
    /// <summary>
    /// Tracks the state of an object, allowing undo and redo functionality.
    /// </summary>
    /// <typeparam name="T">The type of state being tracked.</typeparam>
    public class StateTracker<T>
    {
        private readonly LinkedList<T> stateList = new LinkedList<T>();
        private LinkedListNode<T> currentNode;
        private readonly object lockObject = new object();

        /// <summary>
        /// Gets the current state.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when no state has been set.</exception>
        public T CurrentState
        {
            get
            {
                lock(lockObject)
                {
                    if(currentNode == null)
                    {
                        throw new InvalidOperationException("No state has been set.");
                    }
                    return currentNode.Value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateTracker{T}"/> class with an initial state.
        /// </summary>
        /// <param name="initialState">The initial state to set.</param>
        public StateTracker(T initialState)
        {
            lock(lockObject)
            {
                currentNode = stateList.AddLast(initialState);
            }
        }

        /// <summary>
        /// Saves a new state and makes it the current state. All states that can be redone are removed.
        /// </summary>
        /// <param name="state">The new state to save.</param>
        /// <returns>The current state after saving the new state.</returns>
        public T Save(T state)
        {
            lock(lockObject)
            {
                if(currentNode.Next != null)
                {
                    while(stateList.Last != currentNode)
                    {
                        stateList.RemoveLast();
                    }
                }
                currentNode = stateList.AddLast(state);
                return CurrentState;
            }
        }

        /// <summary>
        /// Reverts to the previous state, if available.
        /// </summary>
        /// <returns>The current state after undoing.</returns>
        public T Undo()
        {
            lock(lockObject)
            {
                if(currentNode.Previous != null)
                {
                    currentNode = currentNode.Previous;
                }
                return CurrentState;
            }
        }

        /// <summary>
        /// Advances to the next state, if available.
        /// </summary>
        /// <returns>The current state after redoing.</returns>
        public T Redo()
        {
            lock(lockObject)
            {
                if(currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                return CurrentState;
            }
        }

        /// <summary>
        /// Gets the history of all saved states.
        /// </summary>
        /// <returns>An enumerable collection of all saved states.</returns>
        public IEnumerable<T> GetHistory()
        {
            lock(lockObject)
            {
                foreach(var state in stateList)
                {
                    yield return state;
                }
            }
        }
    }
}
