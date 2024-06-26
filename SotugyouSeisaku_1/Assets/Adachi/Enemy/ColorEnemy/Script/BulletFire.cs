//弾を発射

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [SerializeField] float span;
    float currentTime = 0;

    [SerializeField] GameObject EnemyBullet;
    [SerializeField] GameObject Search;
    SearchPlayer serchsscript;

    [SerializeField] AudioClip shot;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        BulletGenerate();
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        //Debug.Log(currentTime);
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    void StartUp()
    {
        serchsscript = Search.GetComponent<SearchPlayer>();

        //audioを取得
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 敵の弾を生成する処理
    /// </summary>
    void BulletGenerate()
    {
        if(serchsscript.search == true && currentTime > span) //spanがcurrentTimeを越えたら
        {
            Instantiate(EnemyBullet, this.transform.position, Quaternion.identity); //弾を撃つ
            audioSource.PlayOneShot(shot);
            currentTime = 0; //0に戻す
        }
    }
}
