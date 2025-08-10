using UnityEngine;
using System.Collections.Generic;

public class EnvironmentManager : MonoBehaviour
{
    public Book book;

    [System.Serializable]
    public class TextureSwap
    {
        public Renderer targetRenderer;
        public Texture newTexture;
    }

    [System.Serializable]
    public class PageEnvironmentTrigger
    {
        public int pageNumber;                     // На какой странице срабатывает
        public List<GameObject> activateObjects;   // Объекты, которые включаем
        public List<GameObject> deactivateObjects; // Объекты, которые выключаем
        public List<TextureSwap> textureSwaps;     // Замены текстур

        public bool playSound = true;              // Проигрывать звук при срабатывании триггера
    }

    [Header("Триггеры изменения окружения")]
    public List<PageEnvironmentTrigger> triggers = new List<PageEnvironmentTrigger>();

    [Header("Аудио")]
    public AudioSource audioSource;          // Общий источник звука
    public AudioClip globalTriggerSound;     // Общий звук для всех триггеров

    // Запоминаем последний сработавший триггер
    private int lastTriggeredPage = -1;

    public void OnPageFlipped()
    {
        foreach (var trigger in triggers)
        {
            if (trigger.pageNumber == book.currentPage)
            {
                if (lastTriggeredPage == trigger.pageNumber)
                {
                    // Триггер уже срабатывал на этой странице, пропускаем
                    return;
                }

                ApplyTrigger(trigger);
                lastTriggeredPage = trigger.pageNumber;
                Debug.Log("Worked here " + book.currentPage);
                break;
            }
        }
    }

    private void ApplyTrigger(PageEnvironmentTrigger trigger)
    {
        // Включаем объекты
        foreach (var obj in trigger.activateObjects)
            if (obj != null) obj.SetActive(true);

        // Выключаем объекты
        foreach (var obj in trigger.deactivateObjects)
            if (obj != null) obj.SetActive(false);

        // Меняем текстуры
        foreach (var swap in trigger.textureSwaps)
            if (swap.targetRenderer != null && swap.newTexture != null)
                swap.targetRenderer.material.mainTexture = swap.newTexture;

        // Проигрываем общий звук, если отмечено
        if (trigger.playSound && globalTriggerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(globalTriggerSound);
        }
    }
}
