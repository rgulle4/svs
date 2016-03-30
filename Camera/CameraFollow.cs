using UnityEngine;

public class CameraFollow : MonoBehaviour {
  
  public Transform target;
  public float smoothing = 5f;
  
  // distance between the player and the camera
  Vector3 offset;
  
  // called once per script before the first frame update
  void Start()
  {
    // acquire initial distance bw target and camera.
    // (In the editor, we'll asign the player object to 'target')
    this.offset = this.transform.position - target.position;
  }
  
  // called after every physics update
  void FixedUpdate()
  {
    // move the camera with the player
    Vector3 targetCamPos = target.position + this.offset;
    transform.position = Vector3.Lerp(
      transform.position, 
      targetCamPos, 
      smoothing * Time.deltaTime);
  }
}
