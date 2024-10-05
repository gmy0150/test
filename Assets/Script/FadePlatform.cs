using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadePlatform : MonoBehaviour
{
    public float Duration;
    private Tilemap platform;
    void Start()
    {
        platform = GetComponent<Tilemap>();
    }

    void StartFading()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        float startAlpha = platform.color.a;
        float timeElapsed = 0;
        while (timeElapsed < Duration)
        {
            timeElapsed += Time.deltaTime;
            float alphaValue = Mathf.Lerp(startAlpha, 0, timeElapsed / Duration);
            platform.color = new Color(platform.color.r, platform.color.g, platform.color.b, alphaValue);
            yield return null;
        }
        platform.color = new Color(platform.color.r, platform.color.g, platform.color.b, 0);
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartFading();
        }
    }
}
