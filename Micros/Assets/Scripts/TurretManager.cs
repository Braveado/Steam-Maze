using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour {

    public GameObject player, hit;
    public float torretacd = 5, torretadur = 1;
    float torretacdcounter, torretadurcounter;
    Vector3 campos, lastcampos;
    public Image death, victory, turretalarm;
	
	void Update ()
    {
        if (death.gameObject.activeInHierarchy == false && victory.gameObject.activeInHierarchy == false)
        {
            campos = player.transform.position;
            if (campos != lastcampos)
            {
                torretacdcounter = torretacd;
                if (turretalarm.gameObject.activeInHierarchy == true)
                {
                    turretalarm.gameObject.SetActive(false);
                }
            }
            else if (campos == lastcampos && torretadurcounter <= 0)
            {
                if (torretacdcounter > 0 && GetComponent<LineRenderer>().enabled == false)
                {
                    torretacdcounter -= Time.deltaTime;
                    if (torretacdcounter <= 2.5 && turretalarm.gameObject.activeInHierarchy == false)
                    {
                        turretalarm.gameObject.SetActive(true);
                    }
                }
                else if (torretacdcounter <= 0 && GetComponent<LineRenderer>().enabled == false)
                {
                    GetComponent<LineRenderer>().enabled = true;
                    AudioSource[] huh = GetComponents<AudioSource>();
                    huh[1].Play();
                    torretacdcounter = torretacd;
                    torretadurcounter = torretadur;
                    turretalarm.gameObject.SetActive(false);
                }
            }
            if (torretadurcounter > 0 && GetComponent<LineRenderer>().enabled == true)
            {
                torretadurcounter -= Time.deltaTime;
            }
            else if (torretadurcounter <= 0 && GetComponent<LineRenderer>().enabled == true)
            {
                GetComponent<LineRenderer>().enabled = false;
                hit.SetActive(false);
            }
            if (GetComponent<LineRenderer>().enabled == true)
            {
                hit.gameObject.SetActive(true);
                Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 15, player.transform.position.z);
                GetComponent<LineRenderer>().SetPosition(1, pos);
            }
            lastcampos = campos;
        }
        else if (death.gameObject.activeInHierarchy == true)
        {
            turretalarm.gameObject.SetActive(false);
            GetComponent<LineRenderer>().enabled = false;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            torretacdcounter = torretacd;
        }
    }

}
