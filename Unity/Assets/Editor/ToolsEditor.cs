using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

internal class OpcodeInfo
{
    public string Name;
    public int Opcode;
}

public static class InnerProto2CS
{
    private static readonly char[] splitChars = { ' ', '\t' };
    private static readonly List<OpcodeInfo> msgOpcode = new List<OpcodeInfo>();

    public const int InnerMinOpcode = 100;
    public const int MongoMinOpcode = 1000;
    public const int OuterMinOpcode = 10000;

    public static void Proto2CS()
    {
        string unityPath = System.Environment.CurrentDirectory;
        string projPath = Directory.GetParent(unityPath).ToString();
        string protoSrc = $"{projPath}/Protoc/OuterMessage.proto";
        string clientMessagePath = $"{projPath}/HotFix/Message/";
        string serverMessagePath = $"{projPath}/NetCoreServer/NetCoreApp/Message/";

        // 输出到服务器
        Proto2CS("ET", protoSrc, serverMessagePath, "OuterOpcode", OuterMinOpcode);
        GenerateOpcode("ET", "OuterOpcode", serverMessagePath);

        // 输出到客户端
        Proto2CS("ET", protoSrc, clientMessagePath, "OuterOpcode", OuterMinOpcode);
        GenerateOpcode("ET", "OuterOpcode", clientMessagePath);
    }

    public static void Proto2CS(string ns, string protoName, string outputPath, string opcodeClassName, int startOpcode)
    {
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        msgOpcode.Clear();
        string csPath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(protoName) + ".cs");
        Debug.Log($"csPath={csPath}");

        string s = File.ReadAllText(protoName);

        StringBuilder sb = new StringBuilder();
        sb.Append("using ET;\n");
        sb.Append("using ProtoBuf;\n");
        sb.Append("using System.Collections.Generic;\n");
        sb.Append($"namespace {ns}\n");
        sb.Append("{\n");

        bool isMsgStart = false;
        foreach (string line in s.Split('\n'))
        {
            string newline = line.Trim();

            if (newline == "")
            {
                continue;
            }

            if (newline.StartsWith("//ResponseType"))
            {
                string responseType = line.Split(' ')[1].TrimEnd('\r', '\n');
                sb.AppendLine($"\t[ResponseType(nameof({responseType}))]");
                continue;
            }

            if (newline.StartsWith("//"))
            {
                sb.Append($"{newline}\n");
                continue;
            }

            if (newline.StartsWith("message"))
            {
                string parentClass = "";
                isMsgStart = true;
                string msgName = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries)[1];
                string[] ss = newline.Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

                if (ss.Length == 2)
                {
                    parentClass = ss[1].Trim();
                }

                msgOpcode.Add(new OpcodeInfo() { Name = msgName, Opcode = ++startOpcode });

                sb.Append($"\t[Message({opcodeClassName}.{msgName})]\n");
                sb.Append($"\t[ProtoContract]\n");
                sb.Append($"\tpublic partial class {msgName}: Object");
                if (parentClass == "IActorMessage" || parentClass == "IActorRequest" || parentClass == "IActorResponse")
                {
                    sb.Append($", {parentClass}\n");
                }
                else if (parentClass != "")
                {
                    sb.Append($", {parentClass}\n");
                }
                else
                {
                    sb.Append("\n");
                }

                continue;
            }

            if (isMsgStart)
            {
                if (newline == "{")
                {
                    sb.Append("\t{\n");
                    continue;
                }

                if (newline == "}")
                {
                    isMsgStart = false;
                    sb.Append("\t}\n\n");
                    continue;
                }

                if (newline.Trim().StartsWith("//"))
                {
                    sb.AppendLine(newline);
                    continue;
                }

                if (newline.Trim() != "" && newline != "}")
                {
                    if (newline.StartsWith("repeated"))
                    {
                        Repeated(sb, ns, newline);
                    }
                    else
                    {
                        Members(sb, newline, true);
                    }
                }
            }
        }

        sb.Append("}\n");
        using FileStream txt = new FileStream(csPath, FileMode.Create, FileAccess.ReadWrite);
        using StreamWriter sw = new StreamWriter(txt);
        sw.Write(sb.ToString());
    }

    private static void GenerateOpcode(string ns, string outputFileName, string outputPath)
    {
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"namespace {ns}");
        sb.AppendLine("{");
        sb.AppendLine($"\tpublic static partial class {outputFileName}");
        sb.AppendLine("\t{");
        foreach (OpcodeInfo info in msgOpcode)
        {
            sb.AppendLine($"\t\t public const ushort {info.Name} = {info.Opcode};");
        }

        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string csPath = Path.Combine(outputPath, outputFileName + ".cs");

        using FileStream txt = new FileStream(csPath, FileMode.Create);
        using StreamWriter sw = new StreamWriter(txt);
        sw.Write(sb.ToString());
    }

    private static void Repeated(StringBuilder sb, string ns, string newline)
    {
        try
        {
            int index = newline.IndexOf(";");
            newline = newline.Remove(index);
            string[] ss = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            string type = ss[1];
            type = ConvertType(type);
            string name = ss[2];
            int n = int.Parse(ss[4]);

            sb.Append($"\t\t[ProtoMember({n})]\n");
            sb.Append($"\t\tpublic List<{type}> {name} = new List<{type}>();\n\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{newline}\n {e}");
        }
    }

    private static string ConvertType(string type)
    {
        string typeCs = "";
        switch (type)
        {
            case "int16":
                typeCs = "short";
                break;
            case "int32":
                typeCs = "int";
                break;
            case "bytes":
                typeCs = "byte[]";
                break;
            case "uint32":
                typeCs = "uint";
                break;
            case "long":
                typeCs = "long";
                break;
            case "int64":
                typeCs = "long";
                break;
            case "uint64":
                typeCs = "ulong";
                break;
            case "uint16":
                typeCs = "ushort";
                break;
            default:
                typeCs = type;
                break;
        }

        return typeCs;
    }

    private static void Members(StringBuilder sb, string newline, bool isRequired)
    {
        try
        {
            int index = newline.IndexOf(";");
            newline = newline.Remove(index);
            string[] ss = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            string type = ss[0];
            string name = ss[1];
            int n = int.Parse(ss[3]);
            string typeCs = ConvertType(type);

            sb.Append($"\t\t[ProtoMember({n})]\n");
            sb.Append($"\t\tpublic {typeCs} {name} {{ get; set; }}\n\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{newline}\n {e}");
        }
    }
}