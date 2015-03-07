using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Fractal_Generator
{
    public class FractalResult
    {
        public static int NextID { get; set; }
        public int ID { get; set; }

        public string Filename { get { return String.Format("{0} ({1},{2}) ({3},{4}) {5}x{6}", Fractal.Name, Fractal.XLimit.Min, Fractal.YLimit.Min, Fractal.XLimit.Max, Fractal.YLimit.Max, Fractal.XLimit.Steps, Fractal.YLimit.Steps); } }
        public int[][] Data { get; set; }
        public Fractal Fractal { get; set; }
        public int Width { get { return Data.Length; } }
        public int Height { get { return Data[0].Length; } }

        public FractalResult(Fractal fractal)
        {
            Fractal = fractal;
            ID = NextID++;
            Data = GetArray(Fractal.XLimit.Steps, Fractal.YLimit.Steps);
            GetArray(Fractal.XLimit.Steps, Fractal.YLimit.Steps);
        }

        public void WriteToFile()
        {
            WriteToFile(Filename);
        }
        public void WriteToFile(string filename)
        {
            string[] lines = new string[Data.Length];
            for(int i=0;i<Data.Length;i++)
            {
                int[] lineNums = Data[i];
                string line = "";
                for(int j=0;j<lineNums.Length;j++)
                {
                    line += lineNums[j] + ",";
                }
                line += "\n";
                lines[i] = line;
            }
            File.WriteAllLines(filename + ".csv", lines);
        }

        public int[][] ReadFromFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int[][] output = new int[lines.Length][];
            for (int i = 0; i < lines.Length;i++)
            {
                string[] data = lines[i].Split(',');
                output[i] = new int[data.Length];
                for (int j = 0; j < data.Length; j++) output[i][j] = int.Parse(data[j]);
            }
            return output;
        }

        public void WriteToImage(int limit)
        {
            WriteToImage(Filename + ".jpg", limit);
        }  
        public void WriteToImage(string filename, int limit)
        {
            Bitmap outputBitmap = new Bitmap(Data.Length, Data[0].Length);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    outputBitmap.SetPixel(x, Height - y - 1,Color.White);
                    if (Data[x][y] > limit) outputBitmap.SetPixel(x, Height - y - 1, Color.Black);
                }
            }
            outputBitmap.Save(filename);
        }

        public void WriteToImageGradient(int limit)
        {
            WriteToImageGradient(Filename + ".jpg", limit);
        }  
        public void WriteToImageGradient(string filename, int limit)
        {
            Bitmap outputBitmap = new Bitmap(Data.Length, Data[0].Length);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    outputBitmap.SetPixel(x, Height - y - 1, ColorFromHSV((double)Data[x][y] / (double)limit * 255.0, 1.0, 1.0));
                }
            }

            outputBitmap.Save(filename);
        }

        private static int[][] GetArray(int xSteps, int ySteps)
        {
            int[][] Data = new int[xSteps][];
            for (int i = 0; i < xSteps; i++)
            {
                Data[i] = new int[ySteps];
            }
            return Data;
        }

        private static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
