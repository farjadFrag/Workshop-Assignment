using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private CharacterController characterController;


    [SerializeField] private GunFactory gunFactory;
    [SerializeField] private Transform gunParent;


    private WeaponType currentWeaponType = WeaponType.AK47;

    private IWeapon currentWeapon;

    private Dictionary<WeaponType, IWeapon> instantiatedWeapons;


    public PlayerInputActions playerControls;

    private InputAction move;
    private InputAction fire;

    private Vector2 moveDirection;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();


        AddListeners();
    }

    private void Start()
    {
        instantiatedWeapons = new Dictionary<WeaponType, IWeapon>();
        SetWeapon(WeaponType.AK47);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //ShootWeapon();
        }


        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(WeaponType.AK47);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(WeaponType.M4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(WeaponType.Famas);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetWeapon(WeaponType.GrenadeLauncher);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetWeapon(WeaponType.RocketLauncher);
        }



        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        characterController.Move(new Vector3(moveDirection.x * movementSpeed, 0, moveDirection.y * movementSpeed));
    }


    
    private void ShootWeapon(InputAction.CallbackContext context)
    {
        currentWeapon.Shoot();
    }

    private void SetWeapon(WeaponType weaponType)
    {
        var weapon = currentWeapon as MonoBehaviour;
        weapon?.gameObject.SetActive(false);

        if (instantiatedWeapons.ContainsKey(weaponType))
        {
            currentWeapon = instantiatedWeapons[weaponType];
            weapon = currentWeapon as MonoBehaviour;
            weapon.gameObject.SetActive(true);
        }
        else
        {
            currentWeapon = gunFactory.CreateWeapon(weaponType);
            weapon = currentWeapon as MonoBehaviour;
            weapon.transform.SetParent(gunParent, false);
            weapon.transform.localPosition = Vector3.zero;
            instantiatedWeapons.Add(weaponType, currentWeapon);
        }
    }

    private void AddListeners()
    {
        fire.performed += ShootWeapon;
    }

    private void RemoveListeners()
    {
        fire.performed -= ShootWeapon;
    }


    private void OnDisable()
    {
        RemoveListeners();
        move.Disable();
        fire.Disable();
    }
}
