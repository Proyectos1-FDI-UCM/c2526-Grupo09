//---------------------------------------------------------
// Lógica de cada checkpoint individual
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
public class Checkpoints : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject PlayerNivel;
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private int CheckpointNumber;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Cuando colisiona el jugador con el checkpoint, se guarda como último checkpoint
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si lo que ha colisionado con el checkpoint es el jugador
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            // se guarda como último checkpoint obtenido
            CheckpointsLogic.Instance.LastCheckpointUnlocked(this.gameObject);
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Genera al jugador en el checkpoint cuando se pulsa el botón de respawn
    /// </summary>
    public void GeneratePlayer()
    {
        if (PlayerNivel != null)
        {
            // se elimina el player del nivel antiguo
            Destroy(PlayerNivel.gameObject);
        }
        // se genera uno nuevo en la posición del checkpoint
        GameObject player = Instantiate(PlayerPrefab, transform.position, transform.rotation);
        // se indica como nuevo jugador del nivel
        PlayerNivel = player;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion
}
