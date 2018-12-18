using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform PlayerLeft;
	public Transform PlayerRight;

	public Vector3 midpoint;
	private float offset;

	private Camera m_Camera;

	void Start(){
		m_Camera = GetComponent<Camera>();
		offset = 3f;
	}

	void Update(){
		//find midpoint of the two characters, then add the Y_offset so camera can see above characters
		midpoint = new Vector3(((PlayerLeft.position.x + PlayerRight.position.x)/2), ((PlayerLeft.position.y + PlayerRight.position.y)/2) + offset, -10);

		m_Camera.transform.position = midpoint;

        float mag = midpoint.x * midpoint.x + midpoint.y * midpoint.y;
        mag = Mathf.Sqrt(mag);
        //Test stuff, pls ignore
        /*if(mag > 3)
        {
            m_Camera.orthographicSize = 6.0f;
        }
        else
        {
            m_Camera.orthographicSize = 5.0f;
        }*/
        

    }
}
