using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Para usar Image

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Componente de texto para el di�logo
    public TextMeshProUGUI promptMessage; // Componente de texto para el mensaje "presiona espacio"
    public Image promptImage;             // Imagen de fondo del mensaje "presiona espacio"
    public Image dialogueImage;           // Componente de imagen en el Canvas
    public string[] lines;                // Las l�neas de di�logo
    public float textSpeed;               // Velocidad de escritura del texto
    public GameObject objectToActivate;

    public PlayerMovement playerMovement;

    private int index;                    // �ndice de la l�nea actual del di�logo
    private bool playerInRange;           // Verifica si el jugador est� cerca
    private bool isDialogueActive;        // Verifica si el di�logo est� activo

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;  // Asegura que el texto est� vac�o al inicio
        isDialogueActive = false;           // El di�logo comienza desactivado
        dialogueImage.gameObject.SetActive(false); // Desactiva la imagen al principio
        promptMessage.gameObject.SetActive(false); // Desactiva el mensaje "presiona espacio"
        promptImage.gameObject.SetActive(false);   // Desactiva la imagen de fondo del mensaje
        textComponent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el jugador est� cerca y presiona la tecla espacio, iniciar el di�logo
        if (playerInRange && !isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue();
        }
        else { 
            // Si el di�logo est� activo, procesar la entrada para avanzar con el texto
            if (isDialogueActive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (textComponent.text == lines[index])  // Si el texto ya est� completo
                    {
                        NextLine();  // Avanzar a la siguiente l�nea
                    }
                    else
                    {
                        StopAllCoroutines();  // Detener la escritura en curso
                        textComponent.text = lines[index];  // Mostrar el texto completo
                        Debug.Log("Texto completo, siguiente l�nea");
                    }
                }
            }
        }
    }

    // Inicia el di�logo
    void StartDialogue()
    {
        if (lines.Length == 0) // Comprobamos si no hay l�neas de di�logo
        {
            Debug.LogWarning("No hay l�neas de di�logo asignadas.");
            return;
        }

        Debug.Log("Iniciando di�logo...");

        isDialogueActive = true;  // Marca el di�logo como activo
        index = 0;  // Empieza desde la primera l�nea
        textComponent.text = string.Empty;  // Limpia el texto
        dialogueImage.gameObject.SetActive(true);  // Muestra la imagen en el Canvas
        promptMessage.gameObject.SetActive(false); // Desactiva el mensaje "presiona espacio"
        promptImage.gameObject.SetActive(false);   // Desactiva la imagen de fondo del mensaje
        textComponent.gameObject.SetActive(true);
        StartCoroutine(TypeLine());  // Comienza a escribir el texto de la primera l�nea

        // Desactiva el movimiento del jugador durante el di�logo
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    // Corutina para escribir el texto l�nea por l�nea
    IEnumerator TypeLine()
    {
        Debug.Log("Escribiendo l�nea: " + lines[index]);

        // Verifica si la l�nea a mostrar existe
        if (lines.Length > index && lines[index] != null)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            Debug.Log("L�nea escrita: " + lines[index]);
        }
        else
        {
            Debug.LogWarning("L�nea de di�logo no v�lida.");
        }
    }

    // Avanza a la siguiente l�nea del di�logo
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;  // Incrementa el �ndice
            textComponent.text = string.Empty;  // Limpia el texto
            StartCoroutine(TypeLine());  // Escribe la siguiente l�nea
        }
        else
        {
            EndDialogue();  // Termina el di�logo
        }
    }

    // Termina el di�logo (sin desactivar el NPC)
    void EndDialogue()
    {
        isDialogueActive = false;  // Marca el di�logo como inactivo
        dialogueImage.gameObject.SetActive(false);  // Oculta la imagen del Canvas
        textComponent.gameObject.SetActive(false);
        Debug.Log("Di�logo terminado.");

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Activa el GameObject al finalizar el di�logo
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);  // Activa el GameObject
            Debug.Log("GameObject activado: " + objectToActivate.name);
        }
    }

    // Detecta cuando el jugador entra en el �rea de interacci�n
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto es el jugador
        {
            playerInRange = true; // El jugador est� dentro del rango
            promptMessage.gameObject.SetActive(true); // Muestra el mensaje de "presiona espacio"
            promptImage.gameObject.SetActive(true);   // Muestra la imagen de fondo del mensaje
            Debug.Log("Jugador en rango, mostrando mensaje.");
        }
    }

    // Detecta cuando el jugador sale del �rea de interacci�n
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto es el jugador
        {
            playerInRange = false; // El jugador ha salido del rango
            promptMessage.gameObject.SetActive(false); // Oculta el mensaje de "presiona espacio"
            promptImage.gameObject.SetActive(false);   // Oculta la imagen de fondo del mensaje
            Debug.Log("Jugador fuera de rango, ocultando mensaje.");
        }
    }
}
