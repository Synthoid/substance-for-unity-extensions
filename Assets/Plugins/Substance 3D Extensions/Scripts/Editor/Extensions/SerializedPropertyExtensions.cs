using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SerializedPropertyExtensions
    {
        private const string kArrayPropertyPathPrefix = ".Array.data[";

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


        /// <summary>
        /// Returns the parent <see cref="SerializedProperty"/> for the target property.
        /// </summary>
        /// <param name="property">Property to get the parent property of.</param>
        /// <returns>Parent <see cref="SerializedProperty"/> for the property, or null if no parent exists.</returns>
        public static SerializedProperty GetParentProperty(this SerializedProperty property)
        {
            //Handle array properties...
            string propertyPath = property.propertyPath.Replace(kArrayPropertyPathPrefix, "[");

            int index = propertyPath.LastIndexOf('.');

            if(index < 0) return null; //Property path has no parent, so return null...

            string parentPath;

            if (propertyPath.Contains('['))
            {
                parentPath = propertyPath.Substring(0, propertyPath.LastIndexOf('['));
            }
            else
            {
                parentPath = propertyPath.Substring(0, index);
            }

            return property.serializedObject.FindProperty(parentPath);
        }


        public static FieldInfo GetPropertyFieldInfo(this SerializedProperty property)
        {
            string propertyPath = property.propertyPath.Replace(kArrayPropertyPathPrefix, "[");
            object obj = property.serializedObject.targetObject;
            string[] paths = propertyPath.Split('.');

            for (int i = 0; i < paths.Length - 1; i++)
            {
                if(paths[i].Contains('['))
                {
                    string subPath = paths[i].Substring(0, paths[i].IndexOf('['));
                    int.TryParse(paths[i].Substring(subPath.Length + 1, (paths[i].IndexOf(']') - 1) - subPath.Length), out int index);

                    obj = GetPropertyObject(obj, subPath, index);
                }
                else
                {
                    obj = GetPropertyObject(obj, paths[i]);
                }
            }

            Type sourceType = obj.GetType();

            return sourceType.GetField(paths[paths.Length - 1], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }





        public static object GetPropertyObject(this SerializedProperty property)
        {
            string propertyPath = property.propertyPath.Replace(kArrayPropertyPathPrefix, "[");
            object obj = property.serializedObject.targetObject;
            string[] paths = propertyPath.Split('.');

            for(int i=0; i < paths.Length; i++)
            {
                if(paths[i].Contains('['))
                {
                    string subPath = paths[i].Substring(0, paths[i].IndexOf('['));
                    int.TryParse(paths[i].Substring(subPath.Length + 1, (paths[i].IndexOf(']') - 1) - subPath.Length), out int index);

                    obj = GetPropertyObject(obj, subPath, index);
                }
                else
                {
                    obj = GetPropertyObject(obj, paths[i]);
                }
            }

            return obj;
        }


        private static object GetPropertyObject(object source, string fieldName)
        {
            if(source == null) return null;

            Type sourceType = source.GetType();

            while(sourceType != null)
            {
                FieldInfo fieldInfo = sourceType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                
                if(fieldInfo != null) return fieldInfo.GetValue(source);

                sourceType = sourceType.BaseType;
            }
            
            return null;
        }


        private static object GetPropertyObject(object source, string fieldName, int index)
        {
            IEnumerable enumerable = GetPropertyObject(source, fieldName) as IEnumerable;

            if(enumerable == null) return null;

            IEnumerator enumerator = enumerable.GetEnumerator();

            for(int i=0; i <= index; i++)
            {
                if(!enumerator.MoveNext()) return null;
            }

            return enumerator.Current;
        }
    }
}