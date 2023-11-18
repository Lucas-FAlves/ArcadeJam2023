using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ShotgunScript : MonoBehaviour
{
    private BaseChar baseChar;
    private ShotgunCollisionController shotgunCollisionController;
    private float shootAngle;
    private bool holdingShoot = false;
    

    [SerializeField] private float damage = 1f;
    [SerializeField] private float shotgunAttackCooldownTime = 0f;
    [SerializeField] private float initialShootAngle;
    [SerializeField] private float shotgunAttackCooldown = 3f;

    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player2.Shotgun.started += OnShotgunPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shotgun.performed += OnShotgunPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shotgun.canceled += OnShotgunPerformed;
    }
    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player2.Shotgun.started -= OnShotgunPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shotgun.performed -= OnShotgunPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shotgun.canceled -= OnShotgunPerformed;
    }
    // Start is called before the first frame update
    void Start()
    {
        baseChar = GetComponent<BaseChar>();
        shotgunCollisionController = GetComponentInChildren<ShotgunCollisionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingShoot)
        {

        }
        else
        {
            shotgunAttackCooldownTime -= Time.deltaTime;
        }
    }


    public virtual void OnShotgunPerformed(InputAction.CallbackContext context)
    {
        if (shotgunAttackCooldownTime > 0 || baseChar.MovementScript.Stunned) return;
        if (context.started)
        {
            Debug.Log("Shotgun shoted");
            holdingShoot = true;
            shootAngle = initialShootAngle;
        }
        else if(context.canceled && holdingShoot && shotgunCollisionController != null)
        {
            holdingShoot = false;
            shotgunCollisionController.Shoot(shootAngle - (initialShootAngle) , Mathf.Clamp(damage, 0.15f, damage), 0.5f);

            shotgunAttackCooldownTime = shotgunAttackCooldown;
        }
    }
}
