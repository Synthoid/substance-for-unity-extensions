# Timeline and Substance for Unity

This is the documentation index for added Timeline functionality to Substance for Unity.

<picture>
  <img alt="Substance in Timeline" src="/docs/img/Extensions/Timeline/TimelineExample.png" width="515" height="228">
</picture>

## Tracks

All Timeline functionality for Substance for Unity is controlled by the `SubstanceTrack`. This track accepts a `SubstanceGraphSO` asset as a binding and can be used to drive input values and render the bound substance.

## Clips
These classes handle interacting with substances via Timeline.

### Values
Clips that set input values on bound substances' associated `SubstanceNativeGraph`.

| Clip | Description |
| ---- | ----------- |
| `SetSubstanceInputColorAsset` | Sets a color input value. |
| `SetSubstanceInputFloatAsset` | Sets a float input value. |
| `SetSubstanceInputFloat2Asset` | Sets a float2 input value. |
| `SetSubstanceInputFloat3Asset` | Sets a float3 input value. |
| `SetSubstanceInputFloat4Asset` | Sets a float4 input value. |
| `SetSubstanceInputIntAsset` | Sets an int input value. |
| `SetSubstanceInputInt2Asset` | Sets an int2 input value. |
| `SetSubstanceInputInt3Asset` | Sets an int3 input value. |
| `SetSubstanceInputInt4Asset` | Sets an int4 input value. |
| `SetSubstanceInputStringAsset` | Sets a string input value. |
| `SetSubstanceInputTextureAsset` | Sets an image input value. |

### Rendering
Clips that handle rendering substances.

| Clip | Description |
| ---- | ----------- |
| `RenderSubstanceAsset` | Renders the track's bound substance. |
