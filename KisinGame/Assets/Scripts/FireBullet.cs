using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    int laserBulletDamage = 20;
    float laserBulletSpeed = 15;
    Vector3 targetDirection = new Vector3(1, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity, transform);
        LaserBullet laserBullet = bulletClone.GetComponent<LaserBullet>();
        laserBullet.SetParameters(laserBulletDamage, laserBulletSpeed, targetDirection);
        //Debug.Break();

    }
}
