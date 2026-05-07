//---------------------------------------------------------
// Script que detecta al jugador si se encuentra en el campo de visión del enemigo.
// Álvaro Sosa Rodríguez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script que comunica al script "EnemyLogic" si el jugador se encuentra en el campo de visión del enemigo y llama
/// al metodo "SawThePlayer". Ademas, se encarga de modificar la escala del FOV del enemigo.
/// </summary>
public class EnemyVision : MonoBehaviour
{
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Este metodo detecta si el jugador se encuentra en el rango de vision del enemigo y manda la posicion del jugador.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement PlayerScript = collision.gameObject.GetComponent<PlayerMovement>();
        if (PlayerScript != null && !PlayerScript.GetIsHidden() && !PauseManager.Instance.Pause)
        {
            // Avisar a EnemyLogic que el player esta en el rango de vision
            EnemyLogic enemyLogic = transform.parent.parent.GetComponent<EnemyLogic>();
            if (enemyLogic != null )
            {
                enemyLogic.SawThePlayer(PlayerScript.gameObject.transform);
            }
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Metodo que recibe un Vector3 y segun este cambia la escala del FOV del enemigo.
    /// </summary>
    /// <param name="scale"></param>
    public void ChangeConeScale(Vector3 scale)
    {
        gameObject.transform.localScale = scale;
    }

    #endregion
} // class EnemyVision 
// namespace
