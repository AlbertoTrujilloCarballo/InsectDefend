using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Referencias privadas accesibles desde el inspector
    [SerializeField] GameObject enemy;

    // Variables p�blicas
    public float timeBetweenGenerations = 3f;
    public float spawnLineLength = 2f;


    // M�todo que se ejecuta en el primer frame
    void Start()
    {
        // Lanzamos la corrutina al arrancar el script
        StartCoroutine(GenerateEnemy());
        StartCoroutine(LessTimeBetweenEnemies());
    }

    IEnumerator GenerateEnemy()
    {

        // La corrutina estar� siempre en ejecuci�n
        while (true)
        {

            // Posici�n aleatoria de spawn a lo largo de la l�nea
            float randomPosY = Random.Range(transform.position.y - spawnLineLength, transform.position.y + spawnLineLength);

            // Instanciamos el nuevo enemigo en una posici�n vertical aleatoria
            GameObject newEnemy = Instantiate(enemy, new Vector2(transform.localPosition.x, randomPosY), Quaternion.identity);
            newEnemy.transform.SetParent(null); // Desvinculamos el enemigo del objeto padre

            // La corrutina se pausa durante el tiempo indicado
            // en la variable p�blica timeBetweenGenerations
            yield return new WaitForSeconds(timeBetweenGenerations);

        }
    }

    IEnumerator LessTimeBetweenEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(7.5f);
            //timeBetweenGenerations -= 0.05f;
            timeBetweenGenerations = Mathf.Clamp(timeBetweenGenerations - 0.2f, 1f, 3f);
        }
    }

}
