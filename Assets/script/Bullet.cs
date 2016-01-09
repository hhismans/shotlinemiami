using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public 	float		speed = 30;
	public 	float			lifeTime = 10;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
	/*	Vector3 pos;
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		
		var angle = Vector3.Angle(pos-transform.position,Vector3.down);
		
		if(pos.x > transform.position.x)
			angle*=-1;
		
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,270 - angle),1);


		rb = GetComponent<Rigidbody2D>();
		//rb.velocity = transform.TransformDirection (Vector3.forward * speed);

		if (tag == "cac")
		{
			transform.position += transform.right * 0.3f;
			gameObject.GetComponent<Renderer>().sortingOrder = 10;
		}*/

	}

	public void setDirection(Vector3 pos)
	{
		/*Vector3 pos;
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		*/
		pos.z = 0;
		
		var angle = Vector3.Angle(pos-transform.position,Vector3.down);
		
		if(pos.x > transform.position.x)
			angle*=-1;
		
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,270 - angle),1);
		rb = GetComponent<Rigidbody2D>();
//		transform.position += transform.right * 1f;
		if (tag == "cac")
		{
			transform.position += transform.right * 0.3f;
			gameObject.GetComponent<Renderer>().sortingOrder = 10;
		}
	}

	IEnumerator cacAttack()
	{
		yield return new WaitForSeconds(0.1f);
		Destroy (this.gameObject);
	}
	IEnumerator BulletLifeTime()
	{
		float truelifeTime = lifeTime;
		while (truelifeTime > 0)
		{
			truelifeTime -= timemachine.timeset * 0.1f;
			yield return new WaitForEndOfFrame();
		}
		Destroy (this.gameObject);
	}

	void FixedUpdate () {
		if (tag != "cac")
		{
			rb.velocity = transform.right * speed * timemachine.timeset;
			StartCoroutine(BulletLifeTime ());
		}
		else
		{
			StartCoroutine(cacAttack ());
		}
	}
}
