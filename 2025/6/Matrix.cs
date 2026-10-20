using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6
{
    internal class Matrix
    {
        public char[,] data { get; private set; }


        // TODO: detect where every column has a whitespace = seperator index
        // maybe this is not required if i do matrix rotation
        // rotate by using int instead of chars? (math way)
        public Matrix(List<string> lines)
        {

            //rotate
            char[,] fileAsMatrix = new char[lines.Count, lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    fileAsMatrix[i, j] = lines[i][j];
                }
            }

            this.data = fileAsMatrix;
        }

        public void RotateMatrixCounterClockwise()
        {
            this.data = _RotateMatrixCounterClockwise(this.data);
        }

        // Source - https://stackoverflow.com/a/18035050
        // Posted by wjmolinam, modified by community. See post 'Timeline' for change history
        // Retrieved 2025-12-07, License - CC BY-SA 4.0

        private static char[,] _RotateMatrixCounterClockwise(char[,] oldMatrix)
        {
            char[,] newMatrix = new char[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }

    }
}
