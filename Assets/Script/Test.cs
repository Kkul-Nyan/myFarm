using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Path;
public class Test : MonoBehaviour
{
    float time = 0;
    Vector3 targetposition;
    int count = 0;
    
    private void Start() {
        targetposition = transform.position;
    }
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetposition, Time.deltaTime);
    }
    public void Move(){
        if(count >= Path.instance.FinalNodeList.Count){
            Debug.Log("도착");
            return;
        }
        count += 1;
        targetposition = new Vector3(Path.instance.FinalNodeList[count].x, Path.instance.FinalNodeList[count].y);
     }
    
    /*
    IEnumerator MovePos(){
        if(time <1){
            time += Time.deltaTime;
        }
        transform.position = Mathf.Lerp(curPos, nextPos, time);
        yield return null;
    }*/
}
