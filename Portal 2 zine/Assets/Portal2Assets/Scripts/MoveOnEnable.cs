using UnityEngine;

public class MoveOnEnable : MonoBehaviour
{
    public float activeY = 0f;       // Позиция по Y при активации
    public float inactiveY = -5f;    // Позиция по Y при деактивации
    public float speed = 2f;         // Скорость движения

    private void Awake()
    {
        // Стартуем всегда с позиции inactiveY
        Vector3 pos = transform.localPosition;
        pos.y = inactiveY;
        transform.localPosition = pos;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToY(activeY));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        transform.localPosition = new Vector3(transform.localPosition.x, inactiveY, transform.localPosition.z);
    }

    private System.Collections.IEnumerator MoveToY(float targetY)
    {
        Vector3 pos = transform.localPosition;

        while (Mathf.Abs(pos.y - targetY) > 0.01f)
        {
            pos.y = Mathf.MoveTowards(pos.y, targetY, speed * Time.deltaTime);
            transform.localPosition = pos;
            yield return null;
        }

        pos.y = targetY;
        transform.localPosition = pos;
    }
}