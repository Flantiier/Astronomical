using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : InteractibleObject, IPickable
{
    #region Variables
    private Rigidbody _rb;
    #endregion

    #region Properties
    public Transform Transform => transform;
    #endregion

    #region Builts_In
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        UseGravity(true);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Enable or disable gravity on rb
    /// </summary>
    private void UseGravity(bool useGravity)
    {
        _rb.useGravity = useGravity;
        _rb.isKinematic = !useGravity;
    }
    #endregion

    #region Interface Implementation
    public void PickUp(Transform parent)
    {
        UseGravity(false);
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop(Transform m_transform)
    {
        transform.SetParent(null);
        transform.position = m_transform.position;
        transform.rotation = m_transform.rotation;
        UseGravity(true);
    }
    #endregion
}
