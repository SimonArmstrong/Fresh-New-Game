using UnityEngine;
using System.Collections;

public class AnimationManager {
    public static void OnBeginDash(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("dashing", true);
    }
    public static void OnEndDash(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("dashing", false);
    }
    public static void OnDashing(GameObject sender) {

    }
    public static void OnRun(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("running", true);
    }
    public static void OnWalk(GameObject sender) {

    }
    public static void OnBeginBlock(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("block", true);
    }
    public static void OnEndBlock(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("block", false);
    }
    public static void OnBlocking(GameObject sender) {
        
    }
    public static void OnBeginIdle(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("running", false);
    }

    void Update() {

    }
}
