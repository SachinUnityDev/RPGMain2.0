//
//
//NOTES:
//
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//THIS IS JUST A BASIC EXAMPLE PUT TOGETHER TO DEMONSTRATE VFX ASSETS.
//
//




#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using DG.Tweening; 

public class VFXMoveScript : MonoBehaviour 
{

    public bool rotate = false;
    public float rotateAmount = 45;
    public bool bounce = false;
    public float bounceForce = 10;
     float speed =25f;
	[Tooltip("From 0% to 100%")]
	public float accuracy;
	public float fireRate;
	public GameObject muzzlePrefab;
	public GameObject hitPrefab;
	public List<GameObject> trails;

    private Vector3 startPos;
	//private float speedRandomness;
	private Vector3 offset;
	private bool collided = false;
	private Rigidbody rb;
    //private RotateToMouseScript rotateToMouse;
    public Vector3 targetPos;

	void Start () {
        startPos = transform.position + new Vector3(0,1,0);
        rb = GetComponent <Rigidbody> ();
        transform.DOLookAt(targetPos, 0.001f);
        //used to create a radius for the accuracy and have a very unique randomness
        if (accuracy != 100) {
			accuracy = 1 - (accuracy / 100);

			for (int i = 0; i < 2; i++) {
				var val = 1 * Random.Range (-accuracy, accuracy);
				var index = Random.Range (0, 2);
				if (i == 0) {
					if (index == 0)
						offset = new Vector3 (0, -val, 0);
					else
						offset = new Vector3 (0, val, 0);
				} else {
					if (index == 0)
						offset = new Vector3 (0, offset.y, -val);
					else
						offset = new Vector3 (0, offset.y, val);
				}
			}
		}
			
		if (muzzlePrefab != null) {
			var muzzleVFX = Instantiate (muzzlePrefab, transform.position, Quaternion.identity);
			muzzleVFX.transform.forward = gameObject.transform.forward + offset;
			var ps = muzzleVFX.GetComponent<ParticleSystem>();
			if (ps != null)
				Destroy (muzzleVFX, ps.main.duration);
			else {
				var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
				Destroy (muzzleVFX, psChild.main.duration);
			}
		}
	}

	void FixedUpdate () {

        if(Mathf.Abs(rb.position.x-targetPos.x) <= 1.25f)
        {
            TargetReached(targetPos, Vector3.up);
        }

        //if (target != null)
        //    rotateToMouse.RotateToMouse (gameObject, targetPos);
        //if (rotate)
        //    transform.Rotate(0, 0, rotateAmount, Space.Self);
        if (speed != 0 && rb != null)
        {
            //  rb.position += (transform.forward + offset) * (speed * Time.deltaTime);
            rb.position += (transform.forward) * (speed * Time.deltaTime);
          //  Debug.Log("INSide update" + rb.position);
            transform.LookAt(targetPos, Vector3.up );

        }
    }

    void TargetReached(Vector3 _targetPos, Vector3 _targetNormal)
    {
        collided = true;
        Debug.Log("INSIDE COLLIDER ");
        if (trails.Count > 0)
        {
            trails[0].transform.parent = null;
            for (int i = 0; i < trails.Count; i++)
            {

                var ps = trails[i].GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }
        SkillService.Instance.OnTargetReached();
        speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;

       // ContactPoint contact = collide.contacts[0];
          Quaternion rot = Quaternion.FromToRotation(Vector3.up, _targetNormal);

       // Quaternion rot = Quaternion.FromToRotation(Vector3.up, _targetPos);
     
        Vector3 pos = _targetPos;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;

            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
                Destroy(hitVFX, ps.main.duration);
        }

        StartCoroutine(DestroyParticle(0));

    }

	void OnCollisionEnter (Collision collide) {
        //if (!bounce)
        //{
            if (collide.gameObject.tag == "Char" && !collided)
            {

               // pass in the vector 3 value here .. 
                TargetReached(collide.contacts[0].point, collide.contacts[0].normal);
            }
        //}
        //else
        //{
        //    rb.useGravity = true;
        //    rb.drag = 0.5f;
        //    ContactPoint contact = co.contacts[0];
        //    rb.AddForce (Vector3.Reflect((contact.point - startPos).normalized, contact.normal) * bounceForce, ForceMode.Impulse);

        //    Destroy ( this );
        //}
	}

	public IEnumerator DestroyParticle (float waitTime) {

		if (transform.childCount > 0 && waitTime != 0) {
			List<Transform> tList = new List<Transform> ();

			foreach (Transform t in transform.GetChild(0).transform) {
				tList.Add (t);
			}		

			while (transform.GetChild(0).localScale.x > 0) {
				yield return new WaitForSeconds (0.01f);
				transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				for (int i = 0; i < tList.Count; i++) {
					tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				}
			}
		}
		
		yield return new WaitForSeconds (waitTime);

		Destroy (gameObject);
	}

    //public void SetTarget (GameObject trg, RotateToMouseScript rotateTo)
    //{
    //    //target = trg;
    //    //rotateToMouse = rotateTo;
    //}
}
