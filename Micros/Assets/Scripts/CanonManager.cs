using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonManager : MonoBehaviour {

    public GameObject rayo, death;
    public float rayocd = 8, rayodur = 1.75f;
    float rayocdcounter, rayodurcounter;

    void Start ()
    {
		
	}

    void Update()
    {
        if (rayocdcounter > 0 && GetComponent<AudioSource>().isPlaying == true && death.activeInHierarchy == false)
        {
            rayocdcounter -= Time.deltaTime;
        }
        else if (rayocdcounter <= 0 && GetComponent<AudioSource>().isPlaying == true && death.activeInHierarchy == false)
        {
            GetComponent<AudioSource>().Stop();
            rayo.gameObject.SetActive(true);
            rayodurcounter = rayodur;
        }
        if (rayodurcounter > 0 && rayo.activeInHierarchy == true)
        {
            rayodurcounter -= Time.deltaTime;
        }
        else if (rayodurcounter <= 0 && rayo.activeInHierarchy == true)
        {
            rayo.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && rayocdcounter <= 0 && death.activeInHierarchy == false)
        {
            rayocdcounter = rayocd;
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && death.activeInHierarchy == true)
        {
            GetComponent<AudioSource>().Stop();
        }
        else if (other.gameObject.tag == "Player" && rayocdcounter <= 0 && death.activeInHierarchy == false && GetComponent<AudioSource>().isPlaying == false)
        {
            rayocdcounter = rayocd;
            GetComponent<AudioSource>().Play();
        }
    }

    //void Update ()
    //   {
    //	if(rayodurcounter > 0 && rayo.activeInHierarchy == true)
    //       {
    //           rayodurcounter -= Time.deltaTime;
    //       }
    //       else if(rayodurcounter <= 0 && rayo.activeInHierarchy == true)
    //       {
    //           rayo.gameObject.SetActive(false);
    //       }
    //}

    //   private void OnTriggerEnter(Collider other)
    //   {
    //       if(other.gameObject.tag == "Player" && rayo.activeInHierarchy == false && death.activeInHierarchy == false)
    //       {
    //           rayocdcounter = rayocd;
    //       }  
    //   }

    //   private void OnTriggerStay(Collider other)
    //   {
    //       if (other.gameObject.tag == "Player" && rayocdcounter > 0 && rayo.activeInHierarchy == false && death.activeInHierarchy == false)
    //       {
    //           if(rayocdcounter == rayocd)
    //           {
    //               GetComponent<AudioSource>().Play();
    //           }
    //           rayocdcounter -= Time.deltaTime;
    //       }
    //       else if (other.gameObject.tag == "Player" && rayocdcounter <= 0 && rayo.activeInHierarchy == false && death.activeInHierarchy == false)
    //       {
    //           GetComponent<AudioSource>().Stop();
    //           rayo.gameObject.SetActive(true);
    //           rayocdcounter = rayocd;
    //           rayodurcounter = rayodur;
    //       }
    //       else if(death.activeInHierarchy == true && GetComponent<AudioSource>().isPlaying)
    //       {
    //           GetComponent<AudioSource>().Stop();
    //       }
    //   }

    //   private void OnTriggerExit(Collider other)
    //   {
    //       if (other.gameObject.tag == "Player")
    //       {
    //           GetComponent<AudioSource>().Stop();
    //       }
    //   }
}
