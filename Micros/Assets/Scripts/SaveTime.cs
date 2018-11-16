using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveTime : MonoBehaviour {

    string escribir, leer, savedTime;
    public Text victime;
    public Image victory;
    bool Checado, VamoaGuardar;
    int scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        VamoaGuardar = false;
        Checado = false;
    }

    void Update ()
    {
		if(victory.gameObject.activeInHierarchy == true)
        {
            if (Checado == false)
            {
                CargarTiempo(scene);
                CompararTiempos();
                if (VamoaGuardar == true)
                {
                    GuardarTiempo(scene);
                }
                Checado = true;
            }
        }
	}

    void CompararTiempos()
    {
        string[] newTime = victime.text.Split(':');
        float newtimeinsecs = ((float.Parse(newTime[0]) * 60) + float.Parse(newTime[1]) + (float.Parse(newTime[2]) / 1000));
        string[] fileTime = savedTime.Split(':');
        float savedtimeinsecs = ((float.Parse(fileTime[0]) * 60) + float.Parse(fileTime[1]) + (float.Parse(fileTime[2]) / 1000));
        if (newtimeinsecs < savedtimeinsecs)
        {
            VamoaGuardar = true;
        }
    }

    void CargarTiempo(int index)
    {
        switch (index)
        {
            case 1:
                leer = Application.dataPath + "/TxtFiles/BestTime_Facil.txt";
                CargarArchivo(leer);
                break;
            case 2:
                leer = Application.dataPath + "/TxtFiles/BestTime_Normal.txt";
                CargarArchivo(leer);
                break;
            case 3:
                leer = Application.dataPath + "/TxtFiles/BestTime_Dificil.txt";
                CargarArchivo(leer);
                break;
            case 4:
                leer = Application.dataPath + "/TxtFiles/BestTime_Steamer.txt";
                CargarArchivo(leer);
                break;
        }
    }

    void CargarArchivo(string dir)
    {
        StreamReader SR = new StreamReader(dir);
        if (File.Exists(dir))
        {
            string line = SR.ReadLine();
            if(string.Equals(line, "-:--:---"))
            {
                savedTime = "9:99:999";
            }
            else
            {
                savedTime = line;
            }
            SR.Close();
        }
        else
        {
            savedTime = "9:99:999";
        }
    }

    void GuardarTiempo(int index)
    {
        switch(index)
        {
            case 1:
                escribir = Application.dataPath + "/TxtFiles/BestTime_Facil.txt";
                GuardarEnArchivo(escribir);
                break;
            case 2:
                escribir = Application.dataPath + "/TxtFiles/BestTime_Normal.txt";
                GuardarEnArchivo(escribir);
                break;
            case 3:
                escribir = Application.dataPath + "/TxtFiles/BestTime_Dificil.txt";
                GuardarEnArchivo(escribir);
                break;
            case 4:
                escribir = Application.dataPath + "/TxtFiles/BestTime_Steamer.txt";
                GuardarEnArchivo(escribir);
                break;
        }
    }

    void GuardarEnArchivo(string dir)
    {
        StreamWriter SW;
        if (File.Exists(dir))
        {
            SW = new StreamWriter(dir);
        }
        else
        {
            SW = File.CreateText(dir);
        }
        SW.WriteLine(victime.text);
        SW.Close();
    }
}
