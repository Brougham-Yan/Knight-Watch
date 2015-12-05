using UnityEngine;
using System.Collections;

public class Boss : RaycastController {
	
	public int maxHealth = 100;
	public int curHealth;
	
	public float distance;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;

	public bool lookingRight = true;
	
	public GameObject bullet;
	public Transform target;
	public Animator anim;
	public Transform shootPoint;
	public Transform shootPoint1;
	public Transform shootPoint2;

	public GameObject laserbeam1;
	public GameObject laserbeam2;
	
	public Vector3 move;

	public bool attack1;
	public int a1phase = 0;
	public float a1speed = 28f;

	public bool attack2;
	public int a2phase = 0;
	public float a2speed = 7f;

	public bool attack3;
	public int a3phase = 0;
	public float a3speed = 7f;
	public float a3speed2 = .5f;
	public float shottime = 5f;
	public float shottimer = 0;

	public float time;
	public float maxTime = 2f;

	public AudioClip shot;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		
	}
	public override void Start()
	{
		base.Start ();
		curHealth = maxHealth;
		time = maxTime;
	}
	
	void Update()
	{			
		UpdateRayCastOrigins ();


		if(attack1==false&&attack2==false&&attack3==false)
		{
			if((Mathf.Abs(transform.position.x-36)>1||Mathf.Abs(transform.position.y-54)>1)&&a1phase==0)
			{
				Vector3 velocity = (new Vector3(36,54,0)-transform.position) * 7 *Time.deltaTime;
				transform.Translate (velocity);
			}
			else
			{
				if(time>0)
				{
					time -= Time.deltaTime;
				}
				else
				{
					switch((int)Random.Range (1f,3.99f))
					{
					case 1:attack1 = true;
						break;
					case 2:attack2 = true;
						break;
					case 3:attack3 = true;
						break;
					default:
						break;
					}
					
					time=maxTime;
				}
			}
		}




		if(attack1)
		{
			if((Mathf.Abs(transform.position.x-36)>1||Mathf.Abs(transform.position.y-54)>1)&&a1phase==0)
			{
				Vector3 velocity = (new Vector3(36,54,0)-transform.position) * 7 *Time.deltaTime;
				transform.Translate (velocity);
			}
			else
			{
				if(a1phase<1)
					a1phase=1;

				laserbeam1.GetComponent<SpriteRenderer>().enabled=true;
				laserbeam2.GetComponent<SpriteRenderer>().enabled=true;
				laserbeam1.GetComponent<BoxCollider2D>().enabled=true;
				laserbeam2.GetComponent<BoxCollider2D>().enabled=true;

				if(Mathf.Abs(transform.eulerAngles.z-180)>1&&a1phase==1)
				{
					transform.Rotate (Vector3.back,a1speed*Time.deltaTime);
				}
				else
				{
					if(a1phase<2)
						a1phase=2;
						
					if(Mathf.Abs(transform.eulerAngles.z-360)>1&&a1phase==2)
					{
						transform.Rotate (Vector3.back,a1speed*Time.deltaTime);
					}
					else
					{
						laserbeam1.GetComponent<SpriteRenderer>().enabled=false;
						laserbeam2.GetComponent<SpriteRenderer>().enabled=false;
						laserbeam1.GetComponent<BoxCollider2D>().enabled=false;
						laserbeam2.GetComponent<BoxCollider2D>().enabled=false;
						transform.localEulerAngles = new Vector3(0,0,0);
						attack1=false;
						a1phase=0;
					}
				}
			}

		}

		if(attack2)
		{
			if((Mathf.Abs(transform.position.x-80)>1||Mathf.Abs(transform.position.y-71)>1)&&a2phase==0)
			{
				Vector3 velocity = (new Vector3(80,71,0)-transform.position) * a2speed *Time.deltaTime;
				transform.Translate (velocity);
			}
			else
			{

				if(a2phase<1)
					a2phase=1;

				if(Mathf.Abs(transform.position.y-37.9f)>1&&a2phase==1)
				{
					Vector3 velocity = (new Vector3(80,37.9f,0)-transform.position) * a2speed *Time.deltaTime;
					transform.Translate (velocity);
				}
				else
				{
					if(a2phase<2)
						a2phase=2;

					if(Mathf.Abs(transform.position.x-(-6.2f))>1&&a2phase==2)
					{
						Vector3 velocity = (new Vector3(-6.2f,37.9f,0)-transform.position) * a2speed *Time.deltaTime;
						transform.Translate (velocity);
					}
					else
					{
						if(a2phase<3)
						a2phase=3;

						if(Mathf.Abs(transform.position.y-71)>1&&a2phase==3)
						{
							Vector3 velocity = (new Vector3(-6.2f,71,0)-transform.position) * a2speed *Time.deltaTime;
							transform.Translate (velocity);
						}
						else
						{
							if(a2phase<4)
								a2phase=4;

							if(Mathf.Abs(transform.position.x-80)>1&&a2phase==4)
							{
								Vector3 velocity = (new Vector3(80,71,0)-transform.position) * a2speed *Time.deltaTime;
								transform.Translate (velocity);
							}
							else
							{
								attack2=false;
								a2phase=0;
							}

						}
					}
				}

			}
			
		}
		if(attack3)
		{
			if((Mathf.Abs(transform.position.x-(-17.9f))>1||Mathf.Abs(transform.position.y-37.7f)>1)&&a3phase==0)
			{
				Vector3 velocity = (new Vector3(-17.9f,37.7f,0)-transform.position) * a3speed *Time.deltaTime;
				transform.Translate (velocity);
			}
			else
			{
				if(a3phase<1)
				{
					a3phase=1;
					shottimer = shottime;
				}

				if(shottimer>0 && a3phase==1)
				{
					Attack();
					shottimer -= Time.deltaTime;
				}
				else
				{
					if(a3phase<2)
					{
						a3phase=2;
						shottimer = shottime;
					}


					if(Mathf.Abs(transform.position.y-53.7f)>1&&a3phase==2)
					{
						Vector3 velocity = (new Vector3(-17.9f,53.7f,0)-transform.position) * a3speed * Time.deltaTime;
						transform.Translate (velocity);
					}
					else
					{
						if(a3phase<3)
						{
							a3phase=3;
							shottimer = shottime;
						}

						if(shottimer>0 && a3phase==3)
						{
							Attack();
							shottimer -= Time.deltaTime;
						}
						else
						{
							if(a3phase<4)
							{
								a3phase=4;
								shottimer = shottime;
							}

							if(Mathf.Abs(transform.position.y-73f)>1&&a3phase==4)
							{
								Vector3 velocity = (new Vector3(-17.9f,73f,0)-transform.position) * a3speed * Time.deltaTime;
								transform.Translate (velocity);
							}
							else
							{
								if(a3phase<5)
								{
									a3phase=5;
									shottimer = shottime;
								}

								if(shottimer>0 && a3phase==5)
								{
									Attack();
									shottimer -= Time.deltaTime;
								}
								else
								{
									attack3=false;
									a3phase = 0;
								}
							}
						}
					}

				}
			}		
		}


		
		if (curHealth <= 0) {
			anim.Play ("EnemyDie");
			//Destroy(GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ());
			//Application.LoadLevel ("mainMenu");
			}
		}

	public void Attack()
	{
		if(curHealth<=0)
		{
			return;
		}
		bulletTimer += Time.deltaTime;
		if(bulletTimer >= shootInterval)
		{
			
			GameObject bulletClone;
			bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D>().velocity = Vector2.right*bulletSpeed;
			GameObject bulletClone1;
			bulletClone1 = Instantiate(bullet, shootPoint1.transform.position, shootPoint1.transform.rotation) as GameObject;
			bulletClone1.GetComponent<Rigidbody2D>().velocity = Vector2.right*bulletSpeed;
			GameObject bulletClone2;
			bulletClone2 = Instantiate(bullet, shootPoint2.transform.position, shootPoint2.transform.rotation) as GameObject;
			bulletClone2.GetComponent<Rigidbody2D>().velocity = Vector2.right*bulletSpeed;

			AudioSource.PlayClipAtPoint(shot, shootPoint.transform.position);

			bulletTimer = 0;
			
		}
	}
	
	public void Damage(int damage)
	{
		curHealth -= damage;
	}
	
	public void DestroyGameObject()
	{
		Destroy(gameObject);
	}
	
}
