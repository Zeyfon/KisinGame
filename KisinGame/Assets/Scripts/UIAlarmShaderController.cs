using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIAlarmShaderController : MonoBehaviour
{
    [Range(0,10)]
    [SerializeField] float intensityMaxValue = 5;
    [SerializeField] bool startAlarm = false;
    [SerializeField] bool endAlarm = false;
    [Range(0,3)]
    [SerializeField] float maxTime = 0;
    [Range(0,1)]
    [SerializeField] float glowThreshold = 0;


    PostProcessVolume volume;
    Bloom bloomProfile;
    Coroutine coroutine;
    bool isGlowing = false;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloomProfile);
        bloomProfile.intensity.value = 0;
    }

    IEnumerator AlarmPlayer()
    {
        while (true)
        {
            yield return IntensityUp();
            yield return new WaitForEndOfFrame();
            yield return IntensityDown();
        }

    }
    IEnumerator IntensityUp()
    {
        float intensity = 0;
        while(intensity < intensityMaxValue)
        {
            intensity += (Time.deltaTime * intensityMaxValue)/maxTime;
            if (intensity > intensityMaxValue) intensity = intensityMaxValue;
            bloomProfile.intensity.value = intensity;
            print("Increasing");
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator IntensityDown()
    {
        float intensity = intensityMaxValue;
        while (intensity > 0)
        {
            print("Decreasing");
            intensity -= (Time.deltaTime * intensityMaxValue) / maxTime;
            if (intensity < 0) intensity = 0;
            bloomProfile.intensity.value = intensity;

            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        if (startAlarm)
        {
            EnableAlertEffect();
        }
        if (endAlarm)
        {
            StopAlarmEffect();
        }
    }

    public void UpdateHealthPercentage(float healthPercentage)
    {
        print("Health Percentage:  " + healthPercentage);
        if (healthPercentage >= glowThreshold && isGlowing)
        {
            print("Start Glowing");
            StopAlarmEffect();
            isGlowing = false;
            return;
        }
        else if(healthPercentage< glowThreshold && !isGlowing)
        {
            EnableAlertEffect();
            isGlowing = true;
            return;
        }
    }

    public void EnableAlertEffect()
    {
        startAlarm = false;
        coroutine = StartCoroutine(AlarmPlayer());
    }

    public void StopAlarmEffect()
    {
        endAlarm = false;
        StopCoroutine(coroutine);
        bloomProfile.intensity.value = 0;
    }
}
