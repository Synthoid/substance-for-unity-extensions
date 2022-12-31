# SubstanceOutput
Allows easy selection of Substance output textures via the inspector. The selected output texture's identifier as specified in Substance Designer (ie "basecolor") is stored.

<picture>
  <img alt="SubstanceOutput search window" src="/docs/img/Inspectors/SubstanceOutput01.png" width="354" height="352">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SubstanceOutputExample : MonoBehaviour
{
    [Tooltip("Substance to reference output textures from.")]
    public SubstanceGraphSO substance;
    [Tooltip("Output to reference.")]
    public SubstanceOutput output = new SubstanceOutput();

    private void LogOutputTextureName()
    {
        Texture2D texture = substance.GetOutputTexture(outputs[i].Name);

        Debug.Log(texture == null ? "<Null>" : texture.name);
    }


    private void Start()
    {
        SpawnOutputPreviews();
    }
}
```