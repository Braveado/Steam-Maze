using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslasManager : MonoBehaviour {

    public GameObject rayos;
    float min, max, mincd, maxcd;

	void Start ()
    {
        min = Random.Range(1.5f, 3.0f);
        max = Random.Range(3.0f, 6.0f);
        mincd = min;
        maxcd = max;
	}
	
	void Update ()
    {
		if(mincd > 0 && rayos.activeInHierarchy == false)
        {
            mincd -= Time.deltaTime;
        }
        else if(mincd <= 0 && rayos.activeInHierarchy == false)
        {
            rayos.SetActive(true);
            mincd = min;
        }
        else if(maxcd > 0 && rayos.activeInHierarchy == true)
        {
            maxcd -= Time.deltaTime;
        }
        else if(maxcd <= 0 && rayos.activeInHierarchy == true)
        {
            rayos.SetActive(false);
            maxcd = max;
        }


	}
}
