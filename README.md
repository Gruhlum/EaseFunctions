<h1>
    Collection of several ease functions for Unity
</h1>

> **Ease functions translated from https://easings.net/**

## Examples

<img src="Documentation/Media/sliders.gif">

<img src="Documentation/Media/boxes.gif">

## How To Use

```csharp
    slider.value = easeFunction.GetValue(Time.time);
```
or
```csharp
    slider.value = EaseFunction.EaseOutSine(Time.time);
```