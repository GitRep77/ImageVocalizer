using Tesseract;
using System.Speech.Synthesis;
using System.Configuration;

namespace ImageVocalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Images", "que-es-lorem-ipsum.jpg");
            
            try
            {
                // Initialize Tesseract engine
                using var engine = new TesseractEngine(ConfigurationManager.AppSettings["TesseractDataPath"], "eng", EngineMode.Default);
                using var img = Pix.LoadFromFile(imagePath);
                using var page = engine.Process(img);

                string text = page.GetText();
                Console.WriteLine("Extracted Text:");
                Console.WriteLine(text);

                using SpeechSynthesizer synthesizer = new SpeechSynthesizer();

                // Optional: Set voice options
                synthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet);

                synthesizer.Speak(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
