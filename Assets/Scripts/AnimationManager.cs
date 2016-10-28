using UnityEngine;
using System.Collections;

public class AnimationManager {
    public static void OnBeginDash(GameObject sender) {
        
    }
    public static void OnDashing(GameObject sender) {

    }
    public static void OnRun(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("running", true);
    }
    public static void OnWalk(GameObject sender) {

    }
    public static void OnBeginBlock(GameObject sender) {

    }
    public static void OnBlocking(GameObject sender) {

    }
    public static void OnBeginIdle(GameObject sender) {
        sender.GetComponent<Player>().animator.SetBool("running", false);
    }
}
