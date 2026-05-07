//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Carmen Rosino Vílchez
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
    [SerializeField] private GameObject Cutscene;
    [SerializeField] private GameObject DialogEnd;


    [SerializeField] private AudioSource killSound;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _insideCollider;

    private bool _hasPlayedSound = false;
    private float timer = 0f;
    private bool _killed = false;

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
        Cutscene.SetActive(false);
        DialogEnd.SetActive(false);
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
                
                // Cambios de estado
                Cutscene.SetActive(true);
                Gabriel.SetActive(false);

                DeadGabriel.SetActive(true);
                Flower.SetActive(true);
                _killed = true;
               
            }
        }

        if (_killed)
        {
            timer += Time.deltaTime;
            Debug.Log("Se le ha sumado al contador");
            if (timer >= 2f)
            {
                Debug.Log("Ya ha llegado");
                DialogEnd.SetActive(true);
                _killed = false;
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
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class FlowerSpawn
// namespace
