using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class powerUp {
    public abstract float powerupTime { get; }
    public abstract float maxAmount { get; }
    public abstract float currentCount { get; }

  

}

class invisibility : powerUp {

    public override float powerupTime => 5;

    public override float maxAmount => 1;

    public override float currentCount => 0;
}

class infared : powerUp
{
    public override float powerupTime => 10;

    public override float maxAmount => 2;

    public override float currentCount => 0;

}

public class powerUps : MonoBehaviour
{
   
}
