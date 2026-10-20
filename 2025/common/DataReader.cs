namespace common
{
    public class Test
    {
        public char LR;
        public int offset;
        public Test(string data)
        {
            LR = data[0];
            offset = Convert.ToInt32(data.Substring(1, data.Length-1));
        }
    }
    public class DataReader<T>
    {
        private string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        private string dataPath;
        private string testDataPath;
        private Func<string, T> parserFunction;

        private char seperator;

        public DataReader(Func<string, T> parserFunction, char seperator = '\n')
        {
            dataPath = Path.Combine(projectDir, @"input.txt");
            testDataPath = Path.Combine(projectDir, @"test.txt");
            this.parserFunction = parserFunction;
            this.seperator = seperator;
        }

        public List<T> getData()
        {
            return this.read(dataPath);
        }

        public List<T> getTestData()
        {
            return this.read(testDataPath);
        }

        private List<T> read(string path)
        {
            List<T> list = new List<T>();

            char c;
            string dataChunk = "";
            StreamReader sr = new StreamReader(path);
            int i = 0;
            do
            {
                c = (char)sr.Read();
                if(c != seperator)
                    dataChunk += c;

                if(c == seperator || sr.EndOfStream)
                {
                    if(dataChunk.Length > 0)
                    {
                        dataChunk = dataChunk.Replace("\n", "");
                        dataChunk = dataChunk.Replace("\r", "");
                        list.Add(this.parserFunction(dataChunk));
                    }
                    dataChunk = "";
                    i++;
                }
            }
            while (!sr.EndOfStream);

            return list;
        }

    }
}
