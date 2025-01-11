using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatus status;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform charactorTransform;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private Camera playerCamara;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem gunEffect;

    private Ray mouseRay = new Ray();
    private RaycastHit hitData = new RaycastHit();
    private Vector2 moveValue;
    private bool isMove;
    private bool isReload = false;
    private bool isInputAttack = false;

    private readonly int moveHash = Animator.StringToHash("Move");
    private readonly int deathHash = Animator.StringToHash("Dead");

    private void Update()
    {
        Move();
        LootAt();

        if (isInputAttack)
        {
            Attack();
        }
    }

    private void Move()
    {
        if (moveValue.magnitude != 0f)
            transform.position += (new Vector3(moveValue.x, 0f, moveValue.y) * (Time.deltaTime * status.GetStatus(StatusInfo.Speed)));

        animator.SetFloat(moveHash, moveValue.magnitude);
    }

    private void Attack()
    {
        if (!isReload)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = attackPoint.position;
            bullet.GetComponent<Projectile>().Shoot(attackPoint.forward);
            gunEffect?.Play();
            StartCoroutine(CoReload());
        }
    }

    private void LootAt()
    {
        if(Time.timeScale == 0f)
            return;

        mouseRay = playerCamara.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(mouseRay, out hitData, 1000))
        {
            charactorTransform.rotation = Quaternion.LookRotation(new Vector3(hitData.point.x, charactorTransform.transform.position.y, hitData.point.z) - charactorTransform.position);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
        moveValue.Normalize();
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        isInputAttack = context.performed;
    }

    private IEnumerator CoReload()
    {
        isReload = true;
        yield return new WaitForSeconds(status.GetStatus(StatusInfo.AttackSpeed));
        isReload = false;
    }
}
