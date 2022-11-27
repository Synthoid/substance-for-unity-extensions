using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
    /// <summary>
    /// Base class for playable behaviours created by <see cref="SetSubstanceInputColorAsset"/>classes.
    /// </summary>
    public abstract class SetSubstanceInputValueBehaviour : PlayableBehaviour
    {
        /// <summary>
        /// Input parameter targeted by the behaviour.
        /// </summary>
        public abstract ISubstanceInputParameter Parameter { get; }
        /// <summary>
        /// Raw object value for the target input parameter.
        /// </summary>
        public abstract object ValueRaw { get; }
    }
}