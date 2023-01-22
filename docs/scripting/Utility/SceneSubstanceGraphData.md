# SceneSubstanceGraphData
Handles automatically obtaining references to `SubstanceGraphSO` assets referenced in open scenes. Discovered substance graphs will be stored alphabetically in the `graphs` array. You can customize how substance references are detected by adjusting `Grab Material Settings` at the top of the asset's inspector.

Substance graphs are detected by finding all `Renderer` components in the scene and validating their materials to check if they are associated with a `SubstanceGraphSO` asset.

Any `ISubstanceProvider` components in the scene will also be detected and their referenced substance graphs will be included too.

<picture>
  <img alt="SceneSubstanceGraphData asset populated with some graphs referenced in the scene." src="/docs/img/Inspectors/SceneSubstanceGraphData01.png" width="572" height="372">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SceneSubstanceGraphdDataExample : MonoBehaviour
{
    [Tooltip("Data for substances in the scene.")]
    public SceneSubstanceGraphData substances = null;
	
	private void Start()
	{
		for(int i=0; i < substances.graphs.Length; i++)
		{
			Debug.Log(substances.graphs[i].name);
		}
	}
}
```

## See Also

 - [ISubstanceProvider](/docs/scripting/Interfaces/ISubstanceProvider.md)
 
## Properties

Property | Description
-------- | -----------
graphs | Graphs referenced in the scene.