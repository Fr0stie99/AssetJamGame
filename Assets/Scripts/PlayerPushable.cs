using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerPushable {
    float GetPushback();
    Vector3 GetContactPoint();
}
