using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2020
{
    public class Day12
    {
        List<string> actions;
        Dictionary<char, (int mx, int my)> direction = new Dictionary<char, (int mx, int my)>() { { 'N', (0, 1) }, { 'S', (0, -1) }, { 'E', (1, 0) }, { 'W', (-1, 0) } };
        Dictionary<int, char> angles = new Dictionary<int, char>() { { 0, 'N' }, { 180, 'S' }, { 90, 'E' }, { 270, 'W' } };

        public Day12()
        {
            actions = File.ReadAllLines(@"E:\GitHub\CSharp-Codes\AdventCode2020\ships.txt").ToList();
            Console.WriteLine($"Part 1: {Part(false)}");
            Console.WriteLine($"Part 2: {Part(true)}");
        }

        private int Part(bool isWaypoint)
        {
            (int x, int y) pos = (0, 0); (int x, int y) waypoint = (10, 1);
            int currAngle = 90;
            char currDir = angles[currAngle];

            foreach (string action in actions)
            {
                char c = action[0];
                int val = int.Parse(action[1..]);

                if (!isWaypoint)
                {
                    if (c == 'F')
                    {
                        pos = (pos.x + (direction[currDir].mx * val), pos.y + (direction[currDir].my * val));
                    }
                    else if (c == 'R' || c == 'L')
                    {
                        int tempAngle = c == 'R' ? val : val * -1;
                        currAngle = (currAngle + 360 + tempAngle) % 360;
                        currDir = angles[currAngle];
                    }
                    else if (direction.ContainsKey(c))
                    {
                        pos = (pos.x + (direction[c].mx * val), pos.y + (direction[c].my * val));
                    }
                }
                else
                {
                    if (c == 'F')
                    {
                        pos = (pos.x + (waypoint.x * val), pos.y + (waypoint.y * val));
                    }
                    else if (c == 'R' || c == 'L')
                    {
                        int tempAngle = c == 'R' ? val : val * -1;
                        currAngle = (360 + tempAngle) % 360;
                        currDir = angles[currAngle];
                        (int tempx, int tempy) = direction[currDir];
                        waypoint = (waypoint.x * tempy + waypoint.y * tempx, waypoint.y * tempy - waypoint.x * tempx);
                    }
                    else if (direction.ContainsKey(c))
                    {
                        waypoint = (waypoint.x + (direction[c].mx * val), waypoint.y + (direction[c].my * val));
                    }
                }
            }

            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }
    }
}
