using UnityEngine;

public class StartPlague : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void ExecuteAnim()
    {
        animator.SetBool("hasInput", true);
    }
}
