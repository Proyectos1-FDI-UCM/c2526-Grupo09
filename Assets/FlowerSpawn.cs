//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class FlowerSpawn : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject Flower;
    [SerializeField] private GameObject Gabriel;
    [SerializeField] private GameObject DeadGabriel;

    [SerializeField] private AudioSource killSound;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _insideCollider;

    private bool _hasPlayedSound = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        Flower.SetActive(false);
        DeadGabriel.SetActive(false);
        _insideCollider = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Solo actuamos si el jugador está dentro Y NO hay diálogo activo
        if (_insideCollider && !DialogueManager.Instance.GetIsDialogueInProgress())
        {
            if (!_hasPlayedSound && Gabriel != null && Gabriel.activeSelf)
            {
                _hasPlayedSound = true; // Lo primero es bloquear futuras ejecuciones

                if (killSound != null && killSound.clip != null)
                {
                    // Usamos la posición del transform actual para el sonido 2D
                    AudioSource.PlayClipAtPoint(killSound.clip, transform.position, 1.0f);
                }

                Debug.Log("¡Muerte ejecutada!");
                Debug.Log("_hasPLayedSound: "+_hasPlayedSound);

                // Cambios de estado
                Flower.SetActive(true);
                DeadGabriel.SetActive(true);
                Gabriel.SetActive(false);
            }
        }
    }

    /// <summary>
    /// detecta cuando el jugador entra al collider y lo guarda en la variable
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            _insideCollider = true;
        }
    }

    /// <summary>
    /// detecta cuando el jugador sale del collider y lo guarda en la variable
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            _insideCollider = false;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class FlowerSpawn
// namespace
