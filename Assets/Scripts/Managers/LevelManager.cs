//---------------------------------------------------------
// Gestor de escena. Podemos crear uno diferente con un
// nombre significativo para cada escena, si es necesario
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// Componente que se encarga de la gestión de un nivel concreto.
/// Este componente es un singleton, para que sea accesible para todos
/// los objetos de la escena, pero no tiene el comportamiento de
/// DontDestroyOnLoad, ya que solo vive en una escena.
///
/// Contiene toda la información propia de la escena y puede comunicarse
/// con el GameManager para transferir información importante para
/// la gestión global del juego (información que ha de pasar entre
/// escenas)
/// </summary>
public class LevelManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject PanelWin;
    [SerializeField] private GameObject CheckpointObtained;
    [SerializeField] private GameObject Rock;
    [SerializeField] private GameObject Flower;
    [SerializeField] private GameObject FlowerImage;
    [SerializeField] private TextMeshProUGUI CurrentDay;
    [SerializeField] private TextMeshProUGUI FlowerObtained;
    [SerializeField] private GameObject HUD;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static LevelManager _instance;

    /// <summary>
    /// Int para saber si el jugador se ha pasado el nivel o no,
    /// 0 = esta jugando,
    /// 1 = se ha pasado el nivel,
    /// 2 = el jugador ha muerto
    /// </summary>
    private static int _levelStage = 0;

    private bool _flowerPicked;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----

    #region Métodos de MonoBehaviour
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected void Awake()
    {
        if (_instance == null)
        {
            // Somos la primera y única instancia
            _instance = this;
            Init();
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        PanelWin.SetActive(false);
        CheckpointObtained.SetActive(false);
        Rock.SetActive(false);
        _flowerPicked = false;
    }

    void Update()
    {
        CheckLevelStage();
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----

    #region Métodos públicos

    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static LevelManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    /// <summary>
    /// Devuelve cierto si la instancia del singleton está creada y
    /// falso en otro caso.
    /// Lo normal es que esté creada, pero puede ser útil durante el
    /// cierre para evitar usar el LevelManager que podría haber sido
    /// destruído antes de tiempo.
    /// </summary>
    /// <returns>Cierto si hay instancia creada.</returns>
    public static bool HasInstance()
    {
        return _instance != null;
    }

    public static int LevelStage()
    {
        return _levelStage;
    }

    public static void LevelWon()
    {
        _levelStage = 1;
    }

    public static void LevelLost()
    {
        _levelStage = 2;
    }

    public void RetryLevel()
    {
        LevelReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LevelReset()
    {
        _levelStage = 0;
    }

    /// <summary>
    /// Se llama cuando la flor se ha cogido para cambiar el mensaje de que la
    /// flor se ha cogido.
    /// </summary>
    public void FlowerPicked()
    {
        FlowerObtained.text = "1/1";
        _flowerPicked = true;
    }

    public bool GetFlowerPicked()
    {
        return _flowerPicked;
    }

    public void RockPicked(bool picked)
    {
        Rock.SetActive(picked);
    }

    /// <summary>
    /// Muestra en la esquina superior izquierda del HUD el día actual
    /// </summary>
    public void Day()
    {
        int day = GameManager.Instance.GetCurrentDay();
        CurrentDay.text = "Day " + day;
    }

    /// <summary>
    /// Si se ha obtenido un checkpoint, aparece un mensaje en la esquina superior
    /// derecha durante dos segundos indicando que se ha cogido un checkpoint
    /// </summary>
    public void CheckpointPicked(bool picked)
    {
        CheckpointObtained.SetActive(picked);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Método que activa el panel de FinDeJuego y cambia el texto del panel dependiendo de si pierdes o ganas
    /// </summary>
    /// <param name="loose"></param>
    public void EndGame()
    {
        Destroy(HUD);
    }

    private void EndMessage(GameObject message)
    {
        message.SetActive(false);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados


    private void Init()
    {
        // De momento no hay nada que inicializar
    }

    private void CheckLevelStage()
    {
        switch (_levelStage)
        {
            case 0: return;
            case 1:
                PanelWin.SetActive(true);
                EndGame();
                break;
            case 2: 
                EndGame();
                break;
        }
    }

    #endregion
} // class LevelManager 
// namespace