//---------------------------------------------------------
// Ruido que genera el jugador al caminar
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
public class PlayerNoise : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject Circle;  // círculo de ruido que se genera
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _lastCircle = 0.2f;  // cuando ha aparecido el último circulo
    private float _delay = 1f;  // delay entre aparición de un círculo y otro
    private float _circleSpeed = 1.5f;  // velocidad de aparición de círculo

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Este método genera un círculo de ruido cada vez que el jugador se mueve.
    /// Si se ha generado uno hace poco tiempo, espera para generar otro con un delay.
    /// </summary>
    public void PlayerMoving()
    {
        if (Time.time - _lastCircle < _delay / _circleSpeed)
        {
            return;
        }
        else
        {
            // se instancia un círculo de ruido
            Instantiate(Circle, transform.position, transform.rotation);
            _lastCircle = Time.time;
        }
    }

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   
}
