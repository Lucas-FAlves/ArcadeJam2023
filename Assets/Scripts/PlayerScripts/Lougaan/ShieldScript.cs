using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] private GameObject shieldGO;

    
    private bool shieldActive;
    private float shieldCooldownTime = 0f;
    private float shieldCurrentTime = 0f;
    [SerializeField] private float growthRate = 0.7f;
    [SerializeField] private float maxSize = 3.0f;
    [SerializeField] private float shieldCooldown = 5.0f;
    [SerializeField] private float shieldMaxSize = 3.0f;
    [SerializeField] private float shieldMaxTime = 4.0f;

    private void OnEnable()
    {
        InputMenager.Instance.CharacterInput.Player2.Shield.started += OnShieldPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shield.performed += OnShieldPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shield.canceled += OnShieldPerformed;

    }
    private void OnDisable()
    {
        InputMenager.Instance.CharacterInput.Player2.Shield.started -= OnShieldPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shield.started -= OnShieldPerformed;
        InputMenager.Instance.CharacterInput.Player2.Shield.started -= OnShieldPerformed;
    }
    private void Start()
    {
        //GameObject aux = GameObject.FindWithTag("Shield");
        //if (aux != null)
        //{
        //    Debug.Log("Achou o objeto");
        //    shieldGO = aux.GetComponent<GameObject>();
        //}
    }

    
    void Update()
    {
        if (shieldActive)
        {
            Debug.Log("Escudo está ativo");
            if (shieldGO.transform.localScale.x < shieldMaxSize)
            {
                Debug.Log("Escudo está crescendo");
                // Increase the scale of the object over time
                Vector2 newScale = (Vector2)shieldGO.transform.localScale + new Vector2(growthRate, growthRate) * Time.deltaTime;

                // Clamp the size to shieldMaxSize
                newScale = Vector2.Min(newScale, new Vector2(shieldMaxSize, shieldMaxSize));

                // Apply the new scale to the GameObject
                shieldGO.transform.localScale = new Vector3(newScale.x, newScale.y, 1f);
            }
            else
            {
                Debug.Log("Escudo está no tamanho máximo");
                shieldCurrentTime += Time.deltaTime;
                Debug.Log(shieldCurrentTime);
                if (shieldCurrentTime >= shieldMaxTime)
                {
                    Debug.Log("Escudo está desativado");
                    shieldGO.transform.localScale = new Vector3(1f,1f,1f);
                    shieldCurrentTime = 0f;
                    shieldActive = false;
                    shieldGO.SetActive(false);
                }
            }

        }
        else
        {
            shieldCooldownTime -= Time.deltaTime;
        }
    }

    public virtual void OnShieldPerformed(InputAction.CallbackContext context)
    {
        if (shieldCooldownTime > 0) return;
        if (context.started)
        {
            shieldActive = true;
            shieldGO.SetActive(true);
            Debug.Log("Ativou o escudo");
        }
    }
}
