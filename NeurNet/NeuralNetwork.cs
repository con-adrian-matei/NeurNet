using System;
using System.Collections.Generic;
using System.Text;

namespace NeurNet
{
    public class NeuralNetwork
    {
        // noOfNeurons holds the number of neurons in each layer. noOfNeurons[0] holds 
        // the number of neurons in the first (input) layer, noOfNeurons[1] holds the
        // number of neurons in the second (hidden) layer and so on
        private int[] noOfNeurons = new int[]{784, 64, 16, 10};

        // the activation value of the neurons in each layer
        // neuronInLayer - activation values of neurons in the input layer
        // neuronHid1Layer - activation values of neurons in the first hidden layer
        // neuronHid2Layer - activation values of neurons in the second hidden layer
        // neuronOutLayer - activation values of neurons in the output layer
        public double[] neuronInLayer = new double[784];
        public double[] neuronHid1Layer = new double[64];
        public double[] neuronHid2Layer = new double[16];
        public double[] neuronOutLayer = new double[10];

        // weights (and biases) of the neural network
        // weightsInToHid1 - the weights and biases form the input layer to the first hidden layer
        // weightsHid1ToHid2 - the weights and biases form the first hidden layer to the second hidden layer
        // weightsHid2ToOut - the weights and biases form the second hidden layer to the output layer
        // the first index of a weight matrix represents which neuron from the next layer the weight is targeting
        // the second index of a weight matrix represents which neuron from the current layer the weight is affecting
        // so weightsHid1ToHid2[5,26] holds the weight from neuron 26 of the first hidden layer to neuron 5 of the second hidden layer (I start indexing neurons at 0)
        // the last value from each row holds the bias to that neuron of the next layer
        // so weightsHid2ToOut[2,16] holds the bias to the 3rd output neuron (neuron 2 of the output layer)
        public double[,] weightsInToHid1 = new double[64,785];
        public double[,] weightsHid1ToHid2 = new double[16, 65];
        public double[,] weightsHid2ToOut = new double[10, 17];

        // Constructor
        public NeuralNetwork()
        {
            this.RandomizeWeights();
        }

        // RandomizeWeights randomizes all weights and biases in the neural network
        public void RandomizeWeights()
        {
            var rand = new Random();
            int i, j;

            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 785; j++)
                {
                    weightsInToHid1[i,j] = rand.NextDouble() * 2 - 1;
                }
            }

            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 65; j++)
                {
                    weightsHid1ToHid2[i, j] = rand.NextDouble() * 2 - 1;
                }
            }

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 17; j++)
                {
                    weightsHid2ToOut[i, j] = rand.NextDouble() * 2 - 1;
                }
            }
        }

        // RandomizeInput randomizez the activation values for all input neurons
        public void RandomizeInput()
        {
            var rand = new Random();
            int i;

            for (i = 0; i < 784; i++)
            {
                neuronInLayer[i] = rand.NextDouble();
            }
        }

        // Sets the intput layer to the values given by arg
        public void SetInput(double[] arg)
        {
            neuronInLayer = arg;
        }

        // RunNetwork computes the activation values of the output layer
        public void RunNetwork()
        {
            int i, j;

            for (i = 0; i < 64; i++)
            {
                for (j = 0; j < 784; j++)
                {
                    neuronHid1Layer[i] += neuronInLayer[j] * weightsInToHid1[i, j];
                }
                neuronHid1Layer[i] += weightsInToHid1[i, 784];
                // ReLU activation function
                neuronHid1Layer[i] = ReLU(neuronHid1Layer[i]);
            }

            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 64; j++)
                {
                    neuronHid2Layer[i] += neuronHid1Layer[j] * weightsHid1ToHid2[i, j];
                }
                neuronHid2Layer[i] += weightsHid1ToHid2[i, 64];
                // ReLU activation function
                neuronHid2Layer[i] = ReLU(neuronHid2Layer[i]);
            }

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 16; j++)
                {
                    neuronOutLayer[i] += neuronHid2Layer[j] * weightsHid2ToOut[i, j];
                }
                neuronOutLayer[i] += weightsHid2ToOut[i, 16];
            }

            // Softmax activation function
            neuronOutLayer = Softmax(neuronOutLayer);

        }

        // Returns the output layer
        public double[] GetOutput {
            get
            {
                return neuronOutLayer;
            }
        }

        // Displays the activation values of the output layer
        public void DisplayOutput()
        {
            int i;

            for (i = 0; i < 10; i++)
            {
                Console.WriteLine(i + ": " + (neuronOutLayer[i] * 100).ToString("0.##") + "%");
            }
        }

        // Returns the cost function value of the output layer
        public double ComputeCost(int label)
        {
            double cost = 0;
            int i;

            for (i = 0; i < 10; i++)
            {
                cost += Math.Pow(Convert.ToDouble(i == label) - neuronOutLayer[i], 2);
            }
            

            return cost;
        }

        // Returns softmax function of vector arg
        private double[] Softmax(double[] arg)
        {
            double[] result = arg;

            int i, n = result.Length;

            double denominator = 0;

            for(i=0; i < n; i++)
            {
                denominator += Math.Exp(result[i]);
            }

            for (i = 0; i < n; i++)
            {
                result[i] = Math.Exp(result[i]) / denominator;
            }

            return result;
        }

        // Returns ReLU function of value arg
        private double ReLU(double arg)
        {
            return Math.Max(1E-8 * arg, 0.5*arg);
        }
    }

}
