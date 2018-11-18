using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class damageFlash : MonoBehaviour {

    public static damageFlash instance;
    Image i;

    Coroutine flashCoroutine;
    private void Awake()
    {
        i = GetComponent<Image>();
        instance = this;

     
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(flashingAnimation());
    }


    IEnumerator flashingAnimation()
    {
        int count = 3;
        float lerp = 0;
        float target = 1;
        float start = 0;

        float alpha = 0;
        while (count > -1)
        {

            lerp += Time.deltaTime*5;
            alpha = Mathf.Lerp(start, target, lerp);
            Color c = i.color;
            c.a = alpha;
            i.color = c;
            if (lerp >= 1)
            {
                lerp = 0;
                if (target == 0)
                {
                    count--;
                    target = 1;
                    start = 0;
                }
                else
                {
                    target = 0;
                    start = 1;
                }
            }

            yield return null;
        }

        Color ca = i.color;
        ca.a = 0;
        i.color = ca;
    }
}
