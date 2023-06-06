using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingClientBroadcast : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        MessagingManager.Instance.Broadcast();
    }
}
