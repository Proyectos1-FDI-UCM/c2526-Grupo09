//---------------------------------------------------------
// Contiene el componente GameManager
// Guillermo Jiménez Díaz, Pedro P. Gómez Martín
// Marco A. Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;


/// <summary>
/// Componente responsable de la gestión global del juego. Es un singleton
/// que orquesta el funcionamiento general de la aplicación,
/// sirviendo de comunicación entre las escenas.
///
/// El GameManager ha de sobrevivir entre escenas por lo que hace uso del
/// DontDestroyOnLoad. En caso de usarlo, cada escena debería tener su propio
/// GameManager para evitar problemas al usarlo. Además, se debería producir
/// un intercambio de información entre los GameManager de distintas escenas.
/// Generalmente, esta información debería estar en un LevelManager o similar.
/// </summary>
public class GameManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject player;  // jugador de la escena
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----

    #region Atributos Privados (private fields)

    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static GameManager _instance;

    /// <summary>
    /// Dia en el que estamos actualmente
    /// Dia1 = Level1
    /// Dia2 = Level2
    /// Dia3 = Level3
    /// </summary>
    private int _diaActual = 1;

    /// <summary>
    /// booleano para saber si el jugado para pasar de dia
    /// </summary>
    private bool _haDormido = false;

    // variables que gestionan la activación / desactivación de cheats
    private PlayerNoise _playerNoise;
    private PlayerMovement _playerMovement;
    private bool _activeCheats;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----

    #region Métodos de MonoBehaviour

    /// <summary>
    /// Método llamado en un momento temprano de la inicialización.
    /// En el momento de la carga, si ya hay otra instancia creada,
    /// nos destruimos (al GameObject completo)
    /// </summary>
    protected void Awake()
    {
        if (_instance != null)
        {
            // No somos la primera instancia. Se supone que somos un
            // GameManager de una escena que acaba de cargarse, pero
            // ya había otro en DontDestroyOnLoad que se ha registrado
            // como la única instancia.
            // Si es necesario, transferimos la configuración que es
            // dependiente de este manager al que ya existe.
            // Esto permitirá al GameManager real mantener su estado interno
            // pero acceder a los elementos de la nueva escena
            // o bien olvidar los de la escena previa de la que venimos
            TransferManagerSetup();

            // Y ahora nos destruímos del todo. DestroyImmediate y no Destroy para evitar
            // que se inicialicen el resto de componentes del GameObject para luego ser
            // destruídos. Esto es importante dependiendo de si hay o no más managers
            // en el GameObject.
            DestroyImmediate(this.gameObject);
        }
        else
        {
            // Somos el primer GameManager.
            // Queremos sobrevivir a cambios de escena.
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        } // if-else somos instancia nueva o no.
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        if (player != null)
        {
            _playerNoise = player.GetComponent<PlayerNoise>();
            _playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    /// <summary>
    /// Método llamado cuando se destruye el componente.
    /// </summary>
    protected void OnDestroy()
    {
        if (this == _instance)
        {
            // Éramos la instancia de verdad, no un clon.
            _instance = null;
        } // if somos la instancia principal
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----

    #region Métodos públicos

    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static GameManager Instance
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
    /// cierre para evitar usar el GameManager que podría haber sido
    /// destruído antes de tiempo.
    /// </summary>
    /// <returns>Cierto si hay instancia creada.</returns>
    public static bool HasInstance()
    {
        return _instance != null;
    }

    /// <summary>
    /// Método que cambia la escena actual por la indicada en el parámetro.
    /// </summary>
    /// <param name="index">Índice de la escena (en el build settings)
    /// que se cargará.</param>
    public void ChangeScene(int index)
    {
        // Antes y después de la carga fuerza la recolección de basura, por eficiencia,
        // dado que se espera que la carga tarde un tiempo, y dado que tenemos al
        // usuario esperando podemos aprovechar para hacer limpieza y ahorrarnos algún
        // tirón en otro momento.
        // De Unity Configuration Tips: Memory, Audio, and Textures
        // https://software.intel.com/en-us/blogs/2015/02/05/fix-memory-audio-texture-issues-in-unity
        //
        // "Since Unity's Auto Garbage Collection is usually only called when the heap is full
        // or there is not a large enough freeblock, consider calling (System.GC..Collect) before
        // and after loading a level (or put it on a timer) or otherwise cleanup at transition times."
        //
        // En realidad... todo esto es algo antiguo por lo que lo mismo ya está resuelto)
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        System.GC.Collect();
        CheckPointSystem.Instance.RestartPlayerPos();
    } // ChangeScene

    //Metodo para pasar dia
    public void Sleep()
    {
        _haDormido = true;
        _diaActual++;
        Debug.Log("Has dormido. Mañana será el día: " + _diaActual);
    }

    //Resetea la cama para dormir
    public void ResetBed()
    {
        _haDormido = false; // Al salir a un nivel, reseteamos el estado para la próxima vez
    }

    //Para saber a que escena debemos de cambiar al salir de casa
    public int GetNextScene()
    {
        if (_diaActual == 2) return 7; //devuelve el level2 (7 en la lista de escenas)
        if (_diaActual == 3) return 8; //devuelve level3 (8 en la lista de escenas)
        return 0; // Error o volver al menú
    }

    /// <summary>
    /// carga la escena de la cabaña cuando se supera el nivel
    /// </summary>
    public void ReturnToHut()
    {
        SceneManager.LoadScene(9);  // 9 - escena de la cabaña
    }


    /// <summary>
    /// Permite a otros scripts consultar si el jugador ha dormido hoy.
    /// </summary>
    public bool GetHaDormido()
    {
        return _haDormido;
    }

    /// <summary>
    /// Permite al LevelManager consultar el día actual
    /// </summary>
    /// <returns></returns>
    public int GetCurrentDay()
    {
        return _diaActual;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetCheats()
    {
        // si los cheats no están activos
        if (!_activeCheats)
        {
            // se desactiva el script del círculo de ruido
            _playerNoise.enabled = false;
            // se duplica la velocidad de caminar
            float walkSpeed = _playerMovement.GetWalkSpeed();
            _playerMovement.SetWalkSpeed(walkSpeed * 2);
            // se duplica la velocidad de correr
            float runSpeed = _playerMovement.GetRunSpeed();
            _playerMovement.SetRunSpeed(runSpeed * 2);
            _activeCheats = true;
        }
        else
        {
            // se activa el script del círculo de ruido
            _playerNoise.enabled = true;
            // se reduce la velocidad de caminar a la original
            float walkSpeed = _playerMovement.GetWalkSpeed();
            _playerMovement.SetWalkSpeed(walkSpeed / 2);
            // se reduce la velocidad de correr a la original
            float runSpeed = _playerMovement.GetRunSpeed();
            _playerMovement.SetRunSpeed(runSpeed / 2);
            _activeCheats = false;
        }
        PauseManager.Instance.ChangeCheatsText(_activeCheats);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Dispara la inicialización.
    /// </summary>
    private void Init()
    {
        // De momento no hay nada que inicializar
    }

    private void TransferManagerSetup()
    {
        // De momento no hay que transferir ningún setup
        // a otro manager
    }

    #endregion
} // class GameManager 
  // namespace