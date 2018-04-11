///*
// * Unity压缩文件测试
// * 这里只是演示了压缩MP3文件，以及文件夹。在正式项目中应该是先将资源打包成Assebunde包，再讲AsseBundl包进行压缩。
// * 在游戏初始化的时候，解压，进行加载
//*/

//using UnityEngine;
//using UnityEditor;
//using UnityEngine.UI;
//using System;
//using System.IO;
//using System.Collections;
//using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.Checksums;
//using ICSharpCode.SharpZipLib.LZW;

///// <summary>
///// 资源压缩
///// </summary>
//public class OnSetZip : MonoBehaviour 
//{
//    private Button _button = null;
//    private string zipPath = null;
//    private string outPath = null;
//    private string zipPass = null;

//	private void Start () 
//    {
//        zipPath = Application.dataPath + "/Resources";
//        outPath = Application.streamingAssetsPath + "/Resources/bgm.zip";
//        zipPass = "123456";

//        _button = this.transform.FindChild("Button").GetComponent<Button>();
//        _button.onClick.AddListener(OnCompressionFile);

//        Debug.Log(Application.persistentDataPath);
//	}
	
//	private void Update () {
	
//	}

//    /// <summary>
//    /// 压缩文件
//    /// </summary>
//    private void OnCompressionFile()
//    {
//        // 创建输出文件夹(如果文件夹存在，就先删除再创建)
//        string path = outPath.Substring(0, outPath.LastIndexOf("/"));
//        if (Directory.Exists(path))
//            Directory.Delete(path, true);
//        Directory.CreateDirectory(path);

//        // 创建压缩输出流
//        ZipOutputStream zipOutPut = new ZipOutputStream(File.Create(outPath));
//        // 设置压缩级别
//        zipOutPut.SetLevel(6);
//        // 设置压缩密码
//        if (!string.IsNullOrEmpty(zipPass))
//            zipOutPut.Password = zipPass;

//        OnCompressionFolder(zipPath, zipOutPut, null);

//        zipOutPut.Finish();
//        zipOutPut.Close();

//        AssetDatabase.Refresh();
//    }

//    /// <summary>
//    /// 压缩文件夹
//    /// </summary>
//    private void OnCompressionFolder(string resPath_, ZipOutputStream zipOut_, string dir_)
//    {
//        // 获取当前路径下的所有文件和文件夹
//        string[] resPathFiles = Directory.GetFileSystemEntries(resPath_);
//        // Crc32数据效验算法
//        Crc32 crc = new Crc32();

//        foreach(string file in resPathFiles)
//        {
//            if (Directory.Exists(file))
//            {
//                // 如果是文件夹
//                // 递归查找文件夹压缩 
//                string dir = file.Substring(file.IndexOf("\\") + 1, (file.Length - file.IndexOf("\\")) - 1);
//                Debug.Log(dir);
//                // 创建文件夹就是从根目录开始的文件夹名 + \
//                ZipEntry entryDir = new ZipEntry(dir + "\\");
//                zipOut_.PutNextEntry(entryDir);

//                OnCompressionFolder(file, zipOut_, dir);
//            }
//            else
//            {
//                // 如果是文件
//                // ZipEntry创建一个以当前文件名称为名的zip文件(PS：这里个人理解是创建压缩包里面的一个文本，或者一个MP3文件，不是一个压缩包)
//                // ZipEntry创建的规则是，如果是根目录下的文件，就直接得到文件名GetFileName，如果是子文件夹下的文件，那么就是子文件夹加文件名
//                // 比如：111.MP3 和 TestFoldr\222.mp3 对应在压缩包里面生成时第二个就会先生成一个TestFoldr文件夹再将222.MP3放到TestFoldr下(这个研究好久才明白)

//                string fileName = Path.GetFileName(file);
//                string entryName = dir_ == null || dir_ == "" ? fileName : dir_ + "\\" + fileName;
//                Debug.Log(entryName);
//                ZipEntry entry = new ZipEntry(entryName);

//                // 读取文件流
//                FileStream fileStream = File.OpenRead(file);
//                byte[] buff = new byte[fileStream.Length];
//                fileStream.Read(buff, 0, buff.Length);
//                fileStream.Close();

//                // 设置zip文件的大小和(DateTime没搞明白是做什么用的)
//                crc.Reset();
//                crc.Update(buff);
//                entry.DateTime = DateTime.Now;
//                entry.Size = buff.Length;
//                entry.Crc = crc.Value;

//                // 压入当前zip文件
//                zipOut_.PutNextEntry(entry);
//                zipOut_.Write(buff, 0, buff.Length);
//            }
//        }
//    }
//}
