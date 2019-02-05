using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

	public int playerSpeed = 10;
	public int playerJumpPower = 1250;
	float moveX;
    public bool isGrounded;
    public float distanceToBottomOfPlayer = 0.9f;
    public bool tele = false;
    public Vector3 moveTo;
    private Vector3 respawn;
    private int jumpCount;
    private AudioSource audioSource;
    public AudioClip teleportSound;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        jumpCount = 0;
        moveTo = gameObject.transform.position;
        respawn = gameObject.transform.position;
    }

    
    
    // Update is called once per frame
	void Update () {
		PlayerMoveAlt();
        PlayerRaycast();
        
        if (tele && playerSpeed!=0)//wont allow teleportation during dialoug
        {
            moveTo = gameObject.transform.position;//so it wont move when you push the button
            if (Input.GetMouseButtonDown(0))
            {
                
                audioSource.clip = teleportSound;
                audioSource.Play();
                Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));
                newPos.z = gameObject.transform.position.z;
                if (newPos.y > this.gameObject.transform.position.y + 4)
                {
                    newPos.y = this.gameObject.transform.position.y + 4;
                }

                gameObject.transform.position = newPos;

                tele = false;
                moveTo = newPos;
            }
            
        }
        else if (tele)
        {
            tele = false;
        }
	}
    

    public void teleport(){
        tele = true;


    }

    void PlayerMoveAlt()
    {
        if (playerSpeed == 0)
        {
            moveTo = gameObject.transform.position;
        }
        
        if (gameObject.transform.position.y < -10)
        {
            gameObject.transform.position = respawn;
            moveTo = gameObject.transform.position;
        }
        
        if (Input.GetMouseButtonUp(0) && playerSpeed!=0)
        {
            moveTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
        }
        if (Mathf.Abs(gameObject.transform.position.x-moveTo.x)>0.1f)
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
            if (gameObject.transform.position.x - moveTo.x > 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                GetComponent<SpriteRenderer>().flipX = true;
                

            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                GetComponent<SpriteRenderer>().flipX = false;

            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Animator>().SetBool("IsRunning", false);
        }
    }
	void PlayerMove () {
        //CONTROLS
       

		moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true){
            Jump();
        }

		//ANIMATIONS
        if (moveX != 0) {
            GetComponent<Animator>().SetBool("IsRunning", true);
        } else {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }

		//PLAYER DIRECTION
		if (moveX < 0.0f ) {
            GetComponent<SpriteRenderer>().flipX = true;
		} else if (moveX > 0.0f ) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

		//PHYSICS
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	public void Jump () {
        //JUMPING CODE
        gameObject.transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 6f, gameObject.transform.position.z);
        isGrounded = false;
        jumpCount += 1;

    }

    void PlayerRaycast()
    {
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != null && rayUp.collider != null && rayUp.distance < distanceToBottomOfPlayer && rayUp.collider.name == "CMG_StarBox") {
            Destroy (rayUp.collider.gameObject);
        }
            RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        
        if (rayDown != null && rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag != "Enemy") {
            isGrounded = true;
            jumpCount = 0;

            
        }
    }
}
