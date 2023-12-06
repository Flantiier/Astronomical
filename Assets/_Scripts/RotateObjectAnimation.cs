using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class RotateObjectAnimation : MonoBehaviour
{
    enum RotateDirection { Clockwise, CounterClockwise }
    enum RotateAxis { X, Y, Z }

    [Header("Animation properties")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private RotateDirection rotateDirection;
    [SerializeField] private RotateAxis rotateAxis = RotateAxis.Y;

    [Range(0f, 360f)] public float startAngle = 0f;
    [SerializeField, Range(1f, 360f)] private float targetAngle = 90f;

    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float speedMultiplier = 1f;


    [Header("Editor section")]
    [SerializeField] private RotateAnimationEditorProperties editorProperties;

#if UNITY_EDITOR
    private void Update()
    {
        RunEditMode();
    }
#endif

    /// <summary>
    /// Method to start playing the animation
    /// </summary>
    [ContextMenu("Play Animation")]
    public void PlayAnimation() => StartCoroutine("RotateAnimationRoutine");

    /// <summary>
    /// Simulates a rotation animation around a target tarnsform with a given angle
    /// </summary>
    private void RotateAnimation(float progressValue)
    {
        if (!targetTransform)
            return;

        float evaluatedAngle = animationCurve.Evaluate(progressValue) * targetAngle;
        float angleValue = rotateDirection == RotateDirection.Clockwise ? startAngle + evaluatedAngle : startAngle - evaluatedAngle;

        ModifyEulersVector(angleValue);
    }

    /// <summary>
    /// Coroutine that play the animation over time
    /// </summary>
    private IEnumerator RotateAnimationRoutine()
    {
        ResetAnimation();

        float progress = 0f;

        while (progress < 1)
        {
            RotateAnimation(progress);
            progress += speedMultiplier * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Running code while being in editor mode to easily modify the animation
    /// </summary>
    private void RunEditMode()
    {
        if (!targetTransform || !editorProperties.enableEditMode)
            return;

        float editProgress = editorProperties.progressValue;
        RotateAnimation(editProgress);
    }

    /// <summary>
    /// Reset target transform rotation to start value
    /// </summary>
    private void ResetAnimation()
    {
        ModifyEulersVector(startAngle);
    }

    /// <summary>
    /// Modify eulers on rotate axis by a given value
    /// </summary>
    private void ModifyEulersVector(float targetValue)
    {
        Quaternion rotation = targetTransform.localRotation;

        switch (rotateAxis)
        {
            case RotateAxis.X:
                targetTransform.localRotation = Quaternion.Euler(targetValue, rotation.y, rotation.z);
                break;
            case RotateAxis.Y:
                targetTransform.localRotation = Quaternion.Euler(rotation.x, targetValue, rotation.z);
                break;
            case RotateAxis.Z:
                targetTransform.localRotation = Quaternion.Euler(rotation.x, rotation.y, targetValue);
                break;
        }
    }
}

[Serializable]
public class RotateAnimationEditorProperties
{
    public bool enableEditMode = false;
    [Range(0f, 1f)] public float progressValue = 0f;
}
