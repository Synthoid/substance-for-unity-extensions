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
    }
}