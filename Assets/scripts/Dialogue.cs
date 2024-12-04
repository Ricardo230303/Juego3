using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Para usar Image

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Componente de texto para el diálogo
    public TextMeshProUGUI promptMessage; // Componente de texto para el mensaje "presiona espacio"
    public Image promptImage;             // Imagen de fondo del mensaje "presiona espacio"
    public Image dialogueImage;           // Componente de imagen en el Canvas
    public string[] lines;                // Las líneas de diálogo
    public float textSpeed;               // Velocidad de escritura del texto
    public GameObject objectToActivate;

    public PlayerMovement playerMovement;

    private int index;                    // Índice de la línea actual del diálogo
    private bool playerInRange;           // Verifica si el jugador está cerca
    private bool isDialogueActive;        // Verifica si el diálogo está activo

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;  // Asegura que el texto esté vacío al inicio
        isDialogueActive = false;           // El diálogo comienza desactivado
        dialogueImage.gameObject.SetActive(false); // Desactiva la imagen al principio
        promptMessage.gameObject.SetActive(false); // Desactiva el mensaje "presiona espacio"
        promptImage.gameObject.SetActive(false);   // Desactiva la imagen de fondo del mensaje
        textComponent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el jugador está cerca y presiona la tecla espacio, iniciar el diálogo
        if (playerInRange && !isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue();
        }
        else { 
            // Si el diálogo está activo, procesar la entrada para avanzar con el texto
            if (isDialogueActive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (textComponent.text == lines[index])  // Si el texto ya está completo
                    {
                        NextLine();  // Avanzar a la siguiente línea
                    }
                    else
                    {
                        StopAllCoroutines();  // Detener la escritura en curso
                        textComponent.text = lines[index];  // Mostrar el texto completo
                        Debug.Log("Texto completo, siguiente línea");
                    }
                }
            }
        }
    }

    // Inicia el diálogo
    void StartDialogue()
    {
        if (lines.Length == 0) // Comprobamos si no hay líneas de diálogo
        {
            Debug.LogWarning("No hay líneas de diálogo asignadas.");
            return;
        }

        Debug.Log("Iniciando diálogo...");

        isDialogueActive = true;  // Marca el diálogo como activo
        index = 0;  // Empieza desde la primera línea
        textComponent.text = string.Empty;  // Limpia el texto
        dialogueImage.gameObject.SetActive(true);  // Muestra la imagen en el Canvas
        promptMessage.gameObject.SetActive(false); // Desactiva el mensaje "presiona espacio"
        promptImage.gameObject.SetActive(false);   // Desactiva la imagen de fondo del mensaje
        textComponent.gameObject.SetActive(true);
        StartCoroutine(TypeLine());  // Comienza a escribir el texto de la primera línea

        // Desactiva el movimiento del jugador durante el diálogo
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    // Corutina para escribir el texto línea por línea
    IEnumerator TypeLine()
    {
        Debug.Log("Escribiendo línea: " + lines[index]);

        // Verifica si la línea a mostrar existe
        if (lines.Length > index && lines[index] != null)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            Debug.Log("Línea escrita: " + lines[index]);
        }
        else
        {
            Debug.LogWarning("Línea de diálogo no válida.");
        }
    }

    // Avanza a la siguiente línea del diálogo
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;  // Incrementa el índice
            textComponent.text = string.Empty;  // Limpia el texto
            StartCoroutine(TypeLine());  // Escribe la siguiente línea
        }
        else
        {
            EndDialogue();  // Termina el diálogo
        }
    }

    // Termina el diálogo (sin desactivar el NPC)
    void EndDialogue()
    {
        isDialogueActive = false;  // Marca el diálogo como inactivo
        dialogueImage.gameObject.SetActive(false);  // Oculta la imagen del Canvas
        textComponent.gameObject.SetActive(false);
        Debug.Log("Diálogo terminado.");

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Activa el GameObject al finalizar el diálogo
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);  // Activa el GameObject
            Debug.Log("GameObject activado: " + objectToActivate.name);
        }
    }

    // Detecta cuando el jugador entra en el área de interacción
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto es el jugador
        {
            playerInRange = true; // El jugador está dentro del rango
            promptMessage.gameObject.SetActive(true); // Muestra el mensaje de "presiona espacio"
            promptImage.gameObject.SetActive(true);   // Muestra la imagen de fondo del mensaje
            Debug.Log("Jugador en rango, mostrando mensaje.");
        }
    }

    // Detecta cuando el jugador sale del área de interacción
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
