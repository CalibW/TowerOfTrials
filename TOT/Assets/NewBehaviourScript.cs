using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteTextController : MonoBehaviour
{
    [SerializeField] private Image spriteImage;  // This makes it appear in the Inspector
    [SerializeField] private Text displayText;   // This makes it appear in the Inspector

    private bool isFlashing = false;

    void Start()
    {
        spriteImage.gameObject.SetActive(false);
        displayText.gameObject.SetActive(false);
        StartCoroutine(ShowSpriteAndTextAfterDelay(10f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            HideSpriteAndText();
        }
    }

    IEnumerator ShowSpriteAndTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        spriteImage.gameObject.SetActive(true);
        displayText.gameObject.SetActive(true);

        isFlashing = true;
        StartCoroutine(FlashSpriteAndText());
    }

    IEnumerator FlashSpriteAndText()
    {
        while (isFlashing)
        {
            for (float alpha = 1f; alpha >= 0f; alpha -= 0.05f)
            {
                SetAlpha(alpha);
                yield return new WaitForSeconds(0.1f);
            }

            for (float alpha = 0f; alpha <= 1f; alpha += 0.05f)
            {
                SetAlpha(alpha);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void SetAlpha(float alpha)
    {
        Color spriteColor = spriteImage.color;
        spriteColor.a = alpha;
        spriteImage.color = spriteColor;

        Color textColor = displayText.color;
        textColor.a = alpha;
        displayText.color = textColor;
    }

    void HideSpriteAndText()
    {
        isFlashing = false;
        spriteImage.gameObject.SetActive(false);
        displayText.gameObject.SetActive(false);
    }
}
