using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Ось вращения (по умолчанию Y)
    public float rotationSpeed = 10f;         // Скорость вращения (градусы в секунду)

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}