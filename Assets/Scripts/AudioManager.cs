using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ---------------------------------------------------------------------------------
// SCRIPT PARA LA GESTI�N DE AUDIO (vinculado a un GameObject vac�o "AudioManager")
// ---------------------------------------------------------------------------------
public class AudioManager : MonoBehaviour
{

    // Instancia �nica del AudioManager (porque es una clase Singleton) STATIC
    public static AudioManager instance;

    // Se crean dos AudioSources diferentes para que se puedan
    // reproducir los efectos y la m�sica de fondo al mismo tiempo
    // A�adimos una lista de AudioSources para efectos de sonido
    public List<AudioSource> sfxSources = new List<AudioSource>();
    public AudioSource musicSource; // Componente AudioSource para la m�sica de fondo

    // En vez de usar un vector de AudioClips (que podr�a ser) vamos a utilizar un Diccionario
    // en el que cargaremos directamente los recursos desde la jerarqu�a del proyecto
    // Cada entrada del diccionario tiene una string como clave y un AudioClip como valor
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

    // M�todo Awake que se llama al inicio antes de que se active el objeto. �til para inicializar
    // variables u objetos que ser�n llamados por otros scripts (game managers, clases singleton, etc).
    private void Awake()
    {

        // ----------------------------------------------------------------
        // AQU� ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
        // Garantizamos que solo exista una instancia del AudioManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        // ----------------------------------------------------------------

        // No destruimos el AudioManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);

        // Cargamos los AudioClips en los diccionarios
        LoadSFXClips();
        LoadMusicClips();

    }

    // M�todo privado para cargar los efectos de sonido directamente desde las carpetas
    private void LoadSFXClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI�N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
        sfxClips["Shot"] = Resources.Load<AudioClip>("SFX/shot");
        sfxClips["Trap"] = Resources.Load<AudioClip>("SFX/trap");
        sfxClips["Dead"] = Resources.Load<AudioClip>("SFX/death");
        sfxClips["PickUp"] = Resources.Load<AudioClip>("SFX/pickItem");
        sfxClips["Bite"] = Resources.Load<AudioClip>("SFX/bite");
    }

    // M�todo privado para cargar la m�sica de fondo directamente desde las carpetas
    private void LoadMusicClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI�N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/Music
        musicClips["MainTheme"] = Resources.Load<AudioClip>("Music/Main_Theme");

    }

    // M�todo de la clase singleton para reproducir efectos de sonido
    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            if (clipName == "Shot")
            {
                sfxSources[0].clip = sfxClips[clipName];
                sfxSources[0].Play();
            }
            else if (clipName == "Dead")
            {
                sfxSources[1].clip = sfxClips[clipName];
                sfxSources[1].Play();
            }
            else if (clipName == "Bite")
            {
                sfxSources[2].clip = sfxClips[clipName];
                sfxSources[2].Play();
            }
            else
            {
                sfxSources[3].clip = sfxClips[clipName];
                sfxSources[3].Play();
            }
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontr� en el diccionario de sfxClips.");

       
    }

    // M�todo de la clase singleton para reproducir m�sica de fondo
    public void PlayMusic(string clipName)
    {
        if (musicClips.ContainsKey(clipName))
        {
            musicSource.clip = musicClips[clipName];
            musicSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontr� en el diccionario de musicClips.");

        if (clipName == "MainTheme") musicSource.loop = true;
        else musicSource.loop = false;
    }
}
//}

//// -----------------------------------------
//// EJEMPLOS DE USO �DESDE CUALQUIER SCRIPT!
//// -----------------------------------------
//AudioManager.instance.PlayMusic("MainTheme");
//AudioManager.instance.PlaySFX("Jump");
