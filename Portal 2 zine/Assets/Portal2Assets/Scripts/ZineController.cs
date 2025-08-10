using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class ZineController : MonoBehaviour
{
    [FormerlySerializedAs("book")] [Header("Book Settings")]
    public RectTransform zine;        // Объект книги
    public AutoFlip autoFlip;
    public Book book;
    public Vector3 startPosition;     // Стартовая позиция
    public Vector3 endPosition;       // Конечная позиция
    public Vector3 startPositionend;     // Стартовая позиция в конце
    public Vector3 endPositionend;      // Конечная позиция в конце
    private float time;               // Время анимации
    public Animator zineAnimator;
    public GameObject zinePages;
    public GameObject zineParticles;

    private Coroutine flipCoroutine;


    private void Start()
    {
        zinePages.SetActive(false);
    }

    // Двигаем от старта к концу
    public void MoveForward()
    {
        if (zine == null) return;

        if (flipCoroutine != null && autoFlip.isFlipping != true)
            StopCoroutine(flipCoroutine);
        if(book.currentPage is 0 && autoFlip.isFlipping != true)
            flipCoroutine = StartCoroutine(MoveAnimation(startPosition, endPosition));
        
        if(book.currentPage is 86  && autoFlip.isFlipping != true )
            flipCoroutine = StartCoroutine(MoveAnimation(startPositionend, endPositionend));   
    }

    // Двигаем от конца к старту
    public void MoveBackward()
    {
        if (zine == null) return;

        if (flipCoroutine != null && autoFlip.isFlipping != true)
            StopCoroutine(flipCoroutine);
        
        if(book.currentPage is 2  && autoFlip.isFlipping != true )
            flipCoroutine = StartCoroutine(MoveAnimation(endPosition, startPosition));
        
        if(book.currentPage is 88 && autoFlip.isFlipping != true)
            flipCoroutine = StartCoroutine(MoveAnimation(endPositionend, startPositionend));
    }

    private IEnumerator MoveAnimation(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        zine.anchoredPosition = from;
        time = autoFlip.PageFlipTime * 0.9f; // минус 10%

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            zine.anchoredPosition = Vector3.Lerp(from, to, t);
            yield return null;
        }

        zine.anchoredPosition = to;
    }
    
    // Открыть книгу
    public void OpenZine()
    {
        if (zineAnimator != null)
        {
            zineAnimator.SetBool("isOpened", true);
            zinePages.SetActive(true);
            zineParticles.SetActive(false);
        }
        else
            Debug.LogWarning("Zine Animator не назначен!");
    }

    // Закрыть книгу
    public void CloseZine()
    {
        if (zineAnimator != null)
        {
            zineAnimator.SetBool("isOpened", false);
            zinePages.SetActive(false);
            zineParticles.SetActive(true);
        }
        else
            Debug.LogWarning("Zine Animator не назначен!");
    }
}