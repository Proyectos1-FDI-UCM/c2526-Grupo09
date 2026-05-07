//---------------------------------------------------------
// Este script se encarga de detectar la colision del enemigo con el jugador.
// Alvaro Sosa Rodriguez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Este script se encarga de detectar la colision del enemigo con el jugador, si chocan llama a "KillThePlayer"
/// del script "EnemyLogic"
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private EnemyLogic enemyLogic;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Metodo que detecta cuando el jugador choca con el rango del enemigo.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null && !playerMovement.GetIsHidden())
        {
            EnemyLogic enemyLogic = transform.parent.GetComponent<EnemyLogic>();
            if (enemyLogic != null)
                Debug.Log("LLamando a killThePlayer");
                enemyLogic.KillThePlayer();
        }
    }
    #endregion 

} // class EnemyAttack 
// namespace
