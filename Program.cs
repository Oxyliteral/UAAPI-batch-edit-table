// This reads the entire asset to memory at once; use a different constructor with an AssetBinaryReader if you don't want that
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI;
using System.Diagnostics;
using UAssetAPI.PropertyTypes.Structs;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;

public class ConsoleApp1
{
    static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Error! Program needs \"uassetfilepath\" \"tablenamefilter\" \"propertynamefilter\" \"valuereplacement\" arguments!");
        }
        UAsset myAsset = new UAsset(args[0], EngineVersion.VER_UE4_27);
        int count = 0;

        foreach (DataTableExport table in myAsset.Exports)
        {
            foreach (var export in table.Table.Data.FindAll((StructPropertyData d) => d.Name.ToString().Contains(args[1])))
            {
                foreach (var value in export.Value.FindAll((PropertyData d) => d.Name.ToString().Contains(args[2])))
                {
                    if (value is FloatPropertyData)
                    {
                        FloatPropertyData data = (FloatPropertyData)value;
                        float val = data.Value;
                        foreach (string calc in args[3].Split(" "))
                        {
                            string op = calc.Substring(0, 1);
                            string num = calc.Substring(1);
                            switch (op)
                            {
                                case "+":
                                    val += float.Parse(num);
                                    break;
                                case "-":
                                    val -= float.Parse(num);
                                    break;
                                case "*":
                                    val *= float.Parse(num);
                                    break;
                                case "/":
                                    val /= float.Parse(num);
                                    break;
                                case "r":
                                    if (num.Length == 0)
                                        val = (float)Math.Round(val);
                                    else if (num.Equals("d"))
                                        val = (float)Math.Floor(val);
                                    else if (num.Equals("u"))
                                        val = (float)Math.Ceiling(val);
                                    break;
                            }
                        }
                        Console.WriteLine(table.ObjectName + "." + export.Name + "." + value.Name + ": " + data.Value + " > " + val);
                        data.Value = val;
                        count++;
                    }
                    else if (value is DoublePropertyData)
                    {
                        DoublePropertyData data = (DoublePropertyData)value;
                        double val = data.Value;
                        foreach (string calc in args[3].Split(" "))
                        {
                            string op = calc.Substring(0, 1);
                            string num = calc.Substring(1);
                            switch (op)
                            {
                                case "+":
                                    val += double.Parse(num);
                                    break;
                                case "-":
                                    val -= double.Parse(num);
                                    break;
                                case "*":
                                    val *= double.Parse(num);
                                    break;
                                case "/":
                                    val /= double.Parse(num);
                                    break;
                                case "r":
                                    if (num.Length == 0)
                                        val = Math.Round(val);
                                    else if (num.Equals("d"))
                                        val = Math.Floor(val);
                                    else if (num.Equals("u"))
                                        val = Math.Ceiling(val);
                                    break;
                            }
                        }
                        Console.WriteLine(table.ObjectName + "." + export.Name + "." + value.Name + ": " + data.Value + " > " + val);
                        data.Value = val;
                        count++;
                    }
                    else if (value is BoolPropertyData)
                    {
                        BoolPropertyData data = (BoolPropertyData)value;
                        bool val = bool.Parse(args[3]);
                        Console.WriteLine(table.ObjectName + "." + export.Name + "." + value.Name + ": " + data.Value + " > " + val);
                        data.Value = val;
                        count++;
                    }
                    else if (value is NamePropertyData)
                    {
                        NamePropertyData data = (NamePropertyData)value;
                        Console.WriteLine(table.ObjectName + "." + export.Name + "." + value.Name + ": " + data.Value + " > " + args[3]);
                        data.Value.Value.Value = args[3];
                        count++;
                    }
                }
            }
        }
        Console.WriteLine(count + " values changed.");
        Console.WriteLine("y or n - Overwrite file? (Will write to NEW.uasset if n)");
        string? resp = Console.ReadLine();
        if (resp != null && (resp.ToLower().Equals("y") || (resp.ToLower().Equals("yes"))))
        {
            myAsset.Write(args[0]);
        }
        else
            myAsset.Write("NEW.uasset");
    }
}