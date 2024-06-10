using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPoints : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textoPuntos;
    private int puntos;
    // Start is called before the first frame update
    void Start()
    {
        puntos = PointsManager.instance.getTimePoints();
        mostrarPuntos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarPuntos()
    {
        textoPuntos.text = "Puntos: " + puntos;
    }
}
