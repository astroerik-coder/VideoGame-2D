using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    [SerializeField] private float moveSpeed = 1f; // Velocidad del enemigo
    [SerializeField] private int damage = 1; // Daño que causa al jugador
    private BoxCollider2D z_BoxCollider;
    private Transform playerTransform;
    private Animator z_Animator;

    // Métodos
    private void Start()
    {
        // Obtener componentes necesarios
        z_BoxCollider = GetComponent<BoxCollider2D>();
        z_Animator = GetComponent<Animator>();

        // Intentar encontrar al jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Movement();
        }
    }

    private void Movement()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Calcular el movimiento
        Vector2 moveDelta = direction * moveSpeed;

        // Cambiar la dirección de la escala según el movimiento
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Verificar colisiones en el eje X
        RaycastHit2D castResult = Physics2D.BoxCast(
            transform.position,
            z_BoxCollider.size,
            0,
            new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.fixedDeltaTime),
            LayerMask.GetMask("Player", "BlockMove")
        );
        if (castResult.collider)
        {
            moveDelta.x = 0; // Detener el movimiento en el eje X
        }

        // Verificar colisiones en el eje Y
        castResult = Physics2D.BoxCast(
            transform.position,
            z_BoxCollider.size,
            0,
            new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.fixedDeltaTime),
            LayerMask.GetMask("Player", "BlockMove")
        );
        if (castResult.collider)
        {
            moveDelta.y = 0; // Detener el movimiento en el eje Y
        }

        // Determinar si el enemigo está caminando
        bool isWalking = moveDelta.magnitude > 0;
        if (z_Animator != null)
        {
            z_Animator.SetBool("isWalking", isWalking);
        }

        // Mover al enemigo
        transform.Translate(moveDelta * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage); // Reduce la vida en 1
            }
            else
            {
                Debug.LogError("El jugador no tiene el componente Player.");
            }
        }
    }
}
