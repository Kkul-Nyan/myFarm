using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager> 
{   
    //day가 바뀔시 관련된 메서드들을 작동하기 위해 작성했습니다.
	//예를 들어 day가 1일 추가시, Crop에서 CheckDay가 작동해서,
	//작물들을 다음 성장단계로 변경할지 말지 여부등을 확인합니다.
    public delegate void DayCheckHandler();
    public static event DayCheckHandler DayCheck;

    [SerializeField]
    int day;
    
    //Get,Set을 통해 데이터 보호 및 위에 만들어준 이벤트함수를 작동시킵니다.
    public int Day{
        get{return day;}
        set{
            day = value;
            DayCheck?.Invoke();
        }
    } 


}
