using UnityEngine;
using System.Collections;

public class charactercontroler : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D rb;
	public	float		speed;
	public	bool		weaponIsSet = false;
	public Camera		camera;


	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();	
		Debug.Log(speed);
		Debug.Log(timemachine.timeset);
	}
	
	// Update is called once per frame
	
	public void setWeapon (Weapon weapon)
	{
		weapon.transform.parent = gameObject.transform;
		weapon.gameObject.layer = 3;
		weapon.gameObject.transform.localPosition = new Vector3(0.25f,-0.2f); 
		weapon.gameObject.transform.localEulerAngles = new Vector3(0,0); 
		weapon.gameObject.transform.localScale = new Vector3 (1,1,1);
	}

	void Update()
	{
	/*	if (Input.GetKey("space"))
		{
			transform.position = Vector3.MoveTowards(camera.transform.position, 1); //esquise de tentative, mais jsuis creve
				
		}
		else*/
	}
	void FixedUpdate () {
		//speed *= timemachine.timeset;
		//move
		camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
		float moveH = Input.GetAxis ("Horizontal");
		float moveV = Input.GetAxis("Vertical");

		timemachine.timeset = (moveH + moveV);
		//setting timeset 
		if (timemachine.timeset < 0)
		    timemachine.timeset = -timemachine.timeset;
		if (timemachine.timeset > 1)
			timemachine.timeset = 1;
		rb.MovePosition(transform.position + new Vector3 (moveH * speed, moveV * speed));

		// ROTATION
		Vector3 pos;
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		
		var angle = Vector3.Angle(pos-transform.position,Vector3.down);
		
		if(pos.x > transform.position.x)
			angle*=-1;
		
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,180 - angle),1);

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "bulletenemy")
		{
			Debug.Log ("you loose, l2p noob");
		}
		if (col.gameObject.tag == "tp") {
			Debug.Log("Tp ok");
			Application.LoadLevel(2);
		}
	}
}