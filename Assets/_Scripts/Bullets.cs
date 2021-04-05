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

    public override float speed => 15;

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

            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = shootingDrone.transform.position;
                bullet.transform.rotation = shootingDrone.transform.rotation;
                bullet.SetActive(true);
            }

    
            Bullet bulletType = new slug();

            bullet.transform.position = spawnpoint.position;
            bullet.transform.rotation = Quaternion.Euler(90, 0, 0);
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
            spawnpoint.parent.GetComponent<Collider>());

            float directionalitySpeed = 0f;
            if (shootingDrone.transform.forward.z >= 0)
            {
                directionalitySpeed = bulletType.speed;
            }
            else
            {
                directionalitySpeed = -bulletType.speed;
            }
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, bulletType.speed), ForceMode.Impulse);

            StartCoroutine(repool(bullet));

           
        }
        else {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = shootingDrone.transform.position;
                bullet.transform.rotation = shootingDrone.transform.rotation;
                bullet.SetActive(true);
            }


            Bullet bulletType = new stun();
            bullet.transform.position = spawnpoint.position;
            bullet.transform.rotation = Quaternion.Euler(90, 0, 0);
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
            spawnpoint.parent.GetComponent<Collider>());

            float directionalitySpeed = 0f;
            if (shootingDrone.transform.forward.z >= 0) {
                directionalitySpeed = bulletType.speed;
            }
            else
            {
                directionalitySpeed = -bulletType.speed;
            }
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, directionalitySpeed) , ForceMode.VelocityChange);

            StartCoroutine(repool(bullet));

        }
      
        

    }


   
    private void Start()
    {
        shootBullet();
    }

    //return bullet back to pool after 2 seconds co routine
    IEnumerator repool(GameObject bullet)
    {
         yield return new WaitForSeconds(2);
         bullet.SetActive(false);
    }


}

