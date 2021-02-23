using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Testing_NN
{
    class Program
    {

        static void Main(string[] args)
        {

            Matrix input = new Matrix(1, 3);
            double[,] db = {
                { 0.5, 0.5, 0.5}};
            input.setFrom2dArray(db);

            Matrix tmp = new Matrix(3, 3);
            Matrix tmp1 = new Matrix(1, 3);
            double[,] db1 = { { 1, 0, 0 }, { 1, 0, 0 }, { 1, 0, 0} };
            double[,] db2 = { { -0.5, -0.5, -0.5 } };
            tmp.setFrom2dArray(db1);
            tmp1.setFrom2dArray(db2);

            Layer l = new Layer(3, 3, tmp, tmp1, ActivationFunctions.Activation_ReLU_Forward, ActivationFunctions.Activation_ReLU_Backward);
            Layer l1 = new Layer(3, 3, tmp, tmp1, ActivationFunctions.Activation_ReLU_Forward, ActivationFunctions.Activation_ReLU_Backward);
            Layer l2 = new Layer(3, 3, tmp, tmp1, ActivationFunctions.Activation_ReLU_Forward, ActivationFunctions.Activation_ReLU_Backward);
            Layer l3 = new Layer(3, 3, tmp, tmp1, ActivationFunctions.Activation_ReLU_Forward, ActivationFunctions.Activation_ReLU_Backward);

            l.forward(input);
            l1.forward(l.Output);
            l2.forward(l1.Output);           
            l3.forward(l2.Output);

            NeuralNetwork nn = new NeuralNetwork(l, l1, l2, l3);
            Console.WriteLine(nn);
            Console.WriteLine("Out put:{0}", l3.Output);
            Console.ReadKey();
        }
    }

    class Loss
    {
        /// <summary>
        /// Calculate the loss by the difrences between the expected OnOff and the result. 
        /// </summary>
        /// <param name="Output">Result</param>
        /// <param name="Y">Expected</param>
        /// <returns></returns>
        public static double CalculateByExcpectedOnOff(Matrix Output, Matrix Y)
        {
            if (Output.getRow() > 1 || Y.getRow() > 1)
                throw new Exception("Wrong rows size, expected an horizental vector only.");

            if (Output == Y)
                return 0;

            double loss = 0;
            for (int i = 0; i < Output.getColumn(); i++)
            {
                if (Output[0, i] != Y[0, i])
                    loss++;
            }
            return loss; 
        }

        /// <summary>
        /// Calculate the loss by minimize the effects behavior. 
        /// </summary>
        /// <param name="difrenceInStatistics"></param>
        /// <returns></returns>
        public static double CalculateByStatisticsResult(Matrix difrenceInStatistics)
        {
            if (difrenceInStatistics.getRow() > 1)
                throw new Exception("Wrong rows size, expected an horizental vector only.");

            double Loss = 50 * difrenceInStatistics.getColumn();
            for (int i = 0; i < difrenceInStatistics.getColumn(); i++)
            {
                if (i == 1)
                    Loss += difrenceInStatistics[0, i];
                else
                    Loss -= difrenceInStatistics[0, i];
            }
            return Loss;
        }
    }
    class ActivationFunctions
    {

        /// <summary>
        /// Activation function for the ReLU function
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Activation_ReLU(double value)
        {
            return Math.Max(0, value);
        }
        /// <summary>
        /// Backward activation function for the ReLU function
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix Activation_ReLU_Backward(Matrix dvalue)
        {       
            Matrix dInput = new Matrix(dvalue.getRow(), dvalue.getColumn());
            for (int col = 0; col < dvalue.getColumn(); col++)
            {
                for (int row = 0; row < dvalue.getRow(); row++)
                {
                    dInput[row, col] = Math.Max(0, dvalue[row, col]);
                }       
            }
            return dInput;
        }
        /// <summary>
        /// Activation function for the ReLU function
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix Activation_ReLU_Forward(Matrix value)
        {
            Matrix c = new Matrix(value.getRow(), value.getColumn());
            for (int i = 0; i < value.getColumn(); i++)
            {
                for (int j = 0; j < value.getRow(); j++)
                {
                    c[i, j] = Activation_ReLU(value[i, j]);
                }
            }
            return c;
        }

    } 

    class NeuralNetwork
    {
        int NumberOfEpoch = 0;
        Layer inputLayer;
        Layer firstMidLayer;
        Layer secondMidLayer;
        Layer outputLayer;

        public NeuralNetwork(Layer inputLayer, Layer firstMidLayer, Layer secondMidLayer, Layer outputLayer)
        {
            this.inputLayer = inputLayer;
            this.firstMidLayer = firstMidLayer;
            this.secondMidLayer = secondMidLayer;
            this.outputLayer = outputLayer;
        }

        /// <summary>
        /// Override the ToString method to print the Neural Network
        /// </summary>
        /// <returns>String represention of the Neural Network</returns>
        public override string ToString()
        {
            string msg = "Neural Network Number of Epoches: " + NumberOfEpoch;
            for (int i = 0; i < 3; i++)
            {
                msg += "\n ";

                msg += "_____";
                if (inputLayer.Output[0, i] < 0)
                    msg += "_";
                msg += "    ";
                msg += "_____";
                if (firstMidLayer.Output[0, i] < 0)
                    msg += "_";
                msg += "    ";
                msg += "_____";
                if (secondMidLayer.Output[0, i] < 0)
                    msg += "_";
                msg += "    ";
                msg += "_____";
                if (outputLayer.Output[0, i] < 0)
                    msg += "_";
                msg += "    ";

                msg += "\n";
                msg += String.Format("({0:0.000})--", inputLayer.Output[0, i]);
                msg += String.Format("({0:0.000})--", firstMidLayer.Output[0, i]);
                msg += String.Format("({0:0.000})--", secondMidLayer.Output[0, i]);
                msg += String.Format("({0:0.000})", outputLayer.Output[0, i]); 
                msg += "\n\n";
            }
            return msg;
        }
    }
    class Layer 
    {
        Func<Matrix, Matrix> activation_function_forward;
        Func<Matrix, Matrix> activation_function_backword;
        int NumberOfNeurons;

        public Matrix biases;
        public Matrix weights;
        public Matrix input;
        public Matrix Output = null;

        public Matrix dbiases;
        public Matrix dweights;
        public Matrix dinput;

        public Layer(int NumberOfInputs, int NumberOfNeurons, Func<Matrix, Matrix> activation_function_forward, Func<Matrix, Matrix> activation_function_backword)
        {
            weights = new Matrix(NumberOfInputs, NumberOfNeurons);
            biases = new Matrix(1, NumberOfNeurons);
            weights.SetRandom();
            biases.SetRandom();
            this.NumberOfNeurons = NumberOfNeurons;

            this.activation_function_forward = activation_function_forward;
            this.activation_function_backword = activation_function_backword;
        }
        public Layer(int NumberOfInputs, int NumberOfNeurons, Matrix weights, Func<Matrix, Matrix> activation_function_forward, Func<Matrix, Matrix> activation_function_backword)
        {
            this.weights = new Matrix(weights);
            biases = new Matrix(1, NumberOfNeurons);
            biases.SetRandom();
            this.NumberOfNeurons = NumberOfNeurons;

            this.activation_function_forward = activation_function_forward;
            this.activation_function_backword = activation_function_backword;

        }
        public Layer(int NumberOfInputs, int NumberOfNeurons, Matrix weights, Matrix biases, Func<Matrix, Matrix> activation_function_forward, Func<Matrix, Matrix> activation_function_backword)
        {
            this.weights = new Matrix(weights);
            this.biases = new Matrix(biases);

            this.NumberOfNeurons = NumberOfNeurons;

            this.activation_function_forward = activation_function_forward;
            this.activation_function_backword = activation_function_backword;

        }

        /// <summary>
        /// Forward propagation for the current layer.
        /// </summary>
        /// <param name="input"></param>
        public void forward(Matrix input)
        {
            //Console.WriteLine("input\n" + input);
            //Console.WriteLine("weights\n" + weights);
            //Console.WriteLine("biases\n" + biases);
            Output = new Matrix((input * weights.Transpose()) + biases);
        }

        /// <summary>
        /// Backward propagation for the current layer.
        /// </summary>
        /// <param name="dvalues"></param>
        public void backward(Matrix dvalues)
        {
           
        }

        /// <summary>
        /// Override the ToString method in order to print the layer.
        /// </summary>
        /// <returns>String represention with the OutPut as</returns>
        public override string ToString()
        {
            string msg = "";
            for (int col = 0; col < biases.getColumn(); col++)
            {
                msg += "  ";
                msg += " (O:" + Output[0,col] + ")";
                msg += "\n";
            }
            
            return msg;
        }

    }
    class Matrix
    {
        /////////////////////////////////////
        // Matrix ///////////////////////////
        /////////////////////////////////////
        //____|column|column|column|column|//
        // row|                            //
        // row|                            //
        // row|                            //
        // row|                            //
        // row|                            //
        /////////////////////////////////////

        private double[,] values;
        private int row, column;

        private double[,] _values
        {
            get
            {
                return values;
            }
            set
            {
                if (value.GetLength(0) != 0 && value.GetLength(1) != 0)
                {
                    values = value;
                }
                else
                {
                    throw new Exception("Matrix's row or column cannot be set to zero!");
                }
            }
        }
        public int getRow()
        {
            return row;
        }
        public int getColumn()
        {
            return column;
        }

        public Matrix(int row, int column)
        {
            values = new double[row, column];
            this.row = row;
            this.column = column;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    values[i, j] = 0;
                }
            }

        }
        public Matrix(Matrix matrix)
        {
            values = new double[matrix.getRow(), matrix.getColumn()];
            this.row = matrix.getRow();
            this.column = matrix.getColumn();
            for (int i = 0; i < matrix.getRow(); i++)
            {
                for (int j = 0; j < matrix.getColumn(); j++)
                {
                    values[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Set the values randomly
        /// </summary>
        /// <param name="minVal">Minimum random value</param>
        /// <param name="maxVal">Maximum random value</param>
        public void SetRandom()
        {
            Random rnd = new Random();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    values[i, j] = 0.001 * rnd.Next(100, 1000);
                }
            }
        }

        /// <summary>
        /// Overloading multipication for matrices
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Result matrix</returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.getColumn() != b.getRow())
                throw new Exception("Can't multipiy those sizes");

            Matrix c = new Matrix(a.getRow(), b.getColumn());
            for (int i = 0; i < c.getRow(); i++)
                for (int j = 0; j < c.getColumn(); j++)
                {
                    double sum = 0;
                    for (int k = 0; k < b.getRow(); k++)
                        sum += a[i, k] * b[k, j];
                    c[i, j] = sum;
                }

            return c;
        }

        /// <summary>
        /// Overloading equalization for matrices
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Result matrix</returns>
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (a.getColumn() != b.getColumn() || a.getRow() != b.getRow())
                return false;

            for (int i = 0; i < a.getRow(); i++)
                for (int j = 0; j < a.getColumn(); j++)
                    if (a[i, j] != b[i, j])
                        return false;
            return true;
        }
        /// <summary>
        /// Overloading unequalization for matrices
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Result matrix</returns>
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Overload the add operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator + (Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.getRow(), a.getColumn());
            if (b.row == 1 || b.column == 1)
            {
                if (a.row == b.row)
                    for (int i = 0; i < a.getRow(); i++)
                        for (int j = 0; j < a.getColumn(); j++)
                        {
                            
                            c[i, j] = a[i, j] + b[0, j];
                        }
                if (a.column == b.column)
                    for (int i = 0; i < a.getRow(); i++)
                        for (int j = 0; j < a.getColumn(); j++)
                            c[i, j] = a[i, j] + b[0, j];
            }
            else
            {

                for (int i = 0; i < a.getRow(); i++)
                    for (int j = 0; j < a.getColumn(); j++)
                        c[i, j] = a[i, j] + b[i, j];
            }
            return c;
        }

        /// <summary>
        /// Overloading the [] index opeartor
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>Ref to the value in the current index</returns>
        public double this[int row, int column]
        {
            get => getValue(row, column);
            set => setValue(row, column, value);
        }

        /// <summary>
        /// Transpose the matrix
        /// </summary>
        /// <returns>Transposed matrix</returns>
        public Matrix Transpose()
        {
            Matrix tmp = new Matrix(column, row);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    tmp[j, i] = this[i, j];
                }
            }
            return tmp;
        }

        /// <summary>
        /// Summing the function
        /// </summary>
        /// <returns>The sum</returns>
        public double Sum()
        {
            double sum = 0;
            for (int column = 0; column < 4; column++)
                for (int row = 0; row < 3; row++)
                    if (row == 0 || this[row - 1, column] != 0)
                        sum += this[row, column];
            return sum;
        }

        /// <summary>
        /// Set matrix value equal to given arr array
        /// </summary>
        /// <param name="arr">2D array of</param>
        public void setFrom2dArray(double[,] arr)
        {
            int row = arr.GetLength(0);
            int column = arr.GetLength(1);

            if (row != values.GetLength(0) || column != values.GetLength(1))
                throw new Exception("Wrong sizes");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    values[i, j] = arr[i, j];
                }
            }
            return;
        }

        /// <summary>
        /// Get the value based on row i and column j
        /// </summary>
        /// <param name="i">row</param>
        /// <param name="j">column</param>
        /// <returns>Return value in current index</returns>
        private double getValue(int i, int j)
        {
            try
            {
                return _values[i, j];
            }
            catch
            {
                throw new Exception("Extanded matrix size!");
            }
        }

        /// <summary>
        /// Set value in the i row and j column
        /// </summary>
        /// <param name="i">row</param>
        /// <param name="j">column</param>
        /// <param name="value">value to insert</param>
        private void setValue(int i, int j, double value)
        {
            if (i >= _values.GetLength(0) || j >= _values.GetLength(1))
                throw new Exception("Extanded matrix size!");
            else
                _values[i, j] = value;
        }

        /// <summary>
        /// Print matrix 
        /// </summary>
        /// <returns> String with visual representation</returns>
        public override string ToString()
        {
            string size = String.Format("{0}x{1}", values.GetLength(0), values.GetLength(1));
            string msg = " " +size;
            for (int i = 0; i < (values.GetLength(1) * 6) - size.Length; i++)
            {
                msg += "_";
            }
            msg += "\n";
            for (int i = 0; i < values.GetLength(0); i++)
            {
                msg += "|";
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    if(values[i, j] < 0)
                        msg += String.Format(" {0:0.0000}", values[i, j]);
                    else
                        msg += String.Format("  {0:0.0000}", values[i, j]);
                }
                msg += "|\n";
            }

            return msg;
        }
    }  
}
