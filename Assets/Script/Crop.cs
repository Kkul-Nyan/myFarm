using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    GameObject startCrop;
    GameObject secondCrop;
    GameObject goalCrop;
    GameObject overCrop;
    
    void Start(){
        ResetChildObject();
        startDay = GameManager.Instance.day;
        startCrop = Instantiate(cropData.StartStatus.phasePrefab, transform.position, Quaternion.identity);
        startCrop.transform.parent = this.transform;
        secondCrop = Instantiate(cropData.SecondStatus.phasePrefab, transform.position, Quaternion.identity);
        secondCrop.transform.parent = this.transform;
        goalCrop = Instantiate(cropData.GoalStatus.phasePrefab, transform.position, Quaternion.identity);
        goalCrop.transform.parent = this.transform;
        overCrop = Instantiate(cropData.OverStatus.phasePrefab, transform.position, Quaternion.identity);
        overCrop.transform.parent = this.transform;
        CheckParse();
    }

    //작물이 심어진 이후 며칠이 지났는지 확인합니다.
    public void CheckDay(){
        if(startDay != GameManager.Instance.day){
            curDay = GameManager.Instance.day - startDay;
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
        resetParse();
        switch(phase){
            case (Phase.StartPhase) :
                startCrop.gameObject.SetActive(true);
                secondCrop.gameObject.SetActive(false);
                goalCrop.gameObject.SetActive(false);
                overCrop.gameObject.SetActive(false);
            break;
            case (Phase.SecondPhase) :
                startCrop.gameObject.SetActive(false);
                secondCrop.gameObject.SetActive(true);
                goalCrop.gameObject.SetActive(false);
                overCrop.gameObject.SetActive(false);
            break;
            case (Phase.GoalPhase) :
                startCrop.gameObject.SetActive(false);
                secondCrop.gameObject.SetActive(false);
                goalCrop.gameObject.SetActive(true);
                overCrop.gameObject.SetActive(false);
            break;
            case (Phase.OverPhase) : 
                startCrop.gameObject.SetActive(false);
                secondCrop.gameObject.SetActive(false);
                goalCrop.gameObject.SetActive(false);
                overCrop.gameObject.SetActive(true);
            break;
        }
    }
    
    //켜져있는 모든오브젝트를 꺼버립니다.
    public void resetParse(){
        startCrop.gameObject.SetActive(false);
        secondCrop.gameObject.SetActive(false);
        goalCrop.gameObject.SetActive(false);
        overCrop.gameObject.SetActive(false);    
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
