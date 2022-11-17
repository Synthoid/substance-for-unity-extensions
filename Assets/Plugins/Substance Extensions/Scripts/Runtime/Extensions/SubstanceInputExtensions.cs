using UnityEngine;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    public static class SubstanceInputExtensions
    {
        public static Vector4Int DataVector4Int(this SubstanceInputInt4 input)
        {
            return new Vector4Int(input.Data0, input.Data1, input.Data2, input.Data3);
        }
    }
}