using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class move : MonoBehaviour
{

    [Header("Movimiento")]
    public float velocidadMovimiento = 7f;
    public float velocidadNormal;
    public float velocidadCorrer;
    public bool corriendo;
    public float velocidadEnpuje;
    public int VidaJugador;

    public bool gateando;

    [Header("Salto")]
    public float fuerzaSalto = 10f;
    public bool isPiso = false;
    private float gravedad = -9.81f;
    private Vector3 velocity;
    private Vector3 moveInput;
    public CharacterController characterController;
    public float smoothVelocity;
    public float turnSmoothTime = 0.1f;

    [Header("InteractuarCajon")]
    public Animator animatorCajon;
    public float alcanse;
    public bool cajonAbierto = false;

    [Header("Linterna")]
    public GameObject Lampara;
    private bool linternaRecogida = false;

    private bool agachado;
    public float smoothAgachado;
    public bool agachandose;
    public BoxCollider ActivarIn;

    public GameObject muerte;
    public Animator animator;
    public Vector2 animVelocity;
    public bool isMoving, isRunning, hasLerped;
    public float walkSpeed, walkSpeedMultiplier, clampValue;
    public bool tocandoPiso;
    private bool isCollidingWithPushable = false;

    [Header("Desactivadores")]
    public move movimiento;
    public ObjetoAgarrable agarrable;
    public Respawn respawn;
    public Cordura cordura;
    public GameObject jugadorBase;
    public GameObject jugadorRagdoll;
    public EscalarZona escalar;
    [Header("SubirObjeto")]
    public LayerMask vaultLayer;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;
    public Vector3 vectorZone;

    private bool estaTocandoElObjeto = false;

    bool puedoTocar = false;



    public ControladorCajones cajonDetectado;
    public void SetDamage(int damage)
    {
        this.VidaJugador -= damage;
        VidaJugador = 0;
    }

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();

            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();

                if (animator == null)
                {
                    animator = GetComponentInParent<Animator>();

                    if (animator == null)
                    {
                        Debug.LogError("Animator component not found on " + gameObject.name + " or any of its children or parents.");
                    }
                }
            }
        }
    }
    private void Update()
    {
       
        if (VidaJugador <= 0)
        {
            GetDamage();
            Debug.Log("Daño");
        }

        if (Esconderse.Estado != Esconderse.State.None) return;

        agachado = Input.GetKey(KeyCode.LeftControl);

        if (agachado)
        {
            animator.SetBool("EstoyAgachado", true);
            float targetLocalScaleY = agachado ? 0.65f : 1f;
            characterController.center = new Vector3(0, 0.35f, 0);
            characterController.height = 0;

            if (agachandose == false && isMoving)
            {
                animator.SetBool("EstoyGateando", true);
            }
            else
            {
                animator.SetBool("EstoyGateando", false);
            }
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            characterController.center = new Vector3(0, 1, 0);
            characterController.height = 1.97f;
            animator.SetBool("EstoyAgachado", false);
            animator.SetBool("EstoyGateando", false);
        }

        Salto();
        Movimiento();
        Correr();
        Gravedad();
        PrenderLinterna();
        SubiendoObjeto();
        AnimationFloatControl();
        PuedoTocarCajon();
        DoCollisions();

        Vector3 finalVelocity;
        finalVelocity.x = velocity.x;
        finalVelocity.y = 0;
        finalVelocity.z = velocity.z;

        finalVelocity = Camera.main.transform.rotation * finalVelocity;
        finalVelocity.y = velocity.y;

        characterController.Move(finalVelocity * Time.deltaTime);
    }

    [SerializeField] private Vector3 collisionsOffset = Vector3.forward;
    [SerializeField] private Vector3 collisionsSize = Vector3.one;
    [SerializeField] private LayerMask collisionsLayer;

    private bool empujando = false;
    private Collider empujandoCollider;

    private void DoCollisions()
    {
        Vector3 position = transform.position + (transform.rotation * collisionsOffset);
        Collider[] colliders = Physics.OverlapBox(position, collisionsSize, transform.rotation, collisionsLayer);

        empujando = false;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enpujable"))
            {
                empujandoCollider = collider;
                empujando = true;
            }
        }

        animator.SetBool("EstoyEnpujando", empujando);

        var pushDir = new Vector3(velocity.x, 0, velocity.z);
        if (empujandoCollider && empujando && pushDir != Vector3.zero)
        {
            Rigidbody body = empujandoCollider.attachedRigidbody;

            if (body == null || body.isKinematic) { return; }

            body.velocity = pushDir * 2;
        }
    }

    public void Correr()
    {
        if (Esconderse.Estado != Esconderse.State.None) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            velocidadMovimiento = velocidadCorrer;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            velocidadMovimiento = velocidadNormal;
        }
    }

    private void Gravedad()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravedad * Time.deltaTime;
    }
    private void Salto()
    {
        if (Esconderse.Estado != Esconderse.State.None) return;

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            tocandoPiso = true;
            animator.SetBool("AccionActiva", true);
            animator.SetFloat("SpeedAccions", .25f);
            velocity.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
        }
        else
        {
            tocandoPiso = false;
            animator.SetBool("AccionActiva", false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnPiso"))
        {
            isPiso = true;
        }

    }
    public void Movimiento()
    {
        float entradaHorizontal = Input.GetAxis("Horizontal");
        float entradaVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(entradaHorizontal, 0, entradaVertical).normalized;

        velocity.x = direction.x * velocidadMovimiento;
        velocity.z = direction.z * velocidadMovimiento;

        //characterController.Move(direction * velocidadMovimiento * Time.deltaTime);

        float currentSpeed = direction.magnitude * velocidadNormal;

        if (Esconderse.Estado != Esconderse.State.None) direction = Vector3.zero;

        if (direction.magnitude > 0.1f)
        {
            isMoving = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetAngle += Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0);

        }
        else
        {
            isMoving = false;
        }
    }

    void AnimationFloatControl()
    {
        if (isMoving)
        {
            walkSpeed += Time.deltaTime * walkSpeedMultiplier;

            if (!isRunning)
            {
                clampValue = Mathf.Lerp(clampValue, 0.5f, Time.deltaTime * walkSpeedMultiplier);
                walkSpeed = Mathf.Clamp(walkSpeed, 0, clampValue);
                animator.SetFloat("Speed", walkSpeed);
            }
            else
            {
                clampValue = Mathf.Lerp(clampValue, 1f, Time.deltaTime * walkSpeedMultiplier);
                walkSpeed = Mathf.Clamp(walkSpeed, 0, clampValue);
                animator.SetFloat("Speed", walkSpeed);
            }
        }
        else
        {
            walkSpeed -= Time.deltaTime * walkSpeedMultiplier;
            walkSpeed = Mathf.Clamp(walkSpeed, 0, 1f);
            animator.SetFloat("Speed", walkSpeed);
        }
    }

    public void PuedoTocarCajon()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, alcanse))
        {
            if (hit.collider.CompareTag("Cajon"))
            {
                puedoTocar = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    cajonDetectado.Toggle();
                }
            }
        }
    }
    public void PrenderLinterna()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Lampara != null && linternaRecogida && !Lampara.activeSelf)
        {
            Lampara.SetActive(true);
        }

    }

    public void GetDamage()
    {
        movimiento.enabled = false;
        respawn.enabled = false;
        agarrable.enabled = false;
        cordura.enabled = false;
        jugadorBase.SetActive(false);
        jugadorRagdoll.SetActive(true);
        muerte.SetActive(true);
        escalar.enabled = false;
    }
    private void SubiendoObjeto()
    {


        Vector3 position = jugadorBase.transform.position;
        position += transform.forward * 1f;
        position += transform.up * 2f;

        Collider[] colliders = Physics.OverlapSphere(position, 0.5f, vaultLayer);


        foreach (var collider in colliders)
        {

            Debug.Log(collider.name);

            StartCoroutine(LerpSubiendo(collider.transform.position, 0.5f));

            return;
        }
    }


    IEnumerator LerpSubiendo(Vector3 targetPosition, float duration)
    {

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("AccionActiva", true);
        animator.SetFloat("SpeedAccions", .5f);
        transform.position = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("EstoyAgachado"))
        {
            animator.SetBool("EstoyAgachado", true);

            ActivarIn.enabled = false;
            agachandose = true;

            float newScaleY = Mathf.Lerp(transform.localScale.y, 0.65f, Time.deltaTime * smoothAgachado);

            transform.localScale = new Vector3(1, newScaleY, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EstoyAgachado"))
        {
            animator.SetBool("EstoyAgachado", false);
            ActivarIn.enabled = true;
            agachandose = false;

            float newScaleY = Mathf.Lerp(transform.localScale.y, 1f, Time.deltaTime * smoothAgachado);

            transform.localScale = new Vector3(1, newScaleY, 1);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 position = jugadorBase.transform.position;
        position += transform.forward * 1f;
        position += transform.up * 2f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, vectorZone);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * alcanse);

       
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(position, collisionsSize);
    }

}

