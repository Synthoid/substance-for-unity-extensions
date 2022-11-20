using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    [System.Serializable]
    public class RuntimeParameterControlGroupData
    {
        [Tooltip("Expected input type associated with these controls.")]
        public SubstanceValueType valueType = SubstanceValueType.Float;
        [Tooltip("Widget type for the default control used when no matching widget type is found.")]
        public SubstanceWidgetType defaultControl = SubstanceWidgetType.NoWidget;
        [Tooltip("[Optional] If not null, custom logic used to select a control.")]
        public InputControlSelectLogic selectLogic = null;
        [Tooltip("Controls associated with specific widget types.")]
        public RuntimeParameterControlData[] controls = new RuntimeParameterControlData[0];


        public RuntimeParameterControl GetControlPrefab(ISubstanceInput input)
        {
            SubstanceWidgetType widget = input.Description.WidgetType;

            if(selectLogic != null)
            {
                if(selectLogic.TryGetControl(input, controls, out RuntimeParameterControl control))
                {
                    return control;
                }
            }

            //Search for matching type
            for(int i=0; i < controls.Length; i++)
            {
                if (controls[i].widget == widget) return controls[i].prefab;
            }

            //Search for default
            for(int i=0; i < controls.Length; i++)
            {
                if (controls[i].widget == defaultControl) return controls[i].prefab;
            }

            return null;
        }
    }
}
