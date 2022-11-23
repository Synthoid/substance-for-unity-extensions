using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
    [System.Serializable]
    public class SetSubstanceInputColorAsset : SetSubstanceInputValueAsset
    {
        [SerializeField]
        private SetSubstanceInputColorBehaviour template = new SetSubstanceInputColorBehaviour();

        public override SubstanceBindingParameter TargetInputParameter
        {
            get { return template.parameter; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<SetSubstanceInputColorBehaviour>.Create(graph, template);
        }


        public override string GetClipName()
        {
            return string.Format(kClipNameFormat, template.parameter.Name, template.value.ToString());
        }
    }
}
