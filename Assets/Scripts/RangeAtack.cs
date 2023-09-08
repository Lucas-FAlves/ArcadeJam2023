using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint; // Ponto de origem do tiro.
    public GameObject bulletPrefab; // Prefab do tiro.
    public float bulletSpeed = 10f; // Velocidade do tiro.
    public float maxDistance = 10f; // Alcance máximo do tiro.
    public float cooldownTime = 0.5f; // Tempo de cooldown entre tiros.

    private float nextFireTime; // Momento em que o próximo tiro será permitido.

    void Start()
    {
        nextFireTime = 0f; // Inicializa o próximo tiro para 0.
    }

    private void Update()
    {
        if (Time.time >= nextFireTime && Input.GetButtonDown("Fire1")) // Você pode alterar o botão conforme sua preferência.
        {
            Shoot();
            nextFireTime = Time.time + cooldownTime; // Define o próximo tempo de tiro com base no cooldown.
        }
    }

    void Shoot()
    {
        // Crie uma instância do tiro a partir do prefab.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Acesse o componente Rigidbody2D do tiro para aplicar a velocidade.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Defina a velocidade do tiro na direção em que o personagem está olhando.
        rb.velocity = firePoint.right * bulletSpeed;
        
        // Destrua o tiro após atingir a distância máxima.
        Destroy(bullet, maxDistance / bulletSpeed);
    }
}
