using UnityEngine;
using Zenject;

public class ColorManager : IInitializable
{
    private float _currentHue;
    private float _hueStep = 0.01f;
    public void Initialize()
    {
        _currentHue = Random.Range(0f, 1f);
    }
    public Color GetNextColor()
    {
        Color color = Color.HSVToRGB(_currentHue % 1f, 0.8f, 0.9f);
        _currentHue += _hueStep;
        return color;
    }


}
