using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadTimes : MonoBehaviour {

    string leer;
    public Text facil, normal, dificil, steamer;

    void Start()
    {
        AsignarTiempos();
    }

    void CargarUnNivel(string dir, Text dif)
    {
        StreamReader SR = new StreamReader(dir);
        if (File.Exists(dir))
        {
            string line = SR.ReadLine();
            dif.text = line;
            SR.Close();
        }
        else
        {
            dif.text = "-:--:---";
        }
        
    }

    void AsignarTiempos()
    {
        leer = Application.dataPath + "/TxtFiles/BestTime_Facil.txt";
        CargarUnNivel(leer, facil);
        leer = Application.dataPath + "/TxtFiles/BestTime_Normal.txt";
        CargarUnNivel(leer, normal);
        leer = Application.dataPath + "/TxtFiles/BestTime_Dificil.txt";
        CargarUnNivel(leer, dificil);
        leer = Application.dataPath + "/TxtFiles/BestTime_Steamer.txt";
        CargarUnNivel(leer, steamer);
    }
}
