using System.Collections;
using System.Collections.Generic;
using UnityEngine;



abstract class Bullet {
    public abstract string dmgType { get; }
    public abstract double payload();
    public abstract double dmgMultiplier { get; }
    public abstract float speed { get; }
    public abstract  double dmg { get; }
}

class slug : Bullet
{
  

    public override string dmgType => "lethal";

    public override double dmgMultiplier => 2;

    public override double dmg => 5;

    public override float speed => 2;

    public override double payload()
    {
        return dmg * dmgMultiplier;
    }
}


class stun : Bullet
{
  

    public override string dmgType => "lethal";

    public override double dmgMultiplier => 1;

    public override double dmg => 5;

    public override float speed => 5;

    public override double payload()
    {
        return dmg *dmgMultiplier;
    }
}



public class Bullets : MonoBehaviour
{

    public GameObject shootingDrone;
    public GameObject bulletPrefab;
    public GameObject stunPrefab;
    public Transform spawnpoint;
    public bool isLethal;


    public void shootBullet() {
       
     
   
  
        if (isLethal == true)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            Bullet bulletType = new slug();

            bullet.transform.position = spawnpoint.position;
            bullet.transform.rotation = Quaternion.Euler(90, 0, 0);
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
            spawnpoint.parent.GetComponent<Collider>());
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, bulletType.speed), ForceMode.Impulse);
            Destroy(bullet, 2f);
        }
        else {
            GameObject stun = Instantiate(stunPrefab);

            Bullet bulletType = new stun();
            stun.transform.position = spawnpoint.position;
            stun.transform.rotation = Quaternion.Euler(90, 0, 0);
            Physics.IgnoreCollision(stun.GetComponent<Collider>(),
            spawnpoint.parent.GetComponent<Collider>());

            float directionalitySpeed = 0f;
            if (shootingDrone.transform.forward.z >= 0) {
                directionalitySpeed = bulletType.speed;
            }
            else
            {
                directionalitySpeed = -bulletType.speed;
            }
            stun.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, directionalitySpeed) , ForceMode.Impulse);

            Destroy(stun, 2f);
           
        }
      
        

    }


   
    private void Start()
    {
        shootBullet();
    }

}