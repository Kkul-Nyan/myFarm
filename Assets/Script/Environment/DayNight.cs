using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DayNight : MonoBehaviour
{
    private new Light2D light;
    public float time;
    public float fullDayLength;
    [Range(0,1)]public float startTime;
    public Gradient skyColor;
    public AnimationCurve lightIntense;
    float timeRate;

    private void Awake() {
        light = transform.GetComponent<Light2D>();
    }
    private void Start() {
        timeRate = 1.0f/fullDayLength;
        time = startTime;
    }

    private void Update() {
        IncreaseTime();
        IntenseCheck();
    }

    void IncreaseTime(){
        time += timeRate * Time.deltaTime;
        if(time >= 1){
            time = 0;
            GameManager.Instance.Day += 1;
        }
    }
    void IntenseCheck(){
        light.color = skyColor.Evaluate(time);
        light.intensity = lightIntense.Evaluate(time);
    }
}

