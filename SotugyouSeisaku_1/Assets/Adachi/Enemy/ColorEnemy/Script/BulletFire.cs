//�e�𔭎�

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
    /// �����ݒ�
    /// </summary>
    void StartUp()
    {
        serchsscript = Search.GetComponent<SearchPlayer>();

        //audio���擾
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// �G�̒e�𐶐����鏈��
    /// </summary>
    void BulletGenerate()
    {
        if(serchsscript.search == true && currentTime > span) //span��currentTime���z������
        {
            Instantiate(EnemyBullet, this.transform.position, Quaternion.identity); //�e������
            audioSource.PlayOneShot(shot);
            currentTime = 0; //0�ɖ߂�
        }
    }
}
