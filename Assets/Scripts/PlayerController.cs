using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Public Fields

    public float moveSpeed;

    public float jumpForce;

    public float raySize;

    #endregion

    #region Serializable Private Fields

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private bool isGrounded;

    [SerializeField] private float drainMultiplier;

    #endregion

    #region Private Fields

    private GameControls _gameControls;

    private Vector2 _inputVector;

    private Rigidbody2D _rigidbody2D;
    
    private int _groundMask = 1 << 8;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private float _playerEnergy;
    
    #endregion

    #region MonoBehaviour Callbacks
    
    private void OnEnable()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameControls = new GameControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _playerEnergy = 100f;

        GameManager.Instance.IsGameRunning = true;
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D result = Physics2D.Raycast(transform.position,
            Vector2.down, raySize, _groundMask);
        if (result.collider != null) isGrounded = true;
        else isGrounded = false;
        
        _animator.SetBool("Grounded", isGrounded);
        
        if(GameManager.Instance.IsGameRunning) DrainPlayerEnergy();
        
        Observer.SetSlider(_playerEnergy/100f);
    }


    void FixedUpdate()
    {
        //transform.position += (Vector3)_inputVector * moveSpeed * Time.deltaTime;
        _rigidbody2D.AddForce(_inputVector * moveSpeed * Time.fixedDeltaTime);
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
        _animator.SetFloat("VertSpeed", _rigidbody2D.velocity.y);

        if (_rigidbody2D.velocity.x > 0.1) _spriteRenderer.flipX = false;
        if (_rigidbody2D.velocity.x < -0.1) _spriteRenderer.flipX = true;
    }

    #endregion

    #region Private Methods

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == _gameControls.Gameplay.Move.name)
        {
            _inputVector = obj.ReadValue<Vector2>();
        }

        if (obj.action.name == _gameControls.Gameplay.Jump.name)
        {
            if (obj.performed)
            {
                DoJump();
                GameManager.Instance.ResetGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Battery"))
        {
            RechargePlayerEnergy(10f);
            
            BatteryPoolManager.Instance.DeactivateObject(other.gameObject);
        }
    }

    private void DrainPlayerEnergy()
    {
        _playerEnergy -= Time.deltaTime * drainMultiplier;

        if (_playerEnergy <= 0)
        {
            _playerEnergy = 0;
            GameManager.Instance.PlayerIsDead();
        }
    }

    private void RechargePlayerEnergy(float energy)
    {
        _playerEnergy += energy;

        if (_playerEnergy > 100f) _playerEnergy = 100f;
    }

    #endregion

    #region Public Methods

    public void DoJump()
    {
        if(isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    #endregion
    
    #region Debug Methods

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * raySize, Color.red);
    }

    #endregion
}
