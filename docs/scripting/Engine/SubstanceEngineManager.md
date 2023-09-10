# SubstanceEngineManager
Wraps the Substance plugin's `Engine` class and streamlines interactions with it. This class can conveniently initialize the engine on `Start()` if desired, and fires events for engine initialization and shutdown stages.

<picture>
  <img alt="SubstanceEngineManager inspector" src="/docs/img/Inspectors/SubstanceEngineManager01.png" width="571" height="398">
</picture>

***Example Script***
```C#
using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

public class SubstanceParameterExample : MonoBehaviour
{
	private void OnEngineInitialized()
	{
		Debug.Log("Engine Initialized!");
	}


	private void OnEnginePreShutdown()
	{
		Debug.Log("Engine PRE Shutdown!");
	}


	private void OnEnginePostShutdown()
	{
		Debug.Log("Engine POST Shutdown!");
	}


	private void Start()
	{
		SubstanceEngineManager.Instance.onEngineInitialized.AddListener(OnEngineInitialized);
		SubstanceEngineManager.Instance.onEnginePreShutdown.AddListener(OnEnginePreShutdown);
		SubstanceEngineManager.Instance.onEnginePostShutdown.AddListener(OnEnginePostShutdown);
	}
}
```

## Properties

| Property | Type | Description |
| -------- | ---- | ----------- |
| IsInitialized | `bool` | Returns true if the runtime substance engine has been initialized. |
| RuntimeEngineType | `EngineType` | How the engine is managed. |
| RuntimeEngineInstance | `SubstanceRuntime` | Returns true if the runtime substance engine has been initialized. |
| onEngineInitialized | `UnityEvent` | Fired after the runtime engine is initialized. |
| onEnginePreShutdown | `UnityEvent` | Fired *BEFORE* the runtime engine is shutdown. |
| onEnginePostShutdown | `UnityEvent` | Fired *AFTER* the runtime engine is shutdown. |

## Methods

| Method | Returns | Description |
| ------ | ------- | ----------- |
| InitializeEngine() | void | Initialize the substance engine. Note: This will do nothing if the engine is already initialized. |
| ShutdownEngine() | void | Shutdown the substance engine. Note: This will do nothing if the engine is not currently initialized. |