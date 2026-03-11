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
    private GameObject Player;  // jugador de la escena

    [SerializeField]
    // velocidad de aumento de tamaño del círculo cuando camina
    private float WalkSpeed = 3f;  

    [SerializeField]
    // posición final del círculo si se está caminando
    private Vector3 FinalWalkPos = new Vector3(2, 2, 2);

    [SerializeField]
    // velocidad de aumento de tamaño del círculo cuando corre
    private float RunSpeed = 6f;

    [SerializeField]
    // posición final del círculo si se está corriendo
    private Vector3 FinalRunPos = new Vector3(4, 4, 4);
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _speed;  // velocidad de aumento
    private Vector3 _finalPos;  // posición final
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// En cada frame se mueve al jugador
    /// </summary>
    private void Update()
    {
        // elegir entre los valores del círculo dependiendo si estamos caminando o corriendo
        SetNoiseValues();
        // activa el círculo y se va moviendo frame a frame
        CircleActive(_finalPos, _speed);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Busca si se está caminando o corriendo y dependiendo de ese establece unos valores
    /// para _speed y _finalPos dependiendo de eso.
    /// </summary>
    public void SetNoiseValues()
    {
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        if (playerMovement != null )
        {
            if (InputManager.Instance.RunIsPressed())  // si estamos corriendo
            {
                _speed = RunSpeed;
                _finalPos = FinalRunPos;
            }
            else  // si no estamos corriendo es que estamos caminando
            {
                _speed = WalkSpeed;
                _finalPos = FinalWalkPos;
            }
        } 
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    /// <summary>
    /// Hace que el círculo de ruido vaya creciendo frame a frame y cuando llega a la
    /// posición final se destruye.
    /// </summary>
    /// <param name="_finalPos"></param>
    /// <param name="_speed"></param>
    private void CircleActive(Vector3 _finalPos, float _speed)
    {
        // si todavía no ha llegado a la posición final
        if (transform.localScale.x < _finalPos.x &&
            transform.localScale.y < _finalPos.y)
        {
            // se agranda progresivamente
            transform.localScale += new Vector3(1, 1, 0) * _speed * Time.deltaTime;
        }
        else  // si ya llegó a la posición final
        {
            Destroy(this.gameObject);  // se destruye
        }
    }
    #endregion   

}
