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

    // 하루를 1을 기준으로 타임을 정합니다.
    // 그리고 게임시작시 미리 지정된 스타트 타임부터 시작하도록 합니다. 
    private void Start() {
        timeRate = 1.0f/fullDayLength;
        time = startTime;
    }

    private void Update() {
        IncreaseTime();
        IntenseCheck();
    }
    
    // 시간이 1값이 되면 GameManger의 Set을 통해 day를 1추가해줍니다.
    // Set을 통해 GameManger의 Day갑 변경에 따라 작동해야하는 함수들을 이벤트를 통해 작동시킵니다.
    void IncreaseTime(){
        time += timeRate * Time.deltaTime;
        if(time >= 1){
            time = 0;
            GameManager.Instance.Day += 1;
        }
    }

    //시간에 따라 전체적인 게임 색상(새벽,노을녘)을 변경하고, 밝기를 조정합니다.
    //밝기와 색상은 미리 그라데이션과 AnimationCurve를 통해 자연스럽게 변경하도록 지정해두었습니다.
    void IntenseCheck(){
        light.color = skyColor.Evaluate(time);
        light.intensity = lightIntense.Evaluate(time);
    }
}

