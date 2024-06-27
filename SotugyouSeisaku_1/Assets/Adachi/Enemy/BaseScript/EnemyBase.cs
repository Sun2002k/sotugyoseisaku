//�G�̊�{����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyBase : MonoBehaviour
{
    #region �x�N�^�[
    Vector3 v_enemy; //�G�̃x�N�^�[
    Vector3 v_player; //�v���C���[�̃x�N�^�[
    Vector3 v_range; //�G�ƃv���C���[�̋���
    Vector3 v_move;
    #endregion

    #region �Q�[���I�u�W�F�N�g
    GameObject player;

    #endregion

    #region �ϐ�
    [SerializeField] float speed;
    [SerializeField] protected float hp = 2;
    protected float angle = 270; // �G����]����p�x
    #endregion

    #region ���ʉ�
    [SerializeField] protected AudioClip walk; //���f��
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


    #region �֐���`
    /// <summary>
    /// �����ݒ�
    /// </summary>
    void StartUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //audio���擾
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// hp��0�ɂȂ�����j�󂳂��
    /// </summary>
    void Death()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �v���C���[��ǂ�������
    /// </summary>
    public void BaseMove()
    {
        if(hp > 0)
        {
            //�v���C���[�̌��ɍs��
            v_enemy = this.transform.position;   //�G�̍��W
            v_player = player.transform.position; //�v���C���[�̍��W
            v_range = v_player - v_enemy; //�v���C���[�̍��W�ƓG�̍��W���΃x�N�^�[
            v_move = v_range * speed; //���΃x�N�^�[�ɃX�s�[�h���|����
            this.transform.position += v_move;

            //�v���C���[�̕��֌�������
            float theta = Mathf.Atan2(v_player.x - v_enemy.x, v_player.z - v_enemy.z);//�v���C���[�̍��W�ƓG�̍��W��x��z�ɕ����Ă��ꂼ��v�Z
            float deg = -(angle - theta * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Euler(new Vector3(0, deg, 0));//�v���C���[�̕��֌���
        }
        
    }

    /// <summary>
    /// ����SE���Đ�
    /// </summary>
    void SeStart()
    {
        audioSource.PlayOneShot(walk);
    }

    //�v���C���[�̒e�ɓ�����������
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
