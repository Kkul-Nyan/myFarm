using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    bool fadeOutTitle = false;

    
    public CanvasGroup titleCanvas;
    public CanvasGroup loginCanvas;
    public float fadeoutSpeed;
    DOTweenAnimation titleDotween;
    DOTweenAnimation loginDotween;

    [SerializeField] bool hasToken = false;

    private void Awake() {
        titleCanvas = GameObject.Find("TitleGroup").GetComponent<CanvasGroup>();
        loginCanvas = GameObject.Find("LoginGroup").GetComponent<CanvasGroup>();
        titleDotween = titleCanvas.GetComponent<DOTweenAnimation>();
        loginDotween = loginCanvas.GetComponent<DOTweenAnimation>();
        loginCanvas.gameObject.SetActive(false);
    }
    private void Update() {
        TouchToPlay();
    }

    public void TouchToPlay(){
        if(Input.GetMouseButtonDown(0)){
            titleDotween.DOPlayAllById("CanvasFade");
            CheckToken();
            if(hasToken == true){
                Debug.Log("Login!");
                Invoke("PlayGame",2);
            }
            
        }
        if(titleCanvas.alpha <= 0 && hasToken == false){
            titleCanvas.gameObject.SetActive(false);
            loginCanvas.gameObject.SetActive(true);
            loginDotween.DOPlayAllById("CanvasIn");
            fadeOutTitle = true;
        }
    }

    bool CheckToken(){
        hasToken = true;
        return hasToken;
    }

    

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
