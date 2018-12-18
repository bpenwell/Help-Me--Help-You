using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Basic movement class. I imagine we'll add more to it if we mess with animations
public class PlayerMovement : MonoBehaviour {

    public GameObject playerFacingLeftTool;
    public GameObject playerFacingRightTool;
    private bool isToolOut;

    //X movement Stuff
    private Rigidbody2D bodyRef;
    public float speed;
    private Vector2 speedVec;
    private GameManager m_GameManager;

    //Y movement stuff
    private Grounded groundRef;
    public float jumpTime;  //Used to calculate how far in the air we go after hitting jump
    private float remainingJump;
    private bool m_overLadder;
    public int m_currentPlayerType;

    //player tags- left can shoot arrows, right can mine. neither can jump in "blue mode"
    public int playerNum = 0; //0 = left; 1 = right



	// Use this for initialization
	void Start () {
        isToolOut = false;
        playerFacingLeftTool.SetActive(false);
        playerFacingRightTool.SetActive(false);

        bodyRef = gameObject.GetComponent<Rigidbody2D>();
        speed = 10f;
        speedVec = new Vector2(0, 0);
        groundRef = gameObject.GetComponentInChildren<Grounded>();
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        jumpTime = .2f;
        remainingJump = 0;
        m_currentPlayerType = 2;
        if (playerNum == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
	
	// Update is called once per frame
	void Update () {
        speedVec = new Vector2(0, 0); //Initialize velocity as 0 every frame
        //So long as the player hasn't completed the level, they should be able to move
        if (m_GameManager.isGameComplete() == false)
        {
            speedVec = new Vector2(0, 0); //Initialize velocity as 0 every frame
            if (groundRef.grounded)
            {
                remainingJump = jumpTime;
            }
            //Changing character; color is currently the only thing that changes
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Sets isToolOut bool to determine if tool should be displayed. The direction the player requires 2 tool sprites, see line 106 
                Debug.Log("Switch Character");
                if (isToolOut)
                {
                    isToolOut = false;
                    playerFacingLeftTool.SetActive(false);
                    playerFacingRightTool.SetActive(false);
                }
                else
                {
                    isToolOut = true;
                    if (GetComponent<SpriteRenderer>().flipX)
                    {
                        playerFacingLeftTool.SetActive(true);
                    }
                    else
                    {
                        playerFacingRightTool.SetActive(true);
                    }
                }

                if (m_currentPlayerType == 2)
                {
                    //GetComponent<SpriteRenderer>().color = Color.blue;
                    if(playerNum == 0) //if left player, swap to bow mode
                    {
                        m_currentPlayerType = 0;
                        GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else //if right player, swap to mine mode
                    {
                        m_currentPlayerType = 1;
                        GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    //m_currentPlayerType = 1;
                }
                else if (m_currentPlayerType != 2) //otherwise swap to default
                {
                    if(playerNum == 0)
                    {
                        GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                    
                    m_currentPlayerType = 2;
                }
            }

            //Moving left or right; left trumps right movement
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (isToolOut)
                {
                    playerFacingLeftTool.SetActive(true);
                    playerFacingRightTool.SetActive(false);
                }
                GetComponent<SpriteRenderer>().flipX = true;
                speedVec += new Vector2(-speed, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (isToolOut)
                {
                    playerFacingLeftTool.SetActive(false);
                    playerFacingRightTool.SetActive(true);
                }
                GetComponent<SpriteRenderer>().flipX = false;
                speedVec += new Vector2(speed, 0);
            }

            //Moving up
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && m_currentPlayerType == 2)
            {
                if (m_overLadder)
                {
                    speedVec += (Vector2.up * speed);
                    remainingJump = jumpTime;

                }
                else if (remainingJump > 0)
                {
                    remainingJump -= Time.deltaTime;
                    speedVec += (Vector2.up * speed);
                }
            }
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && m_overLadder)
            {
                speedVec += (Vector2.down * speed);
            }
            //Quick change to make falling faster
            else if (!groundRef.grounded && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                speedVec += (Vector2.down * speed);
            }
            //Jumping; must be touching a floor to jump. Hold up to jump higher
            /*else if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && groundRef.grounded && remainingJump > 0)
            {
                remainingJump -= Time.deltaTime;
                speedVec += (Vector2.up * speed);
            }*/
            /*else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && remainingJump > 0)
            {
                remainingJump -= Time.deltaTime;
                speedVec += (Vector2.up * speed);
            }*/

            //This is so we fall faster. Might not be necessary.
            if (!groundRef.grounded && m_overLadder == false)
            {
                if (!Input.GetKey(KeyCode.UpArrow) || remainingJump <= 0)
                {
                    bodyRef.gravityScale = 15;
                }
            }
            else if (m_overLadder && !groundRef.grounded)
            {
                bodyRef.gravityScale = 0;
            }
            else
            {
                bodyRef.gravityScale = 1;
            }

            //final line updates velocity
            bodyRef.velocity = speedVec;

        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.tag == "Ladder"){
            Debug.Log(gameObject.name + ": Over Ladder");
            m_overLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Ladder"){
            m_overLadder = false;
        }
    }
    public int GetPlayerNum()
    {
        return m_currentPlayerType;
    }
}
