using System;
using System.Collections.Generic;
using System.Text;

namespace NeurNet
{
    public class NeuralNetwork
    {
        public int[] noOfNeurons;
        public double[][] neurons;
        public double[][][] weights;
        public double[][] biases;
        

        public NeuralNetwork()
        {

            var rand = new Random();
            int i, j, k;

            noOfNeurons = new int[]{ 784, 64, 16, 10 };

            neurons = new double[][]
            {
                new double[noOfNeurons[0]],
                new double[noOfNeurons[1]],
                new double[noOfNeurons[2]],
                new double[noOfNeurons[3]]
            };

            weights = new double[][][]
            {
                new double[64][]
                {

                },
                new double[16][]
                {

                },
                new double[10][]
                {

                }
            };

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < noOfNeurons[i+1]; j++)
                {
                    for (k = 0; k < noOfNeurons[i]; k++)
                    {
                        weights[i][j][k] = rand.NextDouble();
                    }
                }
            }

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < noOfNeurons[i + 1]; j++)
                {
                    biases[i][j] = rand.NextDouble();
                }
            }

        }
    }

}
