using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float shakeDecay = 0.002f;   
    public float intensity = .3f;
    public GameObject target;


    private Quaternion originRotation;
    private float shakeIntensity = 0;


   

    void Update()
    {
        if (shakeIntensity > 0) 
        {

            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
            originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
            originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
            originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);
            shakeIntensity -= shakeDecay;
        }
        if(shakeIntensity <= 0)
        {
            if(target != null)
            {
                transform.LookAt(target.transform);
            }

        }


    }

    public void Shake() 
    {

        originRotation = transform.rotation;
        shakeIntensity = intensity;
       
    }
}
