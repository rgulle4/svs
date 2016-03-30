using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  
  public float speed = 6f;
  
  Vector3 movement;
  Animator anim;
  Rigidbody playerRigidbody;
  int floorMask;
  // length of the ray that we cast from the camera
  float camRayLength = 100f;   
  
  // Similar to start, but always gets called, regardless of whether it's enabled.
  // This is useful for setting up references and things like that.
  void Awake() 
  {
    // refers to the "Floor" layer
    floorMask = LayerMask.GetMask("Floor"); 
    // The animator and rigidbody
    anim = GetComponent<Animator>();
    playerRigidbody = GetComponent<Rigidbody>();
  }
  
  // gets called at every phyics update
  void FixedUpdate() 
  {
    // get user input, use GetAxisRaw
    // GetAxisRaw 'snaps' b/w discrete 0/1... more responsive
    float h = Input.GetAxisRaw("Horizontal"); 
    float v = Input.GetAxisRaw("Vertical");   
    
    // update the player 
    Move(h, v);
    Turning();
    Animating(h, v);
  } 
  
  void Move(float h, float v)
  {
    movement.Set(h, 0f, v);
    movement = movement.normalized * speed * Time.deltaTime;
    playerRigidbody.MovePosition(transform.position + movement); 
  }
  
  void Turning()
  {
    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit floorHit;
    
    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
      Vector3 playerToMouse = floorHit.point - transform.position;
      playerToMouse.y = 0f;
      
      Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
      playerRigidbody.MoveRotation(newRotation);
    }
  }
  
  void Animating(float h, float v) 
  {
    bool walking = ((h != 0f) || (v != 0f));
    anim.SetBool("IsWalking", walking);
  }
}
