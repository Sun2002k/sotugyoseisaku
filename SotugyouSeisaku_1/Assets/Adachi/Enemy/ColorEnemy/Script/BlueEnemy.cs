using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : ColorEnemy
{
    protected override void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletB")
        {
            hp--;
        }
    }
}
