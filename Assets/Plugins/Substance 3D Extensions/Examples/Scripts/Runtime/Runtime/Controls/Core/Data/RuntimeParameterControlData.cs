using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    [System.Serializable]
    public class RuntimeParameterControlData
    {
        [Tooltip("Widget associated with this control.")]
        public SubstanceWidgetType widget = SubstanceWidgetType.NoWidget;
        [Tooltip("Prefab spawned for this control.")]
        public RuntimeParameterControl prefab = null;
    }
}