using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pony : MonoBehaviour
{
    public EDir directory;

    public Rigidbody rb;

    public float min_speed;
    public float max_speed;
    public float speed;

    public float min_hight_change;
    public float max_hight_change;
    public float hight_change;

    [Header("audio")]
    public AudioSource _audioSourse;
    public AudioClip _onMouseDie_audioClip;

    [Header("effects")]
    public GameObject dieEffect_prefab;

    //private void OnMouseDown()
    //{
    //    if(transform.tag == "angry_pony")
    //    {
    //        Die(true);
    //    }
    //    else if (transform.tag == "kind_pony")
    //    {
    //        Die(false);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "pony_mooving_sourse")
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _audioSourse = GameObject.FindGameObjectWithTag("audio_sourse").GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        update_speed();

        hight_change = Random.Range(min_hight_change, max_hight_change);
    }

    private void FixedUpdate()
    {
        if (!GameController.playing)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector3(speed, hight_change);
    }

    public void Die(bool incrieseScore)
    {
        if (!GameController.playing)
        {
            return;
        }

        _audioSourse.Play();

        if (dieEffect_prefab != null)
        {
            GameObject dieAnim = Instantiate(dieEffect_prefab, transform.position, Quaternion.identity);
            Destroy(dieAnim, 2.0f);
        }

        if(incrieseScore) GameController._score += 1;
        else GameController._score -= 1;
        Destroy(gameObject);
    }

    public void update_speed()
    {
        if(directory == EDir.right) speed = Random.Range(min_speed, max_speed);

        else speed = Random.Range(min_speed, max_speed) * -1;
    }

    public enum EDir
    {
       right, left
    }
}
