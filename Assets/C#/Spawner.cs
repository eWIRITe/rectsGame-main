using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("recty prefab")]
    public GameObject[] recty_prefab;

    [Space, Header("Spawner variables")]
    public Transform spawning_line_fl;
    public Transform spawning_line_fr;
    public Transform spawning_line_dl;
    public Transform spawning_line_dr;
    [Space]
    public float Timer_min;
    public float Timer_max;
    [Space]
    public int maxRectsOnScene;

    private float _timer;
    void FixedUpdate()
    {
        if (!GameController.playing)
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            spawnRecty();
            updateTimer();
        }
    }

    public void spawnRecty()
    {
        Vector2 spawningPose = new Vector2();

        int rand_vector = Random.Range(0, 5);
        switch (rand_vector)
        {
            case 1:
                spawningPose = new Vector2(spawning_line_fl.position.x, Random.Range(spawning_line_fl.position.y, spawning_line_dl.position.y));
                break;
            case 2:
                spawningPose = new Vector2(Random.Range(spawning_line_fl.position.x, spawning_line_fr.position.x), spawning_line_fl.position.y);
                break;
            case 3:
                spawningPose = new Vector2(spawning_line_fr.position.x, Random.Range(spawning_line_fr.position.y, spawning_line_dr.position.y));
                break;
            case 4:
                spawningPose = new Vector2(Random.Range(spawning_line_dl.position.x, spawning_line_dr.position.x), spawning_line_dl.position.y);
                break;
            default: 
                spawningPose = new Vector2(spawning_line_fl.position.x, Random.Range(spawning_line_fl.position.y, spawning_line_dl.position.y));
                break;
        }

        

        if (gameObject.transform.childCount <= maxRectsOnScene)
        {
            int v = Random.RandomRange(0, 3);
            Instantiate(recty_prefab[v], spawningPose, Quaternion.identity, gameObject.transform);
        }
    }

    public void updateTimer()
    {
        _timer = Random.Range(Timer_min, Timer_max);
    }
}
