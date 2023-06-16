 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;



[CreateAssetMenu(fileName = "CropData", menuName = "New CropData")]
public class CropData : ScriptableObject 
{
    [TabGroup("Info")]
    [Tooltip("세이브/로드 등에 사용될 숫자 아이디값입니다")]
    [ShowInInspector]
    public int id;
    public int ID{
        get{
            return id;
        }
    }
    
    [TabGroup("Info")]
    [Tooltip("인벤토리등에 사용될 이름입니다")]
    [ShowInInspector]
    private string cropName = "*Required";
    public string Name{
        get{
            return cropName;
        }
    }

    [TabGroup("Info")]
    [Tooltip("인벤토리등에 사용될 설명입니다")]
    [ShowInInspector]
    private string description = "*Required";
    public string Description{
        get{
            return description;
        }
    }

    [TabGroup("Info")]
    [Tooltip("최초 최대 수명의 날 단위입니다")]
    [ShowInInspector]
    private int lifeDay = 5;
    public int LifeDime{
        get{
            return lifeDay;
        }
    }
    [TabGroup("Info")]
    [Tooltip("최초 최대 수명의 시간 단위입니다(lifeday = 0 일 때, 작동합니다)")]
    [ShowInInspector]
    private float lifeTime = 100;
    public float LifeTime{
        get{
            return lifeTime;
        }
    }

    [TabGroup("첫단계")]
    [Tooltip("농작물 첫번쨰 단계입니다.")]
    [ShowInInspector]
    [HideReferenceObjectPicker]
    private PhaseStatus startStatus = new PhaseStatus();

    public PhaseStatus StartStatus{
        get{
            return startStatus;
        }
    }

    [TabGroup("두번째")]
    [Tooltip("농작물 두번쨰 단계입니다.")]
    [ShowInInspector]
    [HideReferenceObjectPicker]
    private PhaseStatus secondStatus = new PhaseStatus();
    public PhaseStatus SecondStatus{
        get{
            return secondStatus;
        }
    }
    
    [TabGroup("수확단계")]
    [Tooltip("농작물 수확 해야 할 단계입니다.")]
    [ShowInInspector]
    [HideReferenceObjectPicker]
    private PhaseStatus goalStatus = new PhaseStatus();
    public PhaseStatus GoalStatus{
        get{
            return goalStatus;
        }
    }

    [TabGroup("최종단계")]
    [Tooltip("농작물 수확시기를 놓친 단계입니다.")]
    [ShowInInspector]
    [HideReferenceObjectPicker]
    private PhaseStatus overStatus = new PhaseStatus();
    public PhaseStatus OverStatus{
        get{
            return overStatus;
        }
    }
}

[System.Serializable]
public class PhaseStatus{
    [Tooltip("프리팹 미리보기입니다")]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    public GameObject phasePrefab;

    [Tooltip("현 단계의 시작일 입니다")]
    public int startDay;
    [Tooltip("인스턴스의 최초 최대 생명력입니다.")]
    public int startHealth;

    [Tooltip("드랍아이탬이 존재할떄 체크합니다")]
    public bool isDropCrop;

    [PreviewField(80, ObjectFieldAlignment.Center)]
    [ShowIf("isDropCrop")]
    public GameObject[] dropPrefab;

}
