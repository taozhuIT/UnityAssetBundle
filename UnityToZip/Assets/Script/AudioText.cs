using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Text;

/// <summary>
/// 使用Unity API Microphone录音函数进行录音压缩/和反解析播放
/// </summary>
public class AudioText : MonoBehaviour
{
    private string token;                           //access_token
    private string cuid = "随便写的d";        //用户标识
    private string format = "pcm";                  //语音格式
    private int rate = 8000;                        //采样率
    private int channel = 1;                        //声道数
    private string speech;                          //语音数据，进行base64编码
    private int len;                                //原始语音长度
    private string lan = "zh";                      //语种

    private int maxAudioLength = 10; // 限制录音长度
    private AudioSource audioSou;    // 录音源
    private Byte[] clipByte;         // 音频数据
    private int audioLength;         //录音的长度

    /// <summary>
    /// 开始录音
    /// </summary>
    public void StartRecord()
    {
        Debug.Log("开始说话");

        // 找到是否有可用的麦克风 (devices是一个列表，如果长度等于0表示没有可以使用的麦克风)
        if (Microphone.devices.Length == 0) return;
        // 录音前先停掉录音，录音参数为null时采用的是默认的录音驱动
        Microphone.End(null);

        // 参数1：设备名称  参数2：表示如果达到了长度，录音是否应该继续录制
        // 参数3：长度是由录音产生的声音的长度  参数4：频率由录音产生的AudioClip的采样率
        // 如果为设备名称传递null或空字符串，则使用默认麦克风
        audioSou.clip = Microphone.Start(null, false, maxAudioLength, rate);
    }

    /// <summary>
    /// 停止录音
    /// </summary>
    public void EndRecord()
    {
        Debug.Log("结束说话");

        // 根据设备名  在录音的样品中找到位置
        // 如果为设备名称传递null或空字符串，则使用默认麦克风
        int lastPos = Microphone.GetPosition(null);
        // 查询设备是否正在录制
        if (Microphone.IsRecording(null))
            audioLength = lastPos / rate; //录音时长  
        else
            audioLength = maxAudioLength;
        Microphone.End(null);

        // 将语音数据压缩/转换成二进制
        clipByte = GetClipData();
        len = clipByte.Length;
        // 再将压缩转换后的二进制文件，转换成base64.便于网络传输
        speech = Convert.ToBase64String(clipByte);

        // 后面步骤进行网络传输...............
        // 这里暂时没有实现服务端，所以直接调用解码播放 
        OnPlayerAudio(speech);
        // base64 解码 byte[] bytes = Convert.FromBase64String(result);
    }

    /// <summary>
    /// 播放录音
    /// </summary>
    private void OnPlayerAudio(string speech_)
    {
        AudioClip clip = OnGetAudioClip(speech_);
        
        AudioSource audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.mute = false;
        audioSource.Play();
    }

    /// <summary>
    /// 把录音转换为Byte[]
    /// </summary>
    /// <returns></returns>
    public Byte[] GetClipData()
    {
        if (audioSou.clip == null)
        {
            Debug.LogError("录音数据为空");
            return null;
        }

        float[] samples = new float[audioSou.clip.samples];

        audioSou.clip.GetData(samples, 0);


        Byte[] outData = new byte[samples.Length * 2];

        int rescaleFactor = 32767; //to convert float to Int16   

        for (int i = 0; i < samples.Length; i++)
        {
            short temshort = (short)(samples[i] * rescaleFactor);


            Byte[] temdata = System.BitConverter.GetBytes(temshort);

            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }

        if (outData == null || outData.Length <= 0)
        {
            Debug.LogError("录音数据为空");
            return null;
        }
      
        return outData;
    }

    /// <summary>
    /// 测试函数-将网络得到的音频数据转换成clip
    /// </summary>
    private AudioClip OnGetAudioClip(string speech_)
    {
        // 测试将压缩转换后的二进制语音数据解码
        byte[] bytesTest = Convert.FromBase64String(speech_);

        string aaastr = bytesTest.ToString();
        long aaalength = aaastr.Length;
        Debug.LogError("aaalength=" + aaalength);

        string aaastr1 = Convert.ToString(bytesTest);
        aaalength = aaastr1.Length;
        Debug.LogError("aaalength=" + aaalength);

        if (bytesTest.Length == 0)
        {
            Debug.Log("get intarr clipdata is null");
            return null;
        }
        //从Int16[]到float[]   
        float[] samples = new float[bytesTest.Length];
        int rescaleFactor = 32767;
        for (int i = 0; i < bytesTest.Length; i++)
            samples[i] = (float)bytesTest[i] / rescaleFactor;
        
        //从float[]到Clip   
        AudioClip clip = null;
        if (clip == null)
            clip = AudioClip.Create("playRecordClip", bytesTest.Length, 1, rate, false);

        clip.SetData(samples, 0);

        return clip;
    }
}
