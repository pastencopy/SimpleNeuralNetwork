using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork
{

    // MNIST DATA SET THE MNIST DATABASE : http://yann.lecun.com/exdb/mnist/
    // MNIST READ CODE : https://stackoverflow.com/questions/49407772/reading-mnist-database
    public static class MnistExtensions
    {
        public static int ReadBigInt32(this BinaryReader br)
        {
            var bytes = br.ReadBytes(sizeof(Int32));
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static void ForEach<T>(this T[,] source, Action<int, int> action)
        {
            for (int w = 0; w < source.GetLength(0); w++)
            {
                for (int h = 0; h < source.GetLength(1); h++)
                {
                    action(w, h);
                }
            }
        }
    }
    public class MnistImage
    {
        public byte Label { get; set; }
        public byte[,] Data { get; set; }
    }

    public static class MnistReader
    {
        private const string TrainImages = "train-images.idx3-ubyte";
        private const string TrainLabels = "train-labels.idx1-ubyte";
        private const string TestImages = "t10k-images.idx3-ubyte";
        private const string TestLabels = "t10k-labels.idx1-ubyte";

        public static IEnumerable<MnistImage> ReadTrainingData(int numberOfReadimages = 0)
        {
            foreach (var item in Read(TrainImages, TrainLabels, numberOfReadimages))
            {
                yield return item;
            }
        }

        public static IEnumerable<MnistImage> ReadTestData( int numberOfReadimages = 0)
        {
            foreach (var item in Read(TestImages, TestLabels, numberOfReadimages))
            {
                yield return item;
            }
        }

        private static IEnumerable<MnistImage> Read(string imagesPath, string labelsPath, int numberOfReadimages = 0)
        {
            BinaryReader labels = new BinaryReader(new FileStream(labelsPath, FileMode.Open));
            BinaryReader images = new BinaryReader(new FileStream(imagesPath, FileMode.Open));

            int magicNumber = images.ReadBigInt32();
            int numberOfImages = (numberOfReadimages == 0 || images.ReadBigInt32()< numberOfReadimages) ? images.ReadBigInt32() : numberOfReadimages;
            int width = images.ReadBigInt32();
            int height = images.ReadBigInt32();

            int magicLabel = labels.ReadBigInt32();
            int numberOfLabels = labels.ReadBigInt32();

            for (int i = 0; i < numberOfImages; i++)
            {
                var bytes = images.ReadBytes(width * height);
                var arr = new byte[height, width];

                arr.ForEach((j, k) => arr[j, k] = bytes[j * height + k]);

                yield return new MnistImage()
                {
                    Data = arr,
                    Label = labels.ReadByte()
                };
            }

            labels.Close();
            images.Close();
        }
    }
}
