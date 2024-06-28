using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public float moveSpeed = 10;
    public GameObject player;
    public float lifeSpan = 3.0f;


    void Start()
    {

    }

    void Update()
    {
        // ���� ��� �̵��ϰ� �ʹ�.
        // ����: ����, �̵��ӷ�: float, public
        // �̵�����: p = p0 + vt, p += vt

        // ���� ���� 
        Vector3 worldDir = Vector3.up;

        // ���� ����
        Vector3 localDir = transform.up;

        transform.position += localDir * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;

        //if(gameObject != null)
        //{
        //    lifeSpan += Time.deltaTime;
        //    if (lifeSpan > 3)
        //    {
        //        Destroy(gameObject);
        //    }
        //}

        lifeSpan -= Time.deltaTime;
        if(lifeSpan < 0)
        {
            Destroy(gameObject);
        }

    }

    // ������ �浹�� �߻����� �� ����Ǵ� �̺�Ʈ �Լ�.
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ���� ������Ʈ�� �����Ѵ�.
        Destroy(collision.gameObject);

        // ��(�Ѿ�)�� �����Ѵ�.
        Destroy(gameObject);
    }

    // ������ �浹 ���� �浹 ������ ���� �� ����Ǵ� �̺�Ʈ �Լ�.
    private void OnTriggerEnter(Collider other)
    {
        EnemyMove enemy = other.gameObject.GetComponent<EnemyMove>();

        // enemy ������ ���� �ִٸ�...
        if (enemy != null)
        {
            // �浹�� ���� ������Ʈ�� �����Ѵ�.
            Destroy(other.gameObject);

            // �÷��̾� ���� ������Ʈ�� �پ��ִ� PlayerFire ������Ʈ�� �����´�.
            PlayerFire playerFire = player.GetComponent<PlayerFire>();

            // PlayerFire ������Ʈ�� �ִ� PlayExplosionSound �Լ��� �����Ѵ�.
            playerFire.PlayExplosionSound();
        }

        // ��(�Ѿ�)�� �����Ѵ�.
        Destroy(gameObject);
    }

    
}
