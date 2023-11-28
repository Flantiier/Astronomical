using UnityEngine;

public class AnimatedLight : MonoBehaviour
{
    [SerializeField] private Gradient colors;
    [SerializeField] private float speedMultiplier = 0.2f;
    [SerializeField] private AnimationCurve intensity;
    [SerializeField] private float intensityMultiplier = 2.5f;

    private Light _light;
    private float _animationLoopTime = 0f;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        _animationLoopTime += Time.deltaTime * speedMultiplier;
        _animationLoopTime = Mathf.Repeat(_animationLoopTime, 1);

        _light.color = colors.Evaluate(_animationLoopTime);
        _light.intensity = intensity.Evaluate(_animationLoopTime) * intensityMultiplier;
    }
}
