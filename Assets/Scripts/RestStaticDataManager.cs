using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestStaticDataManager : MonoBehaviour {
    private void Awake() {
        CuttingCounter.RestStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}