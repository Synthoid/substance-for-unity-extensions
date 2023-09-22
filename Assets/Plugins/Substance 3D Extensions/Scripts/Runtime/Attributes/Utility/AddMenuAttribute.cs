using UnityEngine;
using System;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Attribute used to flag classes to use custom paths in add menu interactions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple=false)]
    public class AddMenuAttribute : PropertyAttribute
    {
        public readonly string path = "";
        public readonly string name = "";

        public AddMenuAttribute(string path, string name="")
        {
            this.path = path;
            this.name = name;
        }
    }
}