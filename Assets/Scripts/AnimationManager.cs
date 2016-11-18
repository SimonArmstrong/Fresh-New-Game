using UnityEngine;
using System.Collections;

public class AnimationManager {
    public static void OnBeginDash(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("dashing", true);
        sender.GetComponent<Player>().glowAnimator.SetBool("dashing", true);
    }
    public static void OnEndDash(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("dashing", false);
        sender.GetComponent<Player>().glowAnimator.SetBool("dashing", false);
    }
    public static void OnDashing(GameObject sender) {

    }
    public static void OnRun(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("running", true);
        sender.GetComponent<Player>().glowAnimator.SetBool("running", true);
    }
    public static void OnWalk(GameObject sender) {

    }
    public static void OnBeginBlock(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("block", true);
        sender.GetComponent<Player>().glowAnimator.SetBool("block", true);
    }
    public static void OnEndBlock(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("block", false);
        sender.GetComponent<Player>().glowAnimator.SetBool("block", false);
    }
    public static void OnBlocking(GameObject sender) {
        
    }
    public static void OnBeginIdle(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("running", false);
        sender.GetComponent<Player>().glowAnimator.SetBool("running", false);
    }

    public static void OnGetStunned(GameObject sender) {
        sender.GetComponent<Player>().animator.SetTrigger("stunned");
        sender.GetComponent<Player>().glowAnimator.SetTrigger("stunned");
    }
    public static void OnEndStunned(GameObject sender) {
        //sender.GetComponent<Player>().animator.SetBool("stunned", false);
    }

    void Update() {

    }
}
