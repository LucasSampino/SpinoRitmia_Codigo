using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    enum TimerType{Countdown,Stopwatch}
    [SerializeField] private TimerType timerType;
    [SerializeField] private float timeToDisplay = 60.0f;
    private bool _isRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake(){
        timerText = GetComponent<TMP_Text>();
    }

private void OnEnable(){
    EventManager.TimerStart += EventManagerOnTimerStart;
    EventManager.TimerStop += EventManagerOnTimerStop;
    EventManager.TimerUpdate += EventManagerOnTimerUpdate;

}
private void OnDisable(){
    EventManager.TimerStart -= EventManagerOnTimerStart;
    EventManager.TimerStop -= EventManagerOnTimerStop;
    EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
}
private void EventManagerOnTimerStart() => _isRunning = true;
private void EventManagerOnTimerStop()=> _isRunning = false;
private void EventManagerOnTimerUpdate(float value)=> timeToDisplay += value;
    // Update is called once per frame
   private void Update()
    {
        if(!_isRunning) return;
        if(timerType == TimerType.Countdown && timeToDisplay < 0.0f){ 
            EventManager.OnTimerStop();
            //BF.LoadScene(3);
            SceneManager.LoadScene(3);
            return;}
        timeToDisplay+= timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timerText.text = timeSpan.ToString(format:@"mm\:ss\:ff");
    }
}