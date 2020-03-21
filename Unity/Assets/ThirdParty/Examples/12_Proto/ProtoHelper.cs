using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;

public class ProtoHelper : MonoBehaviour
{
    public static byte[] Serialize(IMessage msg)
    {
        using (MemoryStream rawOutput = new MemoryStream())
        {
            CodedOutputStream output = new CodedOutputStream(rawOutput);
            output.WriteMessage(msg);
            output.Flush();
            byte[] result = rawOutput.ToArray();
            return result;
        }
    }

    public static T Deserialize<T>(byte[] dataBytes) where T : IMessage, new()
    {
        CodedInputStream stream = new CodedInputStream(dataBytes);
        T msg = new T();
        stream.ReadMessage(msg);
        return msg;
    }
}
