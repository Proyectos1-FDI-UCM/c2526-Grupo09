//---------------------------------------------------------
// Gestor de escena. Podemos crear uno diferente con un
// nombre significativo para cada escena, si es necesario
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín, Inés de la Peña
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

    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private GameObject PanelWin;
    [SerializeField] private GameObject PanelLost;
    [SerializeField] private TextMeshProUGUI Text;

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
        PanelLost.SetActive(false);
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LevelReset()
    {
        _levelStage = 0;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Método que activa el panel de FinDeJuego y cambia el texto del panel dependiendo de si pierdes o ganas
    /// </summary>
    /// <param name="loose"></param>
    public void EndGame(bool loose)
    {
        if (loose == true)
        {
            Text.text = "Has perdido";
            PauseManager.Instance.PauseGame();
        }
        else
        {
            Text.text = "Has Ganado";
            PauseManager.Instance.PauseGame();
        }
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
                EndGame(false);
                break;
            case 2: 
                PanelLost.SetActive(true);
                EndGame(true);
                break;
        }
    }

    #endregion
} // class LevelManager 
// namespace