using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
    AudioSource audio;
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;
    public AudioClip BkMusic;
    CharacterController ch;
    Animator anim;
    AnimationState an;
    float speed = 10f;
    float jumpForce = 8.0f;
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
           
            //ch.Move(pos * Time.deltaTime);
        }
    }
	// Update is called once per frame
	void Update () {
        
        if (isDead) return;
        pos.z = speed;
        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    // MOVE RIGHT
                                    pos = swipeType * speed;
                                }
                                else
                                {
                                    // MOVE LEFT
                                    pos = swipeType * speed ;
                                }
                            }
                            if (ch.isGrounded)
                            {
                                if (swipeType.y != 0.0f)
                                {
                                    if (swipeType.y > 0.0f)
                                    {
                                        // MOVE UP
                                        pos.y = jumpForce;
                                        anim.SetBool("jump", true);
                                        Invoke("StopJump", 0.1f);
                                    }
                                    else
                                    {
                                        // MOVE DOWN
                                        anim.SetBool("slide", true);
                                        Invoke("StopJumpOver", 0.1f);
                                    }
                                }

                            }
                        }

                        break;
                }
            }
        }

        /*   direction = Vector3.zero;

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


       }*/
        // direction.y -= gravity * Time.deltaTime;
        pos.y -= gravity * Time.deltaTime;
        ch.Move(pos * Time.deltaTime);
        
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
