//プレイヤーを探す

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    #region ゲームオブジェクト
    [HideInInspector] public bool search; //プレイヤーを見つける

    GameObject Enemy;
    EnemyBase enemyscript;
    #endregion
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CatchPlayer();
    }


    #region 関数定義
    /// <summary>
    /// 初期設定
    /// </summary>
    void StartUp()
    {
        search = false; //searchをfalseにする

        Enemy = transform.parent.gameObject;
        enemyscript = Enemy.GetComponent<EnemyBase>();
    }

    /// <summary>
    /// プレイヤーを見つけた処理
    /// </summary>
    void CatchPlayer()
    {
        if (search == true)
        {
            enemyscript.BaseMove();
        }

        //Debug.Log(search);
    }

    //当たり判定に接触し続けていたら
    //OnTriggerStayは接触している時に呼ばれ続ける（OnTriggerEnterは一回しか呼ばれない）
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            search = true; //searchをtrueにする
        }
    }

    //当たり判定に接触しなかったら
    private void OnTriggerExit(Collider other)
    {
        search = false; //searchをfalseにする
    }
    #endregion
}
