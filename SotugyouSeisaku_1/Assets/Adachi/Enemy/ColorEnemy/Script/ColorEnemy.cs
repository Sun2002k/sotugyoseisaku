//EnemyBaseを継承した三色敵のスクリプト

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEnemy : EnemyBase
{
    #region 変数定義
    GameObject Search;
    SearchPlayer serchsscript;

    private Animator anim;
    #endregion

    override protected void Start()
    {
        base.Start();
        
        StartUp();
    }

    override protected void Update()
    {
        EnemyAnimation();

        DeathAnim();
    }


    #region 関数定義
    /// <summary>
    /// 初期設定
    /// </summary>
    void StartUp()
    {
        //継承元の変数を上書き
        hp = 4;
        angle = 0; 

        Search = transform.GetChild(0).gameObject; //0で一番目の子オブジェクトになる
        serchsscript = Search.GetComponent<SearchPlayer>();
        anim = GetComponent<Animator>();
    }

    //敵が倒される処理
    void DeathAnim()
    {
        //アニメーション
        if (hp <= 0)
        {
            anim.SetTrigger("Death");
        }
    }


    //Walkアニメーションの足が地面に付いたタイミングでeventとして呼び出される
    public void AnimWalk()
    {
        audioSource.PlayOneShot(walk);
    }
    //Deathアニメーションが終わった後にeventとして呼び出される
    public void AnimEnd()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 敵のアニメーション
    /// </summary>
    void EnemyAnimation()
    {
        //歩くアニメーション
        if (serchsscript.search == true)
        {
            anim.SetBool("flag", true);
        }
        else
        {
            anim.SetBool("flag", false);
        }
    }
    #endregion
}
