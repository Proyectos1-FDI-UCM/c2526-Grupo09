//---------------------------------------------------------
// Gestor de escena. Podemos crear uno diferente con un
// nombre significativo para cada escena, si es necesario
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    [Header("Para todas las escenas (menos la de Dios)")]
    [SerializeField] private GameObject PanelWin;
    // mensaje de que se ha obtenido un checkpoint
    [SerializeField] private GameObject CheckpointObtained;
    [SerializeField] private GameObject Rock;
    [SerializeField] private GameObject Flower;
    [SerializeField] private GameObject FlowerImage;
    [SerializeField] private TextMeshProUGUI CurrentDay;
    [SerializeField] private TextMeshProUGUI FlowerObtained;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _hidden;
    [SerializeField] private GameObject _throwMode;
    [SerializeField] private PlayerNoise playerNoise;  // script de ruido
    [SerializeField] private PlayerMovement playerMovement;  // script de movimiento

    [Header("Solo para la escena del FinalPrologue")]
    [SerializeField] private GameObject LilyPart2;
    [SerializeField] private GameObject LilyPart3;
    [SerializeField] private GameObject LilyPart4;
    [SerializeField] private GameObject KillLily;
    [SerializeField] private GameObject KillLilyButton;

    [Header("Solo para la escena del Final")]
    [SerializeField] private GameObject BossDialogue;

    [Header("Solo para la escena de Dios")]
    [SerializeField] private GameObject Choice;
    [SerializeField] private GameObject TriggerRoute1;
    [SerializeField] private GameObject TriggerRoute2;
    [SerializeField] private BackgroundManager backgroundManager;
    [SerializeField] private GameObject GodButton;

    [Header("Solo para la escena de Inicio")]
    [SerializeField] private GameObject Evil;
    [SerializeField] private GameObject Lamb;
    [SerializeField] private GameObject Eve;
    [SerializeField] private GameObject FadeOut;
    [SerializeField] private GameObject Dialogue2;
    [SerializeField] private GameObject Dialogue3;
    [SerializeField] private GameObject Dialogue4;
    [SerializeField] private GameObject Dialogue5;
    [SerializeField] private GameObject FadeIn;

    [Header("Solo para la escena de Ending2")]
    [SerializeField] private TriggerEnding2 Ending2;

    [Header("Solo para la escena de GoingHome")]
    [SerializeField] private GameObject fadeOut;

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

    private int _dialogCont = 0;

    // VARIABLES EXCLUSIVA DE LA GESTIÓN DE LA ESCENA DE DIOS

    /// <summary>
    /// gestiona si se ha tomado ya o no la decisión en la escena de Dios
    /// </summary>
    private bool _choiceMade = false;

    /// <summary>
    /// tendrá valor 1 si se escoge la ruta 1 y 2 si se escoge la 2
    /// </summary>
    private int _route = 0;

    // variable que gestionan la activación / desactivación de cheats
    private bool _activeCheats;
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
        _gameOver.SetActive(false);
        _hidden.SetActive(false);
        _throwMode.SetActive(false);
        Day();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "FinalLevel")
        {
            if (!GameManager.Instance.DialogueExecuted)
            {
                BossDialogue.SetActive(true);
            }
        }
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

    /// <summary>
    /// Se llama cuando la flor se ha cogido para cambiar el mensaje de que la
    /// flor se ha cogido.
    /// </summary>
    public void FlowerPicked()
    {
        FlowerObtained.text = "1/1";
        _flowerPicked = true;
    }

    public void Hidden(bool hidden)
    {
        _hidden.SetActive(hidden);
    }

    public void ThrowingMode(bool mode)
    {
        _throwMode.SetActive(mode);
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
    /// Si se ha obtenido un checkpoint, aparece un mensaje en la esquina superior
    /// derecha durante dos segundos indicando que se ha cogido un checkpoint
    /// </summary>
    public void CheckpointPicked(bool picked)
    {
        CheckpointObtained.SetActive(picked);
    }

    public void GameOver(bool dead)
    {
        _gameOver.SetActive(dead);
    }
   
    /// <summary>
    /// Mira la escena en la que nos encontramos actualmente y ejecuta
    /// la acción correspondiente después de que se termine un diálogo
    /// </summary>
    public void CheckCurrentScene()
    {
        // cuenta los diálogos que han sido ejecutados
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "FinalLevelPrologue")
        {
            if (_dialogCont < 3)
            {
                KillLily.SetActive(true);
                playerMovement.enabled = false;
                SetFirstButton(KillLilyButton);
                _dialogCont++;
            }
            else
            {
                SceneManager.LoadScene("FinalLevel");
            }
        }
        else if (sceneName == "PostBattle")
        {
            SceneManager.LoadScene("GodIntro");
        }
        else if (sceneName == "God")
        {
            if (!_choiceMade)
            {
                Choice.SetActive(true);
                SetFirstButton(GodButton);
                _choiceMade = true;
            }
            else if (_route == 1)
            {
                backgroundManager.StartFade();
            }
            else if (_route == 2)
            {
                SceneManager.LoadScene("EndingRoute2");
            }
        }
        else if (sceneName == "EndingRoute2")
        {
            Ending2.ActivateImage();
        }
        else if (sceneName == "StartCutscene")
        {
            if (_dialogCont  > 0)
            {
                SceneManager.LoadScene("StartCutscene2");
            }
            _dialogCont++;
        }
        else if (sceneName == "StartCutscene2")
        {
            switch (_dialogCont)
            {
                case 0: Evil.SetActive(true); Dialogue2.SetActive(true); _dialogCont++; break;
                case 1: Lamb.SetActive(true); Dialogue3.SetActive(true); _dialogCont++; break;
                case 2: Eve.SetActive(true); Dialogue4.SetActive(true); _dialogCont++; break;
                case 3: Lamb.SetActive(false); Evil.SetActive(false); Eve.SetActive(false);
                        FadeIn.SetActive(true); Dialogue5.SetActive(true); _dialogCont++; break;
                case 4: SceneManager.LoadScene("FirstLevel"); break;
            }
        }
        else if (sceneName == "GoingHome")
        {
            fadeOut.SetActive(true);
            SceneManager.LoadScene("Home");
        }
    }

    public void KillLilyDialogue()
    {
        KillLily.SetActive(false);
        switch (_dialogCont)
        {
            case 1: LilyPart2.SetActive(true); break;
            case 2: LilyPart3.SetActive(true); break;
            case 3: LilyPart4.SetActive(true); break;
        }
    }

    /// <summary>
    /// Activa los diálogosn de la primera ruta
    /// </summary>
    public void Route1()
    {
        Choice.SetActive(false);
        _route = 1;
        TriggerRoute1.SetActive(true);
    }

    /// <summary>
    /// Activa los diálogos de la segunda ruta
    /// </summary>
    public void Route2()
    {
        Choice.SetActive(false);
        _route = 2;
        TriggerRoute2.SetActive(true);
    }

    /// <summary>
    /// Método que borra el botón seleccionado anteriormente y lo sobreescribe con uno nuevo, para poder navegar el UI sin necesidad del ratón.
    /// </summary>
    public void SetFirstButton(GameObject Button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Button);
    }

    /// <summary>
    /// Método que activa los cheats del juego
    /// </summary>
    public void SetCheats()
    {
        // si los cheats no están activos
        if (!_activeCheats)
        {
            // se desactiva el script del círculo de ruido
            playerNoise.enabled = false;
            // se duplica la velocidad de caminar
            float walkSpeed = playerMovement.GetWalkSpeed();
            playerMovement.SetWalkSpeed(walkSpeed * 2);
            // se duplica la velocidad de correr
            float runSpeed = playerMovement.GetRunSpeed();
            playerMovement.SetRunSpeed(runSpeed * 2);
            _activeCheats = true;
        }
        else
        {
            // se activa el script del círculo de ruido
            playerNoise.enabled = true;
            // se reduce la velocidad de caminar a la original
            float walkSpeed = playerMovement.GetWalkSpeed();
            playerMovement.SetWalkSpeed(walkSpeed / 2);
            // se reduce la velocidad de correr a la original
            float runSpeed = playerMovement.GetRunSpeed();
            playerMovement.SetRunSpeed(runSpeed / 2);
            _activeCheats = false;
        }
        PauseManager.Instance.ChangeCheatsText(_activeCheats);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    /// <summary>
    /// Muestra en la esquina superior izquierda del HUD el día actual
    /// </summary>
    private void Day()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "FirstLevel")
        {
            CurrentDay.text = "Day 1";
        }
        else if (sceneName == "SecondLevel")
        {
            CurrentDay.text = "Day 2";
        }
        else if (sceneName == "FinalLevelPrologue" || sceneName == "FinalLevel" || sceneName == "PostBattle")
        {
            CurrentDay.text = "Day 3";
            if (sceneName == "FinalLevel" || sceneName == "PostBattle")
            {
                FlowerObtained.text = "0/0";
            }
        } 
    }

    private void Init()
    {
        // De momento no hay nada que inicializar
    }
    #endregion
} // class LevelManager 
// namespace