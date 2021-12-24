using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameDoor : MonoBehaviour
{
    [SerializeField] private PlayableAsset openTimeline;
    [SerializeField] private PlayableAsset closeTimeline;
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _playableDirector.time = 0;
        _playableDirector.initialTime = 1;
    }

    public void OpenDoor()
    {
        StartCoroutine(PlayAnimation(openTimeline));
    }

    public void CloseDoor()
    {
        StartCoroutine(PlayAnimation(closeTimeline));
    }

    private IEnumerator PlayAnimation(PlayableAsset timeline)
    {
        if (_playableDirector.time != timeline.duration)
        {
            _playableDirector.initialTime = timeline.duration - _playableDirector.time;
        }
        else
        {
            _playableDirector.initialTime = 0;
        }
        
        _playableDirector.Play(timeline);
        yield return new WaitForSeconds((float) timeline.duration);
    }
}
