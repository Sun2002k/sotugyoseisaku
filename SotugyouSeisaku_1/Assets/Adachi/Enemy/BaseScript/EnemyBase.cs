//敵の基本動作

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyBase : MonoBehaviour
{
    #region ベクター
    Vector3 v_enemy; //敵のベクター
    Vector3 v_player; //プレイヤーのベクター
    Vector3 v_range; //敵とプレイヤーの距離
    Vector3 v_move;
    #endregion

    #region ゲームオブジェクト
    GameObject player;

    #endregion

    #region 変数
    [SerializeField] float speed;
    [SerializeField] protected float hp = 2;
    protected float angle = 270; // 敵が回転する角度
    #endregion

    #region 効果音
    [SerializeField] protected AudioClip walk; //音素材
    protected AudioSource audioSource;
    #endregion

    virtual protected void Start()
    {
        StartUp();
    }

    virtual protected void Update()
    {
        Death();
    }


    virtual protected void EnemyMove()
    {
        BaseMove();

        SeStart();
    }


    #region 関数定義
    /// <summary>
    /// 初期設定
    /// </summary>
    void StartUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //audioを取得
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// hpが0になったら破壊される
    /// </summary>
    void Death()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーを追いかける
    /// </summary>
    public void BaseMove()
    {
        if(hp > 0)
        {
            //プレイヤーの元に行く
            v_enemy = this.transform.position;   //敵の座標
            v_player = player.transform.position; //プレイヤーの座標
            v_range = v_player - v_enemy; //プレイヤーの座標と敵の座標相対ベクター
            v_move = v_range * speed; //相対ベクターにスピードを掛ける
            this.transform.position += v_move;

            //プレイヤーの方へ向く処理
            float theta = Mathf.Atan2(v_player.x - v_enemy.x, v_player.z - v_enemy.z);//プレイヤーの座標と敵の座標をxとzに分けてそれぞれ計算
            float deg = -(angle - theta * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Euler(new Vector3(0, deg, 0));//プレイヤーの方へ向く
        }
        
    }

    /// <summary>
    /// 歩くSEを再生
    /// </summary>
    void SeStart()
    {
        audioSource.PlayOneShot(walk);
    }

    //プレイヤーの弾に当たった処理
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BulletR" ||
           collision.gameObject.tag == "BulletB" || 
           collision.gameObject.tag == "BulletY")
        {
            hp --;
        }
    }
    #endregion
}
