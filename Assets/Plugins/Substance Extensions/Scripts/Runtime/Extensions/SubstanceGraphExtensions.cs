using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions
{
    public static class SubstanceGraphExtensions
    {
        #region Inputs

        public static ISubstanceInput GetInput(this SubstanceGraphSO substance, string name)
        {
            return GetInput(substance, GetInputIndex(substance, name));
        }


        public static ISubstanceInput GetInput(this SubstanceGraphSO substance, int index)
        {
            return substance.Input[index];
        }


        public static T GetInput<T>(this SubstanceGraphSO substance, string name) where T : ISubstanceInput
        {
            return GetInput<T>(substance, GetInputIndex(substance, name));
        }


        public static T GetInput<T>(this SubstanceGraphSO substance, int index) where T : ISubstanceInput
        {
            return (T)substance.Input[index];
        }


        public static int GetInputIndex(this SubstanceGraphSO substance, string name)
        {
            for(int i = 0; i < substance.Input.Count; i++)
            {
                if(substance.Input[i].Description.Identifier == name)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region Runtime Editing

        /// <summary>
        /// Initialize a substance for runtime and return a handler to begin editing it. Note, the returned handler must be disposed when you are done with it.
        /// </summary>
        /// <param name="substance">Substance to begin editing.</param>
        /// <returns><see cref="SubstanceNativeHandler"/> controlling the substance editing.</returns>
        public static SubstanceNativeGraph BeginRuntimeEditing(this SubstanceGraphSO substance)
        {
            SubstanceNativeGraph handler = Engine.OpenFile(substance.RawData.FileContent, substance.Index);

            substance.RuntimeInitialize(handler, true);

            /*for(int i = 0; i < substance.Instances.Count; i++)
            {
                substance.Instances[i].RuntimeInitialize(handler);
            }*/

            return handler;
        }


        public static void EndRuntimeEditing(this SubstanceGraphSO substance, SubstanceNativeGraph handler)
        {
            handler.Dispose();
        }

        #endregion

        #region Int2

        public static Vector2Int GetOutputSize(this SubstanceGraphSO substance)
        {
            return GetInt2(substance, SubstanceFileExtensions.PARAM_OUTPUT_SIZE);
        }

        public static Vector2Int GetInt2(this SubstanceGraphSO substance, string name)
        {
            return GetInt2(substance, GetInputIndex(substance, name));
        }


        public static Vector2Int GetInt2(this SubstanceGraphSO substance, int index)
        {
            SubstanceInputInt2 input = GetInput<SubstanceInputInt2>(substance, index);

            return input == null ? Vector2Int.zero : input.Data;
        }

        #endregion
    }
}