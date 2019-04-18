using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorSquare
{
	class Program
	{
		static void Main(string[] args)
		{
			Tuple<String, Int32, List<Color>> parsedArgs = ParseArguments(args);
			Bitmap bitmap = new Bitmap(parsedArgs.Item2, parsedArgs.Item2);
			Graphics g = Graphics.FromImage(bitmap);
			foreach (Color c in parsedArgs.Item3)
			{
				Brush b = new SolidBrush(c);
				g.FillRectangle(b, 0, 0, parsedArgs.Item2, parsedArgs.Item2);
				try
				{
					bitmap.Save(parsedArgs.Item1 + "\\" + ColorTranslator.ToHtml(c) + ".png");
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error saving file: " + ex.Message);
				}
			}
		}

		static Tuple<String, Int32, List<Color>> ParseArguments(string[] args)
		{
			if (args.Length < 2) return null;
			if (!Int32.TryParse(args[1], out Int32 dimension)) return null;
			Tuple<String, Int32, List<Color>> data = new Tuple<String, Int32, List<Color>>(args[0], dimension, new List<Color>());
			for (int i = 1; i < args.Length; i++)
			{
				Color c = ColorTranslator.FromHtml(args[i]);
				if (c == Color.Empty) return null;
				data.Item3.Add(c);
			}
			return data;
		}
	}
}
