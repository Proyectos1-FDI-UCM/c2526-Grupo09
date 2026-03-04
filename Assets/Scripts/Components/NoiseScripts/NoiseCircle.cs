//---------------------------------------------------------
// Comportamiento del círculo que generan los ruidos
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
public class NoiseCircle : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float Speed = 1.0f;  // velocidad de aumento de tamaño

    [SerializeField]
    private Vector3 PosFinal = new Vector3(5, 5, 5);  // posición final del círculo

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    bool _end;  // indica si ha llegado ya a su posición
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    void Start()
    {
        _end = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        CircleActive(ref _end);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void SetNoiseValues(float speed, Vector3 pos)  // establece los valores para el círculo
    {
        Debug.Log("PENEPENEPNEPENPENPENPE");
        Speed = speed;
        PosFinal = pos;
        while (!_end)
        {
            Debug.Log("HOLAHOLAHOLAHOLAHOLA");
            
        }
        _end = false;
        Destroy(this.gameObject);
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void CircleActive(ref bool _end)
    {
        if (transform.localScale.x < PosFinal.x &&
            transform.localScale.y < PosFinal.y)
        {
            transform.localScale += new Vector3(1, 1, 0) * Speed * Time.deltaTime;
        }
        else
        {
            _end = true;
        }     
    }
    #endregion   

} // class NoiseCircle 
// namespace
