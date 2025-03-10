// This reads the entire asset to memory at once; use a different constructor with an AssetBinaryReader if you don't want that
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI;
using System.Diagnostics;
using UAssetAPI.PropertyTypes.Structs;

public class ConsoleApp1
{
	static void Main(String[] args)
	{
		if (args.Length < 4)
		{
			Console.WriteLine("Error! Program needs \"uassetfilepath\" \"tablenamefilter\" \"propertynamefilter\" \"valuereplacement\" arguments!");
		}
		UAsset myAsset = new UAsset(args[0], EngineVersion.VER_UE4_27);

		foreach (DataTableExport table in myAsset.Exports)
		{
			foreach (var export in table.Table.Data.FindAll( (StructPropertyData d) => d.Name.ToString().Contains(args[1]) ))
			{
				foreach (var value in export.Value.FindAll( (PropertyData d) => d.Name.ToString().Contains(args[2]) ))
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
						data.Value = val;
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
						data.Value = val;
					}
					else if (value is BoolPropertyData)
					{
						((BoolPropertyData)value).Value = bool.Parse(args[3]);
					}
				}
			}
		}
		myAsset.Write("NEW.uasset");
	}
}