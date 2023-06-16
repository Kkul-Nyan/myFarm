using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[EnumToggleButtons]
public enum MonsterStateType{
    Aggresive,
    Normal,
    Flee
}

[EnumToggleButtons]
public enum  MonsterAttribute{
    Fire,
    Water,
    Ice,
    Ground,
}

[EnumToggleButtons]
public enum MonsterArmor{
    Heavy,
    Middle,
    Light
}
[CreateAssetMenu(fileName = "MonsterData", menuName = "New MonsterData")]
public class MonsterData : ScriptableObject
{
    [ShowInInspector]
    [PreviewField(80, ObjectFieldAlignment.Center)]
    [Tooltip("몬스터 프리뷰입니다")]
    private GameObject monsterObject;
    public GameObject MonsterObject{
        get{
            return monsterObject;
        }
    }
    
    [Tooltip("세이브/로드 등에 사용될 숫자 아이디값입니다.")]
    [ShowInInspector]
    private int id;
    public int ID{
        get{
            return id;
        }
    }

    [Tooltip("몬스터의 성향 타입니다")]
    [ShowInInspector]
    private MonsterStateType type;
    public MonsterStateType Type{
        get{
            return type;
        }
    }

    [Tooltip("몬스터의 속성 입니다")]
    [ShowInInspector]
    MonsterAttribute attribute;
    public MonsterAttribute Attribute{
        get{
            return attribute;
        }
    }

    [Tooltip("몬스터의 방어타입니다")]
    [ShowInInspector]
    MonsterArmor armorType;
    public MonsterArmor ArmorType{
        get{
            return armorType;
        }
    }

    [Tooltip("UI 등에서 사용될 몬스터의 이름입니다")]
    [ShowInInspector]
    string monsterName = "*Required";
    public string Name{
        get{
            return monsterName;
        }
    }

    [Tooltip("퀘스트 등에서 사용될 몬스터의 설명입니다")]
    [ShowInInspector]
    string description = "*Required";
    public string Description{
        get{
            return description;
        }
    }

    [Tooltip("몬스터의 최초 최대 체력입니다")]
    [ShowInInspector]
    [HideReferenceObjectPicker]
    public float maxHealth = 100;
    public float MaxHealth{
        get{
            return maxHealth;
        }
    }

    [Tooltip("")]
    [ShowInInspector]
    ItemData[] dropItem = {};
    public ItemData[] DropItem{
        get{
            return dropItem;
        }
    }

}

