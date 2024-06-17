//EnemyBase���p�������O�F�G�̃X�N���v�g

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEnemy : EnemyBase
{
    #region �ϐ���`
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


    #region �֐���`
    /// <summary>
    /// �����ݒ�
    /// </summary>
    void StartUp()
    {
        //�p�����̕ϐ����㏑��
        hp = 4;
        angle = 0; 

        Search = transform.GetChild(0).gameObject; //0�ň�Ԗڂ̎q�I�u�W�F�N�g�ɂȂ�
        serchsscript = Search.GetComponent<SearchPlayer>();
        anim = GetComponent<Animator>();
    }

    //�G���|����鏈��
    void DeathAnim()
    {
        //�A�j���[�V����
        if (hp <= 0)
        {
            anim.SetTrigger("Death");
        }
    }


    //Walk�A�j���[�V�����̑����n�ʂɕt�����^�C�~���O��event�Ƃ��ČĂяo�����
    public void AnimWalk()
    {
        audioSource.PlayOneShot(walk);
    }
    //Death�A�j���[�V�������I��������event�Ƃ��ČĂяo�����
    public void AnimEnd()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// �G�̃A�j���[�V����
    /// </summary>
    void EnemyAnimation()
    {
        //�����A�j���[�V����
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
