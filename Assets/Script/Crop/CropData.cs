using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/*
유니티에서 제공해주는 스크립터블오브젝트를 통해 데이터컨테이너에셋을 만들어서 관리했습니다
관련된 모든 원본데이터를 보관합니다. 
오딘 인스펙터와 툴팁을 통해 데이터를 효율적으로 읽고, 관리 할 수 있도록 했습니다.
*/

[CreateAssetMenu(fileName = "CropData", menuName = "New CropData")]
public class CropData : ScriptableObject
{   
    [TabGroup("Info")]
    [Tooltip("세이브/로드 등에 사용될 숫자 아이디값입니다")]
    public int id;
    

    [TabGroup("Info")]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    [Tooltip("씨앗아이탬의 모습입니다.")]
    private Sprite seedSprite;
    
    
    [TabGroup("Info")]
    [Tooltip("인벤토리등에 사용될 이름입니다")]
    //필수적으로 작성해야하는 부분에는 미리 *Required와 기본값으로 표시해두었습니다.
    public string cropName = "*Required";
   

    [TabGroup("Info")]
    [Tooltip("인벤토리등에 사용될 설명입니다")]
    public string description = "*Required";
   

    [TabGroup("Info")]
    [Tooltip("최초 최대 수명의 날 단위입니다")]
    // 이날이 지나게 되면 오브젝트를 파괴하게 만들었습니다.
    public int lifeDay = 5;
    
    [TabGroup("Info")]
    [Tooltip("최초 최대 수명의 시간 단위입니다(lifeday = 0 일 때, 작동합니다)")]
    // 날이 바뀌는 00시에 모든 오브젝트가 파괴되면 이상한 부분이 생길수도있다 생각하여, 
    // 0일이 되면 작동되는 별도의 타이머를 두었습니다.
    public float lifeTime = 100;
   

    [TabGroup("Info")]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    [Tooltip("씨앗의 모습입니다.")]
    public GameObject cropPrefab;
   
   
    // 단계별로 사용되는 변수들이 공통되는 부분이 많기 때문에 클래스를 통해 만들어주었습니다.
    // 관련된 부분은 스크립트 가장 아래에 있습니다.
    [TabGroup("첫단계")]
    [Tooltip("농작물 첫번쨰 단계입니다.")]
    [HideReferenceObjectPicker]
    public PhaseStatus startStatus = new PhaseStatus();

  

    [TabGroup("두번째")]
    [Tooltip("농작물 두번쨰 단계입니다.")]
    [HideReferenceObjectPicker]
    public PhaseStatus secondStatus = new PhaseStatus();
  
    
    [TabGroup("수확단계")]
    [Tooltip("농작물 수확 해야 할 단계입니다.")]
    [HideReferenceObjectPicker]
    public PhaseStatus goalStatus = new PhaseStatus();
   

    [TabGroup("최종단계")]
    [Tooltip("농작물 수확시기를 놓친 단계입니다.")]
    [HideReferenceObjectPicker]
    public PhaseStatus overStatus = new PhaseStatus();

}

[System.Serializable]
public class PhaseStatus{
    // 각 단계의 모습입니다. 굳이 각단계마다 인스턴스를 생성하기보다는 Sprite를 변경하는
    // 방법으로 관리하였습니다.
    [Tooltip("프리팹 미리보기입니다")]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    public Sprite phaseSprite;

    [Tooltip("현 단계의 시작일 입니다")]
    public int startDay;
    [Tooltip("인스턴스의 최초 최대 생명력입니다.")]
    public int startHealth;
    // 예를들어 농작물의 열매등에 해당합니다. 있다고 체크할시 인스펙터창에서 맨마지막 단계가 나타납니다.
    [Tooltip("드랍아이탬이 존재할떄 체크합니다")]
    public bool isDropCrop;
    // 드랍될 아이탬이 하나 이상일수 있으므로 집합을 사용하였으며, 한눈에 프리팹들을 보기 편하게 Preview를 사용했습니다.
    [PreviewField(80, ObjectFieldAlignment.Center)]
    [ShowIf("isDropCrop")]
    public GameObject[] dropPrefab;
}
