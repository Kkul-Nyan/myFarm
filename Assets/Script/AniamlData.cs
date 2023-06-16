using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AnimalData", menuName = "New AnimalData")]
public class AniamlData : ScriptableObject
{
    [ShowInInspector]
    [TabGroup("Info")]
    int id = 0;
    public int ID{
        get{
            return id;
        }
    }

    [ShowInInspector]   
    [TabGroup("Info")]
    string animalName = "*Required";
    public string AnimalName{
        get{
            return animalName;
        }
    }

    [ShowInInspector]
    [TabGroup("Info")]
    string description = "*Required";
    public string Description{
        get{
            return description;
        }
    }

    [ShowInInspector]
    [TabGroup("Bady")]
    [HideReferenceObjectPicker]
    AnimalStatus badyPhase = new AnimalStatus();
    public AnimalStatus BadyPhase{
        get{
            return badyPhase;
        }
    }

    [ShowInInspector]
    [TabGroup("Grown-up")]
    [HideReferenceObjectPicker]
    AnimalStatus grownupPhase = new AnimalStatus();
    public AnimalStatus GrownupPhase{
        get{
            return grownupPhase;
        }
    }
}

[System.Serializable]
public class AnimalStatus{
    [Tooltip("현재 단계 프리팹을 미리봅니다")]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    public GameObject phasePrefab;
    [Tooltip("단계가 시작되는 날입니다.")]
    public int startday;

    [Tooltip("인스턴스의 최초 최대 생명력입니다.")]
    public int startHealth;

    [Tooltip("드랍아이탬 여부를 결정합니다.")]
    public bool isDropAnimal;

    [ShowIf("isDropAnimal")]
    [Tooltip("드랍되는 아이탬입니다.")]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    public GameObject[] dropPrefabs;
}
