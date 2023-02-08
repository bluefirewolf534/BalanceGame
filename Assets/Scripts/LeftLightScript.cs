using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLightScript : MonoBehaviour
{
    SpriteRenderer sr;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        Color c = sr.material.color;
        c.a = 0;
        sr.material.color = c;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            Color c = sr.material.color;
            c.a = 1;
            sr.material.color = c;
            StopCoroutine("FadeOut");
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
