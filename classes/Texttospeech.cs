using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IA_text_to_speech.classes
{
    public class Texttospeech
    {
        public async static Task SynthesizeToSpeaker()
        {
            string textoPconversao = "Sogro de Jojo Todynho, Renato Souza, resolveu falar sobre o motivo de não ter comparecido no casamento do filho, Lucas Souza, com a cantora - lembrando que a cerimônia aconteceu 29 de janeiro, em um sítio, no Rio de Janeiro. Segundo informações da colunista Fábia Oliveira, Renato afirmou que sua ausência foi motivada pela postura do oficial do exército de negar suas origens.";
            var speechConfig = SpeechConfig.FromSubscription("355dfe15905a4ee990f74ddef984a703", "brazilsouth");
            speechConfig.SpeechSynthesisLanguage = "pt-BR";
            speechConfig.SpeechSynthesisVoiceName = "pt-BR-AntonioNeural";

            using var audioConfig = AudioConfig.FromWavFileOutput("file.wav");
            using var synthesizer = new SpeechSynthesizer(speechConfig, audioConfig);
            await synthesizer.SpeakTextAsync(textoPconversao);
        }
    }
}
