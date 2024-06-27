using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemy : ColorEnemy
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletY")
        {
            hp--;
        }
    }
}

