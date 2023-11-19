using UnityEngine;

public class SimpleInteractionObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Interact with object.";

    [SerializeField] private Animator animator;
    [SerializeField] private string propertyName = "";

    public bool IsInteractable { get; set; } = true;

    public virtual void Interact(PlayerInteract interactor)
    {
        PlayInteractAnimation();
    }

    public string GetInteractText() => interactText;

    protected void PlayInteractAnimation()
    {
        bool reverseValue = !animator.GetBool(propertyName);
        animator.SetBool(propertyName, reverseValue);
    }
}
