using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, rotation);

        Vector3 movement_vector = dir * speed * Time.deltaTime;

        gameObject.transform.position += movement_vector;
    }

    public void SetShooter(PlayerStats player)
    {
        shooter = player;
    }

    public void SetMovementData(float rotation, Vector2 dir, float speed)
    {
        this.rotation = rotation;
        this.dir = dir.normalized;
        this.speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.GetComponent<PlayerStats>();

        if(stats != null)
        {
            if(shooter != null)
            {
                if(stats != shooter)
                {
                    EventBulletHitsPlayer ev = new EventBulletHitsPlayer(shooter, stats);
                    EventManager.Instance.SendEvent(ev);

                    Destroy(gameObject);
                }
            }

            return;
        }

        BulletWall wall = collision.GetComponent<BulletWall>();

        if(wall != null)
        {
            Destroy(gameObject);

            return;
        }
    }

    private PlayerStats shooter = null;

    private float rotation = 0;
    private Vector2 dir = Vector2.zero;
    private float speed = 0.0f;
}
