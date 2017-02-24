using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
    AudioSource audio;
    public AudioClip BkMusic;
    CharacterController ch;
    Animator anim;
    AnimationState an;
    float speed = 10f;
    float jumpForce = 20.0f;
    float gravity = 30f;
    private bool isDead = false;
    Vector3 pos = Vector3.zero;
    Vector3 direction = Vector3.zero;
  // public GameObject Fmodel;
	// Use this for initialization
	void Start () {
        ch = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        // cam = GetComponent<Camera>();
        audio.Play();

    }
    void Swipe()
    {
       
        Vector2 delta = Input.GetTouch(0).deltaPosition;
        if (Mathf.Abs(delta.x)>Mathf.Abs(delta.y))
        {
             pos = transform.position;
            pos.x = delta.x * speed;
            pos.z = speed;
        }
        else
        {
            if (ch.isGrounded)
            {
                if (delta.y>0)
                {
                    pos.y = jumpForce;
                    anim.SetBool("jump", true);
                    Invoke("StopJump", 0.1f);
                }
                else
                {
                    // direction.y = jumpForce;
                    anim.SetBool("slide", true);
                    Invoke("StopJumpOver", 0.1f);
                }


            }
            pos.y -= gravity * Time.deltaTime;
            ch.Move(pos * Time.deltaTime);
        }
    }
	// Update is called once per frame
	void Update () {
        
        if (isDead) return;
        if (Input.touchCount > 0) Swipe();
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
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Finish")
        {
            Debug.Log("Hit Player");
            Death();
        }
    }
    
    void OnCollision(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Hit Player");
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
    
}
