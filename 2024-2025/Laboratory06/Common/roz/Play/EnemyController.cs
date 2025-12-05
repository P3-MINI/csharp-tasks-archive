using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace Play
{
    internal class EnemyController
    {
        const int targetCount = 3;
        const int targetPathLength = 3;
        List<Enemy> enemies;
        
        public Dictionary<Point, Enemy> Enemies { get => _enemies; }
        private Dictionary<Point, Enemy> _enemies;
        public EnemyController(Map m)
        {
            enemies = [];
            Random r = new Random();
            var set = new HashSet<Point>();
            while (enemies.Count < targetCount)
            {
                var l = new List<Point>();
                while (l.Count < targetPathLength)
                {
                    int x = r.Next(0, m.Data.GetLength(0));
                    int y = r.Next(0, m.Data.GetLength(1));
                    var p = new Point(x, y);
                    if (m.Data[x, y] == Common.Tile.Floor && !set.Contains(p) && !m.Items.ContainsKey(p))
                    {
                        l.Add(p);
                        set.Add(p);
                    }
                }
                enemies.Add(new Enemy(l));
            }
            _enemies = new(enemies.Select(e => new KeyValuePair<Point, Enemy>(e.Position, e)));
        }
        public void Update()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.MoveNext();
            }
            _enemies = new(enemies.Select(e => new KeyValuePair<Point, Enemy>(e.Position, e)));
        }
        internal static EnemyController? EC { get; set; }
    }
}
