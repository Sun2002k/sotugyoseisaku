using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : ColorEnemy
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BulletR")
        {
            hp--;
        }
    }
}
