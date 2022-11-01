using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoExplosao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destrua este objeto assim que este script completar 4segundos
        Destroy(this.gameObject, 4f);
    }

}
