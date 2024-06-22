using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Animator AiAnimator;

    private const string animShoot = "isShootingAnim";
    private const string animHeading = "isHeadingAnim";


    private const string animShootAi = "isShootingAnim";
    private const string animHeadingAi = "isHeadingAnim";

    private Player player;
    private AIControler aiPlayer;

    private void Start()
    {
        player = GetComponent<Player>();
        aiPlayer = GetComponent<AIControler>();
    }

    private void Update()
    {
        if (player != null)
        {
            playerAnim.SetBool(animShoot, player.isShooting);
            playerAnim.SetBool(animHeading, player.isHeading);
        }

        if (aiPlayer != null)
        {
            AiAnimator.SetBool(animShootAi, aiPlayer.aiCanShoot);
            AiAnimator.SetBool(animHeadingAi, aiPlayer.aiCanHeading);
        }

    }
}
