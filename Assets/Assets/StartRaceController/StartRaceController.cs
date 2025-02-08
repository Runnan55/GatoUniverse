using UnityEngine;
using TMPro;

public class StartRaceController : MonoBehaviour
{
    public static StartRaceController instance;
    static BallMovementController[] playersControllers = null;
    [SerializeField] TextMeshProUGUI screenNotificationText;
    private float countdownTime = 3;


    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
        if (playersControllers == null)
        {
            playersControllers = FindObjectsByType<BallMovementController>(FindObjectsSortMode.None);
        }
        Invoke("StartTimer", 2);
        screenNotificationText.text = string.Empty;
    }

    private void StartTimer()
    {
        screenNotificationText.text = countdownTime.ToString();
        Invoke("CountDownTimer", 1);
    }

    private void CountDownTimer()
    {
        countdownTime--;
        if (countdownTime > 0)
        {
            screenNotificationText.text = countdownTime.ToString();
            Invoke("CountDownTimer", 1);
        }
        else
        {
            screenNotificationText.text = "GO";
            OnStartRace();
            Invoke("RemoveNotificationText", 1);
        }
    }

    private void RemoveNotificationText()
    {
        screenNotificationText.text = "";
    }

    public void OnStartRace()
    {
        foreach (var controller in playersControllers)
        {
            controller.EnableControl();
        }
    }

    public void OnRaceFinished()
    {
        foreach (var controller in playersControllers)
        {
            controller.DisableControl();
        }
        screenNotificationText.text = "FINISH";
    }
}
