using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    public enum BeamBufferOperation
    {
        Add,
        Delete
    }
    public struct BufferedBeam
    {
        public BeamBufferOperation Operation;
        public Vector2 Beam;

        public BufferedBeam(BeamBufferOperation Operation, Vector2 Beam)
        {
            this.Operation = Operation;
            this.Beam = Beam;
        }
    }
    internal class Manifold
    {
        private char[,] data;
        private List<BufferedBeam> beamBuffer = new List<BufferedBeam>();
        public List<Vector2> beams;
        private Vector2 startPos;
        public int SplitCounter
        {
            get; private set;
        }

        public char this[int x, int y]
        {
            get { return data[y, x]; }
            set { data[y, x] = value; }
        }

        public char this[Vector2 v]
        {
            get { return data[(int)v.Y, (int)v.X]; }
            set { data[(int)v.Y, (int)v.X] = value; }
        }

        public Manifold(char[,] data, Vector2 beamStartPos)
        {
            this.data = data;
            this.beams = new List<Vector2>() { beamStartPos };
            this.startPos = beamStartPos;
            this.SplitCounter = 0;
        }

        public char Get(Vector2 c)
        {
            return this[((int)c.X), ((int)c.Y)];
        }

        #region Part 1
        private void SetBeam(Vector2 pos)
        {
            this[pos] = '|';
        }


        public bool SimulateStep()
        {
            for (int i = 0; i < beams.Count; i++)
            {
                beams[i] += new Vector2(0, 1);

                if (beams[i].Y == data.GetLength(0))
                {
                    return true;
                }

                char newPosElement = Get(beams[i]);
                if (newPosElement == '.')
                {
                    this.SetBeam(beams[i]);
                }
                else if (newPosElement == '^')
                {
                    this.Split(beams[i]);
                }
            }

            this.FlushBeamBuffer();

            return false;
        }

        private void Split(Vector2 beam)
        {
            Vector2 a = beam + new Vector2(-1, 0);
            Vector2 b = beam + new Vector2(1, 0);

            SetBeam(a);
            SetBeam(b);

            beamBuffer.Add(new BufferedBeam(BeamBufferOperation.Delete, beam));
            beamBuffer.Add(new BufferedBeam(BeamBufferOperation.Add, a));
            beamBuffer.Add(new BufferedBeam(BeamBufferOperation.Add, b));


            this.SplitCounter++;
        }

        private void FlushBeamBuffer()
        {
            // remove dupes
            //beamBuffer = beamBuffer.Distinct().ToList();

            foreach (var buffer in beamBuffer)
            {
                if(buffer.Operation == BeamBufferOperation.Add && !beams.Contains(buffer.Beam))
                {
                    beams.Add(buffer.Beam);
                }
                else if (buffer.Operation == BeamBufferOperation.Delete)
                {
                    beams.Remove(buffer.Beam);
                }
            }
            beamBuffer.Clear();
        }

        public void Draw()
        {
            for (int y = 0; y < data.GetLength(0); y++)
            {
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    Console.Write(data[y, x]);
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Part 2
        public long CountAllPossiblePaths()
        {
            return CountPaths(this.startPos, 0);
        }

        private static Vector2 moveDown = new Vector2(0, 1);
        private static Vector2 moveLeft = new Vector2(-1, 0);
        private static Vector2 moveRight = new Vector2(1, 0);

        Dictionary<Vector2, long> NodeCache = new Dictionary<Vector2, long>();
        public long CountPaths(Vector2 startPos, long count)
        {
            while (startPos.Y < data.GetLength(0) - 1)
            {
                startPos += moveDown;

                char newPosElement = Get(startPos);
                if (newPosElement == '^')
                {
                    // use cache
                    if (NodeCache.ContainsKey(startPos))
                        return count + NodeCache[startPos];

                    long childPaths = 0;
                    if (startPos.X != 0)
                        childPaths += CountPaths(startPos + moveLeft, 0);
                    if (startPos.X != data.GetLength(1))
                        childPaths += CountPaths(startPos + moveRight, 0);

                    NodeCache.Add(startPos, childPaths);

                    return count + childPaths;
                }
            }

            return 1;
        }

        #endregion
    }
}
