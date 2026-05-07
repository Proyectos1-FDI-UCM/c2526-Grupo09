//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Hao Zheng
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PauseManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private TextMeshProUGUI CheatsText;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject FadeUI;
   

    [Header("Botones del UI de Pausa")]
    [SerializeField] private GameObject ExitControls;
    [SerializeField] private GameObject ResumeButton;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static PauseManager _instance;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void Awake()
    {
        _instance = this;
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        PausePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        Pause = false;
        HUD.SetActive(true);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (InputManager.Instance.PauseWasPressedThisFrame() && EndPanel.activeSelf == false) 
        {
            if (!ControlsPanel.activeSelf &&!UIOpened)
            {
                Pause = !Pause;
                PausePanel.SetActive(Pause);
                HUD.SetActive(!Pause);
                if (FadeUI != null) Destroy(FadeUI);

                LevelManager.Instance.SetFirstButton(ResumeButton);
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public TextMeshProUGUI CheatsEnabledText
    {
        get { return CheatsText; }
    }
    public bool UIOpened { get; private set; }

    public bool Pause 
    { 
        get; private set; 
    }

    public static PauseManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    } // Instance

    /// <summary>
    /// Restaura el juego al estado al que estaba
    /// </summary>
    public void Resume()
    {
        Pause = false;
        PausePanel.SetActive(false);
        HUD.SetActive(true);
    }
    public void ResumeVariable()
    {
        Pause = false;
    }
    public void PauseVariable()
    {
        Pause = true;
    }

    public void OpenUI()
    {
        UIOpened= true;
    }
    public void CloseUI()
    {
        UIOpened= false;
    }
    public void ChangeCheatsText(bool cheats)
    {
        if (cheats)
        {
            CheatsText.text = "Deactivate Cheats";
        }
        else
        {
            CheatsText.text = "Activate Cheats";
        }
    }

    /// <summary>
    /// Muestra el panel de controles y oculta el de pausa. 
    /// </summary>
    public void ShowControls()
    {
        if (PausePanel != null) PausePanel.SetActive(false);
        if (ControlsPanel != null) ControlsPanel.SetActive(true);
        LevelManager.Instance.SetFirstButton(ExitControls);
    }

    /// <summary>
    /// Oculta el panel de controles y vuelve al de pausa.
    /// </summary>
    public void HideControls()
    {
        if (ControlsPanel != null) ControlsPanel.SetActive(false);
        if (PausePanel != null) PausePanel.SetActive(true);
        LevelManager.Instance.SetFirstButton(ResumeButton);
    }
    #endregion

} // class PauseManager 
// namespace
