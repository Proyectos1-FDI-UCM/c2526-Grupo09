//---------------------------------------------------------
// Ruido que genera un objeto al caer
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
public class ObjectNoise : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject Circle;  // círculo de ruido que se genera
    #endregion
    
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// método que se llama cuando se cae un objeto para generar el círculo de ruido
    /// </summary>
    public void GenerateNoise()
    {
        NoiseCircle noisecircle = Circle.GetComponent<NoiseCircle>();
        if (noisecircle != null )
        {
            // antes de generar el círculo, indicamos que se ha caido un objeto
            noisecircle.FallenObject(true);
            // se instamcia el círculo
            Instantiate(Circle, transform.position, transform.rotation);
            // después de generarlo, indicamos que ya no se ha caido ningún objeto
            noisecircle.FallenObject(false);
        }
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   

}
