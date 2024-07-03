using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : ColorEnemy
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletB")
        {
            hp--;
            audioSource.PlayOneShot(hit);
        }
        else if (collision.gameObject.tag == "BulletR" || collision.gameObject.tag == "BulletY")
        {
            audioSource.PlayOneShot(unhit);
        }
    }
}
