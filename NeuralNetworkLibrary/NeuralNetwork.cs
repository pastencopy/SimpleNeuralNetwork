/*
 *   NeuralNetworkLibrary
 * 
 *   N x N 배열에 Hidden레이어 1개로 구성된 단순한 신경망 구조
 * 
 *   2020-02-03 Coded by GwangSu Lee 
 * 
 * 
 * Original Reference : https://github.com/CodingTrain/Toy-Neural-Network-JS/blob/master/lib/nn.js
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class ActivationFunction
    {
        public Func<double, int, int, double> func;
        public Func<double, int, int, double> dfunc;
        public ActivationFunction(Func<double, int, int, double> func, Func<double, int, int, double> dfunc)
        {
            this.func = func;
            this.dfunc = dfunc;
        }

        public static ActivationFunction SIGMOID = new ActivationFunction(
            (x, i, j) => 1 / (1 + Math.Exp(-x)),
            (y, i, j) => y * (1 - y)
        );

        public static ActivationFunction TANH = new ActivationFunction(
            (x, i, j) => Math.Tanh(x),
            (y, i, j) => 1 - (y * y)
        );

    }

    public class NeuralNetwork
    {
        public int input;
        public int hidden;
        public int output;

        public Matrix bias_h;
        public Matrix bias_o;

        public Matrix weights_ih;
        public Matrix weights_ho;

        public double learning_rate;

        public ActivationFunction activation_function;

        public NeuralNetwork(NeuralNetwork dup)
        {
            this.input = dup.input;
            this.hidden = dup.hidden;
            this.output = dup.output;

            this.weights_ho = dup.weights_ho.Copy();
            this.weights_ih = dup.weights_ih.Copy();

            this.bias_h = dup.bias_h.Copy();
            this.bias_o = dup.bias_o.Copy();

            this.Preload();
        }
        public NeuralNetwork(int input, int hidden, int output)
        {
            this.input = input;
            this.hidden = hidden;
            this.output = output;

            this.weights_ih = new Matrix(this.hidden, this.input);
            this.weights_ho = new Matrix(this.output, this.hidden);
            this.weights_ih.Randomize();
            this.weights_ho.Randomize();

            this.bias_h = new Matrix(this.hidden, 1);
            this.bias_o = new Matrix(this.output, 1);
            this.bias_h.Randomize();
            this.bias_o.Randomize();

            this.Preload();
        }

        private void Preload()
        {
            this.SetLearningRate();
            this.SetActivationFunction(ActivationFunction.SIGMOID);
        }

        public void SetLearningRate(double rate = 0.1)
        {
            this.learning_rate = rate;
        }

        public void SetActivationFunction(ActivationFunction func)
        {
            this.activation_function = func;
        }

        public double[] Predict(double[] input_array)
        {
            Matrix inputs = Matrix.ConvertFromArray(input_array);
            Matrix hidden = Matrix.Multiply(this.weights_ih, inputs);
            hidden.Add(this.bias_h);

            hidden.map(this.activation_function.func);

            Matrix output = Matrix.Multiply(this.weights_ho, hidden);
            output.Add(this.bias_o);
            output.map(this.activation_function.func);

            return output.ToArray();
        }

        public void Train(double[] input_array, double[] target_array)
        {
            Matrix inputs = Matrix.ConvertFromArray(input_array);
            Matrix hidden = Matrix.Multiply(this.weights_ih, inputs);
            hidden.Add(this.bias_h);
            hidden.map(this.activation_function.func);

            //출력 만들기
            Matrix outputs = Matrix.Multiply(this.weights_ho, hidden);
            outputs.Add(this.bias_o);
            outputs.map(this.activation_function.func);

            Matrix targets = Matrix.ConvertFromArray(target_array);

            //1. 출력 오차 계산
            // errors = targets - outputs
            Matrix output_errors = Matrix.Substract(targets, outputs);

            //gradient 계산
            Matrix gradients = Matrix.map(outputs, this.activation_function.dfunc);
            gradients.Multiply(output_errors);
            gradients.Multiply(this.learning_rate);

            //DELTA계산
            Matrix hidden_T = Matrix.Transpose(hidden);
            Matrix weight_ho_deltas = Matrix.Multiply(gradients, hidden_T);

            //가중치 보정
            this.weights_ho.Add(weight_ho_deltas);
            this.bias_o.Add(gradients);

            //2. hidden 오차 계산
            Matrix weight_ho_T = Matrix.Transpose(this.weights_ho);
            Matrix hidden_errors = Matrix.Multiply(weight_ho_T, output_errors);

            //gradient 계산
            Matrix hidden_gradient = Matrix.map(hidden, this.activation_function.dfunc);
            hidden_gradient.Multiply(hidden_errors);
            hidden_gradient.Multiply(this.learning_rate);

            //DELTA계산 input->hidden
            Matrix inputs_T = Matrix.Transpose(inputs);
            Matrix weight_ih_deltas = Matrix.Multiply(hidden_gradient, inputs_T);

            this.weights_ih.Add(weight_ih_deltas);
            this.bias_h.Add(hidden_gradient);

        }

        public NeuralNetwork Copy()
        {
            return new NeuralNetwork(this);
        }

        public void Mutate(Func<double, double> func)
        {
            this.weights_ih.map(func);
            this.weights_ho.map(func);
            this.bias_h.map(func);
            this.bias_o.map(func);
        }

        public NeuralNetwork Crossover(NeuralNetwork other)
        {
            //50% 확률 교차한다.
            this.weights_ih.map((v, i, j) =>
            {
                return (RandomGaussian.NextDouble() < 0.5) ? other.weights_ih.data[i,j] : v;
            });

            this.weights_ho.map((v, i, j) =>
            {
                return (RandomGaussian.NextDouble() < 0.5) ? other.weights_ho.data[i, j] : v;
            });
            this.bias_h.map((v, i, j) =>
            {
                return (RandomGaussian.NextDouble() < 0.5) ? other.bias_h.data[i, j] : v;
            });
            this.bias_o.map((v, i, j) =>
            {
                return (RandomGaussian.NextDouble() < 0.5) ? other.bias_o.data[i, j] : v;
            });

            return this.Copy();
        }

    }
}
