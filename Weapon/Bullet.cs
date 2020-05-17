using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 hitPos;
    

    // Start is called before the first frame update
    void Start()
    {
        hitPos = Weapon.hitPoint;

        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPos, speed * Time.deltaTime);


    }
        








}//class
