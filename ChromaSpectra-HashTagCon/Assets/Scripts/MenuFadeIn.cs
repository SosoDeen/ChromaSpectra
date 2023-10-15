using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFadeIn : MonoBehaviour
{

    public CanvasGroup UIMenu;
    public float fadeInDelay;
    private bool startFade;

    // Start is called before the first frame update
    void Start()
    {
        UIMenu.alpha = 0;
        Invoke("fadeIn", fadeInDelay);
    }

    void Update(){
        if (startFade){
            UIMenu.alpha += Time.deltaTime;
            if (UIMenu.alpha >= 1){
                startFade = false;
            }
        }
    }

    void fadeIn(){
        startFade = true;
    }
}
