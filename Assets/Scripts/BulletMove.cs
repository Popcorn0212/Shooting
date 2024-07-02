using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public float moveSpeed = 10;
    public GameObject player;
    public float lifeSpan = 3.0f;
    public GameObject explosionPrefab;

    PlayerFire pFire;

    void Start()
    {
        // Player ������Ʈ�� PlayerFire ������Ʈ�� ������ �����Ѵ�.
        if (player != null)
        {
            pFire = player.GetComponent<PlayerFire>();
        }
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
            if (pFire.useObjectPool || pFire.useArray)
            {
                Reload();
            }
            else
            {
                Destroy(gameObject);
            }
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
            // �浹�� ���ʹ� ������Ʈ�� �����Ѵ�.
            Destroy(other.gameObject);

            // GameManager�� �ִ� currentScore ���� 1 �߰��Ѵ�.
            GameManager.gm.AddScore(1);

            // ���� ����Ʈ �����ո� �ֳʹ̰� �ִ� �ڸ��� �����Ѵ�.
            GameObject fx = Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);

            // ������ ���� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ�� �����ͼ� �÷����Ѵ�.
            ParticleSystem ps = fx.GetComponent<ParticleSystem>();
            ps.Play();

            if (player != null)
            {
                // �÷��̾� ���� ������Ʈ�� �پ��ִ� PlayerFire ������Ʈ�� �����´�.
                PlayerFire playerFire = player.GetComponent<PlayerFire>();

                // PlayerFire ������Ʈ�� �ִ� PlayExplosionSound �Լ��� �����Ѵ�.
                playerFire.PlayExplosionSound();
            }
        }

        // ���� �����Ѵ�.
        if(pFire.useObjectPool || pFire.useArray)
        {
            Reload();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reload()
    {
        if (pFire.useObjectPool)
        {
            // �ڱ� �ڽ��� bullets ����Ʈ�� �߰��ϰ�, ��Ȱ��ȭ�Ѵ�.
            pFire.bullets.Add(gameObject);
            lifeSpan = 3.0f;
            gameObject.SetActive(false);
            if (player != null)
            {
                gameObject.transform.parent = player.transform;
            }
        }
        else if(pFire.useArray)
        {
            // bulletArray �迭�� �� ���� �ִ� ���� ã�´�.
            for(int i = 0; i < pFire.bulletArray.Length; i++)
            {
                // ����, i��° �ε����� ���� null�̶��...
                if (pFire.bulletArray[i] == null)
                {
                    pFire.bulletArray[i] = gameObject;
                    gameObject.SetActive(false);
                    lifeSpan = 3.0f;
                    if (player != null)
                    {
                        gameObject.transform.parent = player.transform;
                    }
                    break;
                }
            }
        }
    }
    
}
