using UnityEngine;

public class ShotgunAtack : MonoBehaviour
{
    public Transform firePoint; // Ponto de origem do tiro.
    public GameObject bulletPrefab; // Prefab do tiro.
    public float bulletSpeed = 10f; // Velocidade do tiro.
    public float maxDistance = 10f; // Alcance máximo do tiro.
    public float shotgunSpreadAngle = 15f; // Ângulo de espalhamento da escopeta.
    public int numberOfBullets = 5; // Número de balas disparadas pela escopeta.
    public float cooldown = 0.5f; // Tempo de recarga entre disparos.

    private float lastShotTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShotTime >= cooldown)
        {
            ShootShotgun();
            lastShotTime = Time.time;
        }
    }

    void ShootShotgun()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calcula o ângulo de espalhamento para cada bala.
            float spreadAngle = Random.Range(-shotgunSpreadAngle, shotgunSpreadAngle);

            // Cria uma instância do tiro a partir do prefab.
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Aplica a rotação com base no ângulo de espalhamento.
            bullet.transform.Rotate(Vector3.forward, spreadAngle);

            // Acesse o componente Rigidbody2D do tiro para aplicar a velocidade.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Define a velocidade do tiro na direção em que o objeto está olhando.
            rb.velocity = bullet.transform.right * bulletSpeed;

            // Destrua o tiro após atingir a distância máxima.
            Destroy(bullet, maxDistance / bulletSpeed);
        }
    }
}
