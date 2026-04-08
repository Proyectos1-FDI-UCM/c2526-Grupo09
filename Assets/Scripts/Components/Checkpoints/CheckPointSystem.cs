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
        Player.position = PlayerPos;
        Camera.position = PlayerPos;
        _lastCheck = PlayerPos;
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public static CheckPointSystem Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    public void LastCheck(Vector2 pos)
    {
        _lastCheck = pos;
    }

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
