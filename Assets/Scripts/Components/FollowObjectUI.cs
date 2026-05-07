//---------------------------------------------------------
//Script que maneja la lógica del botón de interactuar.
// Hao Zheng
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class FollowObjectUI : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Vector3 ButtonScale;


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private Transform Target;
    private TextMeshProUGUI _buttonText;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void Start()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        transform.localScale = ButtonScale;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Target != null)
        {
            transform.position = Target.position + Offset;
        }

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    /// <summary>
    /// Metodo que se le llama desde getobject para transformarlo en el target, para que el botón salga al lado de este. Activa el botón.
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetNewTarget(Transform newTarget)
    {
        Target = newTarget;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Método que se llama desde getobject para que en el momento que coges un objeto, se ponga el target a null para que no vuelva a salir el botón de interactuar.
    /// </summary>
    public void ResetTarget()
    {
        Target = null;
        gameObject.SetActive(false);

    }
    /// <summary>
    /// método que desactiva el boton.
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void ChangeText(string text)
    {
        _buttonText.text = "`E`/`A` to " + text;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class FollowObjectUI 
// namespace
