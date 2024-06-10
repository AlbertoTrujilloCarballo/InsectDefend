using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().disparos++;
        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Cuando las balas salen de la escena se autodestruyen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
