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
public class CheckpointsLogic : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // el último checkpoint que se ha cogido (se inicia con el primer checkpoint del nivel al principio)
    [SerializeField] private GameObject LastCheckpoint;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    /// <summary>
    /// Instancia única de la clase (singleton). EL CheckpointLogic debe ser un singleton
    /// porque solo puede haber uno en el nivel (es como si fuera un segundo LevelManager)
    /// </summary>
    private static CheckpointsLogic _instance;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static CheckpointsLogic Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    /// <summary>
    /// Indica el gameObject del último checkpoint por el que el jugador ha pasado
    /// </summary>
    /// <param name="checkpoint"></param>
    public void LastCheckpointUnlocked(GameObject checkpoint)
    {
        LastCheckpoint = checkpoint;
    }

    /// <summary>
    /// Respawnea al jugador en el último checkpoint
    /// </summary>
    public void Respawn()
    {
        // se llama al método para generar al jugador
        Checkpoints checkpoint = LastCheckpoint.GetComponent<Checkpoints>();
        checkpoint.GeneratePlayer();
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class CheckpointsLogic 
// namespace
