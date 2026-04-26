//---------------------------------------------------------
// Lógica que gestiona todos los checkpoints
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
public class CheckPointSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Camera;
    [SerializeField] private Vector3 PlayerPos;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private static CheckPointSystem _instance;
    private Vector2 _lastCheck;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        RestartPlayerPos();
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Marca el manager de checkpoints como singleton porque no puede haber más de un
    /// mangaer de checkpoints
    /// </summary>
    public static CheckPointSystem Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    /// <summary>
    /// Reinicia la posición del jugador a la del inicio del nivel
    /// </summary>
    public void RestartPlayerPos()
    {
        if (Player != null)
        {
            Player.position = PlayerPos;
            Camera.position = PlayerPos;
            _lastCheck = PlayerPos;
            GameObject[] checks = GameObject.FindGameObjectsWithTag("Checkpoint");
            for (int i = 0; i < checks.Length; i++)
            {
                CheckPoint checkpoint = checks[i].GetComponent<CheckPoint>();
                checkpoint.RestartCheckpoint();
            }
        }
    }

    /// <summary>
    /// Guarda la posición del último checkpoint que se ha recogido
    /// </summary>
    /// <param name="pos"></param>
    public void LastCheck(Vector2 pos)
    {
        _lastCheck = pos;
    }

    /// <summary>
    /// Devuelve la posición dle último checkpoint que se ha cogido
    /// </summary>
    /// <returns></returns>
    public Vector2 GetLastCheck()
    {
        return _lastCheck;
    }

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   

} // class CheckpointsSystem 
// namespace
