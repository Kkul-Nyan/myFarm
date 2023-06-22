using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


//성장의 각단계별로 보기편하게 관리하기 위해 만들어 두었습니다.
[EnumToggleButtons]
public enum Phase{
    StartPhase,
    SecondPhase,
    GoalPhase,
    OverPhase
}
public class Crop : MonoBehaviour
{
    /*
    모든 원본데이터는 CropData라는 데이터 컨테이너에셋에서 보관하고있습니다.
    이를 Get을 통해 읽기전용으로 보호되고 있으므로, 단순히 읽을수만있습니다.
    */
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
        //GameMager가 가지고 있는 이벤트함수인 DayCheck에 CheckDay 함수를 리스너로 등록합니다.
        GameManager.DayCheck += CheckDay;
    }

    private void OnDisable() {
        //GameMager가 가지고 있는 이벤트함수인 DayCheck에 리스너로 등록된 CheckDay 함수를 제거합니다.
        GameManager.DayCheck -= CheckDay;
    }
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start(){
        //데이터에셋이 가지고있는 스프라이트 정보를 읽어옵니다.
        startSprite = cropData.StartStatus.phaseSprite;
        secondSprite = cropData.SecondStatus.phaseSprite;
        goalSprite = cropData.GoalStatus.phaseSprite;
        overSprite = cropData.OverStatus.phaseSprite;
        
        //혹시 잘못된 Child가 있을수 있으므로 초기화 해줍니다.
        ResetChildObject();

        //인스턴스가 생성된 시기를 저장합니다. 이를 기준으로 날짜를 체크합니다.
        startDay = GameManager.Instance.Day;

        //지금은 단순히 생성이지만, 작물을 옮기는 기능을 넣게되면, 모습을 초기화하면 안되므로, 체크데이를 통해 성장단계를 확인합니다.
        //이를통해 현재 상태에 맞는 스프라이트를 적용시킵니다.
        CheckDay();
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

    //성장 단계에 따라 보이는 스프라이트를 변경합니다.
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
