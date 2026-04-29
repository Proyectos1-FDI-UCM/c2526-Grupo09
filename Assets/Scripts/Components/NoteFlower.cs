//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class NoteFlower : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private FlowerCodeSpawner Spawner;
    [SerializeField] private Image[] ImagesFlower;
    [SerializeField] private GameObject NotePanel;
    [SerializeField] private TextMeshProUGUI TextNote;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private bool _nearNote;
    private bool _openNote;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        NotePanel.SetActive(false);
        _openNote = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //solo si esta tocando la nota y presionando E se ejecutara
        if (_nearNote && InputManager.Instance.InteractWasPressedThisFrame())
        {
            if (_openNote)
            {
                HideNote();
            }
            else ShowNote();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearNote = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearNote = false;
        }
    }

    /// <summary>
    /// enseña la nota que contiene el código
    /// </summary>
    public void ShowNote()
    {
        //recorre el array de las 4 flores y coge sus sprites
        FlowerTypes[] flowers = Spawner.GetFlowersArray();
        for (int i = 0; i < flowers.Length; i++)
        {
            SpriteRenderer sprite = flowers[i].GetComponent<SpriteRenderer>();
            ImagesFlower[i].sprite = sprite.sprite;
        }

        NotePanel.SetActive(true);
        _openNote = true;
        TextNote.text = "Pray thy God may save us, for this world is doomed. \nIf you still harbour hope in your heart, may these four flowers guide you to its heart\n Might this be the only way out of this apocalypse";
    }
    public void HideNote()
    {

        NotePanel.SetActive(false);
        _openNote = false;

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

} // class NoteFlower 
// namespace
