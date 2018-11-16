using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    CharacterController player;
    public Transform camara;
    public float vel = 20f, sens = 2f, jumpF = 30f, grav = 20f, jbcd = 20f, VALimit = 30f, zapcd = 1.5f, shakepwr = 3f, shakedur = 1f, slow = 0.5f, camyrot = 4f;
    float movFB, movLR, rotX, rotY, vertV, zapcdcounter, shakecounter, minutos, segundos, milsecs, camyrotcounter, endcounter;
    int lifes = 5;
    bool canjump, inverso, toques;
    public AudioSource footstep, jump, zap;
    public Image lifesicon, jetbootsicon, death, deathindicator, victory, victoryindicator, canonalarm, timertext, restartbtns;
    public GameObject damajo, hit, curar;
    //SerialPort sp = new SerialPort("COM3", 9600);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponent<CharacterController>();
        canjump = true;
        camyrotcounter = camyrot;
        inverso = false;
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            CameraRotation();
            PlayerMovement();
            CameraShake();
            DeathScreen();
            EndOptions();
        }
    }

    private void FixedUpdate()
    {
        if (death.gameObject.activeInHierarchy == false && victory.gameObject.activeInHierarchy == false)
        {
            if (minutos < 10)
            {
                minutos = (int)(Time.timeSinceLevelLoad / 60f);
                segundos = (int)(Time.timeSinceLevelLoad % 60f);
                milsecs = (int)((Time.timeSinceLevelLoad * 1000) % 1000f);
                timertext.gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = minutos.ToString("0") + ":" + segundos.ToString("00") + ":" + milsecs.ToString("000");
            }
            else if (minutos >= 10)
            {
                timertext.gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = "9:59:999";
            }
        }

        if (movFB != 0 || movLR != 0)
        {
            if (camyrotcounter > 0 && inverso == false)
            {
                camara.Rotate(0.1f, 0, 0);
                camyrotcounter -= 0.1f;
            }
            else if (camyrotcounter <= 0 && inverso == false)
            {
                inverso = true;
                camyrotcounter = camyrot;
            }
            else if (camyrotcounter > 0 && inverso == true)
            {
                camara.Rotate(-0.1f, 0, 0);
                camyrotcounter -= 0.1f;
            }
            else if (camyrotcounter <= 0 && inverso == true)
            {
                inverso = false;
                camyrotcounter = camyrot;
            }
        }
    }

    void PlayerMovement()
    {
        if (player.isGrounded)
        {
            if(lifes > 0 && death.gameObject.activeInHierarchy == false && victory.gameObject.activeInHierarchy == false)
            { 
                if (shakecounter > 0)
                {
                    movFB = Input.GetAxis("Vertical") * (vel * slow);
                }
                else
                {
                    movFB = Input.GetAxis("Vertical") * vel;
                }
                if (shakecounter > 0)
                {
                    movLR = Input.GetAxis("Horizontal") * (vel * slow);
                }
                else
                {
                    movLR = Input.GetAxis("Horizontal") * vel;
                }
            }
            else if(lifes == 0 || death.gameObject.activeInHierarchy == true || victory.gameObject.activeInHierarchy == true)
            {
                movFB = 0;
                movLR = 0;
            }
            jump.volume = 0.75f;
            vertV = -grav * Time.deltaTime;
            jump.enabled = false;
            jump.loop = false;
            if (Input.GetButtonDown("Jump") && jetbootsicon.fillAmount == 1 && shakecounter <= 0 && canjump == true && lifes > 0)
            {
                vertV = jumpF;
                jump.enabled = true;
                jump.loop = true;
            }
        }
        else
        {
            movFB = 0;
            movLR = 0;
            if (victory.gameObject.activeInHierarchy == false)
            {
                vertV -= grav * Time.deltaTime;
                if (jetbootsicon.fillAmount > 0 && jump.enabled == true)
                {
                    jetbootsicon.fillAmount -= Time.deltaTime;
                }
                else if (jump.volume > 0.35 && jump.enabled == true)
                {
                    jump.volume -= (Time.deltaTime / 4.5f);
                }
            }
        }
        Vector3 mov = new Vector3(movLR, vertV, movFB);
        mov = transform.rotation * mov;
        player.Move(mov * Time.deltaTime);
        if (jetbootsicon.fillAmount < 1 && jump.enabled == false)
        {
            jetbootsicon.fillAmount += Time.deltaTime / jbcd;
        }
        if (jump.volume >= 0.75)
        {
            if ((movFB != 0 || movLR != 0) && lifes > 0)
            {
                footstep.enabled = true;
                footstep.loop = true;
            }
            else if (movFB == 0 && movLR == 0)
            {
                footstep.enabled = false;
                footstep.loop = false;
            }
        }
        else if (jump.volume <= 0.35)
        {
            footstep.enabled = true;
        }
    }

    void CameraRotation()
    {
        if (lifes > 0 && victory.gameObject.activeInHierarchy == false)
        {
            if (shakecounter > 0)
            {
                rotX = Input.GetAxis("Mouse X") * (sens * slow);
            }
            else
            {
                rotX = Input.GetAxis("Mouse X") * sens;
            }
            transform.Rotate(0, rotX, 0);
            if (shakecounter > 0)
            {
                rotY = (Input.GetAxis("Mouse Y") * (sens * slow)) * -1;
            }
            else
            {
                rotY = (Input.GetAxis("Mouse Y") * sens) * -1;
            }
            if (camara.eulerAngles.x + rotY <= (VALimit * 1.5)|| camara.eulerAngles.x + rotY >= 360 - VALimit)
            {
                camara.Rotate(rotY, 0, 0);
            }
        }

    }

    void LifeUpdate()
    {
        if (lifes > 0)
        {
            float i = 1;
            while (i < lifes)
            {
                i++;
            }
            i /= 5;
            lifesicon.fillAmount = i;
        }
        else if (lifes == 0)
        {
            lifesicon.fillAmount = 0;
            endcounter = 1;
        }
    }

    void CameraShake()
    {
        if (toques == true)
        {
            //sp.Open();
            //sp.Write("2");
            //sp.Close();
           // print("cuando");
            toques = false;
        }
        if (shakecounter > 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakepwr;
            transform.Rotate(0, shakePos.x, 0);
            if (camara.eulerAngles.x + shakePos.y <= (VALimit * 1.5) || camara.eulerAngles.x + shakePos.y >= 360 - VALimit)
            {
                camara.Rotate(shakePos.y, 0, 0);
            }
            shakecounter -= Time.deltaTime;
        }
        else if (shakecounter <= 0 && damajo.gameObject.activeInHierarchy == true)
        {
            damajo.gameObject.SetActive(false);
            zap.enabled = false;
        }

    }

    void DeathScreen()
    {
        if(endcounter > 0 && lifes == 0)
        {
            endcounter -= Time.deltaTime;
        }
        else if (endcounter <= 0 && lifes == 0)
        {
            canonalarm.gameObject.SetActive(false);
            hit.SetActive(false);
            if (death.gameObject.activeInHierarchy == false)
            {
                death.gameObject.SetActive(true);
                DeathHuuuh();
            }
        }
    }

    void DeathHuuuh()
    {
        deathindicator.gameObject.SetActive(true);
        int huuuh = Random.Range(1, 6);
        switch(huuuh)
        {
            case 1:
                deathindicator.gameObject.GetComponentInChildren<Text>().text = "HUUUH();";
                break;
            case 2:
                deathindicator.gameObject.GetComponentInChildren<Text>().text = "RUST IN PEACE";
                break;
            case 3:
                deathindicator.gameObject.GetComponentInChildren<Text>().text = "BI BU BU BOP";
                break;
            case 4:
                deathindicator.gameObject.GetComponentInChildren<Text>().text = "0xD4ED";
                break;
            case 5:
                deathindicator.gameObject.GetComponentInChildren<Text>().text = "RUM4DM8?";
                break;
        }
    }

    void EndOptions()
    {
        if((victory.gameObject.activeInHierarchy == true || death.gameObject.activeInHierarchy == true) && restartbtns.gameObject.activeInHierarchy == false)
        {
            restartbtns.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Placa" || other.gameObject.tag == "Teslas" || other.gameObject.tag == "RayoC" || other.gameObject.tag == "RayoT") && lifes > 0)
        {
            if(zap.enabled == true)
            {
                zap.Play();
            }
            else
            {
                zap.enabled = true;
            }
            if(other.gameObject.tag == "RayoC")
            {
                lifes = 0;
                canonalarm.gameObject.SetActive(false);
            }
            else
            {
                lifes--;
            }
            LifeUpdate();
            shakecounter = shakedur;
            zapcdcounter = zapcd;
            damajo.gameObject.SetActive(true);
            toques = true;
        }
        else if (other.gameObject.tag == "Canon")
        {
            canonalarm.gameObject.SetActive(true);
        }
        if (other.gameObject.tag == "Cobertura")
        {
            canjump = false;
        }
        if (other.gameObject.tag == "Victory" && victory.gameObject.activeInHierarchy == false)
        {
            victory.gameObject.SetActive(true);
            victoryindicator.gameObject.SetActive(true);
            victoryindicator.gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = timertext.gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text;
            timertext.gameObject.SetActive(false);
            curar.SetActive(true);
            endcounter = 1;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (zapcdcounter >= 0)
        {
            zapcdcounter -= Time.deltaTime / zapcd;
        }
        else if ((other.gameObject.tag == "Placa" || other.gameObject.tag == "Teslas") && lifes > 0)
        {
            if (zap.enabled == true)
            {
                zap.Play();
            }
            else
            {
                zap.enabled = true;
            }
            lifes--;
            LifeUpdate();
            shakecounter = shakedur;
            zapcdcounter = zapcd;
            damajo.gameObject.SetActive(true);
            toques = true;
        }
        else if (other.gameObject.tag == "Canon" && canonalarm.gameObject.activeInHierarchy == false)
        {
            canonalarm.gameObject.SetActive(true);
        }
        if (other.gameObject.tag == "Victory" && endcounter > 0)
        {
            endcounter -= Time.deltaTime;
        }
        else if (other.gameObject.tag == "Victory" && endcounter <= 0 && lifes < 5)
        {
            lifes++;
            LifeUpdate();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Placa" || other.gameObject.tag == "Teslas")
        {
            zapcdcounter = zapcd;
        }
        else if (other.gameObject.tag == "Canon")
        {
            canonalarm.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Cobertura")
        {
            canjump = true;
        }
    }

}
