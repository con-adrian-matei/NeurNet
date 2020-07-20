using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace NeurNet
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string trainImgPath = @"C:\Users\Matei\source\repos\NeurNet\NeurNet\train-images.idx3-ubyte";
            string trainTagPath = @"C:\Users\Matei\source\repos\NeurNet\NeurNet\train-labels.idx1-ubyte";

            FileInfo trainImgs = new FileInfo(trainImgPath);
            FileInfo trainTags = new FileInfo(trainTagPath);

            BinaryReader br1 = new BinaryReader(trainImgs.OpenRead());
            BinaryReader br2 = new BinaryReader(trainTags.OpenRead());

            int magicNoImgs = reverseBits(br1.ReadInt32());
            int noOfImages = reverseBits(br1.ReadInt32());
            int noOfRows = reverseBits(br1.ReadInt32());
            int noOfColumns = reverseBits(br1.ReadInt32());

            byte[] pixels = new byte[784];
            double[] neurons = new double[784];

            for(int i = 0; i < 784; i++)
            {
                pixels[i] = br1.ReadByte();
                neurons[i] = Convert.ToDouble(pixels[i])/255;
            }

            int magicNoTags = reverseBits(br2.ReadInt32());
            int noOfItems = reverseBits(br2.ReadInt32());
            byte tag = br2.ReadByte();
            int label = Convert.ToInt32(tag);
            
            NeuralNetwork mnist = new NeuralNetwork();

            mnist.SetInput(neurons);
            mnist.RunNetwork();
            mnist.DisplayOutput();
            Console.WriteLine(mnist.ComputeCost(label));
            */

            Console.WriteLine("Salut");
        }

        static int reverseBits (int arg)
        {
            string bin = Convert.ToString(arg, 2);
            int i, n = 32 - bin.Length;

            for (i = 0; i < n; i++)
            {
                bin = "0" + bin;
            }

            string binAux = "";

            for (i = 3; i >= 0; i--)
            {
                binAux = binAux + bin.Substring((8 * i), 8);
            }

            return Convert.ToInt32( binAux, 2);
        }



        static void displayImage(byte[] pixels)
        {
            int i, j;

            for (i = 0; i < 28; i++)
            {
                for (j = 0; j < 28; j++)
                {
                    switch(pixels[i * 28 + j]){
                        case 0:
                            Console.Write(" ");
                            break;
                        default:
                            Console.Write("#");
                            break;
                    }
                }
                Console.Write("\n");
            }
        }

    }
}
