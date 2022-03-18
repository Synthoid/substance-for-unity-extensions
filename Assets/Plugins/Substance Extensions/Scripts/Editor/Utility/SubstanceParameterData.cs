using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensionsEditor
{
    public struct SubstanceParameterData
    {
        public string name;
        public int index;
        public SubstanceValueType type;
        public SubstanceWidgetType widget;

        public SubstanceParameterData(ISubstanceInput input)
        {
            this.name = input.Description.Identifier;
            this.index = input.Index;
            this.type = input.ValueType;
            this.widget = input.Description.WidgetType;
        }
    }
}