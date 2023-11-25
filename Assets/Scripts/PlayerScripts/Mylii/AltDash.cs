using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltDash : MonoBehaviour
{
    [SerializeField] private GameObject slowArea;
    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player.SlowingDash.performed += OnSlowingDashPerformed;
    }

    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player.SlowingDash.performed -= OnSlowingDashPerformed;
    }

    private void OnSlowingDashPerformed(InputAction.CallbackContext ctx)
    {
        BaseChar player = GetComponent<BaseChar>();
        player.Dash(ctx);
        Instantiate(slowArea, gameObject.GetComponent<Transform>());
    }
}
