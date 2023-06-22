using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[EnumToggleButtons]
public enum Phase{
    StartPhase,
    SecondPhase,
    GoalPhase,
    OverPhase
}
public class Crop : MonoBehaviour
{
    public CropData cropData;
    public int curDay;
    private int startDay;
    public Phase phase;
    SpriteRenderer spriteRenderer;

    Sprite startSprite;
    Sprite secondSprite;
    Sprite goalSprite;
    Sprite overSprite;
    
    private void OnEnable() {
        GameManager.DayCheck += CheckDay;
    }

    private void OnDisable() {
        GameManager.DayCheck -= CheckDay;
    }
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start(){
        startSprite = cropData.StartStatus.phaseSprite;
        secondSprite = cropData.SecondStatus.phaseSprite;
        goalSprite = cropData.GoalStatus.phaseSprite;
        overSprite = cropData.OverStatus.phaseSprite;
        ResetChildObject();
        startDay = GameManager.Instance.Day;
        spriteRenderer.sprite = startSprite;
        
    }

    //작물이 심어진 이후 며칠이 지났는지 확인합니다.
    void CheckDay(){
        if(startDay != GameManager.Instance.Day){
            curDay = GameManager.Instance.Day - startDay;
            CheckParse();
        }
    }

    //현재 작물의 성장단계를 확인합니다.
    [Button]
    public void CheckParse(){
        if (curDay >= cropData.OverStatus.startDay){
            phase = Phase.OverPhase;
            PharseChange();
            return;
        } 
        else if (curDay >= cropData.GoalStatus.startDay) {
            phase = Phase.GoalPhase;
            PharseChange();
            return;
        }
        else if (curDay >= cropData.SecondStatus.startDay) {
            phase = Phase.SecondPhase;
            PharseChange();
            return;
        }
        else if (curDay >= cropData.StartStatus.startDay) {
            phase = Phase.StartPhase;
            PharseChange();
            return;
        }
    }

    //성장 단계에 따라 보이는 오브젝트를 변경합니다.
    public void PharseChange(){
        switch(phase){
            case (Phase.StartPhase) :
                spriteRenderer.sprite = startSprite;
            break;
            case (Phase.SecondPhase) :
                spriteRenderer.sprite = secondSprite;
            break;
            case (Phase.GoalPhase) :
                spriteRenderer.sprite = goalSprite;
            break;
            case (Phase.OverPhase) : 
                spriteRenderer.sprite = overSprite;
            break;
        }
    }

    //혹시 잘못된 child가 있을수 있으니, 시작시 모두 파괴해서 초기화 해줍니다.
    public void ResetChildObject(){
        Transform[] childObjects = GetComponentsInChildren<Transform>();
        foreach(Transform child in childObjects){
            if(child !=childObjects[0]){
                Destroy(child.gameObject);
            }
        }
    }
}
