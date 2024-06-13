//プレイヤーを探す

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    [HideInInspector] public bool search; //プレイヤーを見つける

    GameObject Enemy;
    Enemymove enemyscript;
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(search == true)
        {
            enemyscript.EnemyMove();
        }
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    void StartUp()
    {
        search = false; //searchをfalseにする

        Enemy = transform.parent.gameObject;
        enemyscript = Enemy.GetComponent<Enemymove>();
    }

    //IsTriggerと接触したら
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            search = true; //searchをtrueにする
        }
    }

    //IsTriggerと接触しなかったら
    private void OnTriggerExit(Collider other)
    {
        search = false; //searchをfalseにする
    }
}
