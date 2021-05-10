using UnityEngine;
using UnityEngine.UI;

public class UITurboChargeMeter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController_Flying controller;

    private void Awake()
    {
        controller = player.GetComponent<PlayerController_Flying>();
    }

    private void Update()
    {
        float chargePercentage = Mathf.Clamp01(controller.ChargeValue / controller.MaxChargeValue);
        transform.localScale = new Vector2(chargePercentage, transform.localScale.y);
    }
}
