//---------------------------------------------------------
// Script para los triggers del diálogo.
// Rafa Campos García
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script para aquellas instancias que vayan a generar un diálogo. Se reparten por el mapa y spawnean un diálogo.
/// </summary>

namespace DialogueSystem {
    public class DialogueTrigger : MonoBehaviour
    {
        // ---- ATRIBUTOS DEL INSPECTOR ----
        #region Atributos del Inspector (serialized fields)
        // Documentar cada atributo que aparece aquí.
        // El convenio de nombres de Unity recomienda que los atributos
        // públicos y de inspector se nombren en formato PascalCase
        // (palabras con primera letra mayúscula, incluida la primera letra)
        // Ejemplo: MaxHealthPoints

        /// <summary>
        /// Ronda de dialogos serializada a adjuntar en el inspector.
        /// </summary>

        [SerializeField] private DialogueRound dialogue;


        #endregion

        // ---- ATRIBUTOS PRIVADOS ----
        #region Atributos Privados (private fields)
        // Documentar cada atributo que aparece aquí.
        // El convenio de nombres de Unity recomienda que los atributos
        // privados se nombren en formato _camelCase (comienza con _, 
        // primera palabra en minúsculas y el resto con la 
        // primera letra en mayúsculas)
        // Ejemplo: _maxHealthPoints

   


        #endregion

        // ---- MÉTODOS DE MONOBEHAVIOUR ----
        #region Métodos de MonoBehaviour

        // Por defecto están los típicos (Update y Start) pero:
        // - Hay que añadir todos los que sean necesarios
        // - Hay que borrar los que no se usen 


        #endregion

        // ---- MÉTODOS PÚBLICOS ----
        #region Métodos públicos
        // Documentar cada método que aparece aquí con ///<summary>
        // El convenio de nombres de Unity recomienda que estos métodos
        // se nombren en formato PascalCase (palabras con primera letra
        // mayúscula, incluida la primera letra)
        // Ejemplo: GetPlayerController

        /// <summary>
        /// Inicia el dialogo.
        /// </summary>
        public void TriggerDialogue() 
        {
            //llama a la instancia del dialogueManager y utilizamos el metodo startDialogue
            DialogueManager.Instance.StartDialogue(dialogue); 

            //No vuelve a aparecer el diálogo una vez terminado
            Destroy(this); 
        }

        #endregion

        // ---- MÉTODOS PRIVADOS ----
        #region Métodos Privados
        // Documentar cada método que aparece aquí
        // El convenio de nombres de Unity recomienda que estos métodos
        // se nombren en formato PascalCase (palabras con primera letra
        // mayúscula, incluida la primera letra)

        /// <summary>
        /// Cuando entre el player al collider, llama al método que spawnea el diálogo.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision) 
        {

            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                TriggerDialogue();
            }

        }
        #endregion

    } // class DialogueTrigger 
      // namespace
}
