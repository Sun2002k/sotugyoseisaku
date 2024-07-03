using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : EnemyBase
{
    //ƒvƒŒƒCƒ„[‚Ì’e‚É“–‚½‚Á‚½ˆ—
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletR" ||
           collision.gameObject.tag == "BulletB" ||
           collision.gameObject.tag == "BulletY")
        {
            hp--;
            audioSource.PlayOneShot(hit);
        }
    }
}

