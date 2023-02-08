using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private bool isDisable = false;

    CanvasGroup cv;
    void Start()
    {
        cv = gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisable)
        {
            return;
        }
        if (Input.anyKeyDown)
        {
            isDisable = true;
            StartCoroutine(("FadeOut"));
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            cv.alpha = f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
