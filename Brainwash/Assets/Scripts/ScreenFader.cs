using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour {


    [SerializeField]
    private RectTransform topBar;

    [SerializeField]
    private RectTransform bottomBar;


    public static int SCREEN_WIDTH = 192;
    public static int SCREEN_HEIGHT = 128;

    Vector2 topBarMin = new Vector2(0, SCREEN_HEIGHT);
    Vector2 topBarMax = new Vector2(0, 0);

    Vector2 bottomBarMin = new Vector2(0, 0);
    Vector2 bottomBarMax = new Vector2(0, -SCREEN_HEIGHT);

 

    Animator anim;
    bool fading = false;

    public static ScreenFader instance;
	// Use this for initialization
	void Awake () {

        instance = this;
        anim = GetComponent<Animator>();
        topBar.offsetMin = topBarMin;
        topBar.offsetMax = topBarMax;

        bottomBar.offsetMin = bottomBarMin;
        bottomBar.offsetMax = bottomBarMax;

        topBar.gameObject.SetActive(true);
        bottomBar.gameObject.SetActive(true);
        anim.Play("open");
	}

    // Update is called once per frame


    public void Fade(float initialDelay, float middlePause)
    {
        if (fading)
            return;

        fading = true;

        topBar.gameObject.SetActive(true);
        bottomBar.gameObject.SetActive(true);

        
        StartCoroutine(fadeDelay(initialDelay, middlePause));
    }

    IEnumerator fadeDelay(float initialDelay, float middlePause)
    {

        yield return new WaitForSeconds(initialDelay);
        anim.Play("close");
        yield return new WaitForSeconds(0.6f + middlePause);

        anim.Play("open");

        yield return new WaitForSeconds(2f);

        topBar.gameObject.SetActive(false);
        bottomBar.gameObject.SetActive(false);

        PlayerMovement.playerInstance.CanMove = true;

        fading = false;
    }
   
}
