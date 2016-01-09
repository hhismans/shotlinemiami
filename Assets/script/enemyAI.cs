using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	// Use this for initialization
	public Weapon weapon;
	private GameObject player;
	public GameObject bloodEffect;

	void Start () {
		weapon = Instantiate(weapon, transform.position, Quaternion.identity) as Weapon;
		setWeapon(weapon);
		player = GameObject.Find("player");	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "bullet" || coll.tag == "cac")
		{
			Instantiate(bloodEffect, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
	public void setWeapon (Weapon weapon)
	{
		weapon.transform.parent = gameObject.transform;
		weapon.gameObject.layer = 3;
		weapon.gameObject.transform.localPosition = new Vector3(0.25f,-0.2f); 
		weapon.gameObject.transform.localEulerAngles = new Vector3(0,0); 
		weapon.gameObject.transform.localScale = new Vector3 (1,1,1);

		weapon.isSet = true;
		weapon.GetComponent<Renderer>().sortingOrder = 3;
		weapon.GetComponent<SpriteRenderer>().sprite = weapon.EquipedSprite;
		weapon.tag = "enemyWeapon";
	}
	bool canSeePlayer()
	{
		return true;
	}
	void FixedUpdate () {
		if (canSeePlayer() && weapon.usable == true)
		{
			weapon.shot (player.transform.position);
			weapon.StartCoroutine(weapon.CoolDownWeapon());
			//Bullet newBullet = Instantiate(weapon.bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as Bullet;
			//newBullet.setDirection(player.transform.position);
		}
	}
}
