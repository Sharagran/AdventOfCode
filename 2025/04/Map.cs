using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4
{
    internal class Map : List<Row>
    {
        public List<Location> accessiblePapers = new List<Location>();
        public Map()
        {
        }

        public int CountAccessiblePaperRolls()
        {
            int sum = 0;
            foreach (Row row in this)
            {
                foreach (var l in row.locations)
                {
                    if(l.isPaper && l.CountAdjacentPaper() < 4)
                    {
                        sum++;
                        accessiblePapers.Add(l);
                    }
                }
            }

            // part 2
            foreach (var l in accessiblePapers)
                l.isPaper = false;

            return sum;
        }
    }

    internal class Row
    {
        public List<Location> locations;

        public int yLocation = -1;
        public Map map;
        public Row(string data)
        {
            locations = new List<Location>();
            for (int i = 0; i < data.Length; i++)
            {
                char item = data[i];
                if (item == '@')
                    locations.Add(new Location(true, i, this));
                else if (item == '.')
                    locations.Add(new Location(false, i, this));
            }
        }

        public void setYLocation(int y)
        {
            yLocation = y;
            foreach (var l in this.locations)
            {
                l.updateLocation();
            }
        }

        public void setMap(Map m)
        {
            this.map = m;
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach (var item in locations)
            {
                s += item.isPaper ? '@' : ".";
            }
            return s;
        }
    }

    internal class Location
    {
        public bool isPaper;
        private int x;
        private int y;

        private Row parent;
        public Location(bool isPaper, int x, Row r)
        {
            this.isPaper = isPaper;
            this.x = x;
            
            this.parent = r;
        }

        internal void updateLocation()
        {
            this.y = parent.yLocation;
        }

        public int CountAdjacentPaper()
        {
            var map = this.parent.map;

            int sum = 0;
            foreach (var item in getAdjacentPostions())
            {
                int x = item.Item1;
                int y = item.Item2;

                if (map[y].locations[x].isPaper)
                    sum++;
            }
            return sum;
        }

        private  List<(int, int)> getAdjacentPostions()
        {
            var l = new List<(int, int)>();

            l.Add((this.x - 1, this.y - 1));
            l.Add((this.x, this.y - 1));
            l.Add((this.x + 1, this.y - 1));

            l.Add((this.x - 1, this.y));
            l.Add((this.x + 1, this.y));

            l.Add((this.x - 1, this.y + 1));
            l.Add((this.x, this.y + 1));
            l.Add((this.x + 1, this.y + 1));

            // filter invalid
            l = l.Where((tuple) =>
            {
                int x = tuple.Item1;
                int y = tuple.Item2;

                if (x < 0 || y < 0)
                    return false;
                
                if(x >= this.parent.locations.Count || y >= this.parent.map.Count)
                    return false;

                return true;
            }).ToList();

            return l;
        }

        public override string ToString()
        {
            char c = isPaper ? '@' : '.';
            return $"{c}: {x}, {y}";
        }
    }
}
