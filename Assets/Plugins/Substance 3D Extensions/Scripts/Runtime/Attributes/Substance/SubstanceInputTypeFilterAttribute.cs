using UnityEngine;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Attribute for <see cref="SubstanceParameter"/> and <see cref="SubstanceParameterValue"/> fields that filters what inputs are selectable based on their value type.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class SubstanceInputTypeFilterAttribute : PropertyAttribute
    {
        /// <summary>
        /// Filter for selectable inputs.
        /// </summary>
        public readonly SbsInputTypeFilter filter = SbsInputTypeFilter.Everything;

        /// <summary>
        /// Attribute for <see cref="SubstanceParameter"/> and <see cref="SubstanceParameterValue"/> fields that filters what inputs are selectable based on their value type.
        /// </summary>
        /// <param name="filter">Filter for selectable inputs. By default, this will display all inputs.</param>
        public SubstanceInputTypeFilterAttribute(SbsInputTypeFilter filter=SbsInputTypeFilter.Everything)
        {
            this.filter = filter;
        }
    }
}