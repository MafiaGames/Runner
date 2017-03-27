﻿using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
    AudioSource audio;
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
    public Transform center;
    public Transform left;
    public Transform right;
    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;
    public AudioClip BkMusic;
    CharacterController ch;
    Animator anim;
    AnimationState an;
    //private BoxCollider Floor;
    float speed = 10f;
    float jumpForce = 10.0f;
    float gravity = 30f;
    private bool isDead = false;
    Vector3 pos = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Score sc;
  // public GameObject Fmodel;
	// Use this for initialization
	void Start () {
        ch = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
       // Floor = GetComponent<BoxCollider>();
        // cam = GetComponent<Camera>();
      //  Debug.Log(Floor.bounds.size.x);
        audio.Play();

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
                                //if (swipeType.x > 0.0f)
                                //{
                                //    // MOVE RIGHT
                                //    pos = swipeType * speed;
                                //}
                                //else
                                //{
                                //    // MOVE LEFT
                                //    pos = swipeType * speed;
                                //}
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
       pos.x = Input.GetAxisRaw("Mouse X")*0.9f;
        Debug.Log(pos.x);
        /* direction = Vector3.zero;

          direction.x = Input.GetAxisRaw("Horizontal") * speed;
            direction.z = speed;
*/
         if (ch.isGrounded)
         {
             if (Input.GetKeyDown(KeyCode.Space))
             {
                ch.height = 0.5f;
                pos.y = jumpForce; 
                anim.SetBool("jump", true); 
                Invoke("StopJump", 0.1f);
             }
             if (Input.GetKeyDown(KeyCode.LeftShift))
             {
                // direction.y = jumpForce;
                ch.height = 1f;
                anim.SetBool("slide", true);
                 Invoke("StopJumpOver", 0.1f);
        
             }


         }/*
           direction.y -= gravity * Time.deltaTime;*/
        pos.y -= gravity * Time.deltaTime;
       
        ch.Move(pos * Time.deltaTime);
        
    }
    void StopJump()
    {
        ch.height = 1.6f;
        anim.SetBool("jump", false);
       
    }
    void StopJumpOver()
    {
        ch.height = 1.6f;
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
