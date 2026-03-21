//---------------------------------------------------------
// Script que controla los estados y la forma de actuar de los enemigos.
// Alvaro Sosa Rodriguez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script que controla los estados y la forma de actuar de los enemigos. El enemigo va a detectar los sonidos
/// y cuando el jugador este visible, si lo detecta, el enemigo va a la posicion del sonido o del jugador.
/// Si el enemigo choca con el jugador, pierde la partida.
/// </summary>
public class EnemyLogic : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    /// <summary>
    /// El atributo Positions define las posiciones a las que irá el enemigo cuando este patrullando.
    /// </summary>
    [SerializeField]
    private Transform[] Positions;
    /// <summary>
    /// Escala del FOV del enemigo cuando esta patrullando
    /// </summary>
    [SerializeField]
    private Vector3 ConeScale = new Vector3 (1, 1, 1);
    /// <summary>
    /// Escala del FOV del enemigo cuando persigue al jugador o un sonido.
    /// </summary>
    [SerializeField]
    private Vector3 AlertConeScale = new Vector3 (2, 2, 1);
    /// <summary>
    /// Es el tiempo que tardara el enemigo en volver a su estado de patrullar si no pilla al jugador
    /// </summary>
    [SerializeField]
    private float ChasingTime = 10;
    /// <summary>
    /// Es la velocidad a la que se mueve el enemigo cuando esta patrullando.
    /// </summary>
    [SerializeField]
    private float PatrolSpeed = 1;
    /// <summary>
    /// Es la velocidad a la que se mueve el enemigo cuando esta persiguiendo.
    /// </summary>
    [SerializeField]
    private float ChasingSpeed = 1;
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
    /// La variable _posIndex es el índice que indica en que posición se encuentra el enemigo del array de posiciones
    /// </summary>
    #endregion
    private int _posIndex = 0;
    /// <summary>
    /// Determina si el enemigo ha entrado en contacto con un sonido.
    /// </summary>
    private bool _heardNoise = false;
    /// <summary>
    /// Determina si el jugador está dentro del rango de vision.
    /// </summary>
    private bool _isPlayerInRange = false;
    /// <summary>
    /// Determina si el jugador se encuentra en el FOV del enemigo.
    /// </summary>
    private bool _isPlayerVisible = false;
    /// <summary>
    /// Determina la posicion donde se origina el sonido con el que choca el enemigo.
    /// </summary>
    private Vector3 _noiseOrigin;
    /// <summary>
    /// Determina la posición del jugador cuando entra en el campo de vision del enemigo.
    /// </summary>
    private Transform _playerPos;
    /// <summary>
    /// Controla el tiempo en el que el enemigo persigue.
    /// </summary>
    private float _timer;
    

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Detecta si el jugador se esta escondiendo y cambia el estado del enemigo.
    /// </summary>
    void Update()
    {
        PlayerIsHiding();
        UpdateEnemyState();
    }

    /// <summary>
    /// Detecta si el enemigo entra en contacto con una onda de sonido.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hacer Ducktyping para saber si es un sonido. Si lo es, poner _heardNoise a true
        NoiseCircle noiseCircle = collision.gameObject.GetComponent<NoiseCircle>();
        if (noiseCircle != null && !_heardNoise)
        {
            _noiseOrigin = noiseCircle.gameObject.transform.position;
            _heardNoise = true;
            _timer = 0;
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
    /// Metodo que se llama desde EnemyVision cuando detecta al jugador en el FOV del enemigo.
    /// </summary>
    /// <param name="playerPos"></param>
    public void SawThePlayer(Transform playerPos)
    {
        _playerPos = playerPos;
        _isPlayerVisible = true;
        _timer = 0;
    }
    /// <summary>
    /// Metodo que se llama desde EnemyAttack cuando el enemigo choca con el jugador.
    /// </summary>
    public void KillThePlayer()
    {
        if (!_isPlayerInRange) _isPlayerInRange = true;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra

    /// <summary>
    /// Metodo que mueve al enemigo a una posicion "endPos". Ademas, rota el cono de vision en 8 direcciones.
    /// </summary>
    /// <param name="endPos"></param>
    private void MoveEnemy(Vector3 endPos, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        // Rotacion del cono de vision
        Vector3 dir = endPos - transform.position;
        float rotZ = 0;
        int incrX = Mathf.RoundToInt(dir.x);
        int incrY = Mathf.RoundToInt(dir.y);
        // Derecha
        if (incrX > 0 && incrY == 0) rotZ = -90;
        // Abajo
        else if (incrY < 0 && incrX == 0) rotZ = -180;
        // Izquierda
        else if (incrX < 0 && incrY == 0) rotZ = 90;
        // Arriba
        else if (incrY > 0 && incrX == 0) rotZ = 0;
        // Derecha-Arriba
        else if (incrX > 0 && incrY > 0) rotZ = -45;
        // Izquierda-Abajo
        else if (incrX < 0 && incrY < 0) rotZ = 135;
        // Derecha-Abajo
        else if (incrX > 0 && incrY < 0) rotZ = -135;
        // Izquierda-Arriba
        else if (incrX < 0 && incrY > 0) rotZ = 45;
        EnemyVision enemyVision = transform.gameObject.GetComponentInChildren<EnemyVision>();
        Transform enemyRotate = enemyVision.transform.parent;
        enemyRotate.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    /// <summary>
    /// Metodo que mira si el jugador esta escondido.
    /// </summary>
    private void PlayerIsHiding()
    {
        // Si el jugador se esconde mientras lo perseguíamos, perdemos la visibilidad
        if (_playerPos != null)
        {
            if (_playerPos.GetComponent<PlayerMovement>().GetIsHidden())
            {
                _isPlayerInRange = false;
                _isPlayerVisible = false;
                _heardNoise = false;
            }
        }
    }

    /// <summary>
    /// El metodo UpdateEnemyState maneja los estados del enemigo.
    /// </summary>
    private void UpdateEnemyState()
    {
        if (_isPlayerInRange)
        {
            PerformAttack();
        }
        else if (_isPlayerVisible)
        {
            PerformChase(_playerPos.position);
        }
        else if (_heardNoise)
        {
            PerformChase(_noiseOrigin);

            if (Vector3.Distance(transform.position, _noiseOrigin) < 0.2f)
            {
                _heardNoise = false;
            }
        }
        else
        {
            PerformPatrol();
        }
    }

    /// <summary>
    /// El metodo PerformPatrol se encarga de mover al enemigo en las posiciones indicadas en el array Positions.
    /// </summary>
    private void PerformPatrol()
    {
        if (_posIndex < Positions.Length)
        {
            MoveEnemy(Positions[_posIndex].position, PatrolSpeed);
            EnemyVision enemyVision = transform.gameObject.GetComponentInChildren<EnemyVision>();
            if (enemyVision != null)
            {
                enemyVision.ChangeConeScale(ConeScale);
            }
            if (Vector3.Distance(transform.position, Positions[_posIndex].position) < 0.1f)
            {
                _posIndex = (_posIndex + 1) % Positions.Length;
            }
        }
    }

    /// <summary>
    /// Si el enemigo detecta un sonido, este se movera al origen del mismo.
    /// </summary>
    private void PerformChase(Vector3 positionToGo)
    {
        _timer += Time.deltaTime;
        MoveEnemy(positionToGo, ChasingSpeed);
        EnemyVision enemyVision = transform.gameObject.GetComponentInChildren<EnemyVision>();
        if (enemyVision != null)
        {
            enemyVision.ChangeConeScale(AlertConeScale);
        }
        else Debug.Log("enemyVision no encontrado");
        if (_timer >= ChasingTime)
        {
            _heardNoise = false;
            _isPlayerVisible = false;
            _isPlayerInRange = false;
        }
    }


    /// <summary>
    /// Metodo que se encarga de llamar a EndGame del Level Manager.
    /// </summary>
    private void PerformAttack()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.EndGame(true);
        }
    }
    #endregion   

} // class EnemyMovement 
// namespace
