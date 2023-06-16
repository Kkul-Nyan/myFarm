using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3;
    Vector2 moveInput;
    Rigidbody rig;
    public Animator anim;

    
    private void Awake() {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Move();
    }
    
    void Move(){
        Vector3 dir = transform.forward * moveInput.y + transform.right * moveInput.x;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;
        rig.velocity = dir;
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            anim.SetBool("Move", true);
            moveInput = context.ReadValue<Vector2>();
        }
        else{
            anim.SetBool("Move", false);
            moveInput = Vector2.zero;
        }
    }
    public void AddForceP(){
        if(Input.GetKeyDown("f")){
            rig.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
