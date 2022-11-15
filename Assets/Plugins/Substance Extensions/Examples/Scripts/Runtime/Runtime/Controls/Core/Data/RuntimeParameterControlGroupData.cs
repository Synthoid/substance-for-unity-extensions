using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Examples
{
    [System.Serializable]
    public class RuntimeParameterControlGroupData
    {
        public SubstanceValueType valueType = SubstanceValueType.Float;
        public SubstanceWidgetType defaultControl = SubstanceWidgetType.NoWidget;
        public RuntimeParameterControlData[] controls = new RuntimeParameterControlData[0];


        public RuntimeParameterControl GetControlPrefab(SubstanceWidgetType widget)
        {
            for(int i=0; i < controls.Length; i++)
            {
                if (controls[i].widget == widget) return controls[i].prefab;
            }

            for(int i=0; i < controls.Length; i++)
            {
                if (controls[i].widget == defaultControl) return controls[i].prefab;
            }

            return null;
        }
    }
}
