using UnityEngine;
using UnityEngine.Video;

public class BackgroundVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    private VideoPlayer currentVideoPlayer;

    private void Start()
    {
        // Set the initial video to play
        PlayRandomVideo();
    }

    private void PlayRandomVideo()
    {
        // Choose a random video to play
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            currentVideoPlayer = videoPlayer1;
            videoPlayer2.Stop();
        }
        else
        {
            currentVideoPlayer = videoPlayer2;
            videoPlayer1.Stop();
        }

        // Play the selected video
        currentVideoPlayer.Play();

        // Add listener for loop points
        currentVideoPlayer.loopPointReached += OnLoopPointReached;
    }

    private void OnLoopPointReached(VideoPlayer source)
    {
        // Play the next random video when the current video has ended
        PlayRandomVideo();
    }
}
