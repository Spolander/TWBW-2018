using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class endingFader : MonoBehaviour {

    bool fading = false;

    public Image i;
    // Use this for initialization
    float alpha = 0;
    float timer = 0;
	// Update is called once per frame
	void Update () {
        if (fading)
        {
            Color c = i.color;
            alpha = Mathf.MoveTowards(alpha, 1, Time.deltaTime / 2);
            c.a = alpha;
            i.color = c;
            timer += Time.deltaTime;
        }

        if (timer > 8)
        {
            Application.Quit();
        }
           
	}

    public void Activate()
    {
        fading = true;
    }
}
