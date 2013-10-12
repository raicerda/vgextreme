using UnityEngine;
using System.Collections;

public class WalkController : MonoBehaviour {
	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
	public bool isGrappable = false;
    private Vector3 moveDirection = Vector3.zero;
	
	
    void Update() {
		
        CharacterController controller = GetComponent<CharacterController>();
		
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")){
                moveDirection.y = jumpSpeed;
			}
        }
		if(Input.GetKey(KeyCode.Z)){
			if(isGrappable){
				moveDirection.Set(moveDirection.x,0,0);
				//TODO Sprite Changes
				if(Input.GetButton ("Jump")){
				moveDirection.y = jumpSpeed;
			}
			}
		}
		moveDirection.Set(moveDirection.x,moveDirection.y,0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
	
	
	
	
	// Check if the character can grapple to the wall
	
	void OnTriggerEnter(Collider target){
		if (target.transform.tag.Equals("dirtWall")) {
			isGrappable = true;
		}
	}
	
	void OnTriggerExit(Collider target){
		if (target.transform.tag.Equals("dirtWall")) {
			isGrappable = false;
		}
	}
}

