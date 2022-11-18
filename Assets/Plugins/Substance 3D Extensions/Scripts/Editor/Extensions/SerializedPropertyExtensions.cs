using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SerializedPropertyExtensions
    {
        /// <summary>
        /// Returns true if the property is an immediate child property of an array property.
        /// </summary>
        /// <param name="property">Property to check for array element status.</param>
        /// <returns>True if the property is an immediate child property of an array property.</returns>
        public static bool IsArrayElement(this SerializedProperty property)
        {
            string path = property.propertyPath;

            return path[path.Length - 1] == ']';
        }
    }
}