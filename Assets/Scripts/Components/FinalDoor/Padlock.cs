//---------------------------------------------------------
// Maneja los botones y el codigo del candado de la puerta final.
// Alvaro Sosa Rodriguez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using TMPro;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Se encarga de cambiar el numero que representa el codigo de la puerta final, 
/// ademas de generar el codigo en base a las flores que recibe
/// </summary>
public class Padlock : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    /// <summary>
    /// Text del primer digito del candado
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI[] Codes = new TextMeshProUGUI[4];

    /// <summary>
    /// Referencia al script FlowerCodeSpawner
    /// </summary>
    [SerializeField]
    private FlowerCodeSpawner FlowerCodeSpawner = null;

    /// <summary>
    /// Sprite al que cambia el GameObject
    /// </summary>
    [SerializeField]
    private Sprite DoorOpen;

    /// <summary>
    /// GameObject de la puerta
    /// </summary>
    [SerializeField]
    private GameObject Door;

    /// <summary>
    /// Hace referencia al Script Para enseñar el candado
    /// </summary>
    [SerializeField]
    private ShowPadlock ShowPad;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints


    /// <summary>
    /// Numero que cambia cuando el jugador interactua con el boton
    /// </summary>
    private int[] _nums = new int[4];

    /// <summary>
    /// Array de bools que comprueba que los digitos coinciden con el codigo
    /// </summary>
    private bool[] _codeBools = new bool[4];

    /// <summary>
    /// Guarda el codigo de la puerta
    /// </summary>
    private int[] _doorCode = null; 

    /// <summary>
    /// Array de las flores
    /// </summary>
    private FlowerTypes[] _flowers = null;

    /// <summary>
    /// Sprite de la puerta
    /// </summary>
    private SpriteRenderer _spriteDoor;

    /// <summary>
    /// Collider de la puerta
    /// </summary>
    private Collider2D _spriteCol;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void Start()
    {
        for (int i = 0; i < _codeBools.Length; i++)
        {
            _codeBools[i] = false;
            _nums[i] = 0;
        }

        _spriteDoor= Door.GetComponent<SpriteRenderer>();
        _spriteCol=Door.GetComponent<Collider2D>();

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
    /// Cambia el GUI del candado dependiendo del boton que se pulse, ademas si el digito
    /// coincide con el codigo de la puerta, pone su respectivo bool a true
    /// </summary>
    public void ChangeCodeNumber(int button)
    {
        _nums[button - 1]++;
        _nums[button - 1] = _nums[button - 1] % 10;
        Codes[button - 1].text = Convert.ToString(_nums[button - 1]);
        if (_doorCode != null)
        {
            _codeBools[button - 1] = _nums[button - 1] == _doorCode[button - 1];
        }
    }
    
    /// <summary>
    /// Genera el codigo de la puerta dependiendo del orden de las flores en el array
    /// </summary>
    public void CreatePadlockCode()
    {
        _flowers = FlowerCodeSpawner.GetFlowersArray();
        _doorCode = new int[_flowers.Length];
        for (int i = 0; i < _flowers.Length; i++)
        {
            _doorCode[i] = _flowers[i].NumPetals;
        }
    }

    /// <summary>
    /// Comprueba que el codigo es el correcto cuando el jugador pulsa el boton
    /// </summary>
    public void CheckCode()
    {
        int i = 0;
        while (i < _codeBools.Length && _codeBools[i]) 
        {
            i++;
        }
        if (i == _codeBools.Length)
        {
            ChangeSprite();
            ShowPad.DestroyPad();
        }
        else
        {
            Debug.Log("Codigo incorrecto");
        }
    }

    /// <summary>
    /// Cambia Sprite de la puerta
    /// </summary>
    public void ChangeSprite()
    {
        _spriteDoor.sprite = DoorOpen;
        _spriteCol.enabled = false;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class Padlock 
// namespace
