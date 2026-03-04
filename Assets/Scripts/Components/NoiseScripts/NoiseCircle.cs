//---------------------------------------------------------
// Comportamiento del círculo que generan los ruidos
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using static UnityEditor.PlayerSettings;
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
    private GameObject Player;  // jugador de la escena

    [SerializeField]
    private float WalkSpeed = 3f;  // velocidad de aumento de tamaño

    [SerializeField]
    private Vector3 FinalWalkPos = new Vector3(2, 2, 2);  // posición final del círculo

    [SerializeField]
    private float RunSpeed = 6f;  // velocidad de aumento de tamaño

    [SerializeField]
    private Vector3 FinalRunPos = new Vector3(4, 4, 4);  // posición final del círculo
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float _speed;
    private Vector3 _finalPos;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    //void FixedUpdate()
    //{
    //    CircleActive(ref _end);
    //}

    private void Update()
    {
        SetNoiseValues();
        CircleActive(_finalPos, _speed);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void SetNoiseValues()  // establece los valores para el círculo
    {
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        if (playerMovement != null )
        {
            if (InputManager.Instance.RunIsPressed())
            {
                _speed = RunSpeed;
                _finalPos = FinalRunPos;
            }
            else
            {
                _speed = WalkSpeed;
                _finalPos = FinalWalkPos;
            }
            //if (playerMovement.CurrentlyRunning())
            //{
            //    _speed = RunSpeed;
            //    _finalPos = FinalRunPos;
            //}
            //else
            //{
            //    _speed = WalkSpeed;
            //    _finalPos = FinalWalkPos;
            //}
        } 
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void CircleActive(Vector3 _finalPos, float _speed)
    {
        if (transform.localScale.x < _finalPos.x &&
            transform.localScale.y < _finalPos.y)
        {
            transform.localScale += new Vector3(1, 1, 0) * _speed * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion   

} // class NoiseCircle 
// namespace
