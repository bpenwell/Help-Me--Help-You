using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxIndicator : MonoBehaviour {
    public GameObject crosshairs;
    public GameObject arrow;
    public GameObject PlayerLeft;
    public GameObject PlayerRight;

    [Header("Boulder sprites")]
    public Sprite boulder_touchedOnce;
    public Sprite boulder_touchedTwice;
    public Sprite boulder_touchedThrice;

    private PlayerMovement m_Player;
    private GameManager m_GameManager;
    public float speed;

    private GameObject[] cursor;
    private Vector3 offset;
    //=> is a private property. My coworker showed me this. Essentially a function
    //private Vector3 LeftPlayerPosition_notFlipped => PlayerLeft.transform.position + offset;
    //private Vector3 LeftPlayerPosition_Flipped => PlayerLeft.transform.position - offset;
    //private Vector3 RightPlayerPosition_notFlipped => PlayerRight.transform.position + offset;
    //private Vector3 RightPlayerPosition_Flipped => PlayerRight.transform.position - offset;

    private void Start()
    {
        offset = new Vector3(0.35f,0f,0f);
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {

        //The player should not be able to shoot if the game is complete
        if (m_GameManager.isGameComplete() == false)
        {
            
            if (speed == 0)
            {
                speed = 1f;
            }
            m_Player = GameObject.FindGameObjectWithTag("LeftPlayer").GetComponent<PlayerMovement>();
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            objectPos.z = 0f;
            cursor = GameObject.FindGameObjectsWithTag("cursor");
            for (int i = 0; i < cursor.Length; i++)
            {
                Destroy(cursor[i]);

            }
            //only produce crosshairs if controlling player1
            if(m_Player.GetPlayerNum() == 0)
            {
                Instantiate(crosshairs, objectPos, Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //set shoot direction
                Vector3 shootDirection;
                shootDirection = Input.mousePosition;
                shootDirection.z = 0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection - m_Player.transform.position;

                int Player = m_Player.GetPlayerNum();
                //if character is eligible for bowshot 
                if (Player == 0)
                {
                    Vector3 relativePos = shootDirection - m_Player.transform.position;

                    // the second argument, upwards, defaults to Vector3.up
                    //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                    //added in code to look at the target. i didn't want to screw anything up with the above variables, so i tried to keep it contained
                    Vector3 target = Input.mousePosition;
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(m_Player.transform.position);
                    Vector2 offset = new Vector2(target.x - screenPoint.x, target.y - screenPoint.y);
                    float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;


                    GameObject arrowInstance = Instantiate(arrow, m_Player.transform.position, Quaternion.identity);
                    arrowInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
                    //Finally, rotate
                    arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                //if character is eligible for mining 
                else if (Player == 1)
                {
                    //mining should hit right in front of player
                    if(PlayerLeft.GetComponent<SpriteRenderer>().flipX == true){
                        RaycastHit2D hit = Physics2D.Raycast(PlayerLeft.transform.position - offset, Vector2.left,1f); 
                        if(hit.collider != null){
                            if(hit.collider.gameObject.tag == "boulder"){
                                Debug.Log("Hitting rock");
                                hit.collider.gameObject.GetComponent<boulderController>().hasBeenMined();
                                int MinedNum = hit.collider.gameObject.GetComponent<boulderController>().GetTimesMined();
                                if(MinedNum == 1){
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedOnce;
                                }
                                else if(MinedNum == 2){
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedTwice;
                                }
                                else if(MinedNum == 3){
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedThrice;
                                }
                                else if(MinedNum == 4){
                                    Destroy(hit.collider.gameObject);
                                }
                            }
                            Debug.DrawRay(PlayerLeft.transform.position - offset, Vector2.left, Color.white);
                        }
                        RaycastHit2D hit2 = Physics2D.Raycast(PlayerRight.transform.position - offset, Vector2.left,1f); 
                        if(hit2.collider != null){
                            if(hit2.collider.gameObject.tag == "boulder"){
                                Debug.Log("Hitting rock");
                                hit2.collider.gameObject.GetComponent<boulderController>().hasBeenMined();
                                int MinedNum = hit2.collider.gameObject.GetComponent<boulderController>().GetTimesMined();
                                if(MinedNum == 1){
                                    hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedOnce;
                                }
                                else if(MinedNum == 2){
                                    hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedTwice;
                                }
                                else if(MinedNum == 3){
                                    hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedThrice;
                                }
                                else if(MinedNum == 4){
                                    Destroy(hit2.collider.gameObject);
                                }
                            }
                            Debug.DrawRay(PlayerRight.transform.position - offset, Vector2.left, Color.white);  
                        }

                    }
                    else{
                        RaycastHit2D hit = Physics2D.Raycast(PlayerLeft.transform.position + offset, Vector2.right,1f);
                        if(hit.collider != null){
                            if(hit.collider.gameObject.tag == "boulder"){
                                Debug.Log("Hitting rock");
                                hit.collider.gameObject.GetComponent<boulderController>().hasBeenMined();
                                int MinedNum = hit.collider.gameObject.GetComponent<boulderController>().GetTimesMined();
                                if(MinedNum == 1){
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedOnce;
                                }
                                else if(MinedNum == 2){
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedTwice;
                                }
                                else if(MinedNum == 3){
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedThrice;
                                }
                                else if(MinedNum == 4){
                                    Destroy(hit.collider.gameObject);
                                }
                            }
                            Debug.DrawRay(PlayerLeft.transform.position + offset, Vector2.right, Color.white);
                        }

                        RaycastHit2D hit2 = Physics2D.Raycast(PlayerRight.transform.position + offset, Vector2.right,1f);
                        if(hit2.collider != null){
                            if(hit2.collider.gameObject.tag == "boulder"){
                                Debug.Log("Hitting rock");
                                hit2.collider.gameObject.GetComponent<boulderController>().hasBeenMined();
                                int MinedNum = hit2.collider.gameObject.GetComponent<boulderController>().GetTimesMined();
                                if(MinedNum == 1){
                                    hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedOnce;
                                }
                                else if(MinedNum == 2){
                                    hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedTwice;
                                }
                                else if(MinedNum == 3){
                                    hit2.collider.gameObject.GetComponent<SpriteRenderer>().sprite = boulder_touchedThrice;
                                }
                                else if(MinedNum == 4){
                                    Destroy(hit2.collider.gameObject);
                                }
                            }
                            Debug.DrawRay(PlayerRight.transform.position + offset, Vector2.right, Color.white);
                        }
                    }
                }
            }
        }
    }
}
