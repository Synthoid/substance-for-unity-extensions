using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Contains extensions for <see cref="IList"/> related classes, including arrays and lists.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Returns true if the IList contains the given value.
        /// </summary>
        /// <typeparam name="T">Expected type for the list.</typeparam>
        /// <param name="list">IList to search.</param>
        /// <param name="value">Value to search for.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        public static bool Contains<T>(this IList<T> list, T value)
        {
            return list.Contains(value);
        }

        /// <summary>
        /// Returns true if an element matching the given predicate is found.
        /// </summary>
        /// <typeparam name="T">Expected type for the list.</typeparam>
        /// <param name="list">Elements to search through.</param>
        /// <param name="predicate">Predicate used to determine a valid target element.</param>
        /// <returns>True if an element matching the given predicate is found.</returns>
        public static bool Contains<T>(this IList<T> list, System.Predicate<T> predicate)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if(predicate.Invoke(list[i])) return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the index of the first element that matches the given predicate, or -1 if no valid element is found.
        /// </summary>
        /// <typeparam name="T">Expected type for the list.</typeparam>
        /// <param name="list">Elements to search through.</param>
        /// <param name="predicate">Predicate used to determine a valid target element.</param>
        /// <returns>Index of the first element that matches the given predicate, or -1 if no valid element is found.</returns>
        public static int IndexOf<T>(this IList<T> list, System.Predicate<T> predicate)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if(predicate.Invoke(list[i])) return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the first element that matches the given predicate.
        /// </summary>
        /// <typeparam name="T">Expected type for the list.</typeparam>
        /// <param name="list">Elements to search through.</param>
        /// <param name="predicate">Predicate used to determine a valid target element.</param>
        /// <returns>First element that matches the given predicate, or a default value.</returns>
        public static T Find<T>(this IList<T> list, System.Predicate<T> predicate)
        {
            for(int i=0; i < list.Count; i++)
            {
                if(predicate.Invoke(list[i])) return list[i];
            }

            return default(T);
        }

        /// <summary>
        /// Returns true if the given sequence matches the target sequence's length and elements exactly.
        /// </summary>
        /// <typeparam name="T">Expected type for the sequences.</typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True if the given sequence matches the target sequence's length and elements exactly.</returns>
        public static bool SequenceEquals<T>(this IList<T> a, IList<T> b) where T : System.IComparable<T>
        {
            if(a == null && b == null) return true;
            if((a == null && b != null) || (a != null && b == null)) return false;
            if(a.Count != b.Count) return false;
            
            for(int i=0; i < a.Count; i++)
            {
                if(!a[i].Equals(b[i])) return false;
            }

            return true;
        }
    }
}