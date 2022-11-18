using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Shows a warning when referencing a <see cref="SubstanceGraphSO"/> asset that is not marked as runtime.
    /// </summary>
    public class RuntimeGraphOnlyAttribute : PropertyAttribute
    {
        private const string kDefaultWarning = "Target substance is not marked as runtime!";

        public readonly string warning = "";

        /// <summary>
        /// Shows a warning when referencing a <see cref="SubstanceGraphSO"/> asset that is not marked as runtime.
        /// </summary>
        /// <param name="warning">Text shown when a non-runtime substance graph is referenced.</param>
        public RuntimeGraphOnlyAttribute(string warning=kDefaultWarning)
        {
            this.warning = warning;
        }
    }
}