using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public float fadeDuration = 1f;
    Coroutine FadingCoroutine;

    public Renderer _renderer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.Instance().audioPressets[GetClass()].clip;
    }

    public int GetClass()
    {
        int _class = -1;
        float size = transform.localScale.x;
        for(int i = 0; i < AudioManager.Instance().audioPressets.Length; i++)
        {
            if (size <= AudioManager.Instance().audioPressets[i].maxSize)
            {
                _class = i;
                break;
            }
        }
        if (_class == -1)
            return AudioManager.Instance().audioPressets.Length;

        return _class;
    }

    public void Play()
    {
        if (FadingCoroutine != null)
            StopCoroutine(FadingCoroutine);

        audioSource.enabled = true;
        audioSource.Stop();
        audioSource.Play();
        FadingCoroutine = StartCoroutine(IEFade(0, 1));
        //_renderer.material.color = Color.red;
    }

    public void Stop()
    {
        if (FadingCoroutine != null)
            StopCoroutine(FadingCoroutine);

        FadingCoroutine = StartCoroutine(IEFade(1, 0));
        _renderer.material.color = Color.white;
    }

    IEnumerator IEFade(int start, int end)
    {
        for (float i = 0f; i < fadeDuration; i+=Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(start, end, i / fadeDuration);
            yield return null;
        }

        if (start == 1)
            audioSource.Stop();
    }
}
