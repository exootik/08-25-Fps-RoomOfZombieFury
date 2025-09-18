using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Image targetImage;
    public Text targetText;
    public Button targetButtonRetry;
    public Button targetButtonMenu;
    public float duration = 5f;
    public bool showDeadScreen = false;
    private float targetAlpha = 0.8f;
    private float startAlpha;
    private float elapsedTime = 0f;

    private void Start()
    {
        startAlpha = targetImage.color.a;
    }

    private void Update()
    {
        if (showDeadScreen)
        {
            if (elapsedTime < duration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);

                Color newColor = targetImage.color;
                newColor.a = newAlpha;
                targetImage.color = newColor;

                Color newTextAlpha = targetText.color;
                newTextAlpha.a = newAlpha;
                targetText.color = newTextAlpha;

                elapsedTime += Time.deltaTime;
            }
            else
            {
                targetButtonMenu.enabled = false;
                targetButtonRetry.enabled = false;
                Time.timeScale = 0f;
            }
        }
    }
}
