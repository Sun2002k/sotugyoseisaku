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
            audioSource.PlayOneShot(hit);
        }
        else if(collision.gameObject.tag == "BulletB" || collision.gameObject.tag == "BulletY")
        {
            audioSource.PlayOneShot(unhit);
        }
    }

    #region デバッグ用
    //void FixedUpdate()
    //{
    //    Debug.Log(hp);
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        hp--;
    //        audioSource.PlayOneShot(hit);
    //    }
    //}
    #endregion
}
