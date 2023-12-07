using UnityEngine;

public class InteractAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string propertyName = "";

    /// <summary>
    /// Reverse animator parameter to play interact animation
    /// </summary>
    public void PlayAnimation()
    {
        bool reverseValue = !animator.GetBool(propertyName);
        animator.SetBool(propertyName, reverseValue);
    }
}
