using UnityEngine;
using System;
using System.Reflection;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    public class AddMenuData
    {
        public readonly GUIContent PathLabel = null;
        public readonly Type Type = null;

        public AddMenuData(Type type)
        {
            this.Type = type;

            AddMenuAttribute attr = type.GetCustomAttribute<AddMenuAttribute>();

            if(attr != null)
            {
                PathLabel = new GUIContent(attr.path, attr.name);
            }
            else
            {
                string typeNamespace = type.Namespace.Replace('.', '/');

                PathLabel = new GUIContent(string.IsNullOrEmpty(typeNamespace) ? type.Name : typeNamespace + "/" + type.Name, type.Name);
            }
        }
    }
}