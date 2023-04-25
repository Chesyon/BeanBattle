using UnityEngine;
using System.IO;
public class RuntimeText : MonoBehaviour
{
    public static void WriteString()
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new(path, true);
        writer.WriteLine("Test");
        writer.Close();
        StreamReader reader = new(path);
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    public static string ReadString()
    {
        try
        {
            string path = Application.persistentDataPath + "/test.txt";
            //Read the text from directly from the test.txt file
            StreamReader reader = new(path);
            string retString = reader.ReadToEnd();
            reader.Close();
            return retString;
        }
        catch
        {
            return "failed";
        }

    }
}