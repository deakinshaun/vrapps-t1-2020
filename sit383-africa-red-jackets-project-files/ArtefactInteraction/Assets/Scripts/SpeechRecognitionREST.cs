using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System;


public class SpeechRecognitionREST : MonoBehaviour
{
    public Text outputText;

    // Key 1: 1ea6bdb1950a4911b9f37eb19b89dd5e
    // Key 2: ecb1d5e01e8e419b83f80668c98f6346
    private string subsciptionKey = "1ea6bdb1950a4911b9f37eb19b89dd5e";
    private string token;

    //recording length
    public int recordDuration = 5;

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;
    }
    
    public void Authentication()
    {
        HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("https://westus.api.cognitive.microsoft.com/sts/v1.0/issueToken");
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";
        request.Headers["Ocp-Apim-Subscription-Key"] = subsciptionKey;

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        Debug.Log("Token received: " + responseString);

        token = responseString;
    }

    public void SpeechToText (byte [] wavData)
    {
        // Send the request to the service
        string fetchUri = "https://westus.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1";
        HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(fetchUri + "?language=en-US&format=detailed");
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";
        request.ContentType = "audio/wav; codecs=audio/pcm; samplerate=16000";
        request.Headers["Ocp-Apim-Subscription-Key"] = subsciptionKey;

        Stream rs = request.GetRequestStream();
        rs.Write(wavData, 0, wavData.Length);
        rs.Close();

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        Debug.Log("Response from service: " + responseString);
        if (outputText != null)
        {
            outputText.text = responseString;
        }
    }

    private IEnumerator recordAudio ()
    {
        // Set microphone recording. Service requires 16 kHz sampling
        AudioClip audio = Microphone.Start(null, false, recordDuration, 16000);
        yield return new WaitForSeconds(recordDuration);
        Microphone.End(null);

        // Playback to ensure correct
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();

        // Convert to wav, then upload
        byte[] wavData = ConvertToWav (audio);
        SpeechToText(wavData);
    }

    public void Trigger ()
    {
        StartCoroutine(recordAudio());
    }

    const int HEADER_SIZE = 44;

    static byte [] ConvertToWav (AudioClip clip)
    {
        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        Int16[] intData = new short[samples.Length];

        Byte[] bytesData = new byte[HEADER_SIZE + samples.Length * 2];

        int rescaleFactor = 32767;

        WriteHeader (bytesData, clip);

        for (int i = 0; i<samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = new byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, HEADER_SIZE + i * 2);
        }

        return bytesData;
    }

    static void WriteHeader(byte[] bytesData, AudioClip clip)
    {
        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        riff.CopyTo(bytesData, 0);
        Byte[] chunkSize = BitConverter.GetBytes(HEADER_SIZE + clip.samples * 2 - 8);
        chunkSize.CopyTo(bytesData, 4);
        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        wave.CopyTo(bytesData, 8);
        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt");
        fmt.CopyTo(bytesData, 12);
        Byte[] subChunk1 = BitConverter.GetBytes(16);
        subChunk1.CopyTo(bytesData, 16);

        UInt16 one = 1;
        Byte[] audioFormat = BitConverter.GetBytes(one);
        audioFormat.CopyTo(bytesData, 20);
        Byte[] numChannels = BitConverter.GetBytes(channels);
        numChannels.CopyTo(bytesData, 22);
        Byte[] sampleRate = BitConverter.GetBytes(hz);
        sampleRate.CopyTo(bytesData, 24);
        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        byteRate.CopyTo(bytesData, 28);
        UInt16 blockAlign = (ushort)(channels * 2);
        BitConverter.GetBytes(blockAlign).CopyTo(bytesData, 32);
        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        bitsPerSample.CopyTo(bytesData, 34);
        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        datastring.CopyTo(bytesData, 36);
        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        subChunk2.CopyTo(bytesData, 40);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
