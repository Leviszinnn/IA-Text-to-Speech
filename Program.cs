using System;
using System.IO;
using System.Threading.Tasks;
using IA_text_to_speech.classes;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

class Program
{
    async static Task Main(string[] args)
    {
        var speechConfig = SpeechConfig.FromSubscription("", "brazilsouth");
        speechConfig.SpeechSynthesisLanguage = "pt-BR";
        speechConfig.SpeechSynthesisVoiceName = "pt-BR-AntonioNeural";
        //await Texttospeech.SynthesizeToSpeaker();
        //await Speechtotext.RecognizeFromMic(speechConfig);

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var recognizer = new SpeechRecognizer(speechConfig,"pt-BR", audioConfig);

        var stopRecognition = new TaskCompletionSource<int>();

        recognizer.Recognizing += (s, e) =>
        {
            Console.WriteLine(e.Result.Text);
        };

        recognizer.Recognized += (s, e) =>
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                //Console.WriteLine(e.Result.Text);
            }
            else if (e.Result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
            }
        };

        recognizer.Canceled += (s, e) =>
        {
            Console.WriteLine($"CANCELED: Reason={e.Reason}");

            if (e.Reason == CancellationReason.Error)
            {
                Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                Console.WriteLine($"CANCELED: Did you update the speech key and location/region info?");
            }

            stopRecognition.TrySetResult(0);
        };

        recognizer.SessionStopped += (s, e) =>
        {
            Console.WriteLine("\n    Session stopped event.");
            stopRecognition.TrySetResult(0);
        };

        await recognizer.StartContinuousRecognitionAsync();

        Task.WaitAny(new[] { stopRecognition.Task });
    }
}