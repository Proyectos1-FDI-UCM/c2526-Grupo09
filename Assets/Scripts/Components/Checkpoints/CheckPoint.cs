//---------------------------------------------------------
// Lógica de cada checkpoint individual
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class CheckPoint : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // indica si el checkpoint se ha cogido ya o no
    private bool _checkPicked = false;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Cuando algún elemento colisiona con el checkpoint
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si todavía el checkpoint no se ha obtenido
        if (!_checkPicked)
        {
            // si lo que ha colisionado con el checkpoint es el jugador
            if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                // se escribe el mensaje de que el checkpoint se ha obtenido
                LevelManager.Instance.CheckpointPicked(true);
                // se marca el checkpoint como obtenido
                _checkPicked = true;
                // se guarda la posición del checkpoint como última obtenida
                CheckPointSystem.Instance.LastCheck(transform.position);
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LevelManager.Instance.CheckpointPicked(false);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void RestartCheckpoint()
    {
        _checkPicked = false;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class CheckPoint 
// namespace
