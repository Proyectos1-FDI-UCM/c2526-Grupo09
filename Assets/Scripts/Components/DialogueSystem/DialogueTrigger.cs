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
        /// <summary>
        /// Ronda de dialogos serializada a adjuntar en el inspector.
        /// </summary>
        [SerializeField] private DialogueRound dialogue;

        #endregion

        // ---- MÉTODOS PÚBLICOS ----
        #region Métodos públicos

        /// <summary>
        /// Inicia el dialogo.
        /// </summary>
        public void TriggerDialogue() 
        {
            //llama a la instancia del dialogueManager y utilizamos el metodo startDialogue
            DialogueManager.Instance.StartDialogue(dialogue); 

            //No vuelve a aparecer el diálogo una vez terminado
            Destroy(gameObject); 
        }

        #endregion

        // ---- MÉTODOS PRIVADOS ----
        #region Métodos Privados

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
