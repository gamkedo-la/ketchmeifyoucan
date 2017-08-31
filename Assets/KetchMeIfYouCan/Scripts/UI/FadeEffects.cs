using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffects : MonoBehaviour {
    public static FadeEffects Instance { get; set; }
    Image fadeImage;

    private void Awake() {

        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        fadeImage = transform.Find("FadeImage").GetComponent<Image>();
    }

    public void FadeScreen(float fadeAlpha, float changeStep) {

        StartCoroutine(FadeScreenCR(fadeAlpha, changeStep));
    }

    IEnumerator FadeScreenCR(float f, float c) {

        Color colorTarget = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.g, f);
        float dir = Mathf.Sign(f - fadeImage.color.a);

        while (Mathf.Abs(fadeImage.color.a - f) > 0.0008f) {

            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + c * dir);
            yield return null;
        }

        fadeImage.color = colorTarget;
    }
}