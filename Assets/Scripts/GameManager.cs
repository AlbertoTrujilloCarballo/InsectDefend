using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] TextMeshProUGUI textoDisparos;
    [SerializeField] TextMeshProUGUI textoEnemigos;
    [SerializeField] TextMeshProUGUI textoTiempo;
    private bool textSaw = true;
    private bool textChain = true;

    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject dialoguesObject;

    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject HPBar; // Para referenciar el relleno de la barra de vida

    // Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject itemButton_1;
    [SerializeField] GameObject itemButton_2;

    // Variable pública para modificar desde cualquier script
    public int disparos = 0;
    public int enemigos = 0;
    public int segundos = 0;

    // Variables privadas
    private bool canShoot = true;

    // Variable pública para modificar desde cualquier script
    public float life = 100; // Vida que tiene el jugador en un momento determinado
    public float maxLife = 100; // Vida máxima del jugador

    private Image img;

    // CÓDIGO ADICIONAL PARA EL SCRIPT GAMEMANAGER
    private Inventory inventario; // Para guardar la referencia al script del inventario



    //private void Start()
    //{
    //    // Configuramos el cursor con el sprite de la mirilla.
    //    // El segundo parámetro indica el punto efectivo del cursor (como la
    //    // imagen del cursor tiene 32x32 píxeles, el centro estará en 16, 16)
    //    // y el tercer parámetro indica el modo de renderizado del mismo
    //    Cursor.SetCursor(cursorTarget, new Vector2(16, 16), CursorMode.Auto);

    //}

    private void Start()
    {

        // Actualizamos la información
        UpdateHUD();

        StartCoroutine(Cronometro());

        img = HPBar.GetComponent<Image>();

        // Obtenemos la referencia al script del Inventario que hay asociado al GameManager
        inventario = GetComponent<Inventory>();
    }




    // Update is called once per frame
    // ---------------------------------------------
    // DETECCIÓN DE CLICS DEL MOUSE SOBRE LOS ÍTEMS
    // ---------------------------------------------
    // ---------------------------------------------
    // DETECCIÓN DE CLICS DEL MOUSE SOBRE LOS ITEMS
    // ---------------------------------------------
    void Update()
    {

        // Actualización de la barra de vida
        img.fillAmount = life / maxLife;

        // Código para probar que la barra de vida funciona
        if (Input.GetKey(KeyCode.UpArrow)) life = life + 0.25f;
        if (Input.GetKey(KeyCode.DownArrow)) life = life - 0.25f;

        // Verificar si se ha hecho clic con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {

            // Obtener la posición del clic en la pantalla
            Vector3 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Crear un RaycastHit2D para almacenar la información de colisión 2D
            // Desde la posición en la que se hizo clic en la dirección 0, 0, 0
            // por lo tanto el Raycast será un diminuto punto que detecta la colisión
            RaycastHit2D hit = Physics2D.Raycast(clicPosition, Vector2.zero);

            // Filtrar los objetos que interesan según su etiqueta

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("spikedball_item") && !dialoguesObject.activeSelf)
                {
                    Debug.Log("¡¡BOLA CON PINCHOS!!");
                    // Verificamos si existen huecos libres en el inventario
                    for (int i = 0; i < inventario.slots.Length; i++)
                    {
                        if (!inventario.isFull[i])
                        { // Se pueden añadir items
                            inventario.isFull[i] = true; // Ocupamos la posición
                                                         // Instanciamos un botón en la posición del slot
                            Instantiate(itemButton_1, inventario.slots[i].transform, false);
                            AudioManager.instance.PlaySFX("PickUp");
                            Destroy(hit.collider.gameObject); // Destruimos el objeto
                            DisableFire();
                            if (textChain)
                            {
                                dialoguesObject.SetActive(true); // Activamos los diálogos
                                GameObject.Find("Panel").GetComponent<DialogueController>().StartDialogue("spikedball_item");
                                textChain = false;
                            }
                            break; // Salimos del bucle
                        }
                    }
                }
                if (hit.collider.CompareTag("sawblade_item") && !dialoguesObject.activeSelf)
                {
                    Debug.Log("¡¡DISCO DE SIERRA!!");
                    // Verificamos si existen huecos libres en el inventario
                    for (int i = 0; i < inventario.slots.Length; i++)
                    {
                        if (!inventario.isFull[i])
                        { // Se pueden añadir items
                            inventario.isFull[i] = true; // Ocupamos la posición
                                                         // Instanciamos un botón en la posición del slot
                            Instantiate(itemButton_2, inventario.slots[i].transform, false);
                            AudioManager.instance.PlaySFX("PickUp");
                            Destroy(hit.collider.gameObject); // Destruimos el objeto
                            DisableFire();
                            if (textSaw)
                            {
                                dialoguesObject.SetActive(true); // Activamos los diálogos
                                GameObject.Find("Panel").GetComponent<DialogueController>().StartDialogue("sawblade_item");
                                textSaw = false;
                            }

                            break; // Salimos del bucle
                        }
                    }
                }
            }
        }

        if(life <= 0)
        {
            SCManager.instance.LoadScene("GameOver");
        } 
    }

    // Método para restar vida al Player
    public void TakeDamage(float damage)
    {
        life = Mathf.Clamp(life - damage, 0, maxLife);
    }

    // Método para añadir vida al Player
    public void Heal(int lifeRecovered)
    {
        life = life = Mathf.Clamp(life + lifeRecovered, 0, maxLife); ;
    }




    // ---------------------------------------
    // Método para desactivar los disparos
    // ---------------------------------------
    public void DisableFire()
    {
        canShoot = false;
    }

    // ---------------------------------------
    // Método para activar los disparos
    // ---------------------------------------
    public void EnableFire()
    {
        canShoot = true;
    }

    // -------------------------------------------------
    // Método para devolver si se puede disparar o no
    // -------------------------------------------------
    public bool GetShootingStatus()
    {
        return canShoot;
    }


    // Método público que actualiza el texto de los disparos
    public void UpdateHUD()
    {
        textoTiempo.text = "Tiempo: " + segundos;
        /* Version Antigua
        // Actualizamos el texto de las balas disparadas
        textoDisparos.text = "Disparos: " + disparos;
        textoEnemigos.text = "Enemigos: " + enemigos;
        */
    }

    IEnumerator Cronometro()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            segundos++;
            UpdateHUD();
            PointsManager.instance.SetTimeAndSeconds(segundos);
        }
    }
}
