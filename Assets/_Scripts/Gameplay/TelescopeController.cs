using UnityEngine;

public class TelescopeController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private Transform pivotTransform;
    [SerializeField] private Transform skyTransform;
    [SerializeField] private float rotationSpeed = 5f;

    private float _horizontalInput;

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        _horizontalInput = 0f;
    }

    private void Update()
    {
        RotateTelescope();
    }

    private void RotateTelescope()
    {
        if (!pivotTransform || !skyTransform)
            return;

        pivotTransform.rotation *= Quaternion.Euler(0, _horizontalInput * rotationSpeed * Time.deltaTime, 0);
        skyTransform.rotation = pivotTransform.rotation;
    }

    private void OnMove(Vector2 inputs) => _horizontalInput = inputs.x;
}
