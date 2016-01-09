using UnityEngine;
using System.Collections;

//	 * WARNING : le cd pue la merde, il est set avec la vitesse du joueur au moment du tir, si cette vitesse change, le cd ne changera pas... une solution serait d'utiliser le framerate. mais pas stable
public class Weapon : MonoBehaviour {

	public Sprite onFloorSprite;
	public Sprite EquipedSprite;
	public int	  munition;
	public Bullet bullet;
	public float coolDown = 1;
	public AudioSource	Fire_shoot;
	public bool isSet = false;
	private charactercontroler Player = null;
	public bool usable = true;
	private Rigidbody2D rb;

	private	float prevTimeset = timemachine.timeset;
	/*
	 * get the weapon
	 */
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player" && !isSet && !coll.gameObject.GetComponent<charactercontroler>().weaponIsSet)
		{
			Player = coll.gameObject.GetComponent<charactercontroler>();
			if (!coll.gameObject.GetComponent<charactercontroler>().weaponIsSet)
			{
				isSet = true;
				GetComponent<Renderer>().sortingOrder = 3;
				GetComponent<SpriteRenderer>().sprite = EquipedSprite;
				coll.gameObject.GetComponent<charactercontroler>().weaponIsSet = true;
				transform.parent = coll.gameObject.GetComponent<charactercontroler>().transform;
				gameObject.layer = 3;
				gameObject.transform.localPosition = new Vector3(0.25f,-0.2f); 
				gameObject.transform.localEulerAngles = new Vector3(0,0); 
				gameObject.transform.localScale = new Vector3 (1,1,1);
			}
		}

	}

	void dropWeapon()
	{
		GetComponent<Renderer>().sortingOrder = 0;
		GetComponent<SpriteRenderer>().sprite = onFloorSprite;
		transform.parent = null;
		Player.weaponIsSet = false;
		Player = null;
	}

	IEnumerator throwingMovement()
	{
		Vector3 MousePos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		MousePos.z = 0;
		float speed = 0.7f;

		GetComponent<Renderer>().sortingOrder = 1;
		while (speed > 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, MousePos, speed*timemachine.timeset);
			speed -= 0.05f * timemachine.timeset;
			yield return new WaitForEndOfFrame();
		}
		isSet = false;
		
	}

	/*
	 * WARNING : le cd pue la merde, il est set avec la vitesse du joueur au moment du tir, si cette vitesse change, le cd ne changera pas... une solution serait d'utiliser le framerate. mais pas stable
	 * a verifier / changer !
	 */
	public IEnumerator CoolDownWeapon()
	{
		usable = false;
		float cd = coolDown;
		while (cd > 0) 
		{
			cd -= timemachine.timeset * 0.1f;
			yield return new WaitForEndOfFrame();
		}
		usable = true;
	}
	public void shot(Vector3 pos)
	{
		Bullet newBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as Bullet;
		newBullet.setDirection(pos);
		//newBullet.transform.parent = this.gameObject.transform;
		if (this.tag == "enemyWeapon"){newBullet.tag = "bulletenemy";}
		Fire_shoot.Play ();
		munition--;
	}

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Fire_shoot = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isSet && Player) {
			if (Input.GetMouseButtonDown (0) && usable && (munition >= 0 || gameObject.tag == "cac" )) {
				shot (Camera.main.ScreenToWorldPoint(Input.mousePosition));
				StartCoroutine (CoolDownWeapon ());
			} else if (Input.GetMouseButtonDown (1) && Player) {
				dropWeapon ();
				StartCoroutine (throwingMovement ());
			}
		}
		prevTimeset = timemachine.timeset;
	}
}
