using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;

public class kariplayer : MonoBehaviour
{
    //�W�����v�̑��x��ݒ�
    private const float _velocity = 5.0f;
    public Rigidbody rb; // �v���C���[�̃��W�b�g�{�f�B
    //���n��Ԃ��Ǘ�
    private bool _isGrounded;
    public Vector3 pos;
    public float my; // ���݂�y���W
    [SerializeField] MeshRenderer mr;
    [SerializeField] Camera cam; // �J�������擾
    static public float HP;  // �v���C���[�̗̑�
    static public float guncolor; // �e�̐F�@1.�� 2.�� 3.��
    static public float gunbulletR, gunbulletB, gunbulletY; // ���ꂼ��̎c�e��
    static public float gunstockR, gunstockB, gunstockY; // ���ꂼ��̏e�e�̃X�g�b�N
    static public float guncoolR, guncoolB, guncoolY; // ���ꂼ��̔��ˊԊu
    static public bool cooltimeR, cooltimeB, cooltimeY; // ���ꂼ�ꌂ�Ă邩�ǂ���
    [SerializeField] GameObject BulletR, BulletB, BulletY; // ���ꂼ��̏e�e
    static public float bulletspeed; // �e��
    [SerializeField] GameObject bulletpos; // �e�ۂ����˂����|�C���g
    //[SerializeField] float speed; // �v���C���[�̈ړ����x
    [SerializeField] Image StockIcon1, StockIcon2, StockIcon3; // HP�̃X�g�b�N��\���A�C�R��
    public float Life; // �X�g�b�N�������邩
    [SerializeField] Animator animator;
    public Vector2 debug; // �f�o�b�O�p�̕ϐ�
    [SerializeField] InputAction inputMover; // �ړ��̃C���v�b�g�V�X�e��
    [SerializeField] InputAction inputCamera; // �J�����ړ��̃C���v�b�g�V�X�e��
    [SerializeField] float speed; // �v���C���[�̈ړ����x
    [SerializeField] GameObject centerObj; // �J������]�̒��S
    [SerializeField] float angle; // ��]�̊p�x
    [SerializeField] float camkakudox; // �J�����̊p�x���擾����
    [SerializeField] float camkakudoy; // �J�����̊p�x���擾����
    [SerializeField] float camkakudoz; // �J�����̊p�x���擾����
    [SerializeField] float rotateSpeed;
    // �I�u�W�F�N�g���A�N�e�B�u�ɂȂ����Ƃ��ɌĂ΂��C�x���g
    private void OnDisable()
    {
        inputMover.Disable();
        inputCamera.Disable();
    }

    // �I�u�W�F�N�g����\���ɂȂ����Ƃ��ɌĂ΂��C�x���g
    private void OnEnable()
    {
        inputMover.Enable();
        inputCamera.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();
        //�ŏ��͒��n���Ă��Ȃ����
        _isGrounded = false;
        mr = GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>(); // �A�j���[�V���������̂��߂Ɏ擾
        HP = 100;
        guncolor = 1; // �ԐF�X�^�[�g
        gunbulletR = 20; // �ԏe�̒e��
        gunbulletB = 10; // �e�̒e��
        gunbulletY = 40; // ���e�̒e��
        gunstockR = 40; // �ԏe�̒e��̃X�g�b�N
        gunstockB = 20; // �e�̒e��̃X�g�b�N
        gunstockY = 80; // ���e�̒e��̃X�g�b�N
        guncoolR = 0.5f; // �ԏe�̃N�[���^�C��
        guncoolB = 1.5f; // �e�̃N�[���^�C��
        guncoolY = 0.3f; // ���e�̃N�[���^�C��
        cooltimeR = true; // �ԏe�����Ă邩�ǂ���
        cooltimeB = true; // �e�����Ă邩�ǂ���
        cooltimeY = true; // ���e�����Ă邩�ǂ���
        bulletspeed = 400; // �e�Ɋ|����p���[
        // 1�A�C�R���ɂ�HP��100���邱�Ƃ�����(��HP��300)
        StockIcon1.enabled = true; // HP��0�ɂȂ������ɏ�����(�\�L��0�̂܂�)
        StockIcon2.enabled = true; // HP��100������������ɏ�����(�\�L��100�ɖ߂�)
        StockIcon3.enabled = true; // HP��200������������ɏ�����(�\�L��100�ɖ߂�)
        StockIcon1.color = new Color32(0, 255, 255, 255); // �A�C�R���̐F�͍ŏ��͐��F
        StockIcon2.color = new Color32(0, 255, 255, 255);
        StockIcon3.color = new Color32(0, 255, 255, 255);
        Life = 3; // ���C�t(�A�C�R��)�̐��������ϐ�
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        my = transform.position.y;
        camkakudox = cam.transform.localEulerAngles.x;
        camkakudoy = cam.transform.localEulerAngles.y;
        camkakudoz = cam.transform.localEulerAngles.z;
        // �e�e����0�ȉ��ɂȂ�����ȂǍő�l��Œ�l���z���Ȃ��悤�ɂ��鏈��
        #region �ő�l�Œ�l�Ǘ�
        if (camkakudox >= 100)
        {
            cam.transform.rotation = Quaternion.Euler(99f, camkakudoy, camkakudoz);
        }
        if (camkakudox <= -100)
        {
            cam.transform.rotation = Quaternion.Euler(-99f, camkakudoy, camkakudoz);
        }
        if (guncolor <= 0)
        {
            guncolor = 3;
        }
        if (guncolor >= 4)
        {
            guncolor = 1;
        }
        if (gunbulletR <= 0)
        {
            gunbulletR = 0;
        }
        if (gunbulletB <= 0)
        {
            gunbulletB = 0;
        }
        if (gunbulletY <= 0)
        {
            gunbulletY = 0;
        }
        if (gunbulletR >= 20)
        {
            gunbulletR = 20;
        }
        if (gunbulletB >= 10)
        {
            gunbulletB = 10;
        }
        if (gunbulletY >= 40)
        {
            gunbulletY = 40;
        }
        if (HP <= 0)
        {
            HP = 0;
        }
        if (HP >= 100)
        {
            HP = 100;
        }
        if (Life <= 0)
        {
            Life = 0;
        }
        if (Life >= 3)
        {
            Life = 3;
        }
        #endregion
        // �F���Ƃ̒e��
        #region �e���ύX
        if (guncolor == 1)
        {
            bulletspeed = 400;
        }
        if (guncolor == 2)
        {
            bulletspeed = 300;
        }
        if (guncolor == 3){
            bulletspeed = 600;
        }
        #endregion
        // ���C�t����
        #region ���C�t����
        if (HP <= 0)
        {
            if (Life == 3)
            {
                Life = 2;
                StockIcon3.enabled = false;
                HP = 100;
            }
            else if(Life == 2)
            {
                Life = 1;
                StockIcon2.enabled = false;
                HP = 100;
            }
            else if (Life == 1)
            {
                Life = 0;
                StockIcon1.enabled = false;
                // StartCoroutine(Death());
                // �X�g�b�N���s��������ʉ���炷�ȂǂŃQ�[���I�[�o�[�V�[���Ɉڍs����
            }
        }
        if (Life == 2)
        {
            StockIcon2.color = new Color32(255, 120, 0, 255);
            StockIcon1.color = new Color32(255, 120, 0, 255);
        }
        if (Life == 1)
        {
            StockIcon1.color = new Color32(255, 0, 0, 255);
        }
        #endregion
        // �X�e�B�b�N�ړ�
        #region �X�e�B�b�N�ړ�
        var moveVec = inputMover.ReadValue<Vector2>();
        float Xvalue=moveVec.x*speed*Time.deltaTime;
        float Yvalue=moveVec.y*speed*Time.deltaTime;
        // �v���C���[���ړ�
        if (moveVec != new Vector2(0, 0))
        {
            animator.SetBool("move", true);
        }
        else if (moveVec == new Vector2(0, 0))
        {
            animator.SetBool("move", false);
        }
        /*
        transform.position += new Vector3(
            moveVec.x * speed,
            0,
            moveVec.y * speed);
        */
        /*
        if(moveVec!=new Vector2(0, 0))
        {
            if (moveVec.y >= 0)
            {
                transform.Rotate(0, 0, 0);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else if (moveVec.y <= 0)
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if (moveVec.x >= 0)
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            else if (moveVec.x <= 0)
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
        }
        */
        //���݂�X,Z�x�N�g���ɏ�̏����Ŏ擾�����l��n���B
        Vector3 movedir = new Vector3(Xvalue, 0, Yvalue);

        //���ݒn�ɏ�Ŏ擾�������l�𑫂��Ĉړ�����B
        transform.position += movedir;

        //�i�ޕ����Ɋ��炩�Ɍ����B
        transform.forward = Vector3.Slerp(transform.forward, movedir, Time.deltaTime * rotateSpeed);

        #endregion
        #region �J�����ړ�
        var cameravec = inputCamera.ReadValue<Vector2>();
        debug = cameravec;
        if (cameravec != new Vector2 (0, 0))
        {
            if (cameravec.x >= 0)
            {
                cam.transform.RotateAround(centerObj.transform.position, Vector3.up, angle * Time.deltaTime);
            }
            else if (cameravec.x <= 0)
            {
                cam.transform.RotateAround(centerObj.transform.position, Vector3.down, angle * Time.deltaTime);
            }
            
            if (cameravec.y >= 0)
            {
                cam.transform.Rotate(-cameravec.y, 0, 0);
            }
            else if (cameravec.y <= 0)
            {
                cam.transform.Rotate(cameravec.y, 0, 0);
            }
            
            //this.transform.Rotate(0, cameravec.x, 0);
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.H))
        {
            HP -= 10;
        }

        //���n���Ă��邩�𔻒�
        if (_isGrounded == true)
        {
            //�X�y�[�X�L�[��������Ă��邩�𔻒�
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                //�W�����v�̕�����������̃x�N�g���ɐݒ�
                Vector3 jump_vector = Vector3.up;
                //�W�����v�̑��x���v�Z
                Vector3 jump_velocity = jump_vector * _velocity;

                //������̑��x��ݒ�
                rb.velocity = jump_velocity;
                //�n�ʂ��痣���̂Œ��n��Ԃ���������
                _isGrounded = false;
            }
        }
    }

    #region �C���v�b�g�V�X�e���̃C�x���g
    public void OnJump(InputAction.CallbackContext context) // �n�ʂɐڐG���Ă��鎖�������ɃW�����v���鏈��
    {

        //���n���Ă��邩�𔻒�
        if (_isGrounded == true)
        {
            //�W�����v�̕�����������̃x�N�g���ɐݒ�
            Vector3 jump_vector = Vector3.up;
            //�W�����v�̑��x���v�Z
            Vector3 jump_velocity = jump_vector * _velocity;
            //������̑��x��ݒ�
            rb.velocity = jump_velocity;
            //�n�ʂ��痣���̂Œ��n��Ԃ���������
            _isGrounded = false;
        }
        Debug.Log("�W�����v");
    }

    public void Onspin(InputAction.CallbackContext context) // �E�X�e�B�b�N�����E�ǂ��炩���擾���v���C���[����]�����鏈��
    {
        float hori2 = Input.GetAxis("Horizontal");
        if (hori2 > 0)
        {
            transform.Rotate(0, 0.5f, 0);
        }
        else if (hori2 < 0)
        {
            transform.Rotate(0, -0.5f, 0);
        }
    }

    public void OnReroad(InputAction.CallbackContext context) // X�{�^�����擾���e���[����
    {
        if (context.performed){
            // �����[�h�@�\
            #region �����[�h�@�\
            if (guncolor == 1) // �Ԃ̏e�Ȃ�
            {
                if (gunbulletR == 0) // �c�e��0���Ȃ�
                {
                    if (gunstockR >= 20) // �X�g�b�N��20���ȏ゠��Ȃ炻�̂܂܈ڂ�
                    {
                        gunbulletR = 20;
                        gunstockR = gunstockR - 20;
                    }
                    else // ��������Ȃ��Ȃ獡���镪���ڂ�
                    {
                        gunbulletR = gunstockR;
                        gunstockR = 0;
                    }
                }
                else // �܂��e���]���Ă�Ȃ�
                {
                    float sabun = 0;
                    sabun = 20 - gunbulletR; // �����������̂� ��:10���������Ȃ�sabun��10
                    if (gunstockR >= 20)
                    {
                        gunbulletR = 20;
                        gunstockR = gunstockR - sabun; // �g�p�������炷
                    }
                    else
                    {
                        if (sabun > gunstockR) // �S�ē����ɂ͑���Ȃ��ꍇ
                        {
                            gunbulletR = gunbulletR + gunstockR;
                            gunstockR = 0;
                        }
                        else // sabun�ɑ΂��X�g�b�N�������ꍇ
                        {
                            gunbulletR = 40; // �e���[
                            gunstockR = gunstockR - sabun; // �g�p�������炷
                        }

                    }
                }
            }
            else if (guncolor == 2) // �̏e�Ȃ�
            {
                if (gunbulletB == 0) // �c�e��0���Ȃ�
                {
                    if (gunstockB >= 10) // �X�g�b�N��10���ȏ゠��Ȃ炻�̂܂܈ڂ�
                    {
                        gunbulletB = 10;
                        gunstockB = gunstockB - 10;
                    }
                    else // ��������Ȃ��Ȃ獡���镪���ڂ�
                    {
                        gunbulletB = gunstockB;
                        gunstockB = 0;
                    }
                }
                else // �܂��e���]���Ă�Ȃ�
                {
                    float sabun = 0;
                    sabun = 10 - gunbulletB; // �����������̂� ��:5���������Ȃ�sabun��5
                    if (gunstockB >= 10)
                    {
                        gunbulletB = 10;
                        gunstockB = gunstockB - sabun; // �g�p�������炷
                    }
                    else
                    {
                        if (sabun > gunstockB) // �S�ē����ɂ͑���Ȃ��ꍇ
                        {
                            gunbulletB = gunbulletB + gunstockB;
                            gunstockB = 0;
                        }
                        else // sabun�ɑ΂��X�g�b�N�������ꍇ
                        {
                            gunbulletB = 10; // �e���[
                            gunstockB = gunstockB - sabun; // �g�p�������炷
                        }
                    }
                }
            }
            else if (guncolor == 3) // ���F�̏e�Ȃ�
            {
                if (gunbulletY == 0) // �c�e��0���Ȃ�
                {
                    if (gunstockY >= 40) // �X�g�b�N��40���ȏ゠��Ȃ炻�̂܂܈ڂ�
                    {
                        gunbulletY = 40;
                        gunstockY = gunstockY - 40;
                    }
                    else // ��������Ȃ��Ȃ獡���镪���ڂ�
                    {
                        gunbulletY = gunstockY;
                        gunstockY = 0;
                    }
                }
                else // �܂��e���]���Ă�Ȃ�
                {
                    float sabun = 0;
                    sabun = 40 - gunbulletY; // �����������̂� ��:10���������Ȃ�sabun��30
                    if (gunstockY >= 40)
                    {
                        gunbulletY = 40;
                        gunstockY = gunstockY - sabun; // �g�p�������炷
                    }
                    else
                    {
                        if (sabun > gunstockY) // �S�ē����ɂ͑���Ȃ��ꍇ
                        {
                            gunbulletY = gunbulletY + gunstockY;
                            gunstockY = 0;
                        }
                        else // sabun�ɑ΂��X�g�b�N�������ꍇ
                        {
                            gunbulletY = 40; // �e���[
                            gunstockY = gunstockY - sabun; // �g�p�������炷
                        }

                    }
                }
            }
            #endregion
        }
    }

    public void OnChangeL(InputAction.CallbackContext context) // ���g���K�[���擾���e��I�������ɕς��鏈��
    {
        if (context.performed)
        {
            guncolor--;
        }
    }

    public void OnChangeR(InputAction.CallbackContext context) // ��Ɠ������E�g���K�[���擾���e����E�ɕς��鏈��
    {
        if (context.performed)
        {
            guncolor++;
        }
    }

    public void OnFire(InputAction.CallbackContext context) // R2�{�^��(PS�ł���)���擾���e��������
    {
        if (context.performed)
        {
            if (guncolor == 1 && cooltimeR == true && gunbulletR >= 1)
            {
                GameObject ball = Instantiate(BulletR, bulletpos.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.AddForce(transform.forward * bulletspeed);
                gunbulletR--;
                StartCoroutine(tamakankakuR());
            }
            else if (guncolor == 2 && cooltimeB == true && gunbulletB >= 1)
            {
                GameObject ball = Instantiate(BulletB, bulletpos.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.AddForce(transform.forward * bulletspeed);
                gunbulletB--;
                StartCoroutine(tamakankakuB());
            }
            else if (guncolor == 3 && cooltimeY == true && gunbulletY >= 1)
            {
                GameObject ball = Instantiate(BulletY, bulletpos.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.AddForce(transform.forward * bulletspeed);
                gunbulletY--;
                StartCoroutine(tamakankakuY());
            }
        }
        
    }
    #endregion

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //���n�����o�����̂Œ��n��Ԃ���������
            _isGrounded = true;
        }
    }

    IEnumerator tamakankakuR()
    {
        cooltimeR = false;
        yield return new WaitForSeconds(guncoolR);
        cooltimeR = true;
    }

    IEnumerator tamakankakuB()
    {
        cooltimeB = false;
        yield return new WaitForSeconds(guncoolB);
        cooltimeB = true;
    }

    IEnumerator tamakankakuY()
    {
        cooltimeY = false;
        yield return new WaitForSeconds(guncoolY);
        cooltimeY = true;
    }
}
