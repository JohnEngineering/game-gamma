using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public Vector2 lastMove;
    public float diagonalMoveModifier;
    public float inputThreshold;
    
    private Animator animator;
    private Rigidbody2D myRigidBody;
    private static bool playerExists;
    private float horizontalInput;
    private float verticalInput;
    private float currentMoveSpeed;

    void Start() {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        if (!playerExists) {
            DontDestroyOnLoad(transform.gameObject);
            playerExists = true;
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        UpdateHorizontalMovement();
        UpdateVerticalMovement();

        if (IsMovingDiagonally()) {
            currentMoveSpeed = moveSpeed * diagonalMoveModifier;
        } else {
            currentMoveSpeed = moveSpeed;
        }
    }

    private void UpdateHorizontalMovement() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (IsInputBeyondThreshold(horizontalInput)) {
            myRigidBody.velocity = new Vector2(horizontalInput * currentMoveSpeed, myRigidBody.velocity.y);
            lastMove = new Vector2(horizontalInput, 0f);
        } else {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        }
    }

    private void UpdateVerticalMovement() {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (IsInputBeyondThreshold(verticalInput)) {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, verticalInput * currentMoveSpeed);
            lastMove = new Vector2(0f, verticalInput);
        } else {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
        }
    }

    private bool IsInputBeyondThreshold(float input) {
        return Mathf.Abs(input) > inputThreshold;
    }

    private bool IsMovingDiagonally() {
        return IsInputBeyondThreshold(horizontalInput) && IsInputBeyondThreshold(verticalInput);
    }
}
