using TMPro;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

public class AsyncTextObject
{
    private Task _task;
    private CancellationTokenSource _cancellationToken;

    public TextMeshProUGUI TextMesh {  get; private set; }
    public int TaskDelay { get; set; } = 200;

    public AsyncTextObject(TextMeshProUGUI textMesh, int writeWait)
    {
        TextMesh = textMesh;
        TaskDelay = writeWait;
    }
    
    /// <summary>
    /// Start async function to write some text progressively
    /// </summary>
    /// <param name="text"> Text to write in TextMesh component </param>
    public void StartTextAsync(string text)
    {
        //Stop the current async if not finished
        if (_task != null && !_task.IsCompleted)
        {
            Debug.Write("Task was cancelled.");
            StopTextAsync();
        }

        _cancellationToken = new CancellationTokenSource();
        _task = WriteTextAsync(text);
    }

    /// <summary>
    /// Stop the running async funtion
    /// </summary>
    public void StopTextAsync()
    {
        _cancellationToken.Cancel();
    }

    /// <summary>
    /// Write each character of a string with a given delay (milliseconds)
    /// </summary>
    /// <param name="text"> String being converted into character array </param>
    private async Task WriteTextAsync(string text)
    {
        TextMesh.SetText("");

        foreach (char character in text.ToCharArray())
        {
            TextMesh.text += character;
            await Task.Delay(TaskDelay, _cancellationToken.Token);
        }
    }
}
