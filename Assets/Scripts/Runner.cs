using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
    CharacterController ch;
    Animator anim;
    AnimationState an;
    float speed = 10f;
    float jumpForce = 20.0f;
    float gravity = 30f;
    private bool isDead = false;
    Vector3 direction = Vector3.zero;
  // public GameObject Fmodel;
	// Use this for initialization
	void Start () {
        ch = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        // cam = GetComponent<Camera>();
        

    }
	
	// Update is called once per frame
	void Update () {

        if (isDead) return;
            direction = Vector3.zero;

            direction.x = Input.GetAxisRaw("Horizontal") * speed;
            direction.z = speed;
       
        if (ch.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jumpForce;
                anim.SetBool("jump", true);
                Invoke("StopJump", 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
               // direction.y = jumpForce;
                anim.SetBool("slide", true);
                Invoke("StopJumpOver", 0.1f);
            }
           
           
        }
        direction.y -= gravity * Time.deltaTime;
        ch.Move(direction * Time.deltaTime);
        
    }
    void StopJump()
    {
        anim.SetBool("jump", false);
      
    }
    void StopJumpOver()
    {
        anim.SetBool("slide", false);
    }
    public void SetSpeed(float modifier)
    {
        speed += modifier;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + ch.radius)
        {
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
