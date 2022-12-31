using UnityEngine;
using UnityEngine.Playables;

namespace SOS.SubstanceExtensions.Timeline
{
#if UNITY_EDITOR
    [System.ComponentModel.DisplayName("Set Substance Input Texture Clip")]
#endif
    [System.Serializable]
    public class SetSubstanceInputTextureAsset : SetSubstanceInputValueAsset<SetSubstanceInputTextureBehaviour>
    {
        public override string GetClipName()
        {
            return string.Format(kClipNameFormat, string.IsNullOrEmpty(Template.Parameter.Name) ? "<None>" : Template.Parameter.Name, template.value == null ? "<NULL>" : template.value.name);
        }
    }
}