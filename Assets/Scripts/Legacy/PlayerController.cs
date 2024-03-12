using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//refactored in multiple scripts not used
public class PlayerController : MonoBehaviour {
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed; //playerMovement
    [SerializeField] private float _turnSpeed; //PlayerTurn and CameraMovement
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _invertMouse; //CameraMovement
    [SerializeField] private float _gravity = -9.81f;//playerMovement
    [SerializeField] private float _jumpVelocity; //PlayerJump
    [SerializeField] private float _sprintMultiplier;//playerMovement
    [SerializeField] private float _yTurnMin; //CameraMovement
    [SerializeField] private float _yTurnMax; //CameraMovement

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;//playerMovement
    [SerializeField] private LayerMask _groundMask;//playerMovement
    [SerializeField] private float _groundCheckDistance;//playerMovement

    [Header("Shoot")]
    [SerializeField] private Rigidbody _bulletPrefab; //ShootInteractor
    [SerializeField] private Rigidbody _rocketPrefab; //ShootInteractor
    [SerializeField] private Transform _spawnPoint; //ShootInteractor
    [SerializeField] private float _shootForce; //ShootInteractor

    [Header("Interact")]
    [SerializeField] private Camera _cam; //SimpleInteractor, //PickableInteractor
    [SerializeField] private float _interactionDistance; //SimpleInteractor
    [SerializeField] private LayerMask _interactionLayer; //SimpleInteractor

    [Header("Pick and Drop")]
    [SerializeField] private Transform _attachTransform; //PickableInteractor
    [SerializeField] private LayerMask _pickableLayer; //PickableInteractor
    [SerializeField] private float _pickableDistance; //PickableInteractor

    private CharacterController _characterController; //playerMovement

    private float _horizontal, _vertical; //PlayerInput
    private float _mouseX, _mouseY; //PlayerInput
    private float _camXRotation; //CameraMovement
    private Vector3 _playerVelocity;//playerMovement
    private bool _isGrounded;//playerMovement
    private float _moveMultiplier = 1;//playerMovement

    //Raycast
    private RaycastHit _raycastHit; //SimpleInteractor, PickableInteractor
    private ISelectable _selectable; //SimpleInteractor, PickableInteractor

    //Pick and Drop
    private bool _isPicked = false;
    private IPickable _pickable;

    void Start() {
        _characterController = GetComponent<CharacterController>(); //playerMovement
        //mouse cursoe
        Cursor.lockState = CursorLockMode.Locked; //CameraMovement
        Cursor.visible = false;//CameraMovement
    }

    // Update is called once per frame
    void Update() {
        GetInput();
        GroundCheck();
        MovePlayer();
        TurnPlayer();
        Jump();
        Shoot();
        ShootRocket();

        Interact();
        PickAndDrop();
    }
    
    void GetInput() {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Sprint") ? _sprintMultiplier : 1; //if true get sprint else its 1
    }//Done with input

    void MovePlayer() {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _moveSpeed * Time.deltaTime);

        //Ground Check
        if(_isGrounded && _playerVelocity.y < 0) {
            _playerVelocity.y = -2f;
        }
        //Set Player Velocity
        //v = u + a * t   v = g * t
        _playerVelocity.y += _gravity * Time.deltaTime;

        // V = 1/2 * a * t^2
        _characterController.Move(_playerVelocity * Time.deltaTime);

        //Done in PlayerMovement
    }//Done with PlayerMovement

    void TurnPlayer() {
        //Player turn movement
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX); //PlayerMovement

        //Camera up/down movement
        _camXRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1); //CameraMovement
        _camXRotation = Mathf.Clamp(_camXRotation, _yTurnMin, _yTurnMax); //CameraMovement
        _cameraTransform.localRotation = Quaternion.Euler(_camXRotation, 0, 0); //CameraMovement
    } //done

    void GroundCheck() { 
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }//Done with PlayerMovement

    void Jump() { //Done with PlayerJump
        if(Input.GetButtonDown("Jump")) {
            _playerVelocity.y = _jumpVelocity;
        }
    }//Done with PlayerJump

    void Shoot() {
        if(Input.GetButtonDown("Fire1")) {
            Rigidbody bulletRb = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
            bulletRb.AddForce(_spawnPoint.forward * _shootForce, ForceMode.Impulse);
            Destroy(bulletRb.gameObject, 5f);
        }
    } //Done with ShootInteractor

    void ShootRocket() {
        if (Input.GetButtonDown("Fire2")) {
            Rigidbody rocketRb = Instantiate(_rocketPrefab, _spawnPoint.position, _spawnPoint.rotation);
            rocketRb.AddForce(_spawnPoint.forward * _shootForce, ForceMode.Impulse);
            Destroy(rocketRb.gameObject, 5f);
        }
    } //Done with ShootInteractor

    void Interact() {
        //Get Ray details from screen
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));//centre of the camera
        //returns bool in case of something hitting
        //_raycastHit = contain info about object struck (transform, etc) --- _interacationDistance = makes it so we can interact with objects accross the map --- _interactionLayer = makes sure only certain layers are interactable
        if(Physics.Raycast(ray, out _raycastHit, _interactionDistance, _interactionLayer)) {
            _selectable = _raycastHit.transform.GetComponent<ISelectable>(); //found a selectable object

            if(_selectable != null) {
                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.E)) {
                    _selectable.OnSelect();
                }
            }
        }
        //cuz of ray, out mthod _raycastHit.transform is usable here
        if(_raycastHit.transform == null && _selectable!= null) {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    } //SimpleInteractor

    void PickAndDrop() {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out _raycastHit, _pickableDistance, _pickableLayer)) {
            if(Input.GetKeyDown(KeyCode.E) && !_isPicked) {
                _pickable = _raycastHit.transform.GetComponent<IPickable>();
                if (_pickable == null) return;

                _pickable.OnPicked(_attachTransform);
                _isPicked = true;
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && _isPicked && _pickable != null) {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }//done with PickableInteractor

}
