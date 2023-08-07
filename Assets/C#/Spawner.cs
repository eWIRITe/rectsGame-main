using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("recty prefab")]
    public GameObject[] recty_prefab;
    public int[] spawn_rates;

    [Space, Header("Spawner variables")]
    public Transform spawning_line_fl;
    public Transform spawning_line_dl; 
    public Transform spawning_line_fr;
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
        Vector3 spawningPose = new Vector3();
        Pony.EDir dir = Pony.EDir.right;

        if (Random.Range(0, 2) == 1)
        {
            spawningPose = new Vector2(spawning_line_fl.position.x, Random.Range(spawning_line_fl.position.y, spawning_line_dl.position.y));
        }
        else
        {
            spawningPose = new Vector2(spawning_line_dr.position.x, Random.Range(spawning_line_fr.position.y, spawning_line_dr.position.y)); 

            dir = Pony.EDir.left;
        }

        if (gameObject.transform.childCount <= maxRectsOnScene)
        {
            int _max = 0;
            for (int i = 0; i < spawn_rates.Length; i++)
            {
                _max += spawn_rates[i];
            }
            int v = Random.Range(0, _max);

            for(int i = 0; i < spawn_rates.Length; i++)
            {
                if(spawn_rates[i] > v)
                {
                    GameObject Pony_pref = Instantiate(recty_prefab[i], spawningPose, recty_prefab[i].transform.rotation, gameObject.transform);
                    Pony_pref.GetComponent<Pony>().directory = dir;
                    if (dir == Pony.EDir.left) Pony_pref.transform.Rotate(0, 180, 0);
                    return;
                }
                else
                {
                    v -= spawn_rates[i];
                }
            }
        }
    }

    public void updateTimer()
    {
        _timer = Random.Range(Timer_min, Timer_max);
    }
}
