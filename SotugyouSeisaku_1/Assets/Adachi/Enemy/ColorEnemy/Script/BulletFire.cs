//’e‚ğ”­Ë

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
    /// ‰Šúİ’è
    /// </summary>
    void StartUp()
    {
        serchsscript = Search.GetComponent<SearchPlayer>();

        //audio‚ğæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// “G‚Ì’e‚ğ¶¬‚·‚éˆ—
    /// </summary>
    void BulletGenerate()
    {
        if(serchsscript.search == true && currentTime > span) //span‚ªcurrentTime‚ğ‰z‚¦‚½‚ç
        {
            Instantiate(EnemyBullet, this.transform.position, Quaternion.identity); //’e‚ğŒ‚‚Â
            audioSource.PlayOneShot(shot);
            currentTime = 0; //0‚É–ß‚·
        }
    }
}
