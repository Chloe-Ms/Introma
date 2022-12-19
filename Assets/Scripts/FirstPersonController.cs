using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;


#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
	
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        [Header("Crouch")]
        [SerializeField] private float _standingHeight = 2f;
        [SerializeField] private float _crouchingHeight = 0.5f;
        [SerializeField] private float _timeToCrouch = 0.25f;
        [SerializeField] private Vector3 _crouchingCenter = new Vector3(0f,0.5f,0f);
        [SerializeField] private Vector3 _standingCenter = new Vector3(0f, 1f, 0f);
        [SerializeField] private Vector3 _standingCenterCollider = new Vector3(0f, 0f, 0f);
        [SerializeField] private Vector3 _crouchingCenterCollider = new Vector3(0f, -1f, 0f);
        [SerializeField] private float _cameraStandingCenter = 1.375f;
        [SerializeField] private float _cameraCrouchingCenter = 0.6875f;
        private bool _duringCrouchAnimation;
        private bool _isCrouching;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private GameObject _camera;
		public bool _canMove;

        [SerializeField] private float staminaMax = 12f;
        [SerializeField] private float nbSecForFullStamina = 20f;
        [SerializeField] private float nbSecForEmptyStamina = 10f;
        private float stamina;
		[SerializeField] private RectTransform _staminaSliderTransform;

        private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
			stamina = staminaMax;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

        }

		private void Update()
		{
			if (_canMove)
			{
                Move();
                GroundedCheck();
				CheckStamina(_input.sprint);
				UpdateUIStamina();
                CheckCrouch();
            }
		}

		private void UpdateUIStamina()
		{
			_staminaSliderTransform.offsetMax = new Vector2(-((120 * (1 - stamina / staminaMax)) - 5), _staminaSliderTransform.offsetMax.y);
        }

        private void CheckStamina(bool isDecreasing)
		{
			if (stamina <= staminaMax && stamina >= 0f)
			{
					if (isDecreasing)
					{
						stamina -= (staminaMax * Time.deltaTime) / nbSecForEmptyStamina;
					}
					else
					{
					if (!_input.sprint)
						stamina += (staminaMax * Time.deltaTime) / nbSecForFullStamina;
				}
			}
            if (stamina > staminaMax || stamina < 0f)
			{
                if (stamina < 0f)
                {
                    stamina = 0f;
                }
                else
                {
                    stamina = staminaMax;
                }
            }
        }

		private void LateUpdate()
		{
			if (_canMove)
			{
                CameraRotation();
            }
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint && stamina > 0f? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void CheckCrouch()
		{
			// Crouch
			if (_isCrouching != _input.crouch && !_duringCrouchAnimation)
			{
				StartCoroutine(CrouchStand());
            }
		}

		private IEnumerator CrouchStand()
		{
			_duringCrouchAnimation = true;
			float timeElapsed = 0;
			float targetHeight = _isCrouching ? _standingHeight : _crouchingHeight;
            float currentHeight = _controller.height;
            Vector3 currentCameraPosition = _camera.transform.localPosition;
            Vector3 targetCenter = _isCrouching ? _standingCenter : _crouchingCenter;
			Vector3 currentCenter = _controller.center;
            Vector3 targetCenterCollider = _isCrouching ? _standingCenterCollider : _crouchingCenterCollider;
            float targetCameraPosition = _isCrouching ? _cameraStandingCenter : _cameraCrouchingCenter;

            while (timeElapsed < _timeToCrouch)
			{
				_controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / _timeToCrouch);
                _controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / _timeToCrouch);
				_collider.height = _controller.height;
                _collider.center = Vector3.Lerp(currentCenter, targetCenterCollider, timeElapsed / _timeToCrouch);
				_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, Mathf.Lerp(currentCameraPosition.y, targetCameraPosition, timeElapsed / _timeToCrouch), _camera.transform.localPosition.z); 
                timeElapsed += Time.deltaTime;
				yield return null;
            }

			_controller.height = targetHeight;
            _controller.center = targetCenter;
            _collider.height = _controller.height;
			_collider.center = targetCenterCollider;
			_camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, targetCameraPosition, _camera.transform.localPosition.z);

            _isCrouching = !_isCrouching;
            _duringCrouchAnimation = false;
        }
		

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}

		public void CanMove(bool canMove)
		{
			_canMove = canMove;
		}
	}
}