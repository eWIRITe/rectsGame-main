using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float min_speed;
    public float max_speed;
    private float speed;
    private Rigidbody2D rb;
    public float neededRotation;

    public float straitForse_minTime;
    public float straitForse_maxTime;

    private float straitForse_timer;

    [Header("audio")]
    private AudioSource _audioSourse;
    public AudioClip _onMouseDie_audioClip;

    [Header("effects")]
    public GameObject dieEffect_prefab;

    //private void OnMouseDown()
    //{
    //    Die();
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MainCamera")
        {
            neededRotation = CalculateAngle(transform.position, Vector2.zero);
        }
    }

    private void Start()
    {
        _audioSourse = GameObject.FindGameObjectWithTag("audio_sourse").GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        update_timer();
        update_speed();

        neededRotation = CalculateAngle(transform.position, Vector2.zero);
    }
    private void FixedUpdate()
    {
        if (!GameController.playing)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        //calculating
        straitForse_timer -= Time.deltaTime;
        if(straitForse_timer <= 0)
        {
            update_rotation();
            update_speed();

            update_timer();
        }

        //Mooving
        Vector2 worldVelocity = transform.TransformDirection(new Vector2(speed, 0));
        rb.velocity = worldVelocity;

        //rotating
        if(rb.rotation != neededRotation)
        {
            if(rb.rotation < neededRotation)
            {
                rb.rotation = rb.rotation + 5 > neededRotation ? neededRotation : rb.rotation + 5;
            }
            else if (rb.rotation > neededRotation)
            {
                rb.rotation = rb.rotation - 5 < neededRotation ? neededRotation : rb.rotation - 5;
            }
        }
    }

    public void Die()
    {
        if (!GameController.playing)
        {
            return;
        }

        _audioSourse.Play();

        GameObject dieAnim = Instantiate(dieEffect_prefab, transform.position, Quaternion.identity);
        Destroy(dieAnim, 2.0f);

        GameController._score += 1;
        Destroy(gameObject);
    }

    #region updaters
    public void update_timer()
    {
        straitForse_timer = Random.Range(straitForse_minTime, straitForse_maxTime);
    }
    public void update_rotation()
    {
        neededRotation += Random.Range(-100, 100);

        if (neededRotation >= 360 || neededRotation < 0) neededRotation = 0;
    }
    public void update_speed()
    {
        speed = Random.Range(min_speed, max_speed);
    }
    #endregion

    #region add math
    private float CalculateAngle(Vector2 pointA, Vector2 pointB)
    {
        // Calculate the direction vector from pointA to pointB
        Vector2 direction = pointB - pointA;

        // Calculate the angle in radians using Atan2
        float angleRadians = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle from radians to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        // Ensure the angle is positive
        if (angleDegrees < 0)
        {
            angleDegrees += 360f;
        }

        return angleDegrees;
    }
    #endregion
}
