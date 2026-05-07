//---------------------------------------------------------
//Script que maneja la lógica del botón de interactuar.
// Hao Zheng
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using TMPro;
// Añadir aquí el resto de directivas using

/// <summary>
/// Clase Que gestiona la lógica del botón de interactuar
/// </summary>
public class FollowObjectUI : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Vector3 ButtonScale;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
   
    private Transform Target;
    private TextMeshProUGUI _buttonText;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
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
    /// <summary>
    /// Metodo que se le llama  para transformarlo en el target, para que el botón salga al lado de este. Activa el botón.
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetNewTarget(Transform newTarget)
    {
        Target = newTarget;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Método que se llama para que en el momento que coges un objeto, se ponga el target a null para que no vuelva a salir el botón de interactuar.
    /// </summary>
    public void ResetTarget()
    {
        Target = null;
        gameObject.SetActive(false);

    }
    /// <summary>
    /// Método que desactiva el boton.
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Método que cambia el texto del botón
    /// </summary>
    /// <param name="text"></param>
    public void ChangeText(string text)
    {
        _buttonText.text = "`E`/`A` to " + text;
    }
    #endregion

} // class FollowObjectUI 
// namespace
