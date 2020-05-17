using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform weaponParent;

    public Camera fpsCam;

    public Gun[] loadOut;

    public static Vector3 hitPoint;

    private int currentIndex;
    private GameObject currentWeapon;
    private ParticleSystem muzzelFlash;
    private Transform bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Equip(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Equip(1);

        if (currentWeapon != null)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            Aim(Input.GetMouseButton(1));
        }

    }

    void Equip(int gunKey)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentIndex = gunKey;

        GameObject newWeapon = Instantiate(loadOut[gunKey].prefab, weaponParent.position, weaponParent.rotation, weaponParent);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;

        currentWeapon = newWeapon;
        muzzelFlash = currentWeapon.transform.Find("Anchor/Resources/Muzzel_Flash").GetComponent<ParticleSystem>();
        bulletPos = currentWeapon.transform.Find("Anchor/Resources/Bullet_Spawn_Pos").GetComponent<Transform>();

    }

    void Aim(bool isaiming)
    {
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_ADS = currentWeapon.transform.Find("States/ADS");
        Transform t_Hip = currentWeapon.transform.Find("States/Hip");

        if(isaiming)
        {
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_ADS.position, Time.deltaTime * loadOut[currentIndex].aimSpeed);
        }
        else
        {
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_Hip.position, Time.deltaTime * loadOut[currentIndex].aimSpeed);
        }
    }

    void Shoot()
    {
        muzzelFlash.Play();

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, loadOut[currentIndex].range))
        {
            


            //--------------------Bullet Data--------------------
            Vector3 bullet_Spawn = transform.InverseTransformPoint(bulletPos.position);
            bullet_Spawn = transform.TransformPoint(bullet_Spawn);



            hitPoint = hit.point;

            GameObject bulletClone = Instantiate(loadOut[currentIndex].bullet, bullet_Spawn, Quaternion.identity);        
           

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if(target != null)
            {
                target.TakeDamage(loadOut[currentIndex].damage);
            }

        }


    }























}//class
