//�e�𔭎�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    #region �ϐ���`
    [SerializeField] float span;
    float currentTime = 0;
    #endregion

    #region �Q�[���I�u�W�F�N�g
    [SerializeField] GameObject EnemyBullet;
    [SerializeField] GameObject Search;
    SearchPlayer serchsscript;
    #endregion

    #region ���ʉ�
    [SerializeField] AudioClip shot;
    AudioSource audioSource;
    #endregion
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

    #region �֐���`
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
    #endregion
}
