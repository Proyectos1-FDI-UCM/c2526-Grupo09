//---------------------------------------------------------
// Activa las imagenes de la muerte de Agnus
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class TriggerAgnusDeath : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject Panel;  // panel dónde está la imagen
    [SerializeField] private Image AgnusImage;  // imagen de agnus
    [SerializeField] private Sprite[] images;  // 4 fotos en orden
    [SerializeField] private PlayerMovement Player;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _triggered = false;  // indica si ya se ha entrado en el collider
    private float _timer = 0f;  // contador de tiempo
    private int _currentIndex = 0;  // pos actual en el array
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        AgnusImage.sprite = images[0];
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_triggered)
        {
            // si no estamos en el último fondo
            if (_currentIndex < images.Length - 1)
            {
                _timer += Time.deltaTime;

                if (_timer >= 2f)
                {
                    _timer = 0f;
                    _currentIndex++;
                    AgnusImage.sprite = images[_currentIndex];
                }
            }
            // si ya estamos en el último fondo
            else if (_currentIndex < images.Length)
            {
                _timer += Time.deltaTime;

                if (_timer >= 2f)
                {
                    Panel.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Cuando el player entre en el collider se activan las imagenes
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        Panel.SetActive(true);
        _triggered = true;
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class TriggerAgnusDeath 
// namespace
